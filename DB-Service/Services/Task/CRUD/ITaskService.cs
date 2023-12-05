using DB_Service.Dtos;

namespace DB_Service.Services.Task.CRUD
{
    public interface ITaskService
    {
        Task<int> Create(
            int? stageId,
            string title,
            TimeSpan expectedTime
            );

        Task<int> Update(
            int taskId,
            string? title,
            DateTime? endVerificationDate, 
            DateTime? startedAt,
            DateTime? approvedAt,
            TimeSpan? expectedTime,
            string? signed,
            int? signId
        );

        Task<int> Delete(int taskId);

        Task<TaskDto> Get(int taskId);

        Task<Models.Task> Exist(int taskId);

        Task<List<int>> Comments(int taskId);
    }
}
