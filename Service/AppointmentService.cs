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

        public IEnumerable<Appointments> GetAppointments()
        {
            return _appointmentRepository.GetAll();
        }

        public IEnumerable<Appointments> GetAppointmentsByCalendarId(Guid calendarId)
        {
            return _appointmentRepository.GetByCalendarId(calendarId);
        }

        public Appointments? GetById(Guid appointmentId)
        {
            return _appointmentRepository.GetById(appointmentId);
        }

        public Appointments CreateAppointment(AppointmentCreateDto dto)
        {
            ValidateAppointment(dto);

            var appointment = new Appointments
            {
                AppointmentId = Guid.NewGuid(),
                CalendarId = dto.CalendarId,
                Name = dto.Name.Trim(),
                Location = dto.Location.Trim(),
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };

            _appointmentRepository.Add(appointment);

            return appointment;
        }

        public void UpdateAppointment(Guid appointmentId, AppointmentCreateDto dto)
        {
            ValidateAppointment(dto);

            var appointment = _appointmentRepository.GetById(appointmentId);

            if (appointment == null)
                throw new Exception("Không tìm thấy cuộc hẹn cần cập nhật.");

            appointment.Name = dto.Name.Trim();
            appointment.Location = dto.Location.Trim();
            appointment.StartTime = dto.StartTime;
            appointment.EndTime = dto.EndTime;

            _appointmentRepository.Update(appointment);
        }

        public void DeleteAppointment(Guid appointmentId)
        {
            _appointmentRepository.Delete(appointmentId);
        }

        public IEnumerable<Appointments> GetConflictedAppointments(
            Guid calendarId,
            DateTime startTime,
            DateTime endTime,
            Guid? excludedAppointmentId = null)
        {
            return _appointmentRepository.GetConflictedAppointments(
                calendarId,
                startTime,
                endTime,
                excludedAppointmentId
            );
        }

        private static void ValidateAppointment(AppointmentCreateDto dto)
        {
            if (dto.CalendarId == Guid.Empty)
                throw new ArgumentException("CalendarId không hợp lệ.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Tên cuộc hẹn không được để trống.");

            if (dto.EndTime <= dto.StartTime)
                throw new ArgumentException("Thời gian kết thúc phải lớn hơn thời gian bắt đầu.");
        }
    }
}