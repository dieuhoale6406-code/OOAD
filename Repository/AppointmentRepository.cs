using Microsoft.EntityFrameworkCore;
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
            return _context.Set<Appointments>()
                .Include(a => a.Reminders)
                .ToList();
        }

        public IEnumerable<Appointments> GetByCalendarId(Guid calendarId)
        {
            return _context.Set<Appointments>()
                .Include(a => a.Reminders)
                .Where(a => a.CalendarId == calendarId)
                .OrderBy(a => a.StartTime)
                .ToList();
        }

        public Appointments? GetById(Guid appointmentId)
        {
            return _context.Set<Appointments>()
                .Include(a => a.Reminders)
                .FirstOrDefault(a => a.AppointmentId == appointmentId);
        }

        public IEnumerable<Appointments> GetConflictedAppointments(
            Guid calendarId,
            DateTime startTime,
            DateTime endTime,
            Guid? excludedAppointmentId = null)
        {
            var query = _context.Set<Appointments>()
                .Where(a =>
                    a.CalendarId == calendarId &&
                    a.StartTime < endTime &&
                    startTime < a.EndTime);

            if (excludedAppointmentId.HasValue)
            {
                query = query.Where(a => a.AppointmentId != excludedAppointmentId.Value);
            }

            return query
                .OrderBy(a => a.StartTime)
                .ToList();
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
                return;

            _context.Set<Appointments>().Remove(appointment);
            _context.SaveChanges();
        }
    }
}