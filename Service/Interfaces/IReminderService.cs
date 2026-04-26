using OOAD.Model;

namespace OOAD.Service.Interfaces
{
    public interface IReminderService
    {
        IEnumerable<Reminders> GetRemindersByAppointmentId(Guid appointmentId);
        void CreateReminder(Reminders reminder);
    }
}
