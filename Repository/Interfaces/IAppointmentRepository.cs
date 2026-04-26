using OOAD.Model;

namespace OOAD.Repository.Interfaces
{
    public interface IAppointmentRepository
    {
        IEnumerable<Appointments> GetAll();
        IEnumerable<Appointments> GetByCalendarId(Guid calendarId);
        Appointments? GetById(Guid appointmentId);
        void Add(Appointments appointment);
        void Update(Appointments appointment);
        void Delete(Guid appointmentId);
    }
}
