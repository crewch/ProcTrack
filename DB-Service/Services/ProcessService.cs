using DB_Service.Clients.Http;
using DB_Service.Data;
using DB_Service.Dtos;
using DB_Service.Models;
using Microsoft.EntityFrameworkCore;

namespace DB_Service.Services
{
    public class ProcessService : IProcessService
    {
        private readonly DataContext _context;
        private readonly IAuthDataClient _authClient;
        private readonly IFileDataClient _fileClient;
        private readonly IStageService _stageService;

        public ProcessService(DataContext context, IAuthDataClient authClient, IFileDataClient fileClient, IStageService stageService)
        {
            _context = context;
            _authClient = authClient;
            _fileClient = fileClient;
            _stageService = stageService;
        }

        public async Task<PassportDto> CreatePassport(CreatePassportDto data, int UserId, int Id)
        {
            var passport = new Passport 
            {
                Title = data.Title,
                CreatedAt = DateTime.Now,
                ProcessId = Id,
                Message = data.Message,
            };
            
            _context.Passports.Add(passport);
            _context.SaveChanges();

            var dto = new PassportDto
            {
                Id = passport.Id,
                Title = passport.Title,
                CreatedAt = passport.CreatedAt,
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
                    CreatedAt = DateTime.Now,
                };
                stageMatrix.Add(new Tuple<int, Stage>(stage.Id, newStage));
                _context.Stages.Add(newStage);
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
                CreatedAt = DateTime.Now,
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
                    CreatedAt = passport.CreatedAt,
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

            var res = new List<ProcessDto>();

            foreach (var hold in holds) 
            {
                var processId = _context.Processes
                    .Where(p => p.Id == hold.DestId)
                    .Select(p => p.Id)
                    .FirstOrDefault();
                
                var processDto = await GetProcessById(processId);

                if (processDto != null)
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
            
            var hold = await _authClient.FindHold(process.Id, "Process");
            
            var processDto = new ProcessDto
            {
                Title = process.Title,
                Id = process.Id,
                Priority = process.Priority == null ? null : process.Priority.Title,
                Type = process.Type == null ? null : process.Type.Title,
                CreatedAt = process.CreatedAt,
                ApprovedAt = process.ApprovedAt,
                ExpectedTime = process.ExpectedTime,
                Hold = hold,
            };
            return processDto;
        }

        public async Task<List<StageDto>> GetStagesByProcessId(int id)
        {
            var process = GetProcessById(id).Result;

            var stageModels = _context.Stages
                .Where(s => s.ProcessId == process.Id)
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
    }
}
