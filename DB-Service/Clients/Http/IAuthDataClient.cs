using DB_Service.Dtos;

namespace DB_Service.Clients.Http
{
    public interface IAuthDataClient
    {
        //Task<List<UserWithRoles>> GetUsersWithRoles();
        //Task<List<HoldRightsDto>> GetRightsHolds(LoginTypeDto loginType);
        //Task<UserWithRoles> GetUser(UserLoginDto data);
        //Task<HoldRightsDto> CreateHold(CreateHoldDto data);
        //Task<UsersGroupsDto> GetUsersGroups(GetUserByHoldDto data);
        Task<List<HoldDto>> GetHolds(UserHoldTypeDto data);
        Task<CreateHoldResponceDto> CreateHold(CreateHoldRequestDto data);
        Task<HoldDto> GetHoldById(int id);
        Task<UserDto> GetUserById(int id);
        Task<List<HoldDto>> FindHold(int destId, string type);
        Task<List<UserDto>> GetUsersByGroupId(int GroupId);
    }
}
