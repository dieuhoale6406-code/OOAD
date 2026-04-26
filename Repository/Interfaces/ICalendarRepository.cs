using OOAD.Model;

namespace OOAD.Repository.Interfaces
{
    public interface ICalendarRepository
    {
        IEnumerable<Calendars> GetAll();
        Calendars? GetById(Guid calendarId);
        Calendars? GetByUserId(Guid userId);
        void Add(Calendars calendar);
        void Update(Calendars calendar);
        void Delete(Guid calendarId);
    }
}
