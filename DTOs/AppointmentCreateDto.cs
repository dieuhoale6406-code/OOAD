namespace OOAD.DTOs
{
    public class AppointmentCreateDto
    {
        private string _name = string.Empty;
        private string _location = string.Empty;
        public Guid CalendarId { get; set; }
        public string Name {
            get { return _name; }
            set { _name = value.Trim() ?? string.Empty; }
        }
        public string Location {
            get { return _location; }
            set { _location = value.Trim() ?? string.Empty; }
        }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
