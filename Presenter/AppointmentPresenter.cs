using OOAD.DTOs;
using OOAD.Model;
using OOAD.Service;

namespace OOAD.Presenter
{
    public class AppointmentPresenter
    {
        private readonly Appointment _view;
        private readonly AppointmentService _appointmentService;
        private readonly ReminderService _reminderService;
        private readonly ConflictService _conflictService;
        private readonly GroupMeetingService _groupMeetingService;

        private readonly Guid _userId;
        private readonly Guid _calendarId;
        private Guid? _appointmentId;

        public AppointmentPresenter(
            Appointment view,
            AppointmentService appointmentService,
            ReminderService reminderService,
            ConflictService conflictService,
            GroupMeetingService groupMeetingService,
            Guid userId,
            Guid calendarId,
            Guid? appointmentId)
        {
            _view = view;
            _appointmentService = appointmentService;
            _reminderService = reminderService;
            _conflictService = conflictService;
            _groupMeetingService = groupMeetingService;
            _userId = userId;
            _calendarId = calendarId;
            _appointmentId = appointmentId;
        }

        public void Initialize()
        {
            _view.ViewLoaded += OnViewLoaded;
            _view.SaveRequested += OnSaveRequested;
            _view.CancelRequested += (_, _) => _view.CloseView();
            _view.AddReminderRequested += OnAddReminderRequested;
            _view.DeleteReminderRequested += OnDeleteReminderRequested;
        }

        private void OnViewLoaded(object? sender, EventArgs e)
        {
            _view.CalendarId = _calendarId;
            _view.AppointmentId = _appointmentId;

            if (!_appointmentId.HasValue)
                return;

            var appointment = _appointmentService.GetById(_appointmentId.Value);

            if (appointment == null)
            {
                _view.ShowError("Không tìm thấy cuộc hẹn.");
                return;
            }

            _view.AppointmentName = appointment.Name;
            _view.Location = appointment.Location;
            _view.StartTime = appointment.StartTime;
            _view.EndTime = appointment.EndTime;

            LoadReminders(appointment.AppointmentId);
        }

        private void OnSaveRequested(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_view.AppointmentName))
            {
                _view.ShowError("Tên cuộc hẹn không được để trống.");
                return;
            }

            if (_view.EndTime <= _view.StartTime)
            {
                _view.ShowError("Thời gian kết thúc phải lớn hơn thời gian bắt đầu.");
                return;
            }

            var dto = new AppointmentCreateDto
            {
                CalendarId = _calendarId,
                Name = _view.AppointmentName.Trim(),
                Location = _view.Location.Trim(),
                StartTime = _view.StartTime,
                EndTime = _view.EndTime
            };

            if (_appointmentId.HasValue)
            {
                _appointmentService.UpdateAppointment(_appointmentId.Value, dto);
                _view.ShowMessage("Cập nhật cuộc hẹn thành công.");
                return;
            }

            var createdAppointment = _appointmentService.CreateAppointment(dto);

            _appointmentId = createdAppointment.AppointmentId;
            _view.AppointmentId = createdAppointment.AppointmentId;

            var conflictResult = _conflictService.ResolveConflict(createdAppointment.AppointmentId);

            if (conflictResult.HasConflict)
            {
                _view.OpenConflictResolution(createdAppointment.AppointmentId);

                var stillExists = _appointmentService.GetById(createdAppointment.AppointmentId) != null;

                if (!stillExists)
                {
                    _view.ShowMessage("Cuộc hẹn mới đã bị hủy. Vui lòng chọn thời gian khác.");
                    return;
                }
            }

            var matchedGroupMeeting = _groupMeetingService.FindMatchingGroupMeeting(
                createdAppointment.Name,
                createdAppointment.StartTime,
                createdAppointment.EndTime);

            if (matchedGroupMeeting != null)
            {
                _view.OpenGroupMeetingSuggestion(
                    _userId,
                    createdAppointment.AppointmentId,
                    createdAppointment.Name,
                    createdAppointment.StartTime,
                    createdAppointment.EndTime);

                var stillExists = _appointmentService.GetById(createdAppointment.AppointmentId) != null;

                if (!stillExists)
                    return;
            }

            _view.ShowMessage("Thêm cuộc hẹn thành công.");
        }

        private void OnAddReminderRequested(object? sender, EventArgs e)
        {
            if (!_appointmentId.HasValue)
            {
                _view.ShowError("Vui lòng lưu cuộc hẹn trước khi thêm reminder.");
                return;
            }

            var reminderTime = CalculateReminderTime(_view.StartTime, _view.ReminderType);

            var reminder = new Reminders
            {
                ReminderId = Guid.NewGuid(),
                AppointmentId = _appointmentId.Value,
                ReminderTime = reminderTime,
                Type = _view.ReminderType,
                Message = $"Reminder: {_view.AppointmentName}"
            };

            _reminderService.CreateReminder(reminder);
            LoadReminders(_appointmentId.Value);
        }

        private void OnDeleteReminderRequested(object? sender, EventArgs e)
        {
            if (!_appointmentId.HasValue)
            {
                _view.ShowError("Chưa có cuộc hẹn để xóa reminder.");
                return;
            }

            var reminderId = _view.SelectedReminderId;

            if (!reminderId.HasValue)
            {
                _view.ShowError("Vui lòng chọn reminder để xóa.");
                return;
            }

            _reminderService.DeleteReminder(reminderId.Value);
            LoadReminders(_appointmentId.Value);
        }

        private void LoadReminders(Guid appointmentId)
        {
            var reminders = _reminderService.GetRemindersByAppointmentId(appointmentId);
            _view.BindReminders(reminders);
        }

        private static DateTime CalculateReminderTime(DateTime start, string reminderType)
        {
            return reminderType switch
            {
                "Trước 15 phút" => start.AddMinutes(-15),
                "Trước 30 phút" => start.AddMinutes(-30),
                "Trước 1 tiếng" => start.AddHours(-1),
                "Trước 2 tiếng" => start.AddHours(-2),
                "Trước 1 ngày" => start.AddDays(-1),
                "Trước 2 ngày" => start.AddDays(-2),
                "Trước 1 tuần" => start.AddDays(-7),
                "Trước 2 tuần" => start.AddDays(-14),
                _ => start.AddMinutes(-10)
            };
        }
    }
}