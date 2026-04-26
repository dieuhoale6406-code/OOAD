using OOAD.DTOs;
using OOAD.Model;

namespace OOAD.Service.Interfaces
{
    public interface IAuthService
    {
        Users? Login(LoginRequestDto request);
    }
}
