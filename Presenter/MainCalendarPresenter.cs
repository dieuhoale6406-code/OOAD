using OOAD.Services;
using OOAD.Data;
using OOAD.Repository;
using OOAD.Model;

namespace OOAD.Presenter
{
    public class MainCalendarPresenter
    {
        private MainCalendar _view;
        private CalendarService _calendarService;
        private readonly Guid _userId;
        private Guid _calendarId;

        public MainCalendarPresenter(MainCalendar view, Guid userId)
        {
            _view = view;
            var context = new AppDBContext();
            _calendarService = new CalendarService(context);
            _userId = userId;
        }

        public void Initialize()
        {
            _view.ViewLoaded += OnViewLoaded;
            _view.SelectedDateChanged += (_, _) => LoadAppointments();
            _view.ShowAllAppointmentsChanged += (_, _) => LoadAppointments();
            _view.AddRequested += OnAddRequested;
            _view.UpdateRequested += OnUpdateRequested;
            _view.DeleteRequested += OnDeleteRequested;
        }

        private void LoadAppointments()
        {
            if (_calendarId == Guid.Empty || _userId == Guid.Empty)
                return;

            var result = _view.ShowAllAppointments
                ? _calendarService.GetAppointmentsForUser(_userId, _calendarId)
                : _calendarService.GetAppointmentsForUserByDate(_userId, _calendarId, _view.SelectedDate);

            if (result.Status == HandleStatus.Error)
            {
                _view.ShowError(result.Message ?? "Không thể tải danh sách cuộc hẹn.");
                _view.BindAppointments(new List<Appointments>());
                return;
            }

            _view.BindAppointments(result.Data ?? new List<Appointments>());
        }

        private void OnViewLoaded(object? sender, EventArgs e)
        {
            var result = _calendarService.GetCalendarByUserId(_userId);
            if (result.Status == HandleStatus.Error)
            {
                _view.ShowError("User chưa có calendar.");
                return;
            }

            var calendar = result.Data;
            _calendarId = calendar?.CalendarId ?? Guid.Empty;
            LoadAppointments();
        }

        private void OnAddRequested(object? sender, EventArgs e)
        {
            if (_calendarId == Guid.Empty)
            {
                _view.ShowError("Không tìm thấy calendar.");
                return;
            }

            _view.OpenAppointmentForm(_calendarId, null, _view.SelectedDate);
            LoadAppointments();
        }

        private void OnUpdateRequested(object? sender, EventArgs e)
        {
            if (_calendarId == Guid.Empty)
            {
                _view.ShowError("Không tìm thấy calendar.");
                return;
            }

            var appointmentId = _view.SelectedAppointmentId;
            if (!appointmentId.HasValue)
            {
                _view.ShowError("Vui lòng chọn cuộc hẹn để cập nhật.");
                return;
            }

            _view.OpenAppointmentForm(_calendarId, appointmentId, _view.SelectedDate);
            LoadAppointments();
        }

        private void OnDeleteRequested(object? sender, EventArgs e)
        {
            var appointmentId = _view.SelectedAppointmentId;

            if (!appointmentId.HasValue)
            {
                _view.ShowError("Vui lòng chọn cuộc hẹn để xóa.");
                return;
            }

            if (!_view.ConfirmDelete())
                return;

            var result = _calendarService.DeleteAppointment(_userId, appointmentId.Value);
            if (result.Status == HandleStatus.Error)
            {
                _view.ShowError(result.Message ?? "Không thể xóa cuộc hẹn.");
                return;
            }

            _view.ShowMessage(string.IsNullOrWhiteSpace(result.Message) ? "Đã xóa cuộc hẹn." : result.Message);
            LoadAppointments();
        }
    }
}
