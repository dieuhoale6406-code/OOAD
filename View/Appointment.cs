using OOAD.DTOs;
using OOAD.Model;
using OOAD.Presenter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;

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

        private const int NormalClientWidth = 944;
        private const int GroupClientWidth = 1500;

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
                            ReminderId = id,
                            Type = item.SubItems[1].Text,
                            ReminderTime = DateTime.ParseExact(item.SubItems[0].Text, "dd/MM/yyyy HH:mm", null)
                        });
                    }
                }
                return reminders;
            }
        }
        public ListView ReminderList => listView1;

        public IReadOnlyList<string> ParticipantEmails => _pendingParticipantEmails
            .Where(email => !string.IsNullOrWhiteSpace(email))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
            ApplyAppointmentLegacyTheme();
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


        private void ApplyAppointmentLegacyTheme()
        {
            // Theme runtime để không phụ thuộc hoàn toàn vào Designer.
            // Giữ font title bản cũ, nhưng để chữ thường dễ đọc hơn.
            var primary = Color.RoyalBlue;
            var danger = Color.FromArgb(239, 68, 68);
            var text = Color.FromArgb(17, 24, 39);
            var border = Color.FromArgb(37, 99, 235);

            BackColor = Color.White;

            label1.Font = new Font("Elephant", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = primary;

            foreach (var label in new[] { label2, lable3, label4, label5, label3 })
            {
                label.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
                label.ForeColor = text;
            }

            groupBox1.ForeColor = text;
            rBtnAppointment.ForeColor = text;
            rBtnGroupMeeting.ForeColor = text;

            txtName.ForeColor = text;
            txtLocation.ForeColor = text;
            comboBox1.ForeColor = text;
            listView1.ForeColor = text;

            if (_participantsLabel != null)
            {
                _participantsLabel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
                _participantsLabel.ForeColor = text;
            }

            if (_participantEmailTextBox != null)
            {
                _participantEmailTextBox.ForeColor = text;
                _participantEmailTextBox.BorderStyle = BorderStyle.FixedSingle;
            }

            if (_participantsListView != null)
            {
                _participantsListView.ForeColor = text;
                _participantsListView.BorderStyle = BorderStyle.FixedSingle;
            }

            StylePrimaryButton(btnOK, primary);
            StylePrimaryButton(btnAddReminder, primary);
            StyleSecondaryButton(btnCancel, border);
            StyleDangerButton(btnDeleteReminder, danger);

            if (_addParticipantButton != null)
                StylePrimaryButton(_addParticipantButton, primary);

            if (_removeParticipantButton != null)
                StyleDangerOutlineButton(_removeParticipantButton, danger);
        }

        private static void StylePrimaryButton(Button button, Color color)
        {
            button.BackColor = color;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;
        }

        private static void StyleSecondaryButton(Button button, Color color)
        {
            button.BackColor = Color.White;
            button.ForeColor = color;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = color;
            button.FlatAppearance.BorderSize = 1;
            button.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;
        }

        private static void StyleDangerButton(Button button, Color color)
        {
            button.BackColor = color;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;
        }

        private static void StyleDangerOutlineButton(Button button, Color color)
        {
            button.BackColor = Color.White;
            button.ForeColor = color;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = color;
            button.FlatAppearance.BorderSize = 1;
            button.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;
        }

        private void ConfigureParticipantsUi()
        {
            // Tạo vùng hiển thị người tham gia bằng code để tránh phải phụ thuộc Designer.
            ClientSize = new Size(NormalClientWidth, ClientSize.Height);

            _participantsLabel = new Label
            {
                AutoSize = true,
                Location = new Point(950, 165),
                Name = "lblParticipants",
                Text = "Người tham gia:",
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0)
            };

            _participantEmailTextBox = new TextBox
            {
                Location = new Point(950, 225),
                Name = "txtParticipantEmail",
                PlaceholderText = "Nhập Gmail...",
                Size = new Size(280, 27),
                Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0)
            };

            _addParticipantButton = new Button
            {
                Location = new Point(1240, 224),
                Name = "btnAddParticipant",
                Size = new Size(100, 40),
                Text = "Thêm",
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0)
            };
            _addParticipantButton.Click += (_, _) => AddPendingParticipantEmailsFromInput();

            _removeParticipantButton = new Button
            {
                Location = new Point(1350, 224),
                Name = "btnRemoveParticipant",
                Size = new Size(70, 40),
                Text = "Xóa",
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0)
            };
            _removeParticipantButton.Click += (_, _) => RemoveSelectedPendingParticipantEmails();

            _participantsListView = new ListView
            {
                FullRowSelect = true,
                GridLines = true,
                HeaderStyle = ColumnHeaderStyle.Nonclickable,
                Location = new Point(950, 280),
                Name = "lvParticipants",
                Size = new Size(480, 340),
                UseCompatibleStateImageBehavior = false,
                View = View.Details,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0)
            };

            _participantsListView.Columns.Add("Họ tên / Email", 420);

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