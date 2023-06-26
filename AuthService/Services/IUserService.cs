using AuthService.Dtos;

namespace AuthService.Services
{
    public interface IUserService
    {
        Task<UserWithRoles> GetUserByLogin(UserLoginDto data);
    }
}
