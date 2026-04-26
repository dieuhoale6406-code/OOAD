using OOAD.DTOs;
using OOAD.Service;

namespace OOAD.Presenter
{
    public class LoginPresenter
    {
        private readonly Login _view;
        private readonly AuthService _authService;

        public LoginPresenter(Login view, AuthService authService)
        {
            _view = view;
            _authService = authService;
        }

        public void Initialize()
        {
            _view.LoginRequested += OnLoginRequested;
            _view.ResetRequested += OnResetRequested;
            _view.CancelRequested += OnCancelRequested;
        }

        private void OnLoginRequested(object? sender, EventArgs e)
        {
            if (!Guid.TryParse(_view.UserIdText, out var userId))
            {
                _view.ShowError("UserId phải là GUID hợp lệ.");
                return;
            }

            var user = _authService.Login(new LoginRequestDto
            {
                UserId = userId
            });

            if (user == null)
            {
                _view.ShowError("Không tìm thấy user.");
                return;
            }

            _view.ShowMessage("Đăng nhập thành công.");
            _view.OpenMainCalendar(user.UserId);
        }

        private void OnResetRequested(object? sender, EventArgs e)
        {
            _view.UserIdText = string.Empty;
            _view.PasswordText = string.Empty;
        }

        private void OnCancelRequested(object? sender, EventArgs e)
        {
            _view.CloseView();
        }
    }
}
