using OOAD.Model;
using OOAD.Presenter;
using OOAD.Data;
using OOAD.Repository;
using OOAD.Service;

namespace OOAD
{
    public partial class MainCalendar : Form
    {
        private MainCalendarPresenter? _presenter;
        private readonly Guid _userId;

        public DateTime SelectedDate => monthCalendar1.SelectionStart.Date;
        public bool ShowAllAppointments => checkBox1.Checked;

        public Guid? SelectedAppointmentId
        {
            get
            {
                if (dataGridView1.CurrentRow?.DataBoundItem == null)
                {
                    return null;
                }

                var value = dataGridView1.CurrentRow.Cells["AppointmentId"].Value?.ToString();
                return Guid.TryParse(value, out var id) ? id : null;
            }
        }

        public event EventHandler? ViewLoaded;
        public event EventHandler? SelectedDateChanged;
        public event EventHandler? ShowAllAppointmentsChanged;
        public event EventHandler? AddRequested;
        public event EventHandler? UpdateRequested;
        public event EventHandler? DeleteRequested;

        public MainCalendar(Guid userId)
        {
            InitializeComponent();
            _userId = userId;
            Load += (_, _) => ViewLoaded?.Invoke(this, EventArgs.Empty);
            monthCalendar1.DateSelected += (_, _) =>
            {
                label1.Text = $"Choosing Date: {SelectedDate:dd/MM/yyyy}";
                SelectedDateChanged?.Invoke(this, EventArgs.Empty);
            };
            checkBox1.CheckedChanged += (_, _) => ShowAllAppointmentsChanged?.Invoke(this, EventArgs.Empty);
            btnAdd.Click += (_, _) => AddRequested?.Invoke(this, EventArgs.Empty);
            btnUpdate.Click += (_, _) => UpdateRequested?.Invoke(this, EventArgs.Empty);
            InitializePresenter();
        }

        private void InitializePresenter()
        {
            var dbContext = new AppDBContext();
            var calendarRepository = new CalendarRepository(dbContext);
            var appointmentRepository = new AppointmentRepository(dbContext);

            var calendarService = new CalendarService(calendarRepository);
            var appointmentService = new AppointmentService(appointmentRepository);

            _presenter = new MainCalendarPresenter(this, calendarService, appointmentService, _userId);
            _presenter.Initialize();
        }

        public void BindAppointments(IEnumerable<Appointments> appointments)
        {
            dataGridView1.DataSource = appointments
                .Select(a => new
                {
                    a.AppointmentId,
                    a.Name,
                    a.Location,
                    a.StartTime,
                    a.EndTime
                })
                .OrderBy(a => a.StartTime)
                .ToList();
        }

        public bool ConfirmDelete()
        {
            var result = MessageBox.Show(
                "Bạn có chắc muốn xóa cuộc hẹn đã chọn?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            return result == DialogResult.Yes;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void OpenAppointmentForm(Guid calendarId, Guid? appointmentId, DateTime selectedDate)
        {
            using var form = new Appointment(calendarId, appointmentId, selectedDate);
            form.ShowDialog(this);
        }

        public void OpenConflictResolution(Guid appointmentId)
        {
            using var form = new ConflictResolution(appointmentId);
            form.ShowDialog(this);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
