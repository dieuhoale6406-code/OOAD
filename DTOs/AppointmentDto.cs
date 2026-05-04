using OOAD.DTOs;
using OOAD.Model;

public class AppointmentDto
{
    private string _name = string.Empty;
    private string _location = string.Empty;

    // Dữ liệu chính
    public Guid AppointmentId { get; set; }
    public Guid UserId { get; set; }
    public Guid CalendarId { get; set; }
    public string Name
    {
        get { return _name; }
        set { _name = value.Trim() ?? string.Empty; }
    }
    public string Location
    {
        get { return _location; }
        set { _location = value.Trim() ?? string.Empty; }
    }
    public DateTime StartTime { get; set; } = DateTime.Now;
    public DateTime EndTime { get; set; } = DateTime.Now;

    public List<ReminderDto> Reminders { get; set; }

    public AppointmentDto() { }
}