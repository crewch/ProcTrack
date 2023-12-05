using AuthService.Dtos.User;
using AuthService.Models;
namespace AuthService.Services.User
{
    public interface IUserService
    {
        Task<Models.User> Exist(int UserId);
        // получение всех доступных объектов для какого человека
        Task<List<int>> Holds(string Type, int UserId);
        Task<List<int>> Groups(int UserId);
        Task<UserDto> Get(int UserId);
        Task<List<string>> Roles(int UserId);
        Task<int> Create(string Email, string LongName, string ShortName);
        Task<int> AddRole(int UserId, int RoleId);
    }
}
