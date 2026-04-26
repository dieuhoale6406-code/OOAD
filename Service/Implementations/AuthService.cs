using OOAD.DTOs;
using OOAD.Model;
using OOAD.Repository.Interfaces;
using OOAD.Service.Interfaces;

namespace OOAD.Service.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Users? Login(LoginRequestDto request)
        {
            return _userRepository.GetById(request.UserId);
        }
    }
}
