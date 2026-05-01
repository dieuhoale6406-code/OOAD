using OOAD.Model;
using OOAD.Presenter;
using OOAD.Data;
using OOAD.Repository;
using OOAD.Service;

namespace OOAD
{
    public partial class Appointment : Form
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