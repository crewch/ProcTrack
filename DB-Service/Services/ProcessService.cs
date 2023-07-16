using DB_Service.Clients.Http;
using DB_Service.Data;
using DB_Service.Dtos;
using DB_Service.Models;
using Microsoft.EntityFrameworkCore;
using DB_Service.Tools;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace DB_Service.Services
{
    public class ProcessService : IProcessService
    {
        private readonly DataContext _context;
        private readonly IAuthDataClient _authClient;
        private readonly IFileDataClient _fileClient;
        private readonly IStageService _stageService;
        private readonly ILogService _logService;

        public ProcessService(DataContext context, 
                              IAuthDataClient authClient, 
                              IFileDataClient fileClient, 
                              IStageService stageService,
                              ILogService logService)
        {
            _context = context;
            _authClient = authClient;
            _fileClient = fileClient;
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
            _context.SaveChanges();

            var dto = new PassportDto
            {
                Id = passport.Id,
                Title = passport.Title,
                CreatedAt = passport.CreatedAt == null ? null : DateParser.Parse((DateTime)passport.CreatedAt),
                Message = passport.Message,
                ProcessId = passport.ProcessId,
            };

            return dto;
        }

        public async Task<ProcessDto> CreateProcess(CreateProcessDto data, int UserId)
        {
            // TODO: добавить проверку юзера и группы, на то существуют ли они
            if (data.Process == null)
            {
                return null;
            }

            var template = _context.Processes
                .Include(t => t.Type)
                .Where(t => t.Id == data.TemplateId)
                .FirstOrDefault();

            if (template == null)
            {
                return null;
            }

            var priority = _context.Priorities
                .Where(p => data.Process.Priority == p.Title)
                .FirstOrDefault();

            if (priority == null)
            {
                return null;
            }

            var stages = _context.Stages
                .Where(s => s.ProcessId == template.Id)
                .ToList();

            var defualt_status = _context.Statuses
                .Where(s => s.Title.ToLower() == "не начат")
                .FirstOrDefault();

            var stageMatrix = new List<Tuple<int, Stage>>();
            foreach (var stage in stages)
            {
                var tasks = _context.Tasks
                    .Where(t => t.StageId == stage.Id)
                    .ToList();
                var newTasks = new List<Models.Task>();
                foreach (var task in tasks)
                {
                    var newTask = new Models.Task()
                    {
                        Title = task.Title,
                        ExpectedTime = task.ExpectedTime,
                    };
                    newTasks.Add(newTask);
                    _context.Tasks.Add(newTask);
                }

                var new_status = defualt_status;
                if (stage.Id == template.Head)
                {
                    new_status = _context.Statuses
                        .Where(s => s.Title.ToLower() == "остановлен")
                        .FirstOrDefault();
                }

                var newStage = new Stage()
                {
                    Title = stage.Title,
                    Tasks = newTasks,
                    Addenable = stage.Addenable,
                    Status = new_status,
                    CustomField = stage.CustomField,
                    CreatedAt = DateTime.Now.AddHours(3),
                    Pass = stage.Pass,
                    Mark = stage.Mark,
                };
                stageMatrix.Add(new Tuple<int, Stage>(stage.Id, newStage));
                _context.Stages.Add(newStage);
            }
            _context.SaveChanges();

            foreach (var stage in stages)
            {
                var newCanCreate = new List<int>();
                foreach (var id in stage.CanCreate)
                {
                    newCanCreate.Add(stageMatrix.Find(s => s.Item1 == id).Item2.Id);
                }
                stageMatrix.Find(s => s.Item1 == stage.Id).Item2.CanCreate = newCanCreate;
            }
            _context.SaveChanges();

            var edgeGraph = new Dictionary<int, List<int?>>();

            for (int i = 0; i < stageMatrix.Count; i++)
            {
                var inEdges = _context.Edges
                    .Where(e => e.Start == stageMatrix[i].Item1)
                    .Select(e => e.End)
                    .ToList();

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
                    _context.Edges.Add(newEdge);

                }
            }

            var dependenceGraph = new Dictionary<int, List<int?>>();

            for (int i = 0; i < stageMatrix.Count; i++)
            {
                var inDependences = _context.Dependences
                    .Where(e => e.First == stageMatrix[i].Item1)
                    .Select(e => e.Second)
                    .ToList();

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
                    _context.Dependences.Add(newDependence);

                }
            }

            _context.SaveChanges();

            Console.WriteLine("\n\n\n");
            for (int i = 0; i < stageMatrix.Count; i++)
            {
                Console.WriteLine($"{stageMatrix[i].Item2.Id} : {stageMatrix[i].Item1}");
            }

            var newProcess = new Models.Process
            {
                Title = data.Process.Title,
                PriorityId = priority.Id,
                TypeId = template.TypeId,
                CreatedAt = DateTime.Now.AddHours(3),
                ExpectedTime = template.ExpectedTime,
                IsTemplate = false,
                Head = stageMatrix.Find(d => d.Item1 == template.Head).Item2.Id,
                Tail = stageMatrix.Find(d => d.Item1 == template.Tail).Item2.Id,
                Stages = stageMatrix.Select(s => s.Item2).ToList(),
            };

            _context.Processes.Add(newProcess);
            _context.SaveChanges();

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

            return await GetProcessById(newProcess.Id);
        }

        public async Task<LinkDto> GetLinksByProcessId(int Id)
        {
            var stages = _context.Stages
                .Where(s => s.ProcessId == Id)
                .ToList();

            var edges = new List<Tuple<int, int>>();
            var dependences = new List<Tuple<int, int>>();

            foreach (var stage in stages)
            {
                var inEdges = _context.Edges
                    .Where(e => e.Start == stage.Id)
                    .Select(e => e.End)
                    .ToList();

                foreach (var inEdge in inEdges)
                {
                    edges.Add(new Tuple<int, int>(stage.Id, (int) inEdge));
                }

                var inDependences = _context.Dependences
                    .Where(d => d.First == stage.Id)
                    .Select(d => d.Second)
                    .ToList();

                foreach (var inDependence in inDependences)
                {
                    dependences.Add(new Tuple<int, int>(stage.Id, (int) inDependence));
                }
            }

            var res = new LinkDto
            {
                Edges = edges,
                Dependences = dependences,
            };

            return res;
        }

        public async Task<List<PassportDto>> GetPassports(int Id)
        {
            var passports = _context.Passports
                .Where(p => p.ProcessId == Id)
                .ToList();

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

        public async Task<List<ProcessDto>> GetProcesesByUserId(int UserId)
        {
            var holds = await _authClient.GetHolds(new UserHoldTypeDto{
                Id = UserId,
                HoldType = "Process"
            });

            var used = new HashSet<int>();

            var res = new List<ProcessDto>();

            foreach (var hold in holds) 
            {
                if (used.Any(u => hold.DestId == u))
                {
                    continue;
                }
                used.Add(hold.DestId);
                var processId = _context.Processes
                    .Where(p => p.Id == hold.DestId)
                    .Select(p => p.Id)
                    .FirstOrDefault();
                
                var processDto = await GetProcessById(processId);

                if (processDto != null && !res.Any(r => r.Id == processDto.Id))
                {
                    res.Add(processDto);
                }
            }
            
            return res;
        }

        public async Task<ProcessDto> GetProcessById(int Id)
        {
            var process = _context.Processes
                .Include(p => p.Priority).Include(p => p.Type)
                .Where(p => p.Id == Id)
                .FirstOrDefault();

            if (process == null) {
                return null;
            }
            // status: в процессе, завершен, остановлен, отменен

            var stages = _context.Stages
                .Include(s => s.Status)
                .Where(s => s.ProcessId == process.Id && !(s.Pass ?? false))
                .Select(s => s.Status.Title)
                .ToList();

            string status = "";
            if (stages.Any(s => s.ToLower() == "остановлен"))
            {
                status = "остановлен";
            } 
            else if (stages.Any(s => s.ToLower() == "отменен"))
            {
                status = "отменен";
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
            
            var processDto = new ProcessDto
            {
                Title = process.Title,
                Id = process.Id,
                Priority = process.Priority == null ? null : process.Priority.Title,
                Type = process.Type == null ? null : process.Type.Title,
                CreatedAt = process.CreatedAt == null ? null : DateParser.Parse((DateTime)process.CreatedAt),
                CompletedAt = process.CreatedAt == null ? null : DateParser.Parse((DateTime)process.CreatedAt.Add(process.ExpectedTime)),
                CompletedAtUnparsed = process.CreatedAt.Add(process.ExpectedTime).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                ApprovedAt = process.ApprovedAt == null ? null : DateParser.Parse((DateTime)process.ApprovedAt),
                ExpectedTime = process.ExpectedTime,
                Hold = hold,
                Status = status
            };
            return processDto;
        }

        public async Task<List<StageDto>> GetStagesByProcessId(int id)
        {
            var process = await GetProcessById(id);

            var stageModels = _context.Stages
                .Where(s => s.ProcessId == process.Id && !(s.Pass ?? false))
                .ToList();

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
            var stages = _context.Stages
                .Include(s => s.Status)
                .Where(s => 
                    s.Status.Title.ToLower() == "остановлен" &&
                    s.ProcessId == Id
                )
                .ToList();
            
            var new_status = _context.Statuses
                    .Where(s => s.Title.ToLower() == "отправлен на проверку")
                    .FirstOrDefault();
            foreach (var stage in stages)
            {
                stage.Status = new_status;
            }
            _context.SaveChanges();

            return await GetProcessById(Id);
        }

        public async Task<ProcessDto> StopProcess(int UserId, int Id)
        {
            var stages = _context.Stages
                .Include(s => s.Status)
                .Where(s => 
                    (s.Status.Title.ToLower() == "отправлен на проверку" ||
                    s.Status.Title.ToLower() == "принят на проверку") &&
                    s.ProcessId == Id
                )
                .ToList();

            var new_status = _context.Statuses
                    .Where(s => s.Title.ToLower() == "остановлен")
                    .FirstOrDefault();
            foreach (var stage in stages)
            {
                stage.Status = new_status;
            }
            _context.SaveChanges();

            return await GetProcessById(Id);
        }

        public async Task<ProcessDto> UpdateProcess(ProcessDto data, int UserId, int Id)
        {
            var process = _context.Processes
                .Where(p => p.Id == Id)
                .FirstOrDefault();

            process.Title = data.Title;
            process.Priority = _context.Priorities.Where(p => p.Title == data.Priority).FirstOrDefault();

            _context.SaveChanges();

            var res = await GetProcessById(Id);

            return res;
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
            _context.Processes.Add(process);

            var stageDict = new Dictionary<int, Models.Stage>();
            foreach (var stage in data.Stages)
            {
                var newStage = new Models.Stage
                {
                    Title = stage.Title,
                    Process = process,
                    Addenable = false,
                    CreatedAt = DateTime.Now.AddHours(3),
                    Status = _context.Statuses.Where(t => t.Title.ToLower() == "не начат").FirstOrDefault(),
                    Mark = stage.Mark,
                    Pass = stage.Pass,
                };
                stageDict[stage.Id] = newStage;
                _context.Stages.Add(newStage);
            }
            _context.SaveChanges();
            foreach (var stage in data.Stages)
            {
                var newCanCreate = new List<int>();
                foreach (var id in stage.CanCreate)
                {
                    newCanCreate.Add(stageDict[id].Id);
                }
                stageDict[stage.Id].CanCreate = newCanCreate;
            }
            _context.SaveChanges();

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
                _context.Tasks.Add(newTask);
            }

            var edges = new List<Models.Edge>();
            foreach (var edge in data.Links.Edges)
            {
                var newEdge = new Models.Edge
                {
                    StartStage = stageDict[edge.Item1],
                    EndStage = stageDict[edge.Item2]
                };
                edges.Add(newEdge);
                _context.Edges.Add(newEdge);
            }

            var dependences = new List<Models.Dependence>();
            foreach (var dep in data.Links.Dependences)
            {
                var newDependence = new Models.Dependence
                {
                    FirstStage = stageDict[dep.Item1],
                    SecondStage = stageDict[dep.Item2]
                };
                dependences.Add(newDependence);
                _context.Dependences.Add(newDependence);
            }
            _context.SaveChanges();

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

            return await GetProcessById(process.Id);
        }

        public async Task<ProcessDto> GetProcessByStageId(int StageId)
        {
            var stage = await _stageService.GetStageById(StageId);
            var process = await GetProcessById((int)stage.ProcessId);
            return process;
        }
    }
}
