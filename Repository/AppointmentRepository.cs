using OOAD.Data;
using OOAD.Model;

namespace OOAD.Repository
{
    public class AppointmentRepository
    {
        private readonly AppDBContext _context;

        public AppointmentRepository(AppDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Appointments> GetAll()
        {
            return _context.Set<Appointments>().ToList();
        }

        public IEnumerable<Appointments> GetByCalendarId(Guid calendarId)
        {
            return _context.Set<Appointments>()
                .Where(a => a.CalendarId == calendarId)
                .ToList();
        }

        public Appointments? GetById(Guid appointmentId)
        {
            return _context.Set<Appointments>().Find(appointmentId);
        }

        public void Add(Appointments appointment)
        {
            _context.Set<Appointments>().Add(appointment);
            _context.SaveChanges();
        }

        public void Update(Appointments appointment)
        {
            _context.Set<Appointments>().Update(appointment);
            _context.SaveChanges();
        }

        public void Delete(Guid appointmentId)
        {
            var appointment = GetById(appointmentId);
            if (appointment == null)
            {
                return;
            }

            _context.Set<Appointments>().Remove(appointment);
            _context.SaveChanges();
        }
    }
}
