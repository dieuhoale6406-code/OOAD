using OOAD.Data;
using OOAD.Model;

namespace OOAD.Repository
{
    public class CalendarRepository : BaseRepository<Calendars>
    {
        public CalendarRepository(AppDBContext context) : base(context) { }
    }
}