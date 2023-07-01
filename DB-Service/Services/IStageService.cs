using DB_Service.Dtos;

namespace DB_Service.Services
{
    public interface IStageService
    {
        Task<List<StageDto>> GetStagesByUserId(int userId);

        Task<StageDto> AssignStage(int UserId, int Id);

        Task<StageDto> UpdateStage(int UserId, int Id, StageDto data);

        Task<StageDto> GetStageById(int Id);

        Task<List<TaskDto>> GetTasksByStageId(int Id);
    }
}
