using OOAD.Data;
using OOAD.DTOs;
using OOAD.Services;

namespace OOAD.Presenter
{
    public class LoginPresenter
    {
        private readonly Login _view;
        private readonly AuthService _authService;

        public LoginPresenter(Login view)
        {
            _view = view;
            var context = new AppDBContext();
            _authService = new AuthService(context);
        }

        public void Initialize()
        {
            _view.LoginRequested += OnLoginRequested;
            _view.ResetRequested += OnResetRequested;
            _view.CancelRequested += OnCancelRequested;
        }

        private void OnLoginRequested(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_view.EmailText))
            {
                _view.ShowError("Vui lòng nhập email.");
                return;
            }

            if (string.IsNullOrWhiteSpace(_view.PasswordText))
            {
                _view.ShowError("Vui lòng nhập mật khẩu.");
                return;
            }

            var user = _authService.Login(new LoginRequestDto
            {
                Email = _view.EmailText,
                Password = _view.PasswordText
            });

            if (user == null)
            {
                _view.ShowError("Email hoặc mật khẩu không đúng.");
                return;
            }

            _view.ShowMessage($"Đăng nhập thành công! Xin chào {user.FullName}.");
            _view.OpenMainCalendar(user.UserId);
        }

        private void OnResetRequested(object? sender, EventArgs e)
        {
            _view.EmailText = string.Empty;
            _view.PasswordText = string.Empty;
        }

        private void OnCancelRequested(object? sender, EventArgs e)
        {
            _view.CloseView();
        }
    }
}