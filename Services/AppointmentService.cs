using OOAD.Data;
using OOAD.DTOs;
using OOAD.Model;
using OOAD.Repository;
using OOAD.Utils;

namespace OOAD.Services
{
    public class AppointmentService
    {
        private readonly AppDBContext _context;
        private readonly AppointmentRepository _appointmentRepository;
        private readonly GroupMeetingRepository _groupMeetingRepository;
        private readonly ReminderRepository _reminderRepository;
        private readonly UserGroupRepository _userGroupRepository;

        public AppointmentService(AppDBContext context)
        {
            _context = context;
            _appointmentRepository = new AppointmentRepository(_context);
            _groupMeetingRepository = new GroupMeetingRepository(_context);
            _reminderRepository = new ReminderRepository(_context);
            _userGroupRepository = new UserGroupRepository(_context);
        }

        public ServiceResult<List<Appointments>> GetAppointmentsByCalendarId(Guid calendarId)
        {
            var appointments = _appointmentRepository.Query
                .Where(a => a.CalendarId == calendarId)
                .OrderBy(a => a.StartTime)
                .ToList();

            return ServiceResult<List<Appointments>>.Ok(appointments);
        }

        public ServiceResult<Appointments> GetAppointmentById(Guid appointmentId)
        {
            var appointment = _appointmentRepository.GetById(appointmentId);
            if (appointment == null)
                return ServiceResult<Appointments>.Fail("Không tìm thấy cuộc hẹn.");

            return ServiceResult<Appointments>.Ok(appointment);
        }

        public bool IsGroupMeeting(Guid appointmentId)
        {
            return _groupMeetingRepository.Query.Any(g => g.AppointmentId == appointmentId);
        }

        public ServiceResult<List<string>> GetGroupParticipants(Guid groupMeetingId)
        {
            if (groupMeetingId == Guid.Empty)
                return ServiceResult<List<string>>.Ok(new List<string>());

            var participants = _userGroupRepository.Query
                .Where(ug => ug.GroupMeetingId == groupMeetingId)
                .Select(ug => new
                {
                    ug.User.FullName,
                    ug.User.Email
                })
                .ToList()
                .Select(u => string.IsNullOrWhiteSpace(u.FullName)
                    ? u.Email
                    : $"{u.FullName} ({u.Email})")
                .OrderBy(displayName => displayName)
                .ToList();

            return ServiceResult<List<string>>.Ok(participants);
        }

        public ServiceResult<List<ReminderDto>> GetRemindersByAppointmentId(Guid appointmentId)
        {
            // Chỉ dùng cho lịch cá nhân. Reminder của group meeting phải lấy theo UserId + GroupMeetingId.
            if (IsGroupMeeting(appointmentId))
                return ServiceResult<List<ReminderDto>>.Ok(new List<ReminderDto>());

            var reminders = _reminderRepository.Query
                .Where(r => r.AppointmentId == appointmentId
                            && r.UserId == null
                            && r.GroupMeetingId == null)
                .OrderBy(r => r.ReminderTime)
                .Select(r => new ReminderDto
                {
                    ReminderId = r.ReminderId,
                    ReminderTime = r.ReminderTime,
                    Type = r.Type
                })
                .ToList();

            return ServiceResult<List<ReminderDto>>.Ok(reminders);
        }

        public ServiceResult<List<ReminderDto>> GetRemindersForUser(Guid userId, Guid appointmentId)
        {
            if (appointmentId == Guid.Empty)
                return ServiceResult<List<ReminderDto>>.Ok(new List<ReminderDto>());

            var isGroupMeeting = IsGroupMeeting(appointmentId);

            var query = _reminderRepository.Query;

            query = isGroupMeeting
                ? query.Where(r => r.UserId == userId && r.GroupMeetingId == appointmentId)
                : query.Where(r => r.AppointmentId == appointmentId
                                   && r.UserId == null
                                   && r.GroupMeetingId == null);

            var reminders = query
                .OrderBy(r => r.ReminderTime)
                .Select(r => new ReminderDto
                {
                    ReminderId = r.ReminderId,
                    ReminderTime = r.ReminderTime,
                    Type = r.Type
                })
                .ToList();

            return ServiceResult<List<ReminderDto>>.Ok(reminders);
        }

        public ServiceResult<bool> DeleteReminder(Guid reminderId)
        {
            if (_reminderRepository.Remove(reminderId))
            {
                _reminderRepository.SaveChanges();
                return ServiceResult<bool>.Ok();
            }

            return ServiceResult<bool>.Fail("Không tìm thấy reminder.");
        }

        public int GetReminderMinutes(string reminderType)
        {
            return reminderType switch
            {
                "Trước 15 phút" => 15,
                "Trước 30 phút" => 30,
                "Trước 1 tiếng" => 60,
                "Trước 2 tiếng" => 120,
                "Trước 1 ngày" => 1440,
                "Trước 2 ngày" => 2880,
                "Trước 1 tuần" => 10080,
                "Trước 2 tuần" => 20160,
                _ => throw new ArgumentException($"Loại reminder không hợp lệ: {reminderType}")
            };
        }

        public ServiceResult<Guid> SaveAppointment(
            AppointmentDto dto,
            bool isOverwrite = false,
            bool joinGroup = false,
            bool isGroupMode = false,
            IEnumerable<string>? participantEmails = null)
        {
            var errorMessage = Validate(dto);
            if (!string.IsNullOrWhiteSpace(errorMessage))
                return ServiceResult<Guid>.Fail(errorMessage);

            var participantUsersResult = ResolveParticipantUsers(participantEmails, dto.UserId, isGroupMode);
            if (participantUsersResult.Status == HandleStatus.Error)
                return ServiceResult<Guid>.Fail(participantUsersResult.Message);

            var participantUsers = participantUsersResult.Data ?? new List<Users>();

            // Với lịch nhóm, phải kiểm tra cuộc họp nhóm trùng trước khi kiểm tra conflict.
            // Nếu không, người tham gia sẽ bị hỏi "Ghi đè lịch cũ?" thay vì "Tham gia nhóm?".
            if (isGroupMode)
            {
                var normalizedName = dto.Name.Trim().ToLower();
                var matchedGroup = _groupMeetingRepository.Query
                    .FirstOrDefault(g =>
                        g.AppointmentId != dto.AppointmentId &&
                        g.Name.ToLower() == normalizedName &&
                        g.StartTime == dto.StartTime &&
                        g.EndTime == dto.EndTime);

                if (matchedGroup != null)
                {
                    var alreadyJoined = _userGroupRepository.Query.Any(ug =>
                        ug.UserId == dto.UserId &&
                        ug.GroupMeetingId == matchedGroup.AppointmentId);

                    if (alreadyJoined)
                    {
                        AddParticipantsToGroup(participantUsers, matchedGroup.AppointmentId, matchedGroup);
                        ReplaceRemindersForUser(dto, matchedGroup, isGroupMeeting: true);
                        _context.SaveChanges();

                        return ServiceResult<Guid>.Ok(
                            matchedGroup.AppointmentId,
                            "Bạn đã tham gia cuộc họp nhóm này rồi. Đã cập nhật người tham gia và reminder."
                        );
                    }

                    if (!joinGroup)
                    {
                        return ServiceResult<Guid>.AskGroup(
                            "Đã tồn tại cuộc họp nhóm trùng giờ. Bạn có muốn tham gia nhóm này không?"
                        );
                    }

                    if (JoinGroupMeeting(dto.UserId, matchedGroup.AppointmentId))
                    {
                        AddParticipantsToGroup(participantUsers, matchedGroup.AppointmentId, matchedGroup);
                        ReplaceRemindersForUser(dto, matchedGroup, isGroupMeeting: true);
                        _context.SaveChanges();

                        return ServiceResult<Guid>.Ok(
                            matchedGroup.AppointmentId,
                            "Đã tham gia cuộc họp nhóm thành công."
                        );
                    }

                    return ServiceResult<Guid>.Fail("Không thể tham gia cuộc họp nhóm.");
                }
            }

            var conflicts = FindConflicts(dto);
            if (conflicts.Count > 0)
            {
                if (!isOverwrite)
                    return ServiceResult<Guid>.Conflict("Trùng giờ! Bạn có muốn ghi đè lịch cũ không?");

                foreach (var conflict in conflicts)
                {
                    if (IsGroupMeeting(conflict.AppointmentId))
                    {
                        _userGroupRepository.Remove(dto.UserId, conflict.AppointmentId);
                        CleanupGroupIfEmpty(conflict.AppointmentId);
                    }
                    else
                    {
                        _appointmentRepository.Remove(conflict.AppointmentId);
                    }
                }
            }

            Appointments appointment;
            if (dto.AppointmentId == Guid.Empty)
            {
                appointment = isGroupMode ? new GroupMeetings() : new Appointments();
                appointment.AppointmentId = Guid.NewGuid();
                appointment.CalendarId = dto.CalendarId;
                _appointmentRepository.Add(appointment);
            }
            else
            {
                appointment = _appointmentRepository.GetById(dto.AppointmentId)
                              ?? throw new InvalidOperationException("Appointment not found.");
            }

            appointment.Name = dto.Name;
            appointment.Location = dto.Location;
            appointment.StartTime = dto.StartTime;
            appointment.EndTime = dto.EndTime;

            var isActuallyGroupMeeting = isGroupMode || appointment is GroupMeetings || IsGroupMeeting(appointment.AppointmentId);

            if (isActuallyGroupMeeting && dto.UserId == Guid.Empty)
                return ServiceResult<Guid>.Fail("Người dùng không hợp lệ cho cuộc họp nhóm.");

            if (isActuallyGroupMeeting && appointment is GroupMeetings groupMeeting)
            {
                EnsureUserJoinedGroup(dto.UserId, groupMeeting.AppointmentId, groupMeeting);
                AddParticipantsToGroup(participantUsers, groupMeeting.AppointmentId, groupMeeting);
            }

            ReplaceRemindersForUser(dto, appointment, isActuallyGroupMeeting);

            _context.SaveChanges();
            return ServiceResult<Guid>.Ok(appointment.AppointmentId, "Lưu thành công.");
        }

        public ServiceResult<UserGroupMeetings> JoinMeeting(Guid userId, Guid groupMeetingId)
        {
            var alreadyJoined = _userGroupRepository.Query.Any(ug =>
                ug.UserId == userId &&
                ug.GroupMeetingId == groupMeetingId);

            if (alreadyJoined)
            {
                var existing = _userGroupRepository.Query.First(ug =>
                    ug.UserId == userId &&
                    ug.GroupMeetingId == groupMeetingId);

                return ServiceResult<UserGroupMeetings>.Ok(existing);
            }

            var userGroup = new UserGroupMeetings
            {
                UserId = userId,
                GroupMeetingId = groupMeetingId
            };

            _userGroupRepository.Add(userGroup);
            _context.SaveChanges();

            return ServiceResult<UserGroupMeetings>.Ok(userGroup);
        }

        public ServiceResult<Guid> DeleteAppointment(Guid appointmentId, Guid userId)
        {
            if (IsGroupMeeting(appointmentId))
            {
                _userGroupRepository.Remove(userId, appointmentId);
                CleanupGroupIfEmpty(appointmentId);
            }
            else
            {
                _appointmentRepository.Remove(appointmentId);
            }

            _context.SaveChanges();
            return ServiceResult<Guid>.Ok(appointmentId, "Đã xóa thành công.");
        }

        private static string? Validate(AppointmentDto dto)
        {
            if (dto.CalendarId == Guid.Empty)
                return "Lịch trình không hợp lệ.";

            if (string.IsNullOrWhiteSpace(dto.Name))
                return "Tên cuộc hẹn không được để trống.";


            if (dto.EndTime <= dto.StartTime)
                return "Thời gian kết thúc phải lớn hơn thời gian bắt đầu.";

            return null;
        }

        private ServiceResult<List<Users>> ResolveParticipantUsers(
            IEnumerable<string>? participantEmails,
            Guid ownerUserId,
            bool isGroupMode)
        {
            if (!isGroupMode)
                return ServiceResult<List<Users>>.Ok(new List<Users>());

            var normalizedEmails = NormalizeParticipantEmails(participantEmails).ToList();
            if (!normalizedEmails.Any())
                return ServiceResult<List<Users>>.Ok(new List<Users>());

            var users = _context.Users
                .Where(u => normalizedEmails.Contains(u.Email.ToLower()))
                .ToList();

            var foundEmails = users
                .Select(u => u.Email.ToLower())
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            var notFoundEmails = normalizedEmails
                .Where(email => !foundEmails.Contains(email))
                .ToList();

            if (notFoundEmails.Any())
            {
                return ServiceResult<List<Users>>.Fail(
                    "Không tìm thấy user có Gmail: " + string.Join(", ", notFoundEmails)
                );
            }

            return ServiceResult<List<Users>>.Ok(
                users
                    .Where(u => u.UserId != ownerUserId)
                    .GroupBy(u => u.UserId)
                    .Select(g => g.First())
                    .ToList()
            );
        }

        private static IEnumerable<string> NormalizeParticipantEmails(IEnumerable<string>? participantEmails)
        {
            if (participantEmails == null)
                return Enumerable.Empty<string>();

            return participantEmails
                .Where(email => !string.IsNullOrWhiteSpace(email))
                .Select(email => email.Trim().ToLowerInvariant())
                .Where(EmailValidator.IsValid)   
                .Distinct(StringComparer.OrdinalIgnoreCase);
        }

        private void AddParticipantsToGroup(
            IEnumerable<Users> participantUsers,
            Guid groupMeetingId,
            GroupMeetings? groupMeeting = null)
        {
            foreach (var user in participantUsers)
            {
                EnsureUserJoinedGroup(user.UserId, groupMeetingId, groupMeeting);
            }
        }

        private void ReplaceRemindersForUser(AppointmentDto dto, Appointments appointment, bool isGroupMeeting)
        {
            var existingReminders = isGroupMeeting
                ? _reminderRepository.Query
                    .Where(r => r.UserId == dto.UserId && r.GroupMeetingId == appointment.AppointmentId)
                    .ToList()
                : _reminderRepository.Query
                    .Where(r => r.AppointmentId == appointment.AppointmentId
                                && r.UserId == null
                                && r.GroupMeetingId == null)
                    .ToList();

            foreach (var reminder in existingReminders)
                _reminderRepository.Remove(reminder);

            if (dto.Reminders == null || !dto.Reminders.Any())
                return;

            foreach (var reminderDto in dto.Reminders)
            {
                var reminder = new Reminders
                {
                    ReminderId = reminderDto.ReminderId == Guid.Empty
                        ? Guid.NewGuid()
                        : reminderDto.ReminderId,
                    ReminderTime = reminderDto.ReminderTime,
                    Type = reminderDto.Type
                };

                if (isGroupMeeting)
                {
                    // Reminder của group meeting là reminder riêng của từng user.
                    // Không gắn AppointmentId để tránh cả nhóm cùng load chung reminder.
                    reminder.AppointmentId = null;
                    reminder.UserId = dto.UserId;
                    reminder.GroupMeetingId = appointment.AppointmentId;
                }
                else
                {
                    reminder.AppointmentId = appointment.AppointmentId;
                    reminder.UserId = null;
                    reminder.GroupMeetingId = null;
                }

                _reminderRepository.Add(reminder);
            }
        }

        private void EnsureUserJoinedGroup(Guid userId, Guid groupMeetingId, GroupMeetings? groupMeeting = null)
        {
            if (userId == Guid.Empty || groupMeetingId == Guid.Empty)
                return;

            var alreadyJoined = _userGroupRepository.Query.Any(ug =>
                ug.UserId == userId &&
                ug.GroupMeetingId == groupMeetingId);

            if (alreadyJoined)
                return;

            var userGroupMeeting = new UserGroupMeetings
            {
                UserId = userId,
                GroupMeetingId = groupMeetingId
            };

            if (groupMeeting != null)
                userGroupMeeting.GroupMeeting = groupMeeting;

            _userGroupRepository.Add(userGroupMeeting);
        }

        private List<Appointments> FindConflicts(AppointmentDto dto)
        {
            var conflicts = _appointmentRepository.Query
                .Where(a => a.CalendarId == dto.CalendarId
                            && a.StartTime < dto.EndTime
                            && dto.StartTime < a.EndTime);

            if (dto.AppointmentId != Guid.Empty)
                conflicts = conflicts.Where(a => a.AppointmentId != dto.AppointmentId);

            var result = conflicts.ToList();

            if (dto.UserId != Guid.Empty)
            {
                var userGroupIds = _userGroupRepository.Query
                    .Where(ug => ug.UserId == dto.UserId)
                    .Select(ug => ug.GroupMeetingId)
                    .ToList();

                if (dto.AppointmentId != Guid.Empty)
                    userGroupIds = userGroupIds.Where(id => id != dto.AppointmentId).ToList();

                if (userGroupIds.Any())
                {
                    var groupConflicts = _groupMeetingRepository.Query
                        .Where(g => userGroupIds.Contains(g.AppointmentId)
                                    && g.StartTime < dto.EndTime
                                    && dto.StartTime < g.EndTime)
                        .ToList();

                    result.AddRange(groupConflicts);
                }
            }

            return result
                .GroupBy(a => a.AppointmentId)
                .Select(g => g.First())
                .ToList();
        }

        private bool JoinGroupMeeting(Guid userId, Guid groupMeetingId)
        {
            if (userId == Guid.Empty || groupMeetingId == Guid.Empty)
                return false;

            if (_userGroupRepository.Query.Any(ug =>
                    ug.UserId == userId &&
                    ug.GroupMeetingId == groupMeetingId))
                return true;

            _userGroupRepository.Add(new UserGroupMeetings
            {
                UserId = userId,
                GroupMeetingId = groupMeetingId
            });

            return true;
        }

        private void CleanupGroupIfEmpty(Guid groupMeetingId)
        {
            if (_userGroupRepository.Query.Any(ug => ug.GroupMeetingId == groupMeetingId))
                return;

            var group = _groupMeetingRepository.GetById(groupMeetingId);
            if (group != null)
                _groupMeetingRepository.Remove(group);
        }
    }
}
