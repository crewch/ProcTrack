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

        public Task<PassportDto> CreatePassport(CreatePassportDto data, int UserId, int Id)
        {
            // var passport = new Passport 
            // {
            //     Title = data.Title,
            //     CreatedAt = DateTime.Now,
            // };
            throw new NotImplementedException();
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
                        .Where(s => s.Title.ToLower() == "отменен")
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
                    s.Status.Title.ToLower() == "отменен" &&
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
                    .Where(s => s.Title.ToLower() == "отменен")
                    .FirstOrDefault();
            foreach (var stage in stages)
            {
                stage.Status = new_status;
            }
            _context.SaveChanges();

            return await GetProcessById(Id);
        }

        public Task<ProcessDto> UpdateProcess(ProcessDto data, int UserId, int Id)
        {
            throw new NotImplementedException();
        }
    }
}

        //public async Task<ProcessesDataDto> CreateProcessFromTemplate(FromTemplateCreateDto data)
        //{
        //    var template = _context.Processes
        //        .Include(p => p.Stages)
        //        .Include(p => p.HeadStage)
        //        .Include(p => p.TailStage)
        //        .Include(p => p.Type)
        //        .Include(p => p.Passports)
        //        .Where(t => t.Id == data.TemplateId)
        //        .FirstOrDefault();

        //    Console.WriteLine($"Templates searched {template.ToString()}");
        //    if (template == null)
        //    {
        //        return null;
        //    }
        //    var userLoginDto = new UserLoginDto { Email = data.Email };
        //    var user = await _client.GetUser(userLoginDto);
        //    Console.WriteLine("User searched");
        //    var matrix = new List<Tuple<Stage, int>>();

        //    foreach (var st in template.Stages)
        //    {
        //        Console.WriteLine(st.Id);
        //        var tasks = new List<Models.Task>();
        //        foreach (var task in st.Tasks)
        //        {
        //            var newTask = new Models.Task
        //            {
        //                Title = task.Title,
        //                ExpectedTime = task.ExpectedTime,
        //            };
        //            tasks.Add(newTask);
        //            _context.Tasks.Add(newTask);
        //        }
        //        var newStage = new Stage
        //        {
        //            Title = st.Title,
        //            Addenable = st.Addenable,
        //            Status = st.Status,
        //            CustomField = st.CustomField,
        //            Tasks = tasks,
        //        };
        //        matrix.Add(new Tuple<Stage, int> (newStage, st.Id));
        //        _context.Stages.Add(newStage);
        //    }
        //    _context.SaveChanges();

        //    Console.WriteLine("Stages added");

        //    var edges = new HashSet<Edge>();
        //    var dependences = new HashSet<Dependence>();
            
        //    for (int i = 0; i < matrix.Count; i++)
        //    {
        //        var edge = _context.Edges
        //            .Where(e => e.Start == matrix[i].Item2 || e.End == matrix[i].Item2)
        //            .FirstOrDefault();

        //        var dependence = _context.Dependences
        //            .Where(d => d.First == matrix[i].Item2 || d.Second == matrix[i].Item2)
        //            .FirstOrDefault();
        //        if (edge != null)
        //        {
        //            var newEdge = new Edge
        //            {
        //                StartStage = matrix.Find(e => e.Item2 == edge.Start).Item1,
        //                EndStage = matrix.Find(e => e.Item2 == edge.End).Item1,
        //            };
        //            _context.Edges.Add(newEdge);
        //            edges.Add(newEdge);
        //        }

        //        if (dependence != null)
        //        {
        //            var newDependence = new Dependence
        //            {
        //                FirstStage = matrix.Find(d => d.Item2 == dependence.First).Item1,
        //                SecondStage = matrix.Find(d => d.Item2 == dependence.Second).Item1,
        //            };
        //            _context.Dependences.Add(newDependence);
        //            dependences.Add(newDependence);
        //        }
                
        //    }
        //    _context.SaveChanges();

        //    Console.WriteLine("Edges added");

        //    var priority = _context.Priorities
        //        .Where(p => p.Id == data.PriorityId)
        //        .FirstOrDefault();

        //    Console.WriteLine("Priorities searched");
        //    Console.WriteLine(matrix.Count);
        //    for (int i = 0; i < matrix.Count; i++)
        //    {
        //        Console.WriteLine($"{matrix[i].Item1.Id} : {matrix[i].Item2}");
        //    }

        //    var process = new Process
        //    {
        //        Title = data.Title,
        //        Priority = priority,
        //        Head = matrix.Find(d => d.Item2 == template.Head).Item1.Id,
        //        Tail = matrix.Find(d => d.Item2 == template.Tail).Item1.Id,
        //        Type = template.Type,
        //        CreatedAt = DateTime.Now,
        //        ExpectedTime = template.ExpectedTime,
        //        Stages = matrix.Select(m => m.Item1).ToList(),
        //        IsTemplate = false,
        //    };
        //    _context.Processes.Add(process);
        //    _context.SaveChanges();

        //    Console.WriteLine("Process added");

        //    var processId = process.Id;

        //    var createHoldDto = new CreateHoldDto
        //    {
        //        DestId = process.Id,
        //        Type = "Process",
        //        HolderType = "User",
        //        HolderTypeId = user.Id,
        //    };
        //    var holdRes = await _client.CreateHold(createHoldDto);

        //    var createHoldDto2 = new CreateHoldDto
        //    {
        //        DestId = process.Id,
        //        Type = "Process",
        //        HolderType = "Group",
        //        HolderTypeId = data.GroupId,
        //    };
        //    var holdRes2 = await _client.CreateHold(createHoldDto2);


        //    for (int i = 0; i < matrix.Count; i++)
        //    {
        //        var userGroupsDto = new GetUserByHoldDto
        //        {
        //            DestId = matrix[i].Item2,
        //            Type = "Stage"
        //        };
        //        var usersGroups = await _client.GetUsersGroups(userGroupsDto);
        //        foreach (var iUser in usersGroups.Users)
        //        {
        //            var iCreateHoldDto = new CreateHoldDto
        //            {
        //                DestId = matrix[i].Item1.Id,
        //                Type = "Stage",
        //                HolderType = "User",
        //                HolderTypeId = iUser.Id,
        //            };
        //            var iHoldRes = await _client.CreateHold(iCreateHoldDto);
        //        }
        //        foreach (var iGroup in usersGroups.Groups)
        //        {
        //            var iCreateHoldDto = new CreateHoldDto
        //            {
        //                DestId = matrix[i].Item1.Id,
        //                Type = "Stage",
        //                HolderType = "Group",
        //                HolderTypeId = iGroup.Id,
        //            };
        //            var iHoldRes = await _client.CreateHold(iCreateHoldDto);
        //        }
        //    }
        //    Console.WriteLine("Holds Added");
        //    var newDto = new ProcessesDataDto
        //    {
        //        Id = process.Id,
        //        User = holdRes.User,
        //        Group = holdRes2.Group,
        //        Title = process.Title,
        //        IsTemplate = process.IsTemplate,
        //        Priority = process.Priority.Title,
        //        Type = process.Type.Title,
        //        CreatedAt = process.CreatedAt,
        //        Status = "Не начат",
        //        ApprovedAt = process.ApprovedAt,
        //        ExpectedTime = process.ExpectedTime
        //    };

        //    return newDto;
        //}

        //public async Task<List<ProcessesDataDto>> GetProcessData(LoginTypeDto loginType)
        //{
        //    var res = new List<ProcessesDataDto>();
        //    loginType.Type = "Process";
        //    var HoldRightRes = await _client.GetRightsHolds(loginType);
        //    foreach (var hr in HoldRightRes)
        //    {
        //        var process = _context.Processes
        //            .Where(p => p.Id == hr.DestId)
        //            .Include(p => p.Type)
        //            .Include(p => p.Priority)
        //            .Include(p => p.HeadStage)
        //            .Include(p => p.TailStage)
        //            .FirstOrDefault();

        //        string status = "Не начат";
        //        if (process != null && 
        //            process.HeadStage != null && 
        //            process.HeadStage.Status != null &&
        //            process.TailStage != null &&
        //            process.TailStage.Status != null)
        //        {
        //            if (process.HeadStage.Status.Title == "Не начат")
        //            {
        //                status = "Не начат";
        //            } else if (process.HeadStage.Status.Title != "Не начат" &&
        //                       process.TailStage.Status.Title == "Не начат")
        //            {
        //                status = "В процессе";
        //            } else if (process.HeadStage.Status.Title != "Не начат" &&
        //                       process.TailStage.Status.Title == "Не начат")
        //            {
        //                status = "Согласован";
        //            }
        //        }
        //        if (process != null)
        //        {
        //            var dto = new ProcessesDataDto
        //            {
        //                Id = hr.DestId,
        //                Rights = hr.Rights,
        //                User = hr.User,
        //                Group = hr.Group,
        //                Title = process.Title,
        //                IsTemplate = process.IsTemplate,
        //                Priority = process.Priority.Title,
        //                Type = process.Type.Title,
        //                CreatedAt = process.CreatedAt,
        //                Status = status,
        //            };
        //            res.Add(dto);
        //        }
        //    }
        //    return res;
        //}

        //public async Task<List<StagesDataDto>> GetStageData(LoginTypeDto loginType)
        //{
        //    var res = new List<StagesDataDto>();
        //    loginType.Type = "Stage";
        //    var HoldRightRes = await _client.GetRightsHolds(loginType);
        //    foreach (var hr in HoldRightRes)
        //    {
        //        var stage = _context.Stages
        //            .Where(s => s.Id == hr.DestId && s.Status != null)
        //            .Include(s => s.Status)
        //            .FirstOrDefault();
        //        if (stage != null)
        //        {
        //            var dto = new StagesDataDto
        //            {
        //                Id = hr.DestId,
        //                Rights = hr.Rights,
        //                User = hr.User,
        //                Group = hr.Group,
        //                Title = stage.Title,
        //                Addenable = stage.Addenable,
        //                CreatedAt = stage.CreatedAt,
        //                SignedAt = stage.SignedAt,
        //                Status = stage.Status.Title,
        //                Signed = stage.Signed,
        //                CustomField = stage.CustomField,
        //            };
        //            res.Add(dto);
        //        }
        //    }
        //    return res;
        //}

        //public async Task<Message> AddMessage(Message message)
        //{
        //    await _context.Messages.AddAsync(message);
        //    await _context.SaveChangesAsync();
        //    return message;
        //}

        //public async Task<List<Message>> GetMessages()
        //{
        //    return await _context.Messages.ToListAsync();
        //}