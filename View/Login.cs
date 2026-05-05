using OOAD.Presenter;

namespace OOAD
{
    public partial class Login : Form
    {
        private readonly LoginPresenter _presenter;

        #region Properties for Presenter Binding
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string EmailText
        {
            get => txtName.Text;
            set => txtName.Text = value.Trim();
        }

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string PasswordText
        {
            get => txtPass.Text;
            set => txtPass.Text = value;
        }
        #endregion

        #region Events (View -> Presenter)
        public event EventHandler? LoginRequested;
        public event EventHandler? ResetRequested;
        public event EventHandler? CancelRequested;
        #endregion

        public Login()
        {
            InitializeComponent();
            _presenter = new LoginPresenter(this);

            btnOK.Click += (_, _) => LoginRequested?.Invoke(this, EventArgs.Empty);
            btnReset.Click += (_, _) => ResetRequested?.Invoke(this, EventArgs.Empty);
            btnCancel.Click += (_, _) => CancelRequested?.Invoke(this, EventArgs.Empty);

            _presenter.Initialize();
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void OpenMainCalendar(Guid userId)
        {
            var main = new MainCalendar(userId);
            bool isLoggingOut = false;

            main.LogoutRequested += (_, _) =>
            {
                isLoggingOut = true;

                EmailText = string.Empty;
                PasswordText = string.Empty;

                Show();
                WindowState = FormWindowState.Normal;
                Activate();
            };

            main.FormClosed += (_, _) =>
            {
                if (!isLoggingOut)
                {
                    Close();
                }
            };

            main.Show();
            Hide();
        }

        public void CloseView() => this.Close();
    }
}