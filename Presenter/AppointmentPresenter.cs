using OOAD.Model;
using OOAD.Services;
using OOAD.Data;
using OOAD.Repository;
using OOAD.DTOs;

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
            _view.RequestOpenGroupForm += HandleOpenGroupForm;

            OnViewLoaded(this, EventArgs.Empty);
        }

        private void OnViewLoaded(object? sender, EventArgs e)
        {
            _view.CalendarId = _calendarId;
            _view.AppointmentId = _appointmentId;
            if (!_appointmentId.HasValue)
            {
                _view.AddMode = true;
                return;
            }
            _view.AddMode = false;
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

            var isGroupMeeting = _appointmentService.IsGroupMeeting(appointment.AppointmentId);
            _view.IsGroupMode = isGroupMeeting;

            if (isGroupMeeting)
                LoadParticipants(appointment.AppointmentId);
            else
                _view.BindParticipants(Array.Empty<string>());

            LoadReminders(appointment.AppointmentId);
        }

        private void OnSaveRequested(object? sender, EventArgs e)
        {
            var appointmentDto = BuildDto();

            if (appointmentDto.StartTime <= DateTime.Now && !_view.ConfirmCreatePastAppointment())
                return;

            var result = _appointmentService.SaveAppointment(
                appointmentDto,
                isGroupMode: _view.IsGroupMode,
                participantEmails: _view.ParticipantEmails
            );
            HandleSaveResult(appointmentDto, result);
        }

        private void OnAddReminderRequested(object? sender, EventArgs e)
        {
            var reminderType = _view.ReminderType?.Trim();

            if (string.IsNullOrWhiteSpace(reminderType) || reminderType == "Chọn thời gian")
            {
                _view.ShowError("Vui lòng chọn thời gian nhắc nhở.");
                return;
            }

            DateTime reminderTime;

            if (reminderType == "Khác")
            {
                var defaultReminderTime = _view.StartTime.AddMinutes(-10);
                var customTime = _view.AskCustomReminderTime(defaultReminderTime);

                if (!customTime.HasValue)
                    return;

                reminderTime = customTime.Value;

                if (reminderTime >= _view.StartTime)
                {
                    _view.ShowError("Thời gian nhắc nhở phải trước thời gian bắt đầu cuộc hẹn.");
                    return;
                }
            }
            else
            {
                var minutes = _appointmentService.GetReminderMinutes(reminderType);
                reminderTime = _view.StartTime.AddMinutes(-minutes);
            }

            if (reminderTime < DateTime.Now)
            {
                _view.ShowError("Thời gian nhắc nhở đã qua.");
                return;
            }

            var newReminder = new ReminderDto
            {
                ReminderId = Guid.NewGuid(),
                Type = reminderType,
                Message = $"Nhắc nhở: {_view.AppointmentName}",
                ReminderTime = reminderTime
            };

            var item = new ListViewItem(reminderTime.ToString("dd/MM/yyyy HH:mm"));
            item.SubItems.Add(newReminder.Type);
            item.SubItems.Add(newReminder.Message);
            item.Tag = newReminder.ReminderId;

            _view.ReminderList.Items.Add(item);

            foreach (ListViewItem i in _view.ReminderList.Items)
            {
                i.Selected = false;
            }
        }

        private void OnDeleteReminderRequested(object? sender, EventArgs e)
        {
            if (_view.ReminderList.SelectedItems.Count == 0)
            {
                _view.ShowError("Vui lòng chọn reminder để xóa.");
                return;
            }

            var selectedItems = _view.ReminderList.SelectedItems
                .Cast<ListViewItem>()
                .ToList();

            foreach (var item in selectedItems)
            {
                _view.ReminderList.Items.Remove(item);
            }
        }

        private void LoadReminders(Guid appointmentId)
        {
            var reminders = _appointmentService.GetRemindersForUser(_userId, appointmentId);
            _view.BindReminders(reminders.Data?.ToList() ?? new List<ReminderDto>());
        }

        private void LoadParticipants(Guid appointmentId)
        {
            var participants = _appointmentService.GetGroupParticipants(appointmentId);
            _view.BindParticipants(participants.Data ?? new List<string>());
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
                Reminders = _view.Reminders
            };
        }

        private void HandleSaveResult(AppointmentDto dto, ServiceResult<Guid> result)
        {
            switch (result.Status)
            {
                case HandleStatus.Conflict:
                    if (_view.ConfirmOverwrite())
                    {
                        var overwriteResult = _appointmentService.SaveAppointment(
                            dto,
                            isOverwrite: true,
                            isGroupMode: _view.IsGroupMode,
                            participantEmails: _view.ParticipantEmails
                        );
                        HandleSaveResult(dto, overwriteResult);
                    }
                    break;
                case HandleStatus.GroupDecision:
                    if (_view.IsGroupMode && _view.ConfirmJoinGroup())
                    {
                        var joinResult = _appointmentService.SaveAppointment(
                            dto,
                            joinGroup: true,
                            isGroupMode: _view.IsGroupMode,
                            participantEmails: _view.ParticipantEmails
                        );
                        HandleSaveResult(dto, joinResult);
                    }
                    break;
                case HandleStatus.Success:
                    if (result.Data != Guid.Empty)
                    {
                        _appointmentId = result.Data;
                        _view.AppointmentId = result.Data;

                        var isGroupMeeting = _appointmentService.IsGroupMeeting(result.Data);
                        _view.IsGroupMode = isGroupMeeting;

                        if (isGroupMeeting)
                            LoadParticipants(result.Data);
                        else
                            _view.BindParticipants(Array.Empty<string>());

                        _view.ShowMessage(result.Message ?? "Lưu thành công!");
                        LoadReminders(_appointmentId.Value);
                    }
                    else
                        _view.ShowMessage(result.Message ?? "Thành công");
                    break;

                case HandleStatus.Error:
                    _view.ShowError(result.Message);
                    break;
            }
        }

        public void HandleOpenGroupForm(Guid appId, string name, DateTime start, DateTime end)
        {
            var form = new GroupMeetingSugestion(_userId, appId, name, start, end);
            form.ShowDialog(_view);
        }
    }
}