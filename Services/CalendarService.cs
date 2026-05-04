using OOAD.Data;
using OOAD.Model;
using OOAD.Repository;

namespace OOAD.Services
{
    public class CalendarService
    {
        private readonly AppDBContext _context;
        private readonly CalendarRepository _calendarRepository;
        private readonly AppointmentRepository _appointmentRepository;
        private readonly ReminderRepository _reminderRepository;
        private readonly GroupMeetingRepository _groupRepository;
        private readonly UserGroupRepository _userGroupRepository;

        public CalendarService(AppDBContext context)
        {
            _context = context;
            _calendarRepository = new CalendarRepository(_context);
            _appointmentRepository = new AppointmentRepository(_context);
            _reminderRepository = new ReminderRepository(_context);
            _groupRepository = new GroupMeetingRepository(_context);
            _userGroupRepository = new UserGroupRepository(_context);
        }

        public ServiceResult<Calendars> GetCalendarByUserId(Guid userId)
        {
            var calendar = _calendarRepository.Query.FirstOrDefault(c => c.UserId == userId);
            if (calendar == null)
                return ServiceResult<Calendars>.Fail("Không tìm thấy Calendar cho UserId: " + userId);

            return ServiceResult<Calendars>.Ok(calendar);
        }

        // GIỮ HÀM CŨ để các chỗ khác trong project không bị lỗi compile.
        // Hàm này chỉ lấy lịch thuộc calendar, KHÔNG lấy group meeting user đã join.
        public ServiceResult<List<Appointments>> GetAppointments(Guid calendarId)
        {
            var appointments = _appointmentRepository.Query
                .Where(a => a.CalendarId == calendarId)
                .OrderBy(a => a.StartTime)
                .ToList();

            return ServiceResult<List<Appointments>>.Ok(appointments);
        }

        // GIỮ HÀM CŨ để các chỗ khác trong project không bị lỗi compile.
        // Hàm này chỉ lấy lịch thuộc calendar theo ngày, KHÔNG lấy group meeting user đã join.
        public ServiceResult<List<Appointments>> GetAppointmentsByDate(Guid calendarId, DateTime date)
        {
            var startOfDay = date.Date;
            var endOfDay = startOfDay.AddDays(1);

            var appointments = _appointmentRepository.Query
                .Where(a => a.CalendarId == calendarId
                            && a.StartTime >= startOfDay
                            && a.StartTime < endOfDay)
                .OrderBy(a => a.StartTime)
                .ToList();

            return ServiceResult<List<Appointments>>.Ok(appointments);
        }

        // HÀM MỚI: lấy lịch cá nhân + group meeting mà user đã tham gia.
        public ServiceResult<List<Appointments>> GetAppointmentsForUser(Guid userId, Guid calendarId)
        {
            if (userId == Guid.Empty)
                return ServiceResult<List<Appointments>>.Fail("UserId không hợp lệ.");

            if (calendarId == Guid.Empty)
                return ServiceResult<List<Appointments>>.Fail("CalendarId không hợp lệ.");

            var joinedGroupIds = _userGroupRepository.Query
                .Where(ug => ug.UserId == userId)
                .Select(ug => ug.GroupMeetingId)
                .ToList();

            var appointments = _appointmentRepository.Query
                .Where(a => a.CalendarId == calendarId || joinedGroupIds.Contains(a.AppointmentId))
                .OrderBy(a => a.StartTime)
                .ToList();

            return ServiceResult<List<Appointments>>.Ok(appointments);
        }

        // HÀM MỚI: lấy lịch cá nhân + group meeting đã join theo ngày đang chọn.
        public ServiceResult<List<Appointments>> GetAppointmentsForUserByDate(Guid userId, Guid calendarId, DateTime date)
        {
            if (userId == Guid.Empty)
                return ServiceResult<List<Appointments>>.Fail("UserId không hợp lệ.");

            if (calendarId == Guid.Empty)
                return ServiceResult<List<Appointments>>.Fail("CalendarId không hợp lệ.");

            var startOfDay = date.Date;
            var endOfDay = startOfDay.AddDays(1);

            var joinedGroupIds = _userGroupRepository.Query
                .Where(ug => ug.UserId == userId)
                .Select(ug => ug.GroupMeetingId)
                .ToList();

            var appointments = _appointmentRepository.Query
                .Where(a => (a.CalendarId == calendarId || joinedGroupIds.Contains(a.AppointmentId))
                            && a.StartTime >= startOfDay
                            && a.StartTime < endOfDay)
                .OrderBy(a => a.StartTime)
                .ToList();

            return ServiceResult<List<Appointments>>.Ok(appointments);
        }

        public ServiceResult<bool> DeleteAppointment(Guid? userId, Guid appointmentId)
        {
            if (appointmentId == Guid.Empty)
                return ServiceResult<bool>.Fail("AppointmentId không hợp lệ.");

            var groupMeeting = _groupRepository.GetById(appointmentId);

            // Phải xử lý group meeting TRƯỚC appointment thường.
            // Vì GroupMeetings kế thừa Appointments, nên _appointmentRepository.GetById(groupId)
            // cũng có thể trả về record group. Nếu check appointment trước sẽ xóa cả cuộc họp nhóm.
            if (groupMeeting != null)
            {
                if (!userId.HasValue || userId.Value == Guid.Empty)
                    return ServiceResult<bool>.Fail("Không xác định được người dùng để xóa khỏi nhóm.");

                foreach (var reminder in _reminderRepository.GetRemindersByGroupMeetingId(userId.Value, appointmentId).ToList())
                    _reminderRepository.Remove(reminder);

                _userGroupRepository.Remove(userId.Value, appointmentId);

                if (_userGroupRepository.IsGroupMeetingEmpty(appointmentId))
                {
                    foreach (var reminder in _reminderRepository.GetRemindersByAppointmentId(appointmentId).ToList())
                        _reminderRepository.Remove(reminder);

                    _groupRepository.Remove(groupMeeting);
                }

                _context.SaveChanges();
                return ServiceResult<bool>.Ok(true, "Đã rời khỏi cuộc họp nhóm.");
            }

            var appointment = _appointmentRepository.GetById(appointmentId);
            if (appointment == null)
                return ServiceResult<bool>.Fail("Không tìm thấy cuộc hẹn với Id: " + appointmentId);

            foreach (var reminder in _reminderRepository.GetRemindersByAppointmentId(appointmentId).ToList())
                _reminderRepository.Remove(reminder);

            _appointmentRepository.Remove(appointment);
            _context.SaveChanges();

            return ServiceResult<bool>.Ok(true, "Đã xóa cuộc hẹn.");
        }
    }
}
