using System.ComponentModel.DataAnnotations;

namespace OOAD.Model
{
    public class Appointments
    {
        [Key]
        public Guid AppointmentId { get; set; }
        public Guid CalendarId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime EndTime { get; set; } = DateTime.Now;
        public Calendars Calendar { get; set; } = null!;
        public List<Reminders> Reminders { get; set; } = new List<Reminders>();
    }
}
