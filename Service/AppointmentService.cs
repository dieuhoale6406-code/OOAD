using OOAD.DTOs;
using OOAD.Model;
using OOAD.Repository;

namespace OOAD.Service
{
    public class AppointmentService
    {
        private readonly AppointmentRepository _appointmentRepository;

        public AppointmentService(AppointmentRepository appointmentRepository)
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
