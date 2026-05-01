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

        public IEnumerable<Reminders> GetReminders()
        {
            return _reminderRepository.GetAll();
        }

        public IEnumerable<Reminders> GetRemindersByAppointmentId(Guid appointmentId)
        {
            return _reminderRepository.GetByAppointmentId(appointmentId);
        }

        public Reminders? GetById(Guid reminderId)
        {
            return _reminderRepository.GetById(reminderId);
        }

        public void CreateReminder(Reminders reminder)
        {
            if (reminder.AppointmentId == Guid.Empty)
                throw new ArgumentException("AppointmentId không hợp lệ.");

            if (reminder.ReminderId == Guid.Empty)
            {
                reminder.ReminderId = Guid.NewGuid();
            }

            if (string.IsNullOrWhiteSpace(reminder.Type))
            {
                reminder.Type = "Default";
            }

            if (string.IsNullOrWhiteSpace(reminder.Message))
            {
                reminder.Message = "Reminder";
            }

            _reminderRepository.Add(reminder);
        }

        public void UpdateReminder(Reminders reminder)
        {
            _reminderRepository.Update(reminder);
        }

        public void DeleteReminder(Guid reminderId)
        {
            _reminderRepository.Delete(reminderId);
        }
    }
}