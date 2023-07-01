using DB_Service.Clients.Http;
using DB_Service.Data;
using DB_Service.Dtos;

namespace DB_Service.Services
{
    public class TaskService : ITaskService
    {
        private readonly DataContext _context;
        private readonly IAuthDataClient _authClient;
        private readonly IFileDataClient _fileClient;

        public TaskService(DataContext context, IAuthDataClient authClient, IFileDataClient fileClient)
        {
            _context = context;
            _authClient = authClient;
            _fileClient = fileClient;
        }

        public Task<TaskDto> AssignTask(int UserId, int Id)
        {
            throw new NotImplementedException();
        }

        public Task<CommentDto> CreateCommment(int Id, CommentDto data)
        {
            throw new NotImplementedException();
        }

        public Task<List<CommentDto>> GetComentsByTaskId(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<TaskDto> GetTaskById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<TaskDto> StartTask(int UserId, int Id)
        {
            throw new NotImplementedException();
        }

        public Task<TaskDto> StopTask(int UserId, int Id)
        {
            throw new NotImplementedException();
        }
    }
}
