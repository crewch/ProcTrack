using DB_Service.Dtos;

namespace DB_Service.Clients.Http
{
    public interface IAuthDataClient
    {
        Task<List<UserWithRoles>> GetUsersWithRoles();
        Task<List<HoldRightsDto>> GetRightsHolds(LoginTypeDto loginType);
    }
}
