using OOAD.Presenter;
using OOAD.Data;
using OOAD.Repository;
using OOAD.Service;

namespace OOAD
{
    public partial class Login : Form
    {
        private LoginPresenter? _presenter;

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string EmailText
        {
            get => txtName.Text;
            set => txtName.Text = value;
        }

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string PasswordText
        {
            get => txtPass.Text;
            set => txtPass.Text = value;
        }

        public event EventHandler? LoginRequested;
        public event EventHandler? ResetRequested;
        public event EventHandler? CancelRequested;

        public Login()
        {
            InitializeComponent();

            btnOK.Click += (_, _) => LoginRequested?.Invoke(this, EventArgs.Empty);
            btnReset.Click += (_, _) => ResetRequested?.Invoke(this, EventArgs.Empty);
            btnCancel.Click += (_, _) => CancelRequested?.Invoke(this, EventArgs.Empty);

            InitializePresenter();
        }

        private void InitializePresenter()
        {
            var dbContext = new AppDBContext();
            var userRepo = new UserRepository(dbContext);
            var authService = new AuthService(userRepo);

            _presenter = new LoginPresenter(this, authService);
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
            main.Show();
            Hide();
        }

        public void CloseView()
        {
            Close();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}