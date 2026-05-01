using OOAD.DTOs;
using OOAD.Model;
using OOAD.Repository;
using OOAD.Utils;

namespace OOAD.Service
{
    public class AuthService
    {
        private readonly UserRepository _userRepository;

        public AuthService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Users? Login(LoginRequestDto request)
        {
            if (request == null)
                return null;

            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                return null;

            var user = _userRepository.GetByEmail(request.Email);
            if (user == null)
                return null;

            return PasswordHasher.Verify(request.Password, user.PasswordHash) ? user : null;
        }
    }
}
