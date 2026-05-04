namespace OOAD.DTOs
{
    public class LoginRequestDto
    {
        private string _email = string.Empty;
        public string Email
        {
            get { return _email; }
            set { _email = value?.Trim() ?? string.Empty; }
        }
        public string Password { get; set; } = string.Empty;
    }
}