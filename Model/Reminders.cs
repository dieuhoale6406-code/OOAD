using System;
using System.ComponentModel.DataAnnotations;

namespace OOAD.Model
{
    public class Reminders
    {
        [Key]
        public Guid ReminderId { get; set; }
        public DateTime ReminderTime { get; set; }
        public string Type { get; set; } = string.Empty;

        // Appointment
        public Guid? AppointmentId { get; set; }
        public Appointments? Appointment { get; set; }

        // GroupMeeting
        public Guid? UserId { get; set; }
        public Guid? GroupMeetingId { get; set; }
        public UserGroupMeetings? UserGroupMeeting { get; set; }
    }
}
