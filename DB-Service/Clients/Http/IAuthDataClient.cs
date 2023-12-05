using DB_Service.Dtos.Auth.Hold;
using DB_Service.Dtos.Auth.User;
namespace DB_Service.Clients.Http
{
    public interface IAuthDataClient
    {
        Task<HoldDto> GetHold(int id);

        Task<HoldDto> CreateHold(CreateHoldRequestDto data);

        Task<HoldDto> AddUserToHold(AddUserToHoldRequestDto data, int id);

        Task<HoldDto> AddGroupToHold(AddGroupToHoldRequestDto data, int id);

        Task<List<UserHoldDto>> Users(int id);

        Task<List<GroupHoldDto>> Groups(int id);

        Task<List<HoldDto>> CopyHold(int HoldId, int NewHoldId);

        Task<HoldDto> FindHold(string Type, int DestId);

        Task<List<HoldDto>> GetHoldsByUserId(int UserId, string Type);

        Task<UserDto> GetUser(int userId);
    }
}
