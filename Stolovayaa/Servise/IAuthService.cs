using Stolovayaa.DTOs;

namespace Stolovayaa.Servise
{
    public interface IAuthService
    {
        UserDto Login(LoginDto loginDto);
        void Register(RegisterDto registerDto);
        bool IsLoggedIn { get; }
        UserDto CurrentUser { get; }
        void Logout();
    }
}