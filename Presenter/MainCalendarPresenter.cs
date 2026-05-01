using OOAD.Service;

namespace OOAD.Presenter
{
    public class MainCalendarPresenter
    {
        private readonly MainCalendar _view;
        private readonly CalendarService _calendarService;
        private readonly AppointmentService _appointmentService;
        private readonly Guid _userId;
        private Guid _calendarId;

        public MainCalendarPresenter(
            MainCalendar view,
            CalendarService calendarService,
            AppointmentService appointmentService,
            Guid userId)
        {
            _view = view;
            _calendarService = calendarService;
            _appointmentService = appointmentService;
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

        private void OnViewLoaded(object? sender, EventArgs e)
        {
            var calendar = _calendarService.GetCalendarByUserId(_userId);

            if (calendar == null)
            {
                _view.ShowError("User chưa có calendar.");
                return;
            }

            _calendarId = calendar.CalendarId;
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            if (_calendarId == Guid.Empty)
                return;

            var appointments = _appointmentService.GetAppointmentsByCalendarId(_calendarId);

            if (!_view.ShowAllAppointments)
            {
                appointments = appointments
                    .Where(a => a.StartTime.Date == _view.SelectedDate.Date)
                    .ToList();
            }

            _view.BindAppointments(appointments);
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

            _appointmentService.DeleteAppointment(appointmentId.Value);
            _view.ShowMessage("Đã xóa cuộc hẹn.");
            LoadAppointments();
        }
    }
}