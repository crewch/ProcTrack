using DB_Service.Clients.Http;
using DB_Service.Data;
using DB_Service.Dtos;
using Microsoft.EntityFrameworkCore;
using DB_Service.Services;
using DB_Service.Tools;

namespace DB_Service.Services
{
    public class StageService : IStageService
    {
        private readonly DataContext _context;
        private readonly IAuthDataClient _authClient;
        private readonly IFileDataClient _fileClient;
        private readonly ITaskService _taskService;

        public StageService(DataContext context, 
                            IAuthDataClient authClient, 
                            IFileDataClient fileClient, 
                            ITaskService taskService)
        {
            _context = context;
            _authClient = authClient;
            _fileClient = fileClient;
            _taskService = taskService;
        }

        public async Task<StageDto> CancelStageById(int Id, int UserId)
        {
            var stage = _context.Stages
                .Include(s => s.Status)
                .Where(s => s.Id == Id)
                .FirstOrDefault();

            if (stage == null)
            {
                return null;
            }

            stage.Signed = null;
            stage.SignedAt = null;
            stage.SignId = null;
            stage.Status = _context.Statuses
                .Where(s => s.Title.ToLower() == "отменен")
                .FirstOrDefault();
            _context.SaveChanges();

            return await GetStageById(Id);
        }

        public async Task<StageDto> AssignStage(int UserId, int Id)
        {
            var stage = _context.Stages
                .Include(s => s.Status)
                .Where(s => s.Id == Id)
                .FirstOrDefault();

            if (stage == null)
            {
                return null;
            }
            if (stage.Pass ?? false)
            {
                var nextStagesPass = _context.Edges
                    .Include(e => e.EndStage.Status)
                    .Where(e => e.Start == stage.Id && e.EndStage.Status.Title.ToLower() == "не начат")
                    .Select(e => e.EndStage)
                    .ToList();

                var newStatusPass = _context.Statuses
                    .Where(s => s.Title.ToLower() == "отправлен на проверку")
                    .FirstOrDefault();
                foreach (var next in nextStagesPass)
                {
                    if (next.Pass == null ? false : (bool) next.Pass)
                    {
                        await AssignStage(UserId, next.Id);
                        continue;
                    }
                    next.Status = newStatusPass;
                }
                _context.SaveChanges();

                return await GetStageById(Id);
            }

            bool blockStage = stage.Status.Title.ToLower() == "отменено";

            stage.Signed = UserId.ToString();
            stage.SignedAt = DateTime.Now.AddHours(3);
            
            if (_context.Stages
                    .Include(s => s.Status)
                    .Where(s => s.ProcessId == stage.ProcessId && 
                            s.Id != stage.Id && (
                            s.Status.Title.ToLower() == "отменено" ||
                            s.Status.Title.ToLower() == "остановлен"
                        ))
                    .Select(s => s.Status.Title)
                    .ToList()
                    .Count() != 0)
            {
                stage.Status = _context.Statuses
                    .Where(s => s.Title.ToLower() == "согласовано-блокировано")
                    .FirstOrDefault();
                _context.SaveChanges();
                return await GetStageById(Id);
            }

            var dependences = _context.Dependences
                .Include(d => d.SecondStage.Status)
                .Where(d => d.First == stage.Id)
                .Select(d => d.SecondStage)
                .ToList();
            
            if (dependences.All(d => 
                    d.Status.Title.ToLower() == "согласовано-блокировано" ||
                    d.Status.Title.ToLower() == "согласовано") || 
                dependences == null)
            {
                stage.Status = _context.Statuses
                    .Where(s => s.Title.ToLower() == "согласовано")
                    .FirstOrDefault();
                _context.SaveChanges();
            } else {
                stage.Status = _context.Statuses
                    .Where(s => s.Title.ToLower() == "согласовано-блокировано")
                    .FirstOrDefault();
                _context.SaveChanges();
                return await GetStageById(Id);
            }
            
            var dependent = _context.Dependences
                .Include(d => d.FirstStage.Status)
                .Where(d => d.Second == stage.Id && d.FirstStage.Status.Title.ToLower() == "согласовано-блокировано")
                .Select(d => d.FirstStage)
                .ToList();
            
            foreach (var depStage in dependent)
            {
                await AssignStage(UserId, depStage.Id);
            }

            var nextStages = _context.Edges
                .Include(e => e.EndStage.Status)
                .Where(e => e.Start == stage.Id && e.EndStage.Status.Title.ToLower() == "не начат")
                .Select(e => e.EndStage)
                .ToList();

            var newStatus = _context.Statuses
                .Where(s => s.Title.ToLower() == "отправлен на проверку")
                .FirstOrDefault();
            foreach (var next in nextStages)
            {
                if (next.Pass ?? false)
                {
                    await AssignStage(UserId, next.Id);
                    continue;
                }
                next.Status = newStatus;
            }
            _context.SaveChanges();

            if (blockStage) {
                var blockingStagesIds = _context.Stages
                    .Include(s => s.Status)
                    .Where(s => s.ProcessId == stage.ProcessId &&
                                s.Status.Title.ToLower() == "согласовано-блокировано")
                    .Select(s => s.Id)
                    .ToList();
                foreach (var blockingStageId in blockingStagesIds)
                {
                    await AssignStage(UserId, blockingStageId);
                }
            }

            return await GetStageById(Id);
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
                ProcessId = stageModel.ProcessId,
                Title = stageModel.Title,
                Status = status == null ? null : status.Title,
                StatusValue = status == null ? null : status.Value,
                Holds = holds,
                User = user,
                CreatedAt = stageModel.CreatedAt == null ? null : DateParser.Parse((DateTime)stageModel.CreatedAt),
                SignedAt = stageModel.SignedAt == null ? null : DateParser.Parse((DateTime)stageModel.SignedAt),
                ApprovedAt = stageModel.ApprovedAt == null ? null : DateParser.Parse((DateTime)stageModel.ApprovedAt),
                Mark = stageModel.Mark,
                Pass = stageModel.Pass,
                CanCreate = stageModel.CanCreate
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
            
            var used = new HashSet<int>();
            
            var res = new List<StageDto>();

            if (holds == null) 
            {
                return null;
            }

            foreach (var hold in holds)
            {
                if (used.Any(u => hold.DestId == u))
                {
                    continue;
                }

                used.Add(hold.DestId);

                var stageModel = _context.Stages
                    .Include(s => s.Status)
                    .Where(s => s.Id == hold.DestId &&
                                s.Status != null && 
                                s.Status.Title.ToLower() != "не начат" &&
                                s.Status.Title.ToLower() != "остановлен"
                    )
                    .FirstOrDefault();

                if (stageModel != null)
                {
                    var stageDto = await GetStageById(stageModel.Id);

                    if (stageDto != null)
                    {
                        res.Add(stageDto);
                    }
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

        public async Task<StageDto> UpdateStage(int UserId, int Id, StageDto data)
        {
            var stage = _context.Stages
                .Where(s => s.Id == Id)
                .FirstOrDefault();

            if (stage == null)
            {
                return null;
            }
            if (data.Title != null) {
                stage.Title = data.Title;
            }
            if (data.Pass != null) {
                stage.Pass = data.Pass;
            }
            
            if (data.Status != null) {
                var status = _context.Statuses
                    .Where(s => s.Title == data.Status)
                    .FirstOrDefault();

                stage.Status = status;
            }
            
            _context.SaveChanges();

            var res = await GetStageById(Id);

            return res;
        }
    }
}
