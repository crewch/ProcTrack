using DB_Service.Clients.Http;
using DB_Service.Data;
using DB_Service.Dtos;
using DB_Service.Models;
using Microsoft.EntityFrameworkCore;
using DB_Service.Tools;

namespace DB_Service.Services
{
    public class ProcessService : IProcessService
    {
        private readonly DataContext _context;
        private readonly IAuthDataClient _authClient;
        private readonly IMailService _mailService;
        private readonly IStageService _stageService;
        private readonly ILogService _logService;

        public ProcessService(DataContext context, 
                              IAuthDataClient authClient,
                              IMailService mailService,
                              IStageService stageService,
                              ILogService logService)
        {
            _context = context;
            _authClient = authClient;
            _mailService = mailService;
            _stageService = stageService;
            _logService = logService;
        }

        public async Task<PassportDto> CreatePassport(CreatePassportDto data, int UserId, int Id)
        {
            var passport = new Passport 
            {
                Title = data.Title,
                CreatedAt = DateTime.Now.AddHours(3),
                ProcessId = Id,
                Message = data.Message,
            };
            
            _context.Passports.Add(passport);
            
            var stages = _context.Stages
                .Include(s => s.Status)
                .Where(s => s.ProcessId == Id && 
                            (s.Status.Title.ToLower() == "согласовано" || 
                             s.Status.Title.ToLower() == "согласовано-блокировано"))
                .ToList();

            foreach (var stage in stages)
            {
                stage.Status = _context.Statuses.Where(s => s.Title.ToLower() == "в доработке").FirstOrDefault();    
            }

            await _context.SaveChangesAsync();

            var user = await _authClient.GetUserById(UserId);
            if (user != null)
            {
                await _logService.AddLog(new Log
                {
                    Table = "Passport",
                    Operation = "Create",
                    LogId = passport.Id.ToString(),
                    New = passport.Title,
                    Author = user.ShortName.ToString(),
                    CreatedAt = DateTime.Now.AddHours(3)
                });
                await _context.SaveChangesAsync();
            }

            return new PassportDto
            {
                Id = passport.Id,
                Title = passport.Title,
                CreatedAt = passport.CreatedAt == null ? null : DateParser.Parse((DateTime)passport.CreatedAt),
                Message = passport.Message,
                ProcessId = passport.ProcessId,
            };
        }

        public async Task<ProcessDto> CreateProcess(CreateProcessDto data, int UserId)
        {
            if (data.Process == null)
            {
                return null;
            }

            var template = await _context.Processes
                .Include(t => t.Type)
                .Where(t => t.Id == data.TemplateId)
                .FirstOrDefaultAsync();

            if (template == null)
            {
                return null;
            }

            var priority = await _context.Priorities
                .Where(p => data.Process.Priority == p.Title)
                .FirstOrDefaultAsync();

            if (priority == null)
            {
                return null;
            }

            var stages = await _context.Stages
                .Where(s => s.ProcessId == template.Id)
                .ToListAsync();

            var defualt_status = await _context.Statuses
                .Where(s => s.Title.ToLower() == "не начат")
                .FirstOrDefaultAsync();

            var stageMatrix = new List<Tuple<int, Stage>>();
            foreach (var stage in stages)
            {
                var tasks = await _context.Tasks
                    .Where(t => t.StageId == stage.Id)
                    .ToListAsync();

                var newTasks = new List<Models.Task>();
                
                foreach (var task in tasks)
                {
                    var newTask = new Models.Task()
                    {
                        Title = task.Title,
                        ExpectedTime = task.ExpectedTime,
                    };
                    newTasks.Add(newTask);
                    await _context.Tasks.AddAsync(newTask);
                }

                var new_status = defualt_status;
                if (stage.Id == template.Head)
                {
                    new_status = await _context.Statuses
                        .Where(s => s.Title.ToLower() == "остановлен")
                        .FirstOrDefaultAsync();
                }

                var newStage = new Stage()
                {
                    Title = stage.Title,
                    Number = stage.Number,
                    Tasks = newTasks,
                    Addenable = stage.Addenable,
                    Status = new_status,
                    CustomField = stage.CustomField,
                    CreatedAt = DateTime.Now.AddHours(3),
                    Pass = stage.Pass,
                    Mark = stage.Mark,
                };

                stageMatrix.Add(new Tuple<int, Stage>(stage.Id, newStage));
                await _context.Stages.AddAsync(newStage);
            }

            await _context.SaveChangesAsync();
            
            foreach (var stage in stages)
            {
                var newCanCreate = new List<int>();

                foreach (var id in stage.CanCreate)
                {
                    newCanCreate.Add(stageMatrix.Find(s => s.Item1 == id).Item2.Id);
                }
                stageMatrix.Find(s => s.Item1 == stage.Id).Item2.CanCreate = newCanCreate;
            }

            await _context.SaveChangesAsync();

            var edgeGraph = new Dictionary<int, List<int?>>();

            for (int i = 0; i < stageMatrix.Count; i++)
            {
                var inEdges = await _context.Edges
                    .Where(e => e.Start == stageMatrix[i].Item1)
                    .Select(e => e.End)
                    .ToListAsync();

                edgeGraph[stageMatrix[i].Item1] = inEdges;
            }

            foreach (var i in edgeGraph)
            {
                foreach(var j in i.Value)
                {
                    var newEdge = new Edge
                    {
                        StartStage = stageMatrix.Find(e => e.Item1 == i.Key).Item2,
                        EndStage = stageMatrix.Find(e => e.Item1 == j).Item2,
                    };
                    await _context.Edges.AddAsync(newEdge);
                }
            }

            var dependenceGraph = new Dictionary<int, List<int?>>();

            for (int i = 0; i < stageMatrix.Count; i++)
            {
                var inDependences = await _context.Dependences
                    .Where(e => e.First == stageMatrix[i].Item1)
                    .Select(e => e.Second)
                    .ToListAsync();

                dependenceGraph[stageMatrix[i].Item1] = inDependences;
            }

            foreach (var i in dependenceGraph)
            {
                foreach(var j in i.Value)
                {
                    var newDependence = new Dependence
                    {
                        FirstStage = stageMatrix.Find(e => e.Item1 == i.Key).Item2,
                        SecondStage = stageMatrix.Find(e => e.Item1 == j).Item2,
                    };
                    await _context.Dependences.AddAsync(newDependence);
                }
            }

            var newProcess = new Models.Process
            {
                Title = data.Process.Title,
                Description = data.Process.Description,
                PriorityId = priority.Id,
                TypeId = template.TypeId,
                CreatedAt = DateTime.Now.AddHours(3),
                ExpectedTime = template.ExpectedTime,
                IsTemplate = false,
                Head = stageMatrix.Find(d => d.Item1 == template.Head).Item2.Id,
                Tail = stageMatrix.Find(d => d.Item1 == template.Tail).Item2.Id,
                Stages = stageMatrix.Select(s => s.Item2).ToList(),
            };

            await _context.Processes.AddAsync(newProcess);
            await _context.SaveChangesAsync();

            var CreateHoldUser = await _authClient.CreateHold(new CreateHoldRequestDto
            {
                DestId = newProcess.Id,
                DestType = "Process",
                HolderId = UserId,
                HolderType = "User"
            });

            var CreateHoldGroup = await _authClient.CreateHold(new CreateHoldRequestDto
            {
                DestId = newProcess.Id,
                DestType = "Process",
                HolderId = (int) data.GroupId,
                HolderType = "Group",
            });

            foreach (var stageTuple in stageMatrix)
            {
                if (stageTuple.Item2.Id == newProcess.HeadStage.Id)
                {
                    var newHold = await _authClient.CreateHold(new CreateHoldRequestDto
                    {
                        DestId = stageTuple.Item2.Id,
                        DestType = "Stage",
                        HolderId = (int) data.GroupId,
                        HolderType = "Group"
                    });
                    continue;
                }

                var templateHold = await _authClient.FindHold(stageTuple.Item1, "Stage");

                foreach (var hold in templateHold)
                {
                    foreach (var group in hold.Groups)
                    {
                        var newHold = await _authClient.CreateHold(new CreateHoldRequestDto
                        {
                            DestId = stageTuple.Item2.Id,
                            DestType = "Stage",
                            HolderId = group.Id,
                            HolderType = "Group"
                        });
                    }

                    foreach (var user in hold.Users)
                    {
                        var newHold = await _authClient.CreateHold(new CreateHoldRequestDto
                        {
                            DestId = stageTuple.Item2.Id,
                            DestType = "Stage",
                            HolderId = user.Id,
                            HolderType = "User"
                        });
                    }

                }
            }

            await _context.SaveChangesAsync();

            var logUser = await _authClient.GetUserById(UserId);
            if (logUser != null)
            {
                await _logService.AddLog(new Log
                {
                    Table = "Process",
                    Operation = "Create",
                    LogId = newProcess.Id.ToString(),
                    New = newProcess.Title,
                    Author = logUser.ShortName.ToString(),
                    CreatedAt = DateTime.Now.AddHours(3)
                });
                await _context.SaveChangesAsync();
            }

            return await GetProcessById(newProcess.Id);
        }

        public async Task<LinkDto> GetLinksByProcessId(int Id)
        {
            var stages = await _context.Stages
                .Where(s => s.ProcessId == Id)
                .ToListAsync();

            var edges = new List<Tuple<int, int>>();
            var dependences = new List<Tuple<int, int>>();

            foreach (var stage in stages)
            {
                var inEdges = await _context.Edges
                    .Where(e => e.Start == stage.Id)
                    .Select(e => e.End)
                    .ToListAsync();

                foreach (var inEdge in inEdges)
                {
                    edges.Add(new Tuple<int, int>(stage.Id, (int) inEdge));
                }

                var inDependences = await _context.Dependences
                    .Where(d => d.First == stage.Id)
                    .Select(d => d.Second)
                    .ToListAsync();

                foreach (var inDependence in inDependences)
                {
                    dependences.Add(new Tuple<int, int>(stage.Id, (int) inDependence));
                }
            }

            return new LinkDto
            {
                Edges = edges,
                Dependences = dependences,
            };
        }

        public async Task<List<PassportDto>> GetPassports(int Id)
        {
            var passports = await _context.Passports
                .Where(p => p.ProcessId == Id)
                .ToListAsync();

            var res = new List<PassportDto>();

            foreach (var passport in passports)
            {
                res.Add(new PassportDto
                {
                    Id = passport.Id,
                    Title = passport.Title,
                    CreatedAt = passport.CreatedAt == null ? null : DateParser.Parse((DateTime)passport.CreatedAt),
                    Message = passport.Message,
                    ProcessId = Id,
                });
            }

            return res;
        }

        public async Task<List<ProcessDto>> GetProcesesByUserId(int UserId, FilterProcessDto filter, int limit, int offset)
        {
            var holds = await _authClient.GetHolds(
                new UserHoldTypeDto
                {
                    Id = UserId,
                    HoldType = "Process"
                }
            );

            var used = new HashSet<int>();

            var res = new List<ProcessDto>();

            foreach (var hold in holds) 
            {
                if (used.Any(u => hold.DestId == u))
                {
                    continue;
                }

                used.Add(hold.DestId);

                var processId = await _context.Processes
                    .Include(p => p.Type)
                    .Include(p => p.Priority)
                    .Where(p => p.Id == hold.DestId &&
                                        (filter.Types == null || filter.Types.Count == 0 || filter.Types.Contains(p.Type.Title)) &&
                                        (filter.Priorities == null || filter.Priorities.Count == 0 || filter.Priorities.Contains(p.Priority.Title)) &&
                                        (filter.Text == null || filter.Text.Length == 0 || (p.Title + p.Description).Contains(filter.Text)))
                    .Select(p => p.Id)
                    .FirstOrDefaultAsync();
                
                var processDto = await GetProcessById(processId);

                if (processDto != null && 
                    !res.Any(r => r.Id == processDto.Id) && 
                    ((filter.Statuses == null || filter.Statuses.Count == 0)
                    || filter.Statuses.Contains(processDto.Status)) && 
                    !(processDto.Status.ToLower() == "завершен" && !filter.ShowCompleted))
                {
                    res.Add(processDto);
                }
            }

            res.Sort((x, y) =>
            {
                int cmp = x.PriorityValue == y.PriorityValue
                    ?
                        0
                    : 
                        x.PriorityValue > y.PriorityValue ? 1 : -1;
                if (cmp != 0)
                {
                    return cmp;
                }
                return x.CompletedAtUnparsed > y.CompletedAtUnparsed ? -1 : 1;
            });
            
            return res.Skip(Math.Min(offset * limit, res.Count - 1))
                      .Take(Math.Min(limit, res.Count - offset))
                      .ToList();
        }

        public async Task<ProcessDto> GetProcessById(int Id)
        {
            var process = await _context.Processes
                .Include(p => p.Priority).Include(p => p.Type)
                .Where(p => p.Id == Id)
                .FirstOrDefaultAsync();

            if (process == null)
            {
                return null;
            }
            var stages = await _context.Stages
                .Include(s => s.Status)
                .Where(s => s.ProcessId == process.Id && !(s.Pass ?? false))
                .Select(s => s.Status.Title)
                .ToListAsync();

            string status = "";
            if (stages.Any(s => s.ToLower() == "остановлен"))
            {
                status = "остановлен";
            } 
            else if (stages.Any(s => s.ToLower() == "в доработке"))
            {
                status = "в доработке";
            }
            else if (stages.All(s => s.ToLower() == "согласовано"))
            {
                status = "завершен";
            }
            else
            {
                status = "в процессе";
            }
            
            var hold = await _authClient.FindHold(process.Id, "Process");
            
            return new ProcessDto
            {
                Id = process.Id,
                Title = process.Title,
                Description = process.Description,
                Priority = process.Priority == null ? null : process.Priority.Title,
                PriorityValue = process.Priority == null ? null : process.Priority.Value,
                Type = process.Type == null ? null : process.Type.Title,
                CreatedAt = process.CreatedAt == null ? null : DateParser.Parse((DateTime)process.CreatedAt),
                CompletedAt = process.CreatedAt == null ? null : DateParser.Parse((DateTime)process.CreatedAt.Add(process.ExpectedTime)),
                CompletedAtUnparsed = process.CreatedAt.Add(process.ExpectedTime),
                ApprovedAt = process.ApprovedAt == null ? null : DateParser.Parse((DateTime)process.ApprovedAt),
                ExpectedTime = process.ExpectedTime,
                Hold = hold,
                Status = status
            };
        }

        public async Task<List<StageDto>> GetStagesByProcessId(int id)
        {
            var process = await GetProcessById(id);

            var stageModels = await _context.Stages
                .Where(s => s.ProcessId == process.Id && !(s.Pass ?? false))
                .OrderBy(s => s.Number)
                .ToListAsync();

            var stages = new List<StageDto>();

            foreach (var stage in stageModels)
            {
                var dto = await _stageService.GetStageById(stage.Id);
                stages.Add(dto);
            }

            return stages;
        }

        public async Task<ProcessDto> StartProcess(int UserId, int Id)
        {
            var processForNotification = await _context.Processes
                .Where(p => p.Id == Id)
                .FirstOrDefaultAsync();

            var stages = await _context.Stages
                .Include(s => s.Status)
                .Where(s => s.Status.Title.ToLower() == "остановлен" && s.ProcessId == Id)
                .ToListAsync();
            
            var new_status = await _context.Statuses
                    .Where(s => s.Title.ToLower() == "отправлен на проверку")
                    .FirstOrDefaultAsync();

            foreach (var stage in stages)
            {
                stage.Status = new_status;

                var holdsForNotificate = await _authClient.FindHold(Id, "Stage");
                
                if (holdsForNotificate.Count < 1)
                {
                    continue;
                }

                foreach (var group in holdsForNotificate[0]?.Groups)
                {
                    var NotificatedUsers = await _authClient.GetUsersByGroupId(group.Id);
                    foreach (var user in NotificatedUsers)
                    {
                        _mailService.SendProcessMailToChecker(processForNotification, user, group, stage);
                    }
                }

                var notificatedReleaserHolds = await _authClient.FindHold(processForNotification.Id, "Process");
                var notificatedReleaser = notificatedReleaserHolds[0]?.Users[0];

                _mailService.SendProcessMailToReleaser(processForNotification, stage, notificatedReleaser);
            }

            await _context.SaveChangesAsync();

            var logUser = await _authClient.GetUserById(UserId);
            if (logUser != null)
            {
                await _logService.AddLog(new Log
                {
                    Table = "Process",
                    Field = "Status",
                    Operation = "Update",
                    LogId = Id.ToString(),
                    Old = "Остановлен",
                    New = "Отправлен на проверку",
                    Author = logUser.ShortName.ToString(),
                    CreatedAt = DateTime.Now.AddHours(3)
                });
                await _context.SaveChangesAsync();
            }

            return await GetProcessById(Id);
        }

        public async Task<ProcessDto> StopProcess(int UserId, int Id)
        {
            var processForNotification = await _context.Processes
                .Where(p => p.Id == Id)
                .FirstOrDefaultAsync();

            var stages = await _context.Stages
                .Include(s => s.Status)
                .Where(s => 
                    (s.Status.Title.ToLower() == "отправлен на проверку" ||
                    s.Status.Title.ToLower() == "принят на проверку") &&
                    s.ProcessId == Id
                )
                .ToListAsync();

            string oldStatus = stages[0].Title;

            var newStatus = await _context.Statuses
                    .Where(s => s.Title.ToLower() == "остановлен")
                    .FirstOrDefaultAsync();
            
            foreach (var stage in stages)
            {
                stage.Status = newStatus;

                var notificatedReleaserHolds = await _authClient.FindHold(processForNotification.Id, "Process");
                var notificatedReleaser = notificatedReleaserHolds[0]?.Users[0];

                _mailService.SendProcessMailToReleaser(processForNotification, stage, notificatedReleaser);
            }

            await _context.SaveChangesAsync();

            var logUser = await _authClient.GetUserById(UserId);
            if (logUser != null)
            {
                await _logService.AddLog(new Log
                {
                    Table = "Process",
                    Field = "Status",
                    Operation = "Update",
                    LogId = Id.ToString(),
                    Old = oldStatus,
                    New = "Остановлен",
                    Author = logUser.ShortName.ToString(),
                    CreatedAt = DateTime.Now.AddHours(3)
                });
                await _context.SaveChangesAsync();
            }

            return await GetProcessById(Id);
        }

        public async Task<ProcessDto> UpdateProcess(ProcessDto data, int UserId, int Id)
        {
            var process = await _context.Processes
                .Where(p => p.Id == Id)
                .FirstOrDefaultAsync();

            string oldTitle = process.Title;
            string oldDescription = process.Description;
            string oldPriority = process?.Priority?.Title;

            process.Title = data.Title;
            process.Description = data.Description;
            process.Priority = _context.Priorities.Where(p => p.Title == data.Priority).FirstOrDefault();

            await _context.SaveChangesAsync();
            
            var logUser = await _authClient.GetUserById(UserId);
            if (logUser != null)
            {
                await _logService.AddLog(new Log
                {
                    Table = "Process",
                    Field = "Title, Description, Priority",
                    Operation = "Update",
                    LogId = process.Id.ToString(),
                    Old = $"{oldTitle}, {oldDescription}, {oldPriority}",
                    New = $"{process.Title}, {process.Description}, {process.Priority}",
                    Author = logUser.ShortName.ToString(),
                    CreatedAt = DateTime.Now.AddHours(3)
                });
                await _context.SaveChangesAsync();
            }

            return await GetProcessById(Id);
        }

        public async Task<ProcessDto?> CreateTemplate(TemplateDto data)
        {
            var process = new Models.Process
            {
                Title = data.Process.Title,
                IsTemplate = true,
                Priority = _context.Priorities.Where(p => p.Title == data.Process.Priority).FirstOrDefault(),
                Type = _context.Types.Where(t => t.Title == data.Process.Type).FirstOrDefault(),
                ExpectedTime = (System.TimeSpan) data.Process.ExpectedTime,
                CreatedAt = DateTime.Now.AddHours(3)
            };

            await _context.Processes.AddAsync(process);

            var stageDict = new Dictionary<int, Stage>();
            int number = 0;
            foreach (var stage in data.Stages)
            {
                var newStage = new Stage
                {
                    Title = stage.Title,
                    Number = number,
                    Process = process,
                    Addenable = false,
                    CreatedAt = DateTime.Now.AddHours(3),
                    Status = _context.Statuses.Where(t => t.Title.ToLower() == "не начат").FirstOrDefault(),
                    Mark = stage.Mark,
                    Pass = stage.Pass,
                };
                number++;
                stageDict[stage.Id] = newStage;
                await _context.Stages.AddAsync(newStage);
            }

            await _context.SaveChangesAsync();

            foreach (var stage in data.Stages)
            {
                var newCanCreate = new List<int>();
                foreach (var id in stage.CanCreate)
                {
                    newCanCreate.Add(stageDict[id].Id);
                }
                stageDict[stage.Id].CanCreate = newCanCreate;
            }

            await _context.SaveChangesAsync();

            process.Head = stageDict[(int)data.StartStage].Id;
            process.Tail = stageDict[(int)data.EndStage].Id;

            var tasks = new List<Models.Task>();
            foreach (var task in data.Tasks)
            {
                var newTask = new Models.Task
                {
                    Title = task.Title,
                    ExpectedTime = (System.TimeSpan) task.ExpectedTime,
                    Stage = stageDict[task.StageId],
                };
                tasks.Add(newTask);
                await _context.Tasks.AddAsync(newTask);
            }

            var edges = new List<Models.Edge>();
            foreach (var edge in data.Links.Edges)
            {
                var newEdge = new Edge
                {
                    StartStage = stageDict[edge.Item1],
                    EndStage = stageDict[edge.Item2]
                };
                edges.Add(newEdge);
                await _context.Edges.AddAsync(newEdge);
            }

            var dependences = new List<Dependence>();
            foreach (var dep in data.Links.Dependences)
            {
                var newDependence = new Dependence
                {
                    FirstStage = stageDict[dep.Item1],
                    SecondStage = stageDict[dep.Item2]
                };
                dependences.Add(newDependence);
                await _context.Dependences.AddAsync(newDependence);
            }

            await _context.SaveChangesAsync();

            foreach (var stage in data.Stages)
            {
                foreach (var hold in stage.Holds)
                {
                    foreach (var group in hold.Groups)
                    {
                        await _authClient.CreateHold(new CreateHoldRequestDto
                        {
                            DestId = stageDict[stage.Id].Id,
                            DestType = "Stage",
                            HolderId = group.Id,
                            HolderType = "Group"
                        });
                    }
                    foreach (var user in hold.Users)
                    {
                        await _authClient.CreateHold(new CreateHoldRequestDto
                        {
                            DestId = stageDict[stage.Id].Id,
                            DestType = "Stage",
                            HolderId = user.Id,
                            HolderType = "User"
                        });
                    }
                }
            }

            await _context.SaveChangesAsync();

            return await GetProcessById(process.Id);
        }

        public async Task<ProcessDto> GetProcessByStageId(int StageId)
        {
            var stage = await _stageService.GetStageById(StageId);
            
            return await GetProcessById((int)stage.ProcessId);
        }

        public async Task<int> GetProcessCount(int UserId, FilterProcessDto filter)
        {
            var res = await GetProcesesByUserId(UserId, filter, int.MaxValue, 0);

            return res.Count;
        }
    }
}
