using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualBasic.ApplicationServices;
using OOAD.Data;
using OOAD.DTOs;
using OOAD.Model;
using OOAD.Repository;

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
            if(appointment == null)
                return ServiceResult<Appointments>.Fail("Không tìm thấy cuộc hẹn.");
            return ServiceResult<Appointments>.Ok(appointment);
        }

        public ServiceResult<List<ReminderDto>> GetRemindersByAppointmentId(Guid appointmentId)
        {
            var reminders = _reminderRepository.Query
                .Where(r => r.AppointmentId == appointmentId)
                .OrderBy(r => r.ReminderTime)
                .Select(r => new ReminderDto
                {
                    ReminderTime = r.ReminderTime,
                    Type = r.Type,
                    Message = r.Message
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
            return ServiceResult<bool>.Fail("không tìm thấy reminder");
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
                _ => 10
            };
        }

        public ServiceResult<Guid> SaveAppointment(AppointmentDto dto, bool isOverwrite = false, bool joinGroup = false)
        {
            var errorMessage = Validate(dto);
            if (!string.IsNullOrWhiteSpace(errorMessage))
                return ServiceResult<Guid>.Fail(errorMessage);
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
                        _appointmentRepository.Remove(conflict.AppointmentId);
                }
            }

            var matchedGroup = _groupMeetingRepository.Query
                                .FirstOrDefault(g => g.Name.ToLower() == dto.Name.ToLower()
                                                && g.StartTime == dto.StartTime
                                                && g.EndTime == dto.EndTime);
            if (matchedGroup != null)
            {
                if (!joinGroup)
                    return ServiceResult<Guid>.AskGroup("Có cuộc họp nhóm trùng giờ, bạn muốn tham gia không?");
                if (JoinGroupMeeting(dto.UserId, matchedGroup.AppointmentId))
                    return ServiceResult<Guid>.Ok(matchedGroup.AppointmentId, "Đã tham gia cuộc họp nhóm.");
                return ServiceResult<Guid>.Fail("Không thể tham gia cuộc họp nhóm.");
            }
            var appointment = dto.AppointmentId != Guid.Empty
                ? _appointmentRepository.GetById(dto.AppointmentId)
                : null;
            if (appointment == null)
            {
                appointment = new Appointments
                {
                    AppointmentId = Guid.NewGuid(),
                    CalendarId = dto.CalendarId
                };
                _appointmentRepository.Add(appointment);
            }

            appointment.Name = dto.Name;
            appointment.Location = dto.Location;
            appointment.StartTime = dto.StartTime;
            appointment.EndTime = dto.EndTime;

            var existingReminders = _reminderRepository.GetRemindersByAppointmentId(appointment.AppointmentId).ToList();
            foreach (var reminder in existingReminders)
                _reminderRepository.Remove(reminder);

            if (dto.Reminders != null)
            {
                foreach (var reminderDto in dto.Reminders)
                {
                    _reminderRepository.Add(new Reminders
                    {
                        ReminderId = Guid.NewGuid(),
                        AppointmentId = appointment.AppointmentId,
                        ReminderTime = reminderDto.ReminderTime,
                        Type = reminderDto.Type,
                        Message = $"Nhắc nhở: {appointment.Name}"
                    });
                }
            }

            _appointmentRepository.SaveChanges();
            return ServiceResult<Guid>.Ok(appointment.AppointmentId, "Lưu thành công.");
        }

        public ServiceResult<Guid> DeleteAppointment(Guid appointmentId, Guid userId)
        {
            if (IsGroupMeeting(appointmentId))
            {
                _userGroupRepository.Remove(userId, appointmentId);
                CleanupGroupIfEmpty(appointmentId);
            }
            else
                _appointmentRepository.Remove(appointmentId);

            _appointmentRepository.SaveChanges();
            return ServiceResult<Guid>.Ok("Đã xóa thành công.");
        }

        #region Private Helpers
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
            return result;
        }

        private bool JoinGroupMeeting(Guid userId, Guid groupMeetingId)
        {
            if (userId == Guid.Empty || groupMeetingId == Guid.Empty) return false;
            if (_userGroupRepository.Query.Any(ug => ug.UserId == userId && ug.GroupMeetingId == groupMeetingId))
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

        private bool IsGroupMeeting(Guid appointmentId)
        {
            return _groupMeetingRepository.Query.Any(g => g.AppointmentId == appointmentId);
        }

        #endregion
    }
}