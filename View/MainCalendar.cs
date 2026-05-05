using OOAD.Model;
using OOAD.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;

namespace OOAD
{
    public partial class MainCalendar : Form
    {
        private MainCalendarPresenter _presenter;
        private readonly Guid _userId;

        #region Properties for Presenter Binding
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime SelectedDate => monthCalendar1.SelectionStart.Date;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ShowAllAppointments => checkBox1.Checked;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string GreetingText
        {
            get => lblGreeting.Text;
            set => lblGreeting.Text = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Guid? SelectedAppointmentId
        {
            get
            {
                if (dgvAppointment.CurrentRow == null)
                    return null;

                return Guid.TryParse(
                    dgvAppointment.CurrentRow.Cells["AppointmentId"].Value?.ToString(),
                    out var id
                ) ? id : null;
            }
        }
        #endregion

        #region Events (View -> Presenter)
        public event EventHandler? ViewLoaded;
        public event EventHandler? SelectedDateChanged;
        public event EventHandler? ShowAllAppointmentsChanged;
        public event EventHandler? AddRequested;
        public event EventHandler? UpdateRequested;
        public event EventHandler? DeleteRequested;
        public event EventHandler? LogoutRequested;
        #endregion

        public MainCalendar(Guid userId)
        {
            InitializeComponent();
            _userId = userId;
            _presenter = new MainCalendarPresenter(this, _userId);

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
            btnLogout.Click += (_, _) => Logout();

            _presenter.Initialize();
        }

        public void BindAppointments(IEnumerable<Appointments> appointments)
        {
            dgvAppointment.DataSource = appointments
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

            if (dgvAppointment.Columns["AppointmentId"] != null)
                dgvAppointment.Columns["AppointmentId"]!.Visible = false;

            if (dgvAppointment.Columns["Name"] != null)
            {
                dgvAppointment.Columns["Name"]!.HeaderText = "Tên cuộc hẹn";
                dgvAppointment.Columns["Name"]!.Width = 160;
            }

            if (dgvAppointment.Columns["Location"] != null)
            {
                dgvAppointment.Columns["Location"]!.HeaderText = "Địa điểm";
                dgvAppointment.Columns["Location"]!.Width = 130;
            }

            if (dgvAppointment.Columns["StartTime"] != null)
            {
                dgvAppointment.Columns["StartTime"]!.HeaderText = "Bắt đầu";
                dgvAppointment.Columns["StartTime"]!.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvAppointment.Columns["StartTime"]!.Width = 140;
            }

            if (dgvAppointment.Columns["EndTime"] != null)
            {
                dgvAppointment.Columns["EndTime"]!.HeaderText = "Kết thúc";
                dgvAppointment.Columns["EndTime"]!.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvAppointment.Columns["EndTime"]!.Width = 140;
            }
        }

        public bool ConfirmDelete()
        {
            var result = MessageBox.Show(
                "Bạn có chắc muốn xóa cuộc hẹn đã chọn?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

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

            SelectedDateChanged?.Invoke(this, EventArgs.Empty);
        }

        private void Logout()
        {
            var confirm = MessageBox.Show(
                "Bạn có chắc muốn đăng xuất không?",
                "Đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes)
                return;

            LogoutRequested?.Invoke(this, EventArgs.Empty);
            Close();
        }
    }
}
