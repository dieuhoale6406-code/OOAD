using OOAD.DTOs;
using OOAD.Repository;

namespace OOAD.Service
{
    public class ConflictService
    {
        private readonly AppointmentRepository _appointmentRepository;

        public ConflictService(AppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public ConflictResolutionDto ResolveConflict(Guid appointmentId)
        {
            var current = _appointmentRepository.GetById(appointmentId);
            if (current == null)
            {
                return new ConflictResolutionDto
                {
                    HasConflict = false,
                    Message = "Appointment not found"
                };
            }

            var conflicts = _appointmentRepository.GetByCalendarId(current.CalendarId)
                .Where(a => a.AppointmentId != current.AppointmentId)
                .Where(a => a.StartTime < current.EndTime && current.StartTime < a.EndTime)
                .Select(a => a.AppointmentId)
                .ToList();

            return new ConflictResolutionDto
            {
                HasConflict = conflicts.Count > 0,
                Message = conflicts.Count > 0 ? "Conflict detected" : "No conflict",
                ConflictedAppointmentIds = conflicts
            };
        }
    }
}
