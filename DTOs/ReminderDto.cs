namespace OOAD.DTOs
{
    public class ReminderDto
    {
        public Guid ReminderId { get; set; }
        public DateTime ReminderTime { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
