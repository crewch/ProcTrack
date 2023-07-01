using DB_Service.Dtos;

namespace DB_Service.Services
{
    public interface ITaskService
    {
        Task<TaskDto> AssignTask(int UserId, int Id);

        Task<TaskDto> GetTaskById(int Id);

        Task<TaskDto> StartTask(int UserId, int Id);

        Task<TaskDto> StopTask(int UserId, int Id);

        Task<List<CommentDto>> GetComentsByTaskId(int Id);

        Task<CommentDto> CreateCommment(int Id, CommentDto data);
    }
}
