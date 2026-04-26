using OOAD.Model;

namespace OOAD.Repository.Interfaces
{
    public interface IReminderRepository
    {
        IEnumerable<Reminders> GetAll();
        IEnumerable<Reminders> GetByAppointmentId(Guid appointmentId);
        Reminders? GetById(Guid reminderId);
        void Add(Reminders reminder);
        void Update(Reminders reminder);
        void Delete(Guid reminderId);
    }
}
