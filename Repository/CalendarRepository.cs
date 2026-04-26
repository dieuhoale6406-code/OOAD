using OOAD.Data;
using OOAD.Model;

namespace OOAD.Repository
{
    public class CalendarRepository
    {
        private readonly AppDBContext _context;

        public CalendarRepository(AppDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Calendars> GetAll()
        {
            return _context.Set<Calendars>().ToList();
        }

        public Calendars? GetById(Guid calendarId)
        {
            return _context.Set<Calendars>().Find(calendarId);
        }

        public Calendars? GetByUserId(Guid userId)
        {
            return _context.Set<Calendars>().FirstOrDefault(c => c.UserId == userId);
        }

        public void Add(Calendars calendar)
        {
            _context.Set<Calendars>().Add(calendar);
            _context.SaveChanges();
        }

        public void Update(Calendars calendar)
        {
            _context.Set<Calendars>().Update(calendar);
            _context.SaveChanges();
        }

        public void Delete(Guid calendarId)
        {
            var calendar = GetById(calendarId);
            if (calendar == null)
            {
                return;
            }

            _context.Set<Calendars>().Remove(calendar);
            _context.SaveChanges();
        }
    }
}
