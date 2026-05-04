using OOAD.DTOs;
using OOAD.Model;
using OOAD.Presenter;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OOAD
{
    public partial class Appointment : Form
    {
        private readonly AppointmentPresenter _presenter;

        private readonly Guid _userId;
        private readonly Guid _initialCalendarId;
        private readonly Guid? _initialAppointmentId;

        #region Properties for Presenter Binding

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
                if (listView1.SelectedItems.Count == 0) return null;
                return listView1.SelectedItems[0].Tag is Guid id ? id : null;
            }
        }
        public List<ReminderDto> Reminders
        {
            get
            {
                var reminders = new List<ReminderDto>();
                foreach (ListViewItem item in ReminderList.Items)
                {
                    if (item.Tag is Guid id)
                    {
                        reminders.Add(new ReminderDto
                        {
                            Type = item.SubItems[1].Text,
                            Message = item.SubItems[2].Text,
                            ReminderTime = DateTime.Parse(item.SubItems[0].Text)
                        });
                    }
                }
                return reminders;
            }
        }
        public ListView ReminderList => listView1;
        public bool IsAppointment => rBtnAppointment.Checked;
        #endregion

        #region Events (View -> Presenter)
        public event EventHandler? ViewLoaded;
        public event EventHandler? SaveRequested;
        public event EventHandler? CancelRequested;
        public event EventHandler? AddReminderRequested;
        public event EventHandler? DeleteReminderRequested;

        public event Action<Guid>? RequestOpenConflictForm;
        public event Action<Guid, string, DateTime, DateTime>? RequestOpenGroupForm;
        #endregion

        public Appointment(Guid userId, Guid calendarId, Guid? appointmentId, DateTime selectedDate)
        {
            InitializeComponent();
            _presenter = new AppointmentPresenter(this, userId, calendarId, appointmentId);

            _userId = userId;
            _initialCalendarId = calendarId;
            _initialAppointmentId = appointmentId;

            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.CustomFormat = "dd/MM/yyyy HH:mm";

            listView1.Columns.Clear();
            listView1.Columns.Add("Time", 180);
            listView1.Columns.Add("Type", 180);
            listView1.Columns.Add("Message", 220);

            rBtnAppointment.Checked = true;

            StartTime = selectedDate.Date.AddHours(8);
            EndTime = selectedDate.Date.AddHours(9);

            Load += (_, _) => ViewLoaded?.Invoke(this, EventArgs.Empty);
            btnOK.Click += (_, _) => SaveRequested?.Invoke(this, EventArgs.Empty);
            btnCancel.Click += (_, _) => CancelRequested?.Invoke(this, EventArgs.Empty);
            btnAddReminder.Click += (_, _) => AddReminderRequested?.Invoke(this, EventArgs.Empty);
            btnDeleteReminder.Click += (_, _) => DeleteReminderRequested?.Invoke(this, EventArgs.Empty);

            _presenter.Initialize();
        }

        public void BindReminders(IEnumerable<ReminderDto> reminders)
        {
            listView1.Items.Clear();
            foreach (var reminder in reminders)
            {
                var item = new ListViewItem(reminder.ReminderTime.ToString("dd/MM/yyyy HH:mm"));
                item.SubItems.Add(reminder.Type);
                item.SubItems.Add(reminder.Message);
                listView1.Items.Add(item);
            }
        }

        public void ShowMessage(string message) =>
            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        public void ShowError(string message) =>
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public bool ConfirmOverwrite() =>
            MessageBox.Show("Ghi đè lịch cũ?", "Xung đột lịch", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

        public bool ConfirmJoinGroup() =>
            MessageBox.Show("Tham gia nhóm?", "Cuộc họp nhóm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

        public void CloseView() => this.Close();

        public void TriggerConflictResolution(Guid appointmentId)
        {
            var form = new ConflictResolution(appointmentId);
            form.ShowDialog(this);
        }
        public void TriggerGroupSuggestion(Guid appId, string name, DateTime start, DateTime end) =>
            RequestOpenGroupForm?.Invoke(appId, name, start, end);
    }
}