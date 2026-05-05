using OOAD.Services;
using OOAD.Data;

namespace OOAD.Presenter
{
    public class GroupMeetingSuggestionPresenter
    {
        private GroupMeetingSugestion _view;
        private AppointmentService _appointmentService;
        private Guid _userId;
        private Guid _groupMeetingId;

        public GroupMeetingSuggestionPresenter(GroupMeetingSugestion view, Guid userId, Guid groupMeetingId)
        {
            _view = view;
            var context = new AppDBContext();
            _appointmentService = new AppointmentService(context);
            _userId = userId;
            _groupMeetingId = groupMeetingId;
        }

        public void Initialize()
        {
            _view.ViewLoaded += OnViewLoaded;
            _view.JoinRequested += (_, _) => 
            {
                HandleJoinRequest();
                _view.CloseView();
            };
            _view.DeclineRequested += (_, _) => _view.CloseView();
        }

        private void OnViewLoaded(object? sender, EventArgs e)
        {
            _view.ShowMessage("Gợi ý tham gia nhóm được xử lý trực tiếp trong màn hình cuộc hẹn.");
            _view.CloseView();
        }

        private void HandleJoinRequest()
        {
            _appointmentService.JoinMeeting(_userId, _groupMeetingId);
        }
    }
}