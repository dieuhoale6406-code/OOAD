using OOAD.Model;
using OOAD.Presenter;
using OOAD.Data;
using OOAD.Repository;
using OOAD.Service;
using MaterialSkin;
using MaterialSkin.Controls;

namespace OOAD
{
    public partial class GroupMeetingSugestion : MaterialForm
    {
        private GroupMeetingSuggestionPresenter? _presenter;

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

            UserId = userId;
            AppointmentId = appointmentId;
            AppointmentName = appointmentName;
            SelectedStartTime = selectedStartTime;
            SelectedEndTime = selectedEndTime;

            Load += (_, _) => ViewLoaded?.Invoke(this, EventArgs.Empty);
            btnJoin.Click += (_, _) => JoinRequested?.Invoke(this, EventArgs.Empty);
            btnNoThanks.Click += (_, _) => DeclineRequested?.Invoke(this, EventArgs.Empty);

            InitializePresenter();

            ApplyGroupMeetingSuggestionTheme();
        }

        private void ApplyGroupMeetingSuggestionTheme()
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

            // Message Style
            label2.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(51, 65, 85);
            label2.BackColor = Color.Transparent;

            // Button Style - Join (Primary)
            btnJoin.BackColor = Color.FromArgb(37, 99, 235);
            btnJoin.ForeColor = Color.White;
            btnJoin.FlatStyle = FlatStyle.Flat;
            btnJoin.FlatAppearance.BorderSize = 0;
            btnJoin.FlatAppearance.MouseOverBackColor = Color.FromArgb(29, 78, 216);
            btnJoin.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 64, 175);
            btnJoin.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnJoin.Cursor = Cursors.Hand;
            btnJoin.UseVisualStyleBackColor = false;

            // Button Style - No Thanks (Outline)
            btnNoThanks.BackColor = Color.White;
            btnNoThanks.ForeColor = Color.FromArgb(37, 99, 235);
            btnNoThanks.FlatStyle = FlatStyle.Flat;
            btnNoThanks.FlatAppearance.BorderSize = 2;
            btnNoThanks.FlatAppearance.BorderColor = Color.FromArgb(37, 99, 235);
            btnNoThanks.FlatAppearance.MouseOverBackColor = Color.White;
            btnNoThanks.FlatAppearance.MouseDownBackColor = Color.FromArgb(239, 246, 255);
            btnNoThanks.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNoThanks.Cursor = Cursors.Hand;
            btnNoThanks.UseVisualStyleBackColor = false;

            Invalidate();
        }

        private void InitializePresenter()
        {
            var dbContext = new AppDBContext();

            var groupMeetingRepository = new GroupMeetingRepository(dbContext);
            var appointmentRepository = new AppointmentRepository(dbContext);

            var groupMeetingService = new GroupMeetingService(groupMeetingRepository);
            var appointmentService = new AppointmentService(appointmentRepository);

            _presenter = new GroupMeetingSuggestionPresenter(
                this,
                groupMeetingService,
                appointmentService);

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