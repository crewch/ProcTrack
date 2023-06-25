using AuthService.Dtos;

namespace AuthService.Services
{
    public interface IHoldService
    {
        public Task<List<HoldRightsDto>> GetHoldIdsAndRights(LoginTypeDto loginType);
    }
}
