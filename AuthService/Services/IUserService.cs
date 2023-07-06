using AuthService.Dtos;

namespace AuthService.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUserById(int id);
        Task<List<GroupDto>> GetGroups();
    }
}
