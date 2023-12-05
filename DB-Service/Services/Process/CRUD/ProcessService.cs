using DB_Service.Clients.Http;
using DB_Service.Data;
using DB_Service.Dtos.Process;
using DB_Service.Exceptions;
using DB_Service.Models;
using DB_Service.Services.Dependence.CRUD;
using DB_Service.Services.Edge.CRUD;
using DB_Service.Services.Passport.CRUD;
using DB_Service.Services.Priority.CRUD;
using DB_Service.Services.Program.CRUD;
using DB_Service.Services.Stage.CRUD;
using DB_Service.Services.Status.CRUD;
using DB_Service.Services.Type.CRUD;
using DB_Service.Tools;
using Microsoft.EntityFrameworkCore;

namespace DB_Service.Services.Process.CRUD
{
    public class ProcessService : IProcessService
    {
        private readonly DataContext _context;
        private readonly ITypeService _typeService;
        private readonly IProgramService _programService;
        private readonly IPriorityService _priorityService;
        private readonly IPassportService _passportService;
        private readonly IStageService _stageService;
        private readonly IStatusService _statusService;
        private readonly IEdgeService _edgeService;
        private readonly IDependenceService _dependenceService;
        private readonly IAuthDataClient _authDataClient;

        public ProcessService(
            DataContext context,
            IPriorityService priorityService,
            ITypeService typeService,
            IProgramService programService,
            IPassportService passportService,
            IStageService stageService,
            IStatusService statusService,
            IEdgeService edgeService,
            IDependenceService dependenceService,
            IAuthDataClient authDataClient
            )
        {
            _context = context;
            _typeService = typeService;
            _priorityService = priorityService;
            _programService = programService;
            _passportService = passportService;
            _stageService = stageService;
            _statusService = statusService;
            _edgeService = edgeService;
            _dependenceService = dependenceService;
            _authDataClient = authDataClient;
        }

        public async Task<int> Create(
            string title, 
            string? description, 
            bool isTemplate, 
            int? priorityId,
            int? programId,
            int? typeId, 
            int? head, 
            int? tail, 
            TimeSpan? expectedTime
            )
        {
            var newProcess = new Models.Process
            {
                Title = title,
                Description = description,
                IsTemplate = isTemplate,
                PriorityId = priorityId,
                ProgramId = programId,
                TypeId = typeId,
                CreatedAt = DateTime.Now.AddHours(3),
                ExpectedTime = expectedTime.GetValueOrDefault(),
                Head = head,
                Tail = tail,
            };

            await _context.Processes.AddAsync(newProcess);
            await _context.SaveChangesAsync();

            await _authDataClient.CreateHold(new Dtos.Auth.Hold.CreateHoldRequestDto()
            {
                DestId = newProcess.Id,
                Type = "Stage"
            });

            return newProcess.Id;
        }

        public async Task<bool> Delete(int processId)
        {
            try
            {
                var process = await Exist(processId);

                _context.Processes.Remove(process);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<List<Tuple<int, int>>> Dependences(int processId)
        {
            var stageIds = await Stages(processId);

            var dependences = new List<Tuple<int, int>>();

            foreach (var stageId in stageIds)
            {
                var inDependences = await _context.Dependences
                    .Where(d => d.First == stageId)
                    .Select(d => d.Second)
                    .ToListAsync();

                inDependences.ForEach(id =>
                {
                    dependences.Add(new Tuple<int, int>(stageId, id.GetValueOrDefault()));
                });     
            }

            return dependences;
        }

        public async Task<List<Tuple<int, int>>> Edges(int processId)
        {
            var stageIds = await Stages(processId);

            var edges = new List<Tuple<int, int>>();

            foreach (var stageId in stageIds)
            {
                var inEdges = await _context.Edges
                    .Where(e => e.Start == stageId)
                    .Select(e => e.End)
                    .ToListAsync();

                inEdges.ForEach(ie =>
                {
                    edges.Add(new Tuple<int, int>(stageId, ie.GetValueOrDefault()));
                });
            }

            return edges;
        }

        public async Task<Models.Process> Exist(int processId)
        {
            var process = await _context.Processes
                .Include(p => p.HeadStage)
                .Include(p => p.TailStage)
                .Include(p => p.Type)
                .Include(p => p.Priority)
                .Include(p => p.Program)
                .Include(p => p.Passports)
                .Include(p => p.Stages)
                .Where(p => p.Id ==  processId)
                .FirstOrDefaultAsync() ??
                throw new NotFoundException($"Process with id = {processId} not found");

            return process;
        }

        public async Task<ProcessDto> Get(int processId)
        {
            try
            {
                var process = await Exist(processId);
                var hold = await _authDataClient.FindHold("Process", process.Id);

                var priority = await _priorityService
                    .Exist(process.PriorityId.GetValueOrDefault());

                return new ProcessDto
                {
                    Id = process.Id,
                    Title = process.Title,
                    Description = process.Description,
                    Priority = priority.Title,
                    PriorityValue = priority.Value,
                    Type = await _typeService.Get(process.TypeId.GetValueOrDefault()),
                    Program = await _programService.Get(process.ProgramId.GetValueOrDefault()),
                    CreatedAt = DateParser.Parse(process.CreatedAt),
                    CompletedAt = DateParser.Parse(process.CreatedAt.Add(process.ExpectedTime)),
                    CompletedAtUnparsed = process.CreatedAt.Add(process.ExpectedTime),
                    ApprovedAt = DateParser.TryParse(process.ApprovedAt),
                    ExpectedTime = process.ExpectedTime,
                    Status = await GetStatus(processId),
                    Hold = hold
                };
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<List<int>> Passports(int processId)
        {
            return await _context.Passports
                .Where(p => p.ProcessId == processId)
                .Select(p => p.Id)
                .ToListAsync();
        }

        public async Task<List<int>> Stages(int processId)
        {
            return await _context.Stages
                .Where(s => s.ProcessId == processId)
                .Select(s => s.Id)
                .ToListAsync();
        }

        public async Task<string> GetStatus(int processId)
        {
            var stageIds = await Stages(processId);
            var stageStatusesNotAwaited = stageIds
                .Select(async id => await _stageService.GetStatus(id));
            var stageStatuses = System.Threading.Tasks.Task
                .WhenAll(stageStatusesNotAwaited)
                .Result.ToList();

            int stopped = await _statusService.Stopped();
            int inRework = await _statusService.InRework();
            int assigned = await _statusService.Assigned();
            
            if (stageStatuses.Any(s => s == stopped))
            {
                return "остановлен";
            }
            else if (stageStatuses.Any(s => s == inRework))
            {
                return "в доработке";
            }
            else if (stageStatuses.All(s => s == assigned))
            {
                return "завершен";
            }
            return "в процессе";
        }

        public Task<List<string>> Statuses()
        {
            var res = new List<string>()
            {
                "остановлен",
                "в доработке",
                "завершен",
                "в процессе"
            };

            return System.Threading.Tasks.Task.FromResult(res);
        }

        public async Task<int> Update(
            int processId, 
            string? title, 
            string? description, 
            int? priorityId,
            int? programId,
            int? typeId, 
            TimeSpan? expectedTime
            )
        {
            try
            {
                var process = await Exist(processId);

                process.Title = title ?? process.Title;
                process.Description = description ?? process.Description;
                process.PriorityId = priorityId ?? process.PriorityId;
                process.ProgramId = programId ?? process.ProgramId;
                process.TypeId = typeId ?? process.TypeId;
                process.ExpectedTime = expectedTime ?? process.ExpectedTime;
                
                await _context.SaveChangesAsync();

                return process.Id;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<List<int>> CopyStages(int oldProcessId, int newProcessId)
        {
            try
            {
                var oldStageIds = await Stages(oldProcessId);
                var oldStagesNotAwaited = oldStageIds
                    .Select(async id => await _stageService.Exist(id));
                var oldStages = System.Threading.Tasks.Task
                    .WhenAll(oldStagesNotAwaited)
                    .Result.ToList();

                var newStageIds = new List<int>();
                var matrix = new Dictionary<int, int>();

                oldStages.ForEach(async stage =>
                {
                    var newStageId = await _stageService
                        .Create(stage.Title,
                                stage.Number,
                                stage.ProcessId,
                                stage.StatusId,
                                null,
                                stage.Mark,
                                stage.Pass
                                );
                    await _stageService.CopyTasks(stage.Id, newStageId);
                    matrix.Add(stage.Id, newStageId);
                    newStageIds.Add(newStageId);
                });

                oldStages.ForEach(async stage =>
                {
                    if (stage.CanCreate != null)
                    {
                        var newCanCreate = new List<int>();
                        stage.CanCreate.ForEach(c =>
                        {
                            newCanCreate.Add(matrix[c]);
                        });
                        await _stageService.Update(
                            stageId: matrix[stage.Id],
                            title: null,
                            signedAt: null,
                            approvedAt: null,
                            statusId: null,
                            signed: null,
                            canCreate: newCanCreate,
                            mark: null,
                            pass: null
                            );
                    }
                });

                var graph = new Dictionary<int, List<int?>>();

                foreach (var item in matrix)
                {
                    var inEdges = await _context.Edges
                        .Where(e => e.Start == item.Key)
                        .Select(e => e.End)
                        .ToListAsync();

                    graph[item.Key] = inEdges;
                }

                foreach (var i in graph)
                {
                    foreach (var j in i.Value)
                    {
                        await _edgeService.Create(i.Key, j.GetValueOrDefault());
                    }
                }

                graph.Clear();

                foreach (var item in matrix)
                {
                    var inDependences = await _context.Dependences
                        .Where(d => d.First == item.Key)
                        .Select(d => d.Second)
                        .ToListAsync();

                    graph[item.Key] = inDependences;
                }

                foreach (var i in graph)
                {
                    foreach (var j in i.Value)
                    {
                        await _dependenceService.Create(i.Key, j.GetValueOrDefault());
                    }
                }

                foreach (var item in matrix)
                {
                    var oldHold = await _authDataClient.FindHold("Stage", item.Key);
                    var newHold = await _authDataClient.FindHold("Stage", item.Value);
                    await _authDataClient.CopyHold(oldHold.Id, newHold.Id);
                }

                return newStageIds;
            }   
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public Task<List<int>> VisibleStages(int processId)
        {
            throw new NotImplementedException();
        }
    }
}
