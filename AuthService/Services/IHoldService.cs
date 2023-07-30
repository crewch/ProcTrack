using AuthService.Dtos;

namespace AuthService.Services
{
    public interface IHoldService
    {
        public Task<List<HoldDto>> GetHolds(UserHoldTypeDto data);
        public Task<CreateHoldResponceDto> CreateHold(CreateHoldRequestDto data);
        public Task<HoldDto> GetHoldById(int id);
        public Task<List<HoldDto>> FindHold(int destId, string type);
        public Task<GetRightResponseDto> GetRights(GetRightRequestDto data);
    }
}
