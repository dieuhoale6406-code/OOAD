using OOAD.Services;
using OOAD.Data;
using OOAD.Repository;
using OOAD.Model;

namespace OOAD.Presenter
{
    public class GroupMeetingSuggestionPresenter
    {
        private GroupMeetingSugestion _view;
        private AppointmentService _appointmentService;

        public GroupMeetingSuggestionPresenter(
            GroupMeetingSugestion view)
        {
            _view = view;
            var context = new AppDBContext();
            _appointmentService = new AppointmentService(context);
        }

        public void Initialize()
        {
            _view.ViewLoaded += OnViewLoaded;
            _view.JoinRequested += (_, _) => _view.CloseView();
            _view.DeclineRequested += (_, _) => _view.CloseView();
        }
        private void OnViewLoaded(object? sender, EventArgs e)
        {
            _view.ShowMessage("Gợi ý tham gia nhóm được xử lý trực tiếp trong màn hình cuộc hẹn.");
            _view.CloseView();
        }
    }
}