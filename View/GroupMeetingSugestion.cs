using OOAD.Model;
using OOAD.Presenter;

namespace OOAD
{
    public partial class GroupMeetingSugestion : Form
    {
        private GroupMeetingSuggestionPresenter _presenter;

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public Guid UserId { get; set; }

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public Guid AppointmentId { get; set; }

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string AppointmentName { get; set; } = string.Empty;

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public DateTime SelectedStartTime { get; set; } = DateTime.Now;

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public DateTime SelectedEndTime { get; set; } = DateTime.Now.AddHours(1);

        public event EventHandler? ViewLoaded;
        public event EventHandler? JoinRequested;
        public event EventHandler? DeclineRequested;

        public GroupMeetingSugestion(
            Guid userId,
            Guid appointmentId,
            string appointmentName,
            DateTime selectedStartTime,
            DateTime selectedEndTime)
        {
            InitializeComponent();

            _presenter = new GroupMeetingSuggestionPresenter(this, UserId, AppointmentId);

            UserId = userId;
            AppointmentId = appointmentId;
            AppointmentName = appointmentName;
            SelectedStartTime = selectedStartTime;
            SelectedEndTime = selectedEndTime;

            Load += (_, _) => ViewLoaded?.Invoke(this, EventArgs.Empty);
            btnJoin.Click += (_, _) => JoinRequested?.Invoke(this, EventArgs.Empty);
            btnNoThanks.Click += (_, _) => DeclineRequested?.Invoke(this, EventArgs.Empty);

            _presenter.Initialize();
        }

        public void ShowSuggestion(GroupMeetings? meeting)
        {
            label2.Text = meeting == null
                ? "Không có cuộc họp nhóm tương tự."
                : $"Có cuộc họp nhóm phù hợp: {meeting.Name} ({meeting.StartTime:dd/MM HH:mm} - {meeting.EndTime:HH:mm}). Bạn có muốn tham gia không?";
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