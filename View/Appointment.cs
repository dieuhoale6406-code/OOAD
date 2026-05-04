using OOAD.Model;
using OOAD.Presenter;
using OOAD.Data;
using OOAD.Repository;
using OOAD.Service;
using MaterialSkin;
using MaterialSkin.Controls;

namespace OOAD
{
    public partial class Appointment : MaterialForm
    {
        private AppointmentPresenter? _presenter;

        private readonly Guid _userId;
        private readonly Guid _initialCalendarId;
        private readonly Guid? _initialAppointmentId;

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public Guid CalendarId { get; set; }

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public Guid? AppointmentId { get; set; }

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string AppointmentName
        {
            get => txtName.Text;
            set => txtName.Text = value;
        }

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new string Location
        {
            get => txtLocation.Text;
            set => txtLocation.Text = value;
        }

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public DateTime StartTime
        {
            get => dtpStart.Value;
            set => dtpStart.Value = value;
        }

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public DateTime EndTime
        {
            get => dtpEnd.Value;
            set => dtpEnd.Value = value;
        }

        public string ReminderType => comboBox1.Text;

        public Guid? SelectedReminderId
        {
            get
            {
                if (listView1.SelectedItems.Count == 0)
                    return null;

                return listView1.SelectedItems[0].Tag is Guid id ? id : null;
            }
        }

        public event EventHandler? ViewLoaded;
        public event EventHandler? SaveRequested;
        public event EventHandler? CancelRequested;
        public event EventHandler? AddReminderRequested;
        public event EventHandler? DeleteReminderRequested;

        public Appointment(Guid userId, Guid calendarId, Guid? appointmentId, DateTime selectedDate)
        {
            InitializeComponent();

            _userId = userId;
            _initialCalendarId = calendarId;
            _initialAppointmentId = appointmentId;

            // Format DateTimePicker: MM là tháng, mm là phút
            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.CustomFormat = "dd/MM/yyyy HH:mm";

            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.CustomFormat = "dd/MM/yyyy HH:mm";

            Load += (_, _) => ViewLoaded?.Invoke(this, EventArgs.Empty);
            btnOK.Click += (_, _) => SaveRequested?.Invoke(this, EventArgs.Empty);
            btnCancel.Click += (_, _) => CancelRequested?.Invoke(this, EventArgs.Empty);
            btnAddReminder.Click += (_, _) => AddReminderRequested?.Invoke(this, EventArgs.Empty);
            btnDeleteReminder.Click += (_, _) => DeleteReminderRequested?.Invoke(this, EventArgs.Empty);

            listView1.Columns.Clear();
            listView1.Columns.Add("ReminderId", 0);
            listView1.Columns.Add("Reminder Time", 180);
            listView1.Columns.Add("Type", 180);
            listView1.Columns.Add("Message", 220);

            StartTime = selectedDate.Date.AddHours(8);
            EndTime = selectedDate.Date.AddHours(9);

            InitializePresenter();

            ApplyAppointmentTheme();
        }

        private void ApplyAppointmentTheme()
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
            MaximizeBox = false;
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

            // Title Style
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(37, 99, 235);
            label1.BackColor = Color.Transparent;

            // Label Style
            var labelColor = Color.FromArgb(51, 65, 85);
            var labelFont = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);

            label2.Font = labelFont; label2.ForeColor = labelColor; label2.BackColor = Color.Transparent;
            lable3.Font = labelFont; lable3.ForeColor = labelColor; lable3.BackColor = Color.Transparent;
            label4.Font = labelFont; label4.ForeColor = labelColor; label4.BackColor = Color.Transparent;
            label5.Font = labelFont; label5.ForeColor = labelColor; label5.BackColor = Color.Transparent;
            label3.Font = labelFont; label3.ForeColor = labelColor; label3.BackColor = Color.Transparent;

            // Input Style
            var inputFont = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            var inputForeColor = Color.FromArgb(15, 23, 42);

            txtName.Font = inputFont; txtName.ForeColor = inputForeColor; txtName.BorderStyle = BorderStyle.FixedSingle;
            txtLocation.Font = inputFont; txtLocation.ForeColor = inputForeColor; txtLocation.BorderStyle = BorderStyle.FixedSingle;
            comboBox1.Font = inputFont; comboBox1.ForeColor = inputForeColor;
            dtpStart.Font = inputFont;
            dtpEnd.Font = inputFont;

            // Button Style - Primary (OK, Add)
            StylePrimaryButton(btnOK);
            StylePrimaryButton(btnAddReminder);

            // Button Style - Secondary (Delete)
            StyleSecondaryButton(btnDeleteReminder);

            // Button Style - Outline (Cancel)
            StyleOutlineButton(btnCancel);

            // ListView Style
            listView1.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            listView1.ForeColor = Color.FromArgb(15, 23, 42);
            listView1.BorderStyle = BorderStyle.None;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;

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

        private void StyleOutlineButton(Button btn)
        {
            btn.BackColor = Color.White;
            btn.ForeColor = Color.FromArgb(37, 99, 235);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 2;
            btn.FlatAppearance.BorderColor = Color.FromArgb(37, 99, 235);
            btn.FlatAppearance.MouseOverBackColor = Color.White;
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(239, 246, 255);
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
        }

        private void InitializePresenter()
        {
            var dbContext = new AppDBContext();

            var appointmentRepository = new AppointmentRepository(dbContext);
            var reminderRepository = new ReminderRepository(dbContext);
            var groupMeetingRepository = new GroupMeetingRepository(dbContext);

            var appointmentService = new AppointmentService(appointmentRepository);
            var reminderService = new ReminderService(reminderRepository);
            var conflictService = new ConflictService(appointmentRepository);
            var groupMeetingService = new GroupMeetingService(groupMeetingRepository);

            _presenter = new AppointmentPresenter(
                this,
                appointmentService,
                reminderService,
                conflictService,
                groupMeetingService,
                _userId,
                _initialCalendarId,
                _initialAppointmentId);

            _presenter.Initialize();
        }

        public void BindReminders(IEnumerable<Reminders> reminders)
        {
            listView1.Items.Clear();

            foreach (var reminder in reminders)
            {
                var item = new ListViewItem(reminder.ReminderId.ToString())
                {
                    Tag = reminder.ReminderId
                };

                item.SubItems.Add(reminder.ReminderTime.ToString("dd/MM/yyyy HH:mm"));
                item.SubItems.Add(reminder.Type);
                item.SubItems.Add(reminder.Message);

                listView1.Items.Add(item);
            }
        }

        public void OpenConflictResolution(Guid appointmentId)
        {
            using var form = new ConflictResolution(appointmentId);
            form.ShowDialog(this);
        }

        public void OpenGroupMeetingSuggestion(
            Guid userId,
            Guid appointmentId,
            string appointmentName,
            DateTime startTime,
            DateTime endTime)
        {
            using var form = new GroupMeetingSugestion(
                userId,
                appointmentId,
                appointmentName,
                startTime,
                endTime);

            form.ShowDialog(this);
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