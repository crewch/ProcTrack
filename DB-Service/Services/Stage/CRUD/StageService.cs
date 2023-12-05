using DB_Service.Clients.Http;
using DB_Service.Data;
using DB_Service.Dtos;
using DB_Service.Dtos.Auth.User;
using DB_Service.Dtos.Stage;
using DB_Service.Exceptions;
using DB_Service.Models;
using DB_Service.Services.Dependence.CRUD;
using DB_Service.Services.Edge.CRUD;
using DB_Service.Services.Status.CRUD;
using DB_Service.Services.Task.CRUD;
using DB_Service.Tools;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;
using static System.Net.Mime.MediaTypeNames;

namespace DB_Service.Services.Stage.CRUD
{
    public class StageService : IStageService
    {
        private readonly DataContext _context;
        private readonly ITaskService _taskService;
        private readonly IStatusService _statusService;
        private readonly IEdgeService _edgeService;
        private readonly IDependenceService _dependenceService;

        private readonly IAuthDataClient _authDataClient;
        
        public StageService(
            DataContext context,
            ITaskService taskService,
            IStatusService statusService,
            IEdgeService edgeService,
            IDependenceService dependenceService,
            IAuthDataClient authDataClient
            )
        {
            _context = context;
            _taskService = taskService;
            _statusService = statusService;
            _edgeService = edgeService;
            _dependenceService = dependenceService;
            _authDataClient = authDataClient;
        }

        public async Task<int> Create(
            string title, 
            int number, 
            int? processId, 
            int? statusId,
            List<int>? CanCreate,
            bool? mark, 
            bool? pass
            )
        {
            var newStage = new Models.Stage
            {
                Title = title,
                Number = number,
                ProcessId = processId,
                StatusId = statusId,
                CanCreate = CanCreate,
                Mark = mark,
                Pass = pass,
                CreatedAt = DateTime.Now.AddHours(3),
            };
            
            await _context.Stages.AddAsync(newStage);
            await _context.SaveChangesAsync();

            await _authDataClient.CreateHold(new Dtos.Auth.Hold.CreateHoldRequestDto()
            {
                DestId = newStage.Id,
                Type = "Stage"
            });

            return newStage.Id;
        }
        
        public async Task<int> Update(
            int stageId,
            string? title,
            DateTime? signedAt,
            DateTime? approvedAt,
            int? statusId,
            string? signed,
            List<int>? canCreate,
            bool? mark,
            bool? pass)
        {
            try
            {
                var stage = await Exist(stageId);

                stage.Title = title ?? stage.Title;
                stage.SignedAt = signedAt ?? stage.SignedAt;
                stage.ApprovedAt = approvedAt ?? stage.ApprovedAt;
                stage.StatusId = statusId ?? stage.StatusId;
                stage.Signed = signed ?? stage.Signed;
                stage.CanCreate = canCreate ?? stage.CanCreate;
                stage.Mark = mark ?? stage.Mark;
                stage.Pass = pass ?? stage.Pass;

                await _context.SaveChangesAsync();

                return stage.Id;
            }
            catch (NotFoundException ex) 
            {
                throw ex;
            }
        }

        public async Task<int> Delete(int stageId)
        {
            try
            {
                var stage = await Exist(stageId);

                var processId = stage.ProcessId.GetValueOrDefault();

                _context.Stages.Remove(stage);
                await _context.SaveChangesAsync();

                return processId;
            }
            catch (NotFoundException ex) 
            {
                throw ex;
            }
        }

        public async Task<Models.Stage> Exist(int stageId)
        {
            var stage = await _context.Stages
                .Include(s => s.Process)
                .Include(s => s.Status)
                .Include(s => s.Tasks)
                .Where(s => s.Id == stageId)
                .FirstOrDefaultAsync() ??
                throw new NotFoundException($"Stage with Id = {stageId} bot found");

            return stage;
        }

        public async Task<StageDto> Get(int stageId)
        {
            try
            {
                var stage = await Exist(stageId);

                // TODO: может добавить поля в StageDto
                var hold = await _authDataClient.FindHold("Stage", stage.Id);

                var userHolds = await _authDataClient.Users(hold.Id);
                var users = new List<UserDto>();
                foreach (var user in userHolds)
                {
                    users.Add(await _authDataClient.GetUser(user.UserId));
                }

                var groupHolds = await _authDataClient.Groups(hold.Id);
                var groups = new List<GroupDto>();
                foreach (var group in groupHolds)
                {
                    // groups.Add(await _authDataClient.Get) // TODO: добавить GetGroup
                }

                return new StageDto
                {
                    Id = stage.Id,
                    Title = stage.Title,
                    Number = stage.Number,
                    ProcessId = stage.ProcessId,
                    ProcessName = stage.Process?.Title,
                    Status = await _statusService.Get(stage.StatusId.GetValueOrDefault()),
                    Hold = hold,
                    Users = users,
                    Groups = new List<GroupDto>(),
                    CreatedAt = DateParser.TryParse(stage.CreatedAt),
                    CreatedAtUnparsed = stage.CreatedAt,
                    SignedAt = DateParser.TryParse(stage.SignedAt),
                    ApprovedAt = DateParser.TryParse(stage.ApprovedAt),
                    Mark = stage.Mark,
                    Pass = stage.Pass,
                    CanCreate = stage.CanCreate
                };
            }
            catch (NotFoundException ex) 
            {
                throw ex;
            }
        }

        public Task<List<int>> GetDependent(int stageId)
        {
            throw new NotImplementedException();
        }

        public Task<List<int>> GetNext(int stageId)
        {
            throw new NotImplementedException();
        }

        public Task<List<int>> GetPrev(int stageId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetStatus(int stageId)
        {
            try
            {
                var stage = await Exist(stageId);

                return stage.StatusId
                    .GetValueOrDefault();
            }
            catch (NotFoundException ex) 
            {
                throw ex;
            }
        }

        public async Task<List<int>> Where(StageParametersDto data)
        {
            return await _context.Stages
                .Include(s => s.Process)
                .Include(s => s.Status)
                .Include(s => s.Tasks)
                .Where(s => data.Statuses.Contains(s.StatusId.GetValueOrDefault())) // TODO: добавить другие параметры к Where и потестить
                .Select(s => s.Id)
                .ToListAsync();
        }

        public async Task<List<int>> Tasks(int stageId)
        {
            var tasks = await _context.Tasks
                .Where(t => t.StageId == stageId)
                .Select(t => t.Id)
                .ToListAsync();

            return tasks;
        }

        public async Task<List<int>> CopyTasks(int oldStageId, int newStageId)
        {
            try
            {
                var oldTaskIds = await Tasks(oldStageId);
                var oldTasksNotAwated = oldTaskIds
                    .Select(async id => await _taskService.Exist(id)); // TODO: эта хрень может вызывать ошибки, проверить
                var oldTasks = await System.Threading.Tasks.Task
                    .WhenAll(oldTasksNotAwated);

                var newTasks = new List<int>();

                oldTasks.ToList()
                    .ForEach(async task =>
                    {
                        newTasks.Add(await _taskService
                            .Create(newStageId, task.Title, task.ExpectedTime));
                    });

                return newTasks;
            }
            catch (NotFoundException ex) 
            {
                throw ex;
            }
        }
    }
}
