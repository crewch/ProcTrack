using AuthService.Dtos;

namespace AuthService.Services
{
    public interface IHoldService
    {
        //public Task<List<HoldRightsDto>> GetHoldIdsAndRights(LoginTypeDto loginType);
        //public Task<HoldRightsDto> CreateHold(HoldDto data);
        //public Task<UsersGroupsDto> GetUsersGroupsByHold(GetUserByHoldDto data);
        //public Task<HoldDto> GetHold(GetHoldTypeDto data);
        public Task<List<HoldDto>> GetHolds(UserHoldTypeDto data);
        public Task<CreateHoldResponceDto> CreateHold(CreateHoldRequestDto data);
        public Task<HoldDto> GetHoldById(int id);
        public Task<List<HoldDto>> FindHold(int destId, string type);
    }
}
