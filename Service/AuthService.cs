using OOAD.DTOs;
using OOAD.Model;
using OOAD.Repository;

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
            return _userRepository.GetById(request.UserId);
        }
    }
}
