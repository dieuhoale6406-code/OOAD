using OOAD.Model;
using OOAD.Service;

namespace OOAD.Presenter
{
    public class GroupMeetingSuggestionPresenter
    {
        private readonly GroupMeetingSugestion _view;
        private readonly GroupMeetingService _groupMeetingService;
        private readonly AppointmentService _appointmentService;

        private GroupMeetings? _suggestedMeeting;

        public GroupMeetingSuggestionPresenter(
            GroupMeetingSugestion view,
            GroupMeetingService groupMeetingService,
            AppointmentService appointmentService)
        {
            _view = view;
            _groupMeetingService = groupMeetingService;
            _appointmentService = appointmentService;
        }

        public void Initialize()
        {
            _view.ViewLoaded += OnViewLoaded;
            _view.JoinRequested += OnJoinRequested;
            _view.DeclineRequested += OnDeclineRequested;
        }

        private void OnViewLoaded(object? sender, EventArgs e)
        {
            _suggestedMeeting = _groupMeetingService.FindMatchingGroupMeeting(
                _view.AppointmentName,
                _view.SelectedStartTime,
                _view.SelectedEndTime);

            _view.ShowSuggestion(_suggestedMeeting);
        }

        private void OnJoinRequested(object? sender, EventArgs e)
        {
            if (_suggestedMeeting == null)
            {
                _view.ShowError("Không có cuộc họp nhóm phù hợp.");
                return;
            }

            if (_view.UserId == Guid.Empty)
            {
                _view.ShowError("Thiếu thông tin user.");
                return;
            }

            _groupMeetingService.JoinGroupMeeting(
                _view.UserId,
                _suggestedMeeting.AppointmentId);

            if (_view.AppointmentId != Guid.Empty)
            {
                _appointmentService.DeleteAppointment(_view.AppointmentId);
            }

            _view.ShowMessage($"Đã tham gia cuộc họp nhóm: {_suggestedMeeting.Name}");
            _view.CloseView();
        }

        private void OnDeclineRequested(object? sender, EventArgs e)
        {
            _view.CloseView();
        }
    }
}