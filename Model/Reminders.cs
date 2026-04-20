using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OOAD.Model
{
    public class Reminders
    {
        [Key]
        public Guid ReminderId { get; set; }
        public Guid AppointmentId { get; set; }
        public DateTime ReminderTime { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public Appointments Appointment { get; set; } = null!;
    }
}
