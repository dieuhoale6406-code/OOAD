using OOAD.Presenter;

namespace OOAD
{
    public partial class ConflictResolution : Form
    {
        private ConflictResolutionPresenter _presenter;

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public Guid AppointmentId { get; set; }

        public bool ReplaceOldAppointments => radioButton2.Checked;

        public event EventHandler? ViewLoaded;
        public event EventHandler? ConfirmRequested;
        public event EventHandler? CancelRequested;

        public ConflictResolution(ConflictResolutionPresenter presenter, Guid appointmentId)
        {
            InitializeComponent();

            _presenter = presenter;
            _presenter.AttachView(this);

            AppointmentId = appointmentId;

            Load += (_, _) => ViewLoaded?.Invoke(this, EventArgs.Empty);
            btnConfirm.Click += (_, _) => ConfirmRequested?.Invoke(this, EventArgs.Empty);
            btnCancel.Click += (_, _) => CancelRequested?.Invoke(this, EventArgs.Empty);

            _presenter.Initialize();
        }

        public ConflictResolution(ConflictResolutionPresenter presenter, List<Guid> conflictedAppointmentIds)
        {
            InitializeComponent();

            _presenter = presenter;
            _presenter.AttachView(this);

            AppointmentId = Guid.Empty;
            BindConflicts(conflictedAppointmentIds);
        }

        private void InitializePresenter()
        {
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