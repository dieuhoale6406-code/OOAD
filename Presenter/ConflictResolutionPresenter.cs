using OOAD.Service;

namespace OOAD.Presenter
{
    public class ConflictResolutionPresenter
    {
        private readonly ConflictResolution _view;
        private readonly ConflictService _conflictService;
        private readonly AppointmentService _appointmentService;

        private List<Guid> _conflictedIds = new List<Guid>();

        public ConflictResolutionPresenter(
            ConflictResolution view,
            ConflictService conflictService,
            AppointmentService appointmentService)
        {
            _view = view;
            _conflictService = conflictService;
            _appointmentService = appointmentService;
        }

        public void Initialize()
        {
            _view.ViewLoaded += OnViewLoaded;
            _view.ConfirmRequested += OnConfirmRequested;
            _view.CancelRequested += OnCancelRequested;
        }

        private void OnViewLoaded(object? sender, EventArgs e)
        {
            if (_view.AppointmentId == Guid.Empty)
            {
                _view.ShowError("Thiếu appointmentId để kiểm tra xung đột.");
                return;
            }

            var result = _conflictService.ResolveConflict(_view.AppointmentId);
            _conflictedIds = result.ConflictedAppointmentIds;

            _view.ShowConflictMessage(result.Message);
            _view.BindConflicts(_conflictedIds);
        }

        private void OnConfirmRequested(object? sender, EventArgs e)
        {
            if (_view.AppointmentId == Guid.Empty)
            {
                _view.ShowError("Thiếu appointmentId.");
                return;
            }

            if (_view.ReplaceOldAppointments)
            {
                foreach (var conflictedId in _conflictedIds)
                {
                    if (conflictedId == _view.AppointmentId)
                        continue;

                    _appointmentService.DeleteAppointment(conflictedId);
                }

                _view.ShowMessage("Đã thay thế các cuộc hẹn xung đột.");
                _view.CloseView();
                return;
            }

            _appointmentService.DeleteAppointment(_view.AppointmentId);

            _view.ShowMessage("Đã hủy cuộc hẹn mới. Vui lòng chọn thời gian khác.");
            _view.CloseView();
        }

        private void OnCancelRequested(object? sender, EventArgs e)
        {
            if (_view.AppointmentId != Guid.Empty)
            {
                _appointmentService.DeleteAppointment(_view.AppointmentId);
            }

            _view.CloseView();
        }
    }
}