using AuthService.Dtos;

namespace AuthService.Services
{
    public interface ILoginService
    {
        Task<UserDto> Authorize(AuthDto data);
    }
}
