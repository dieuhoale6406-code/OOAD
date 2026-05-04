using OOAD.Model;
using OOAD.Services;
using OOAD.Data;
using OOAD.Repository;

namespace OOAD.Presenter
{
    public class AppointmentPresenter
    {
        private readonly Appointment _view;
        private readonly AppointmentService _appointmentService;

        private readonly Guid _userId;
        private readonly Guid _calendarId;
        private Guid? _appointmentId;

        public AppointmentPresenter(Appointment view, Guid userId, Guid calendarId, Guid? appointmentId)
        {
            _view = view;
            var context = new AppDBContext();
            _appointmentService = new AppointmentService(context);
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
            _view.RequestOpenConflictForm += HandleOpenConflictForm;
            _view.RequestOpenGroupForm += HandleOpenGroupForm;

            OnViewLoaded(this, EventArgs.Empty);
        }

        private void OnViewLoaded(object? sender, EventArgs e)
        {
            _view.CalendarId = _calendarId;
            _view.AppointmentId = _appointmentId;

            if (!_appointmentId.HasValue)
                return;
            var result = _appointmentService.GetAppointmentById(_appointmentId.Value);
            if (result.Status == HandleStatus.Error)
            {
                _view.ShowError("Không tìm thấy cuộc hẹn.");
                return;
            }
            var appointment = result.Data;
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
            var appointmentDto = BuildDto();
            var result = _appointmentService.SaveAppointment(appointmentDto);
            HandleSaveResult(appointmentDto, result);
        }

        private void OnAddReminderRequested(object? sender, EventArgs e)
        {

        }

        private void OnDeleteReminderRequested(object? sender, EventArgs e)
        {
            if (!_appointmentId.HasValue || !_view.SelectedReminderId.HasValue)
            {
                _view.ShowError("Vui lòng chọn reminder để xóa.");
                return;
            }

            _appointmentService.DeleteReminder(_view.SelectedReminderId.Value);
            LoadReminders(_appointmentId.Value);
        }

        private void LoadReminders(Guid appointmentId)
        {
            var reminders = _appointmentService.GetRemindersByAppointmentId(appointmentId);
            _view.BindReminders(reminders.Data.ToList());
        }

        private AppointmentDto BuildDto()
        {
            return new AppointmentDto
            {
                AppointmentId = _appointmentId ?? Guid.Empty,
                UserId = _userId,
                CalendarId = _calendarId,
                Name = _view.AppointmentName.Trim(),
                Location = _view.Location.Trim(),
                StartTime = _view.StartTime,
                EndTime = _view.EndTime,
                Reminders = _view.Reminders,
                ReminderMinutesBefore = _view.Reminders.Count > 0 ? _appointmentService.GetReminderMinutes(_view.ReminderType) : 0
            };
        }

        private void HandleSaveResult(AppointmentDto dto, ServiceResult<Guid> result)
        {
            switch (result.Status)
            {
                case HandleStatus.Conflict:
                    if (_view.ConfirmOverwrite())
                    {
                        // Đệ quy gọi lại với flag isOverwrite = true
                        var overwriteResult = _appointmentService.SaveAppointment(dto, isOverwrite: true);
                        HandleSaveResult(dto, overwriteResult);
                    }
                    break;

                case HandleStatus.GroupDecision:
                    if (_view.ConfirmJoinGroup())
                    {
                        // Đệ quy gọi lại với flag joinGroup = true
                        var joinResult = _appointmentService.SaveAppointment(dto, joinGroup: true);
                        HandleSaveResult(dto, joinResult);
                    }
                    break;

                case HandleStatus.Success:
                    if (result.Data != null && result.Data != Guid.Empty)
                    {
                        _appointmentId = result.Data;
                        _view.AppointmentId = result.Data;
                        _view.ShowMessage("Lưu thành công!");
                        LoadReminders(_appointmentId.Value);
                    }
                    else
                    {
                        _view.ShowMessage(result.Message ?? "Thành công");
                    }
                    break;

                case HandleStatus.Error:
                    _view.ShowError(result.Message);
                    break;
            }
        }


        public void HandleOpenConflictForm(Guid appointmentId)
        {
            var presenter = new ConflictResolutionPresenter(null, _appointmentService);
            using var form = new ConflictResolution(presenter, appointmentId);
            form.ShowDialog(_view);
        }

        public void HandleOpenGroupForm(Guid appId, string name, DateTime start, DateTime end)
        {
            var form = new GroupMeetingSugestion(_userId, appId, name, start, end);
            form.ShowDialog(_view);
        }
    }
}