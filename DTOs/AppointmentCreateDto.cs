namespace OOAD.DTOs
{
    public class AppointmentCreateDto
    {
        public Guid CalendarId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
