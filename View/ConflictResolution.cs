using OOAD.Presenter;
using OOAD.Data;
using OOAD.Repository;
using OOAD.Service;

namespace OOAD
{
    public partial class ConflictResolution : Form
    {
        private ConflictResolutionPresenter? _presenter;

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public Guid AppointmentId { get; set; }

        public bool ReplaceOldAppointments => radioButton2.Checked;

        public event EventHandler? ViewLoaded;
        public event EventHandler? ConfirmRequested;
        public event EventHandler? CancelRequested;

        public ConflictResolution(Guid appointmentId)
        {
            InitializeComponent();

            AppointmentId = appointmentId;

            Load += (_, _) => ViewLoaded?.Invoke(this, EventArgs.Empty);
            btnConfirm.Click += (_, _) => ConfirmRequested?.Invoke(this, EventArgs.Empty);
            btnCancel.Click += (_, _) => CancelRequested?.Invoke(this, EventArgs.Empty);

            InitializePresenter();
        }

        private void InitializePresenter()
        {
            var dbContext = new AppDBContext();
            var appointmentRepository = new AppointmentRepository(dbContext);

            var conflictService = new ConflictService(appointmentRepository);
            var appointmentService = new AppointmentService(appointmentRepository);

            _presenter = new ConflictResolutionPresenter(this, conflictService, appointmentService);
            _presenter.Initialize();
        }

        public void ShowConflictMessage(string message)
        {
            labelConflict.Text = message;
        }

        public void BindConflicts(IEnumerable<Guid> conflictedAppointmentIds)
        {
            dataGridView1.DataSource = conflictedAppointmentIds
                .Select(id => new { AppointmentId = id })
                .ToList();
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void CloseView()
        {
            Close();
        }
    }
}