using OOAD.Model;
using OOAD.Repository;
namespace OOAD.Service
{
    public class ReminderService
    {
        private readonly ReminderRepository _reminderRepository;

        public ReminderService(ReminderRepository reminderRepository)
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

        public void DeleteReminder(Guid reminderId)
        {
            _reminderRepository.Delete(reminderId);
        }
    }
}
