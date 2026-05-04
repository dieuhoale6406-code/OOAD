using OOAD.Model;
using OOAD.Presenter;
using OOAD.Data;
using OOAD.Repository;
using OOAD.Service;
using MaterialSkin;
using MaterialSkin.Controls;

namespace OOAD
{
    public partial class MainCalendar : MaterialForm
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
                    return null;

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
            btnDelete.Click += (_, _) => DeleteRequested?.Invoke(this, EventArgs.Empty);

            InitializePresenter();

            ApplyMainCalendarTheme();
        }

        private void ApplyMainCalendarTheme()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue600,
                Primary.Blue700,
                Primary.Blue200,
                Accent.LightBlue200,
                TextShade.WHITE
            );

            // Form properties
            BackColor = Color.White;
            MaximizeBox = true;
            DoubleBuffered = true;

            // White body override
            Paint += (sender, e) =>
            {
                using var whiteBrush = new SolidBrush(Color.White);
                e.Graphics.FillRectangle(
                    whiteBrush,
                    new Rectangle(0, 64, ClientSize.Width, ClientSize.Height - 64)
                );
            };

            Resize += (sender, e) => Invalidate();

            // Label Style
            var labelColor = Color.FromArgb(51, 65, 85);
            var labelFont = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);

            label1.Font = labelFont; label1.ForeColor = labelColor; label1.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(37, 99, 235);
            label2.BackColor = Color.Transparent;

            // CheckBox Style
            checkBox1.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBox1.ForeColor = labelColor;
            checkBox1.BackColor = Color.Transparent;

            // Button Style - Primary (Add, Update)
            StylePrimaryButton(btnAdd);
            StylePrimaryButton(btnUpdate);

            // Button Style - Secondary (Delete)
            StyleSecondaryButton(btnDelete);

            // DataGridView Style (Visuals only)
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(37, 99, 235);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 64, 175);
            dataGridView1.GridColor = Color.FromArgb(226, 232, 240);
            dataGridView1.RowHeadersVisible = false;

            Invalidate();
        }

        private void StylePrimaryButton(Button btn)
        {
            btn.BackColor = Color.FromArgb(37, 99, 235);
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(29, 78, 216);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 64, 175);
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
        }

        private void StyleSecondaryButton(Button btn)
        {
            btn.BackColor = Color.FromArgb(96, 165, 250);
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(59, 130, 246);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(37, 99, 235);
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
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

            if (dataGridView1.Columns["AppointmentId"] != null)
            {
                dataGridView1.Columns["AppointmentId"].Visible = false;
            }

            if (dataGridView1.Columns["Name"] != null)
            {
                dataGridView1.Columns["Name"].HeaderText = "Tên cuộc hẹn";
                dataGridView1.Columns["Name"].Width = 160;
            }

            if (dataGridView1.Columns["Location"] != null)
            {
                dataGridView1.Columns["Location"].HeaderText = "Địa điểm";
                dataGridView1.Columns["Location"].Width = 130;
            }

            if (dataGridView1.Columns["StartTime"] != null)
            {
                dataGridView1.Columns["StartTime"].HeaderText = "Bắt đầu";
                dataGridView1.Columns["StartTime"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dataGridView1.Columns["StartTime"].Width = 140;
            }

            if (dataGridView1.Columns["EndTime"] != null)
            {
                dataGridView1.Columns["EndTime"].HeaderText = "Kết thúc";
                dataGridView1.Columns["EndTime"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dataGridView1.Columns["EndTime"].Width = 140;
            }
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
            using var form = new Appointment(_userId, calendarId, appointmentId, selectedDate);
            form.ShowDialog(this);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}