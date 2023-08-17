using AuthService.Dtos;

namespace AuthService.Services
{
    public interface ILoginService
    {
        Task<TokenDto> Authorize(AuthDto data);

        Task<TokenDto> RefreshToken(TokenDto data);
    }
}
