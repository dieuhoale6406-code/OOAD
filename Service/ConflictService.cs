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
                    Message = "Không tìm thấy cuộc hẹn cần kiểm tra.",
                    ConflictedAppointmentIds = new List<Guid>()
                };
            }

            var conflicts = _appointmentRepository.GetConflictedAppointments(
                    current.CalendarId,
                    current.StartTime,
                    current.EndTime,
                    current.AppointmentId
                )
                .Select(a => a.AppointmentId)
                .ToList();

            return new ConflictResolutionDto
            {
                HasConflict = conflicts.Count > 0,
                Message = conflicts.Count > 0
                    ? "Thời gian này đã có cuộc hẹn khác. Bạn muốn thay thế cuộc hẹn cũ hay chọn thời gian khác?"
                    : "Không có xung đột thời gian.",
                ConflictedAppointmentIds = conflicts
            };
        }
    }
}