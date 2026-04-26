using OOAD.Data;
using OOAD.Model;

namespace OOAD.Repository
{
    public class ReminderRepository
    {
        private readonly AppDBContext _context;

        public ReminderRepository(AppDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Reminders> GetAll()
        {
            return _context.Set<Reminders>().ToList();
        }

        public IEnumerable<Reminders> GetByAppointmentId(Guid appointmentId)
        {
            return _context.Set<Reminders>()
                .Where(r => r.AppointmentId == appointmentId)
                .ToList();
        }

        public Reminders? GetById(Guid reminderId)
        {
            return _context.Set<Reminders>().Find(reminderId);
        }

        public void Add(Reminders reminder)
        {
            _context.Set<Reminders>().Add(reminder);
            _context.SaveChanges();
        }

        public void Update(Reminders reminder)
        {
            _context.Set<Reminders>().Update(reminder);
            _context.SaveChanges();
        }

        public void Delete(Guid reminderId)
        {
            var reminder = GetById(reminderId);
            if (reminder == null)
            {
                return;
            }

            _context.Set<Reminders>().Remove(reminder);
            _context.SaveChanges();
        }
    }
}
