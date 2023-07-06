using AuthService.Dtos;

namespace AuthService.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUserByEmail(string Email);

        Task<UserDto> GetUserById(int id);

        Task<List<GroupDto>> GetGroups();

        Task<GroupDto> GetGroupByTitle(string Title);

        // Task<UserDto> CreateUser(UserDto data);

        // Task<GroupDto> CreateGroup(GroupDto data);
    }
}
