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

        public Calendars? GetCalendarByUserId(Guid userId)
        {
            return _calendarRepository.GetByUserId(userId);
        }
    }
}
