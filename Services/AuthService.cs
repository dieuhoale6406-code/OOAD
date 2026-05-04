using OOAD.Data;
using OOAD.DTOs;
using OOAD.Model;
using OOAD.Repository;
using OOAD.Utils;

namespace OOAD.Services
{
    public class AuthService
    {
        private readonly AppDBContext _context;
        private readonly UserRepository _userRepository;

        public AuthService(AppDBContext context)
        {
            _context = context;
            _userRepository = new UserRepository(_context);
        }

        public Users? Login(LoginRequestDto request)
        {
            if (request == null)
                return null;
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                return null;

            var normalizedEmail = request.Email.Trim().ToLower();
            var user = _userRepository.Query
                .FirstOrDefault(u => u.Email.ToLower() == normalizedEmail);
            if (user == null)
                return null;

            return PasswordHasher.Verify(request.Password, user.PasswordHash) ? user : null;
        }
    }
}
