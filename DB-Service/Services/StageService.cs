using DB_Service.Clients.Http;
using DB_Service.Data;
using DB_Service.Dtos;
using Microsoft.EntityFrameworkCore;
using DB_Service.Services;

namespace DB_Service.Services
{
    public class StageService : IStageService
    {
        private readonly DataContext _context;
        private readonly IAuthDataClient _authClient;
        private readonly IFileDataClient _fileClient;
        private readonly ITaskService _taskService;

        public StageService(DataContext context, IAuthDataClient authClient, IFileDataClient fileClient, ITaskService taskService)
        {
            _context = context;
            _authClient = authClient;
            _fileClient = fileClient;
            _taskService = taskService;
        }

        public Task<StageDto> AssignStage(int UserId, int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<StageDto> GetStageById(int Id)
        {
            var stageModel = _context.Stages
                .Include(s => s.Status)
                .Where(s => s.Id == Id)
                .FirstOrDefault();

            if (stageModel == null)
            {
                return null;
            }

            var user = new UserDto();

            if (stageModel.SignId != null)
            {
                user = await _authClient.GetUserById((int)stageModel.SignId);
            }

            var holds = await _authClient.FindHold(Id, "Stage");

            var status = _context.Statuses
                .Where(s => s.Id == stageModel.StatusId)
                .FirstOrDefault();

            var res = new StageDto
            {
                Id = stageModel.Id,
                Title = stageModel.Title,
                Status = status == null ? null : status.Title,
                Holds = holds,
                User = user,
                CreatedAt = stageModel.CreatedAt,
            };

            return res;
        }

        public async Task<List<StageDto>> GetStagesByUserId(int UserId)
        {
            var req = new UserHoldTypeDto
            {
                Id = UserId,
                HoldType = "Stage"
            };
            
            var holds = await _authClient.GetHolds(req);

            var res = new List<StageDto>();

            foreach (var hold in holds)
            {
                var stageModel = _context.Stages
                    .Where(s => s.Id == hold.DestId && 
                                s.Status.Title.ToLower() != "не начат" &&
                                s.Status.Title.ToLower() != "отменен"
                    )
                    .FirstOrDefault();
                
                var stageDto = await GetStageById(stageModel.Id);

                if (stageDto != null)
                {
                    res.Add(stageDto);
                }
            }
            return res;
        }

        public async Task<List<TaskDto>> GetTasksByStageId(int Id)
        {
            var taskModels = _context.Tasks
                .Where(s => s.StageId == Id)
                .ToList();

            var res = new List<TaskDto>();

            foreach(var task in taskModels)
            {
                var taskDto = await _taskService.GetTaskById(task.Id);
                if (taskDto != null)
                {
                    res.Add(taskDto);
                }
            }
            return res;
        }

        public Task<StageDto> UpdateStage(int UserId, int Id, StageDto data)
        {
            throw new NotImplementedException();
        }
    }
}
