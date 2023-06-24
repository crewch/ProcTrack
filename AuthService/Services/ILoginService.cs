using AuthService.Models;

namespace AuthService.Services
{
    public interface ILoginService
    {
        Task<UserToken> Login(UserLogin userLogin);
    }
}
