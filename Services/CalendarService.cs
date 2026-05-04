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

        public ServiceResult<List<Appointments>> GetAppointments(Guid calendarId)
        {
            var appointments = _appointmentRepository.Query.Where(a => a.CalendarId == calendarId).ToList();
            return ServiceResult<List<Appointments>>.Ok(appointments);
        }
        public ServiceResult<List<Appointments>> GetAppointmentsByDate(Guid calendarId, DateTime date)
        {
            var appointments = _appointmentRepository.Query
                .Where(a => a.CalendarId == calendarId && a.StartTime.Date == date.Date)
                .ToList();
            return ServiceResult<List<Appointments>>.Ok(appointments);
        }
        public ServiceResult<bool> DeleteAppointment(Guid? userId, Guid appointmentId)
        {
            var appointment = _appointmentRepository.GetById(appointmentId);
            var groupMeeting = _groupRepository.GetById(appointmentId);
            if (appointment == null && groupMeeting == null)
                return ServiceResult<bool>.Fail("Không tìm thấy cuộc hẹn với Id: " + appointmentId);
            if(groupMeeting != null && (!userId.HasValue || userId == Guid.Empty))
                return ServiceResult<bool>.Fail("Không xác định được người dùng để xóa khỏi nhóm");
            if(appointment != null)
            {
                foreach(var reminder in _reminderRepository.GetRemindersByAppointmentId(appointmentId).ToList())
                    _reminderRepository.Remove(reminder);
                _appointmentRepository.Remove(appointment);
            }
            else if(groupMeeting != null)
            {
                foreach(var reminder in _reminderRepository.GetRemindersByGroupMeetingId(userId.Value, appointmentId).ToList())
                    _reminderRepository.Remove(reminder);
                _userGroupRepository.Remove(userId.Value, appointmentId);
                if(_userGroupRepository.IsGroupMeetingEmpty(appointmentId))
                    _groupRepository.Remove(groupMeeting);
            }
            _context.SaveChanges();
            return ServiceResult<bool>.Ok(true);
        }
    }
}