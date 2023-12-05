using AuthService.Dtos.Group;
using AuthService.Models;

namespace AuthService.Services.Group
{
    public interface IGroupService
    {
        Task<Models.Group> Exist(int GroupId);
        Task<List<int>> Users(int GroupId);
        Task<int> AddUser(int GroupId, int UserId);
        Task<int> Create(string Title, int BossUserId);
        Task<GroupDto> Get(int GroupId);
    }
}
