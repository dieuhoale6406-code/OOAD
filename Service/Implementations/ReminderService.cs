using OOAD.Model;
using OOAD.Repository.Interfaces;
using OOAD.Service.Interfaces;

namespace OOAD.Service.Implementations
{
    public class ReminderService : IReminderService
    {
        private readonly IReminderRepository _reminderRepository;

        public ReminderService(IReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }

        public IEnumerable<Reminders> GetRemindersByAppointmentId(Guid appointmentId)
        {
            return _reminderRepository.GetByAppointmentId(appointmentId);
        }

        public void CreateReminder(Reminders reminder)
        {
            if (reminder.ReminderId == Guid.Empty)
            {
                reminder.ReminderId = Guid.NewGuid();
            }

            _reminderRepository.Add(reminder);
        }
    }
}
