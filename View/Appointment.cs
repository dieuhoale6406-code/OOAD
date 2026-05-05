using OOAD.DTOs;
using OOAD.Model;
using OOAD.Presenter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OOAD
{
    public partial class Appointment : Form
    {
        private readonly AppointmentPresenter _presenter;
        private Label? _participantsLabel;
        private TextBox? _participantEmailTextBox;
        private Button? _addParticipantButton;
        private Button? _removeParticipantButton;
        private ListView? _participantsListView;
        private readonly List<string> _savedParticipantDisplays = new();
        private readonly List<string> _pendingParticipantEmails = new();

        private const int NormalClientWidth = 680;
        private const int GroupClientWidth = 1120;

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

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string ReminderType => comboBox1.Text;

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public Guid? SelectedReminderId
        {
            get
            {
                if (listView1.SelectedItems.Count == 0) return null;
                return listView1.SelectedItems[0].Tag is Guid id ? id : null;
            }
        }
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
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
                            ReminderId = id,
                            Type = item.SubItems[1].Text,
                            ReminderTime = DateTime.ParseExact(item.SubItems[0].Text, "dd/MM/yyyy HH:mm", null)
                        });
                    }
                }
                return reminders;
            }
        }
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public ListView ReminderList => listView1;

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public IReadOnlyList<string> ParticipantEmails => _pendingParticipantEmails
            .Where(email => !string.IsNullOrWhiteSpace(email))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool IsGroupMode
        {
            get => rBtnGroupMeeting.Checked;
            set
            {
                rBtnGroupMeeting.Checked = value;
                rBtnAppointment.Checked = !value;
                SetParticipantsVisible(value);
            }
        }
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool AddMode
        {
            set
            {
                rBtnAppointment.Enabled = value;
                rBtnGroupMeeting.Enabled = value;
            }
        }
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
            listView1.Columns.Add("Type", 360);

            ConfigureParticipantsUi();
            IsGroupMode = false;
            rBtnGroupMeeting.CheckedChanged += (_, _) => SetParticipantsVisible(IsGroupMode);

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
                item.Tag = reminder.ReminderId;
                listView1.Items.Add(item);
            }
        }


        public void BindParticipants(IEnumerable<string> participants)
        {
            _savedParticipantDisplays.Clear();
            _savedParticipantDisplays.AddRange(
                participants
                    .Where(p => !string.IsNullOrWhiteSpace(p))
                    .Select(p => p.Trim())
                    .Distinct(StringComparer.OrdinalIgnoreCase)
            );

            // Sau khi load lại từ DB, các email chờ lưu đã trở thành dữ liệu thật.
            _pendingParticipantEmails.Clear();
            RefreshParticipantsList();
        }

        private void RefreshParticipantsList()
        {
            if (_participantsListView == null)
                return;

            _participantsListView.Items.Clear();

            foreach (var participant in _savedParticipantDisplays)
            {
                _participantsListView.Items.Add(new ListViewItem(participant));
            }

            foreach (var email in _pendingParticipantEmails
                         .Distinct(StringComparer.OrdinalIgnoreCase))
            {
                var item = new ListViewItem($"{email} (chưa lưu)")
                {
                    Tag = email
                };
                _participantsListView.Items.Add(item);
            }

            if (_participantsListView.Items.Count == 0)
                _participantsListView.Items.Add(new ListViewItem("Chưa có người tham gia"));
        }

        private void AddPendingParticipantEmailsFromInput()
        {
            if (!IsGroupMode)
            {
                ShowError("Chỉ cuộc họp nhóm mới thêm người tham gia.");
                return;
            }

            if (_participantEmailTextBox == null)
                return;

            var emails = ParseParticipantEmails(_participantEmailTextBox.Text).ToList();
            if (!emails.Any())
            {
                ShowError("Vui lòng nhập Gmail người tham gia.");
                return;
            }

            var addedAny = false;
            foreach (var email in emails)
            {
                if (_pendingParticipantEmails.Contains(email, StringComparer.OrdinalIgnoreCase))
                    continue;

                // Nếu người này đã load từ DB rồi thì không thêm lại vào danh sách chờ.
                if (_savedParticipantDisplays.Any(p => p.Contains(email, StringComparison.OrdinalIgnoreCase)))
                    continue;

                _pendingParticipantEmails.Add(email);
                addedAny = true;
            }

            if (!addedAny)
            {
                ShowError("Gmail này đã có trong danh sách người tham gia.");
                return;
            }

            _participantEmailTextBox.Clear();
            RefreshParticipantsList();
        }

        private void RemoveSelectedPendingParticipantEmails()
        {
            if (_participantsListView == null || _participantsListView.SelectedItems.Count == 0)
            {
                ShowError("Vui lòng chọn Gmail cần xóa khỏi danh sách.");
                return;
            }

            var removedAny = false;
            foreach (ListViewItem item in _participantsListView.SelectedItems)
            {
                if (item.Tag is not string email)
                    continue;

                _pendingParticipantEmails.RemoveAll(e =>
                    string.Equals(e, email, StringComparison.OrdinalIgnoreCase));
                removedAny = true;
            }

            if (!removedAny)
            {
                ShowError("Chỉ xóa được Gmail vừa thêm trước khi lưu. Người đã lưu trong DB cần xử lý chức năng rời/xóa thành viên riêng.");
                return;
            }

            RefreshParticipantsList();
        }

        private static IEnumerable<string> ParseParticipantEmails(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return Enumerable.Empty<string>();

            var separators = new[] { ',', ';', '\r', '\n', '\t', ' ' };
            return raw
                .Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .Select(email => email.Trim().ToLowerInvariant())
                .Where(email => email.Contains('@') && email.Contains('.'))
                .Distinct(StringComparer.OrdinalIgnoreCase);
        }

        private void ConfigureParticipantsUi()
        {
            // Tạo vùng hiển thị người tham gia bằng code để tránh phải phụ thuộc Designer.
            ClientSize = new Size(NormalClientWidth, ClientSize.Height);

            _participantsLabel = new Label
            {
                AutoSize = true,
                Location = new Point(660, 92),
                Name = "lblParticipants",
                Text = "Người tham gia:"
            };

            _participantEmailTextBox = new TextBox
            {
                Location = new Point(660, 120),
                Name = "txtParticipantEmail",
                PlaceholderText = "Nhập Gmail...",
                Size = new Size(260, 27)
            };

            _addParticipantButton = new Button
            {
                Location = new Point(930, 119),
                Name = "btnAddParticipant",
                Size = new Size(70, 29),
                Text = "Add"
            };
            _addParticipantButton.Click += (_, _) => AddPendingParticipantEmailsFromInput();

            _removeParticipantButton = new Button
            {
                Location = new Point(1010, 119),
                Name = "btnRemoveParticipant",
                Size = new Size(90, 29),
                Text = "Remove"
            };
            _removeParticipantButton.Click += (_, _) => RemoveSelectedPendingParticipantEmails();

            _participantsListView = new ListView
            {
                FullRowSelect = true,
                GridLines = true,
                HeaderStyle = ColumnHeaderStyle.Nonclickable,
                Location = new Point(660, 160),
                Name = "lvParticipants",
                Size = new Size(440, 338),
                UseCompatibleStateImageBehavior = false,
                View = View.Details
            };

            _participantsListView.Columns.Add("Họ tên / Email", 415);

            Controls.Add(_participantsLabel);
            Controls.Add(_participantEmailTextBox);
            Controls.Add(_addParticipantButton);
            Controls.Add(_removeParticipantButton);
            Controls.Add(_participantsListView);

            SetParticipantsVisible(false);
        }

        private void SetParticipantsVisible(bool visible)
        {
            if (_participantsLabel != null)
                _participantsLabel.Visible = visible;

            if (_participantEmailTextBox != null)
                _participantEmailTextBox.Visible = visible;

            if (_addParticipantButton != null)
                _addParticipantButton.Visible = visible;

            if (_removeParticipantButton != null)
                _removeParticipantButton.Visible = visible;

            if (_participantsListView != null)
                _participantsListView.Visible = visible;

            var targetWidth = visible ? GroupClientWidth : NormalClientWidth;
            if (ClientSize.Width != targetWidth)
                ClientSize = new Size(targetWidth, ClientSize.Height);
        }

        public void ShowMessage(string message) =>
            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        public void ShowError(string message) =>
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public bool ConfirmOverwrite() =>
            MessageBox.Show("Ghi đè lịch cũ?", "Xung đột lịch", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

        public bool ConfirmJoinGroup() =>
            MessageBox.Show("Tham gia nhóm?", "Cuộc họp nhóm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

        public bool ConfirmCreatePastAppointment() =>
            MessageBox.Show(
                "Thời gian bắt đầu của cuộc hẹn đã qua. Bạn vẫn muốn lưu cuộc hẹn này không?",
                "Xác nhận lịch quá khứ",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            ) == DialogResult.Yes;


        public DateTime? AskCustomReminderTime(DateTime defaultValue)
        {
            using var form = new Form
            {
                Text = "Chọn thời gian nhắc nhở",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                ClientSize = new Size(520, 180),
                MaximizeBox = false,
                MinimizeBox = false,
                AutoScaleMode = AutoScaleMode.Dpi
            };

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                ColumnCount = 2,
                RowCount = 3
            };

            root.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160));
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));

            var label = new Label
            {
                Text = "Thời gian nhắc nhở:",
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                TextAlign = ContentAlignment.MiddleLeft
            };

            var picker = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy HH:mm",
                ShowUpDown = true,
                Width = 260,
                Anchor = AnchorStyles.Left,
                Value = defaultValue < DateTime.Now
                    ? DateTime.Now.AddMinutes(5)
                    : defaultValue
            };

            var buttonPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.RightToLeft,
                Dock = DockStyle.Fill,
                Padding = new Padding(0),
                Margin = new Padding(0)
            };

            var btnCancel = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Width = 90,
                Height = 32
            };

            var btnOk = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Width = 90,
                Height = 32
            };

            buttonPanel.Controls.Add(btnCancel);
            buttonPanel.Controls.Add(btnOk);

            root.Controls.Add(label, 0, 0);
            root.Controls.Add(picker, 1, 0);
            root.Controls.Add(buttonPanel, 0, 2);
            root.SetColumnSpan(buttonPanel, 2);

            form.Controls.Add(root);
            form.AcceptButton = btnOk;
            form.CancelButton = btnCancel;

            return form.ShowDialog(this) == DialogResult.OK
                ? picker.Value
                : null;
        }

        public void CloseView() => this.Close();

        public void TriggerGroupSuggestion(Guid appId, string name, DateTime start, DateTime end) =>
            RequestOpenGroupForm?.Invoke(appId, name, start, end);
    }
}