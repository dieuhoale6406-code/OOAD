using OOAD.Model;

namespace OOAD.Service.Interfaces
{
    public interface ICalendarService
    {
        IEnumerable<Calendars> GetCalendars();
        Calendars? GetCalendarByUserId(Guid userId);
    }
}
