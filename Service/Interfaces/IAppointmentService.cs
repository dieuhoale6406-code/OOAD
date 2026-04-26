using OOAD.DTOs;
using OOAD.Model;

namespace OOAD.Service.Interfaces
{
    public interface IAppointmentService
    {
        IEnumerable<Appointments> GetAppointmentsByCalendarId(Guid calendarId);
        void CreateAppointment(AppointmentCreateDto dto);
        void DeleteAppointment(Guid appointmentId);
    }
}
