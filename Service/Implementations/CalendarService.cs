using OOAD.Model;
using OOAD.Repository.Interfaces;
using OOAD.Service.Interfaces;

namespace OOAD.Service.Implementations
{
    public class CalendarService : ICalendarService
    {
        private readonly ICalendarRepository _calendarRepository;

        public CalendarService(ICalendarRepository calendarRepository)
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
