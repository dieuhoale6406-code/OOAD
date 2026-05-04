using OOAD.Data;
using OOAD.Services;

namespace OOAD.Presenter
{
    public class ConflictResolutionPresenter
    {
        private ConflictResolution _view;
        private AppointmentService _appointmentService;

        public ConflictResolutionPresenter(ConflictResolution view)
        {
            _view = view;
            var context = new AppDBContext();
            _appointmentService = new AppointmentService(context);
        }

        public void AttachView(ConflictResolution view)
        {
            _view = view;
        }

        public void Initialize()
        {
            _view.ViewLoaded += OnViewLoaded;
            _view.ConfirmRequested += (_, _) => _view.CloseView();
            _view.CancelRequested += (_, _) => _view.CloseView();
        }

        private void OnViewLoaded(object? sender, EventArgs e)
        {
            _view.ShowMessage("Xử lý xung đột đã được chuyển sang màn hình cuộc hẹn.");
            _view.CloseView();
        }
    }
}