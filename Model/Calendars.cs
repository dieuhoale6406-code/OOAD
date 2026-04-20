using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OOAD.Model
{
    public class Calendars
    {
        [Key]
        public Guid CalendarId { get; set; }
        public Guid UserId { get; set; }
        public Users User { get; set; } = null!;
        public List<Appointments> Appointments { get; set; } = new List<Appointments>();
    }
}
