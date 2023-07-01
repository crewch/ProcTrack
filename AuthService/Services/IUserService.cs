using AuthService.Dtos;

namespace AuthService.Services
{
    public interface IUserService
    {
        //Task<UserWithRoles> GetUserByLogin(UserEmailDto data);
        Task<UserDto> GetUserById(int id);
    }
}
