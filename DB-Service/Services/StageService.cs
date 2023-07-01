using DB_Service.Clients.Http;
using DB_Service.Data;
using DB_Service.Dtos;

namespace DB_Service.Services
{
    public class StageService : IStageService
    {
        private readonly DataContext _context;
        private readonly IAuthDataClient _authClient;
        private readonly IFileDataClient _fileClient;

        public StageService(DataContext context, IAuthDataClient authClient, IFileDataClient fileClient)
        {
            _context = context;
            _authClient = authClient;
            _fileClient = fileClient;
        }

        public Task<StageDto> AssignStage(int UserId, int Id)
        {
            throw new NotImplementedException();
        }

        public Task<StageDto> GetStageById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<StageDto>> GetStagesByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TaskDto>> GetTasksByStageId(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<StageDto> UpdateStage(int UserId, int Id, StageDto data)
        {
            throw new NotImplementedException();
        }
    }
}
