using DB_Service.Dtos;
using DB_Service.Dtos.Stage;

namespace DB_Service.Services.Stage.CRUD
{
    public interface IStageService //TODO:добавить Create
    {
        Task<int> Create(
            string title,
            int number,
            int? processId,
            int? statusId,
            List<int>? CanCreate,
            bool? mark,
            bool? pass
            );

        Task<StageDto> Get(int stageId);

        Task<Models.Stage> Exist(int stageId);

        Task<List<int>> Where(StageParametersDto data);

        Task<List<int>> GetNext(int stageId);

        Task<List<int>> GetPrev(int stageId);

        Task<List<int>> GetDependent(int stageId);

        Task<int> GetStatus(int stageId);

        Task<int> Update(
            int stageId,
            string? title,
            DateTime? signedAt,
            DateTime? approvedAt,
            int? statusId,
            string? signed,
            List<int>? canCreate,
            bool? mark,
            bool? pass
            );

        Task<int> Delete(int stageId);

        Task<List<int>> Tasks(int stageId);

        Task<List<int>> CopyTasks(int oldStageId, int newStageId);
    }
}
