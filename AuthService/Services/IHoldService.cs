using AuthService.Dtos;

namespace AuthService.Services
{
    public interface IHoldService
    {
        public Task<List<HoldRightsDto>> GetHoldIdsAndRights(LoginTypeDto loginType);
        public Task<HoldRightsDto> CreateHold(CreateHoldDto data);
        public Task<UsersGroupsDto> GetUsersGroupsByHold(GetUserByHoldDto data);
    }
}
