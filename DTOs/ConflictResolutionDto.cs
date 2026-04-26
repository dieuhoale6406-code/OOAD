namespace OOAD.DTOs
{
    public class ConflictResolutionDto
    {
        public bool HasConflict { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<Guid> ConflictedAppointmentIds { get; set; } = new List<Guid>();
    }
}
