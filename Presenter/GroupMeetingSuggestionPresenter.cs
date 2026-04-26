using OOAD.Model;
using OOAD.Service;

namespace OOAD.Presenter
{
    public class GroupMeetingSuggestionPresenter
    {
        private readonly GroupMeetingSugestion _view;
        private readonly GroupMeetingService _groupMeetingService;
        private GroupMeetings? _suggestedMeeting;

        public GroupMeetingSuggestionPresenter(
            GroupMeetingSugestion view,
            GroupMeetingService groupMeetingService)
        {
            _view = view;
            _groupMeetingService = groupMeetingService;
        }

        public void Initialize()
        {
            _view.ViewLoaded += OnViewLoaded;
            _view.JoinRequested += OnJoinRequested;
            _view.DeclineRequested += (_, _) => _view.CloseView();
        }

        private void OnViewLoaded(object? sender, EventArgs e)
        {
            _suggestedMeeting = _groupMeetingService.GetGroupMeetings()
                .FirstOrDefault(m => m.StartTime < _view.SelectedEndTime && _view.SelectedStartTime < m.EndTime);

            _view.ShowSuggestion(_suggestedMeeting);
        }

        private void OnJoinRequested(object? sender, EventArgs e)
        {
            if (_suggestedMeeting == null)
            {
                _view.ShowError("Không có cuộc họp nhóm phù hợp.");
                return;
            }

            _view.ShowMessage($"Đã chọn tham gia cuộc họp: {_suggestedMeeting.Name}");
            _view.CloseView();
        }
    }
}
