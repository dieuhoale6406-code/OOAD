using OOAD.Presenter;
using MaterialSkin;
using MaterialSkin.Controls;

using OOAD.Data;
using OOAD.Repository;
using OOAD.Service;

namespace OOAD
{
    public partial class Login : MaterialForm
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

            ApplyMaterialTheme();

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

        private void ApplyMaterialTheme()
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

            // ===== FORM STYLE ONLY =====
            Text = "Login";
            BackColor = Color.White;
            MaximizeBox = false;
            DoubleBuffered = true;

            // Không set StartPosition, không set FormBorderStyle nếu muốn giữ đúng cấu hình Designer.

            // ===== WHITE BODY FOR MATERIAL FORM =====
            Paint += (sender, e) =>
            {
                using var whiteBrush = new SolidBrush(Color.White);
                e.Graphics.FillRectangle(
                    whiteBrush,
                    new Rectangle(0, 64, ClientSize.Width, ClientSize.Height - 64)
                );
            };

            Resize += (sender, e) =>
            {
                Invalidate();
            };

            // ===== TITLE STYLE ONLY =====
            label3.Text = "Login";
            label3.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(37, 99, 235);
            label3.BackColor = Color.Transparent;
            label3.AutoSize = true;

            // ===== LABEL STYLE ONLY =====
            label1.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(51, 65, 85);
            label1.BackColor = Color.Transparent;

            label2.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(51, 65, 85);
            label2.BackColor = Color.Transparent;

            // ===== TEXTBOX STYLE ONLY =====
            txtName.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtName.BackColor = Color.White;
            txtName.ForeColor = Color.FromArgb(15, 23, 42);
            txtName.BorderStyle = BorderStyle.FixedSingle;

            txtPass.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPass.BackColor = Color.White;
            txtPass.ForeColor = Color.FromArgb(15, 23, 42);
            txtPass.BorderStyle = BorderStyle.FixedSingle;

            txtName.Enter += (sender, e) => txtName.BackColor = Color.White;
            txtName.Leave += (sender, e) => txtName.BackColor = Color.White;
            txtPass.Enter += (sender, e) => txtPass.BackColor = Color.White;
            txtPass.Leave += (sender, e) => txtPass.BackColor = Color.White;

            // ===== BUTTON OK STYLE ONLY =====
            btnOK.BackColor = Color.FromArgb(37, 99, 235);
            btnOK.ForeColor = Color.White;
            btnOK.FlatStyle = FlatStyle.Flat;
            btnOK.FlatAppearance.BorderSize = 0;
            btnOK.FlatAppearance.MouseOverBackColor = Color.FromArgb(29, 78, 216);
            btnOK.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 64, 175);
            btnOK.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnOK.Cursor = Cursors.Hand;
            btnOK.UseVisualStyleBackColor = false;

            // ===== BUTTON RESET STYLE ONLY =====
            btnReset.BackColor = Color.FromArgb(96, 165, 250);
            btnReset.ForeColor = Color.White;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.FlatAppearance.MouseOverBackColor = Color.FromArgb(59, 130, 246);
            btnReset.FlatAppearance.MouseDownBackColor = Color.FromArgb(37, 99, 235);
            btnReset.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnReset.Cursor = Cursors.Hand;
            btnReset.UseVisualStyleBackColor = false;

            // ===== BUTTON CANCEL STYLE ONLY =====
            btnCancel.BackColor = Color.White;
            btnCancel.ForeColor = Color.FromArgb(37, 99, 235);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 2;
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(37, 99, 235);
            btnCancel.FlatAppearance.MouseOverBackColor = Color.White;
            btnCancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(239, 246, 255);
            btnCancel.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.UseVisualStyleBackColor = false;

            Invalidate();
        }
    }
}