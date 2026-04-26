using OOAD.DTOs;
using OOAD.Model;
using OOAD.Repository.Interfaces;
using OOAD.Service.Interfaces;

namespace OOAD.Service.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public IEnumerable<Appointments> GetAppointmentsByCalendarId(Guid calendarId)
        {
            return _appointmentRepository.GetByCalendarId(calendarId);
        }

        public void CreateAppointment(AppointmentCreateDto dto)
        {
            var appointment = new Appointments
            {
                AppointmentId = Guid.NewGuid(),
                CalendarId = dto.CalendarId,
                Name = dto.Name,
                Location = dto.Location,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };

            _appointmentRepository.Add(appointment);
        }

        public void DeleteAppointment(Guid appointmentId)
        {
            _appointmentRepository.Delete(appointmentId);
        }
    }
}
