using OOAD.Model;
using OOAD.Repository;

namespace OOAD.Service
{
    public class CalendarService
    {
        private readonly CalendarRepository _calendarRepository;

        public CalendarService(CalendarRepository calendarRepository)
        {
            _calendarRepository = calendarRepository;
        }

        public IEnumerable<Calendars> GetCalendars()
        {
            return _calendarRepository.GetAll();
        }

        public Calendars? GetCalendarById(Guid calendarId)
        {
            return _calendarRepository.GetById(calendarId);
        }

        public Calendars? GetCalendarByUserId(Guid userId)
        {
            return _calendarRepository.GetByUserId(userId);
        }

        public void CreateCalendar(Calendars calendar)
        {
            if (calendar.CalendarId == Guid.Empty)
            {
                calendar.CalendarId = Guid.NewGuid();
            }

            _calendarRepository.Add(calendar);
        }

        public void UpdateCalendar(Calendars calendar)
        {
            _calendarRepository.Update(calendar);
        }

        public void DeleteCalendar(Guid calendarId)
        {
            _calendarRepository.Delete(calendarId);
        }
    }
}