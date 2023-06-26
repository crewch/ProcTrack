using DB_Service.Clients.Http;
using DB_Service.Data;
using DB_Service.Dtos;
using DB_Service.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DB_Service.Services
{
    public class DataService : IDataService
    {
        private readonly DataContext _context;
        private readonly IAuthDataClient _client;

        public DataService(DataContext context, IAuthDataClient client)
        {
            _context = context;
            _client = client;
        }

        public async Task<ProcessesDataDto> CreateProcessFromTemplate(FromTemplateCreateDto data)
        {
            var template = _context.Processes
                .Include(p => p.Stages)
                .Include(p => p.HeadStage)
                .Include(p => p.TailStage)
                .Include(p => p.Type)
                .Include(p => p.Passports)
                .Where(t => t.Id == data.TemplateId)
                .FirstOrDefault();

            Console.WriteLine($"Templates searched {template.ToString()}");
            if (template == null)
            {
                return null;
            }
            var userLoginDto = new UserLoginDto { Email = data.Email };
            var user = await _client.GetUser(userLoginDto);
            Console.WriteLine("User searched");
            var matrix = new List<Tuple<Stage, int>>();

            foreach (var st in template.Stages)
            {
                Console.WriteLine(st.Id);
                var tasks = new List<Models.Task>();
                foreach (var task in st.Tasks)
                {
                    var newTask = new Models.Task
                    {
                        Title = task.Title,
                        ExpectedTime = task.ExpectedTime,
                    };
                    tasks.Add(newTask);
                    _context.Tasks.Add(newTask);
                }
                var newStage = new Stage
                {
                    Title = st.Title,
                    Addenable = st.Addenable,
                    Status = st.Status,
                    CustomField = st.CustomField,
                    Tasks = tasks,
                };
                matrix.Add(new Tuple<Stage, int> (newStage, st.Id));
                _context.Stages.Add(newStage);
            }
            _context.SaveChanges();

            Console.WriteLine("Stages added");

            var edges = new HashSet<Edge>();
            var dependences = new HashSet<Dependence>();
            
            for (int i = 0; i < matrix.Count; i++)
            {
                var edge = _context.Edges
                    .Where(e => e.Start == matrix[i].Item2 || e.End == matrix[i].Item2)
                    .FirstOrDefault();

                var dependence = _context.Dependences
                    .Where(d => d.First == matrix[i].Item2 || d.Second == matrix[i].Item2)
                    .FirstOrDefault();
                if (edge != null)
                {
                    var newEdge = new Edge
                    {
                        StartStage = matrix.Find(e => e.Item2 == edge.Start).Item1,
                        EndStage = matrix.Find(e => e.Item2 == edge.End).Item1,
                    };
                    _context.Edges.Add(newEdge);
                    edges.Add(newEdge);
                }

                if (dependence != null)
                {
                    var newDependence = new Dependence
                    {
                        FirstStage = matrix.Find(d => d.Item2 == dependence.First).Item1,
                        SecondStage = matrix.Find(d => d.Item2 == dependence.Second).Item1,
                    };
                    _context.Dependences.Add(newDependence);
                    dependences.Add(newDependence);
                }
                
            }
            _context.SaveChanges();

            Console.WriteLine("Edges added");

            var priority = _context.Priorities
                .Where(p => p.Id == data.PriorityId)
                .FirstOrDefault();

            Console.WriteLine("Priorities searched");
            Console.WriteLine(matrix.Count);
            for (int i = 0; i < matrix.Count; i++)
            {
                Console.WriteLine($"{matrix[i].Item1.Id} : {matrix[i].Item2}");
            }

            var process = new Process
            {
                Title = data.Title,
                Priority = priority,
                Head = matrix.Find(d => d.Item2 == template.Head).Item1.Id,
                Tail = matrix.Find(d => d.Item2 == template.Tail).Item1.Id,
                Type = template.Type,
                CreatedAt = DateTime.Now,
                ExpectedTime = template.ExpectedTime,
                Stages = matrix.Select(m => m.Item1).ToList(),
                IsTemplate = false,
            };
            _context.Processes.Add(process);
            _context.SaveChanges();

            Console.WriteLine("Process added");

            var processId = process.Id;

            var createHoldDto = new CreateHoldDto
            {
                DestId = process.Id,
                Type = "Process",
                HolderType = "User",
                HolderTypeId = user.Id,
            };
            var holdRes = await _client.CreateHold(createHoldDto);

            var createHoldDto2 = new CreateHoldDto
            {
                DestId = process.Id,
                Type = "Process",
                HolderType = "Group",
                HolderTypeId = data.GroupId,
            };
            var holdRes2 = await _client.CreateHold(createHoldDto);

            for (int i = 0; i < matrix.Count; i++)
            {
                var userGroupsDto = new GetUserByHoldDto
                {
                    DestId = matrix[i].Item2,
                    Type = "Stage"
                };
                var usersGroups = await _client.GetUsersGroups(userGroupsDto);
                foreach (var iUser in usersGroups.Users)
                {
                    var iCreateHoldDto = new CreateHoldDto
                    {
                        DestId = matrix[i].Item1.Id,
                        Type = "Stage",
                        HolderType = "User",
                        HolderTypeId = iUser.Id,
                    };
                    var iHoldRes = await _client.CreateHold(iCreateHoldDto);
                }
                foreach (var iGroup in usersGroups.Groups)
                {
                    var iCreateHoldDto = new CreateHoldDto
                    {
                        DestId = matrix[i].Item1.Id,
                        Type = "Stage",
                        HolderType = "Group",
                        HolderTypeId = iGroup.Id,
                    };
                    var iHoldRes = await _client.CreateHold(iCreateHoldDto);
                }
            }
            Console.WriteLine("Holds Added");
            var newDto = new ProcessesDataDto
            {
                Id = process.Id,
                User = holdRes.User,
                Group = holdRes2.Group,
                Title = process.Title,
                IsTemplate = process.IsTemplate,
                Priority = process.Priority.Title,
                Type = process.Type.Title,
                CreatedAt = process.CreatedAt,
                Status = "Не начат",
                ApprovedAt = process.ApprovedAt,
                ExpectedTime = process.ExpectedTime
            };

            return newDto;
        }

        public async Task<List<ProcessesDataDto>> GetProcessData(LoginTypeDto loginType)
        {
            var res = new List<ProcessesDataDto>();
            loginType.Type = "Process";
            var HoldRightRes = await _client.GetRightsHolds(loginType);
            foreach (var hr in HoldRightRes)
            {
                var process = _context.Processes
                    .Where(p => p.Id == hr.HoldId)
                    .Include(p => p.Type)
                    .Include(p => p.Priority)
                    .Include(p => p.HeadStage)
                    .Include(p => p.TailStage)
                    .FirstOrDefault();

                string status = "Не начат";
                if (process != null && 
                    process.HeadStage != null && 
                    process.HeadStage.Status != null &&
                    process.TailStage != null &&
                    process.TailStage.Status != null)
                {
                    if (process.HeadStage.Status.Title == "Не начат")
                    {
                        status = "Не начат";
                    } else if (process.HeadStage.Status.Title != "Не начат" &&
                               process.TailStage.Status.Title == "Не начат")
                    {
                        status = "В процессе";
                    } else if (process.HeadStage.Status.Title != "Не начат" &&
                               process.TailStage.Status.Title == "Не начат")
                    {
                        status = "Согласован";
                    }
                }
                if (process != null)
                {
                    var dto = new ProcessesDataDto
                    {
                        Id = hr.HoldId,
                        Rights = hr.Rights,
                        User = hr.User,
                        Group = hr.Group,
                        Title = process.Title,
                        IsTemplate = process.IsTemplate,
                        Priority = process.Priority.Title,
                        Type = process.Type.Title,
                        CreatedAt = process.CreatedAt,
                        Status = status,
                    };
                    res.Add(dto);
                }
            }
            return res;
        }

        public async Task<List<StagesDataDto>> GetStageData(LoginTypeDto loginType)
        {
            var res = new List<StagesDataDto>();
            loginType.Type = "Stage";
            var HoldRightRes = await _client.GetRightsHolds(loginType);
            foreach (var hr in HoldRightRes)
            {
                var stage = _context.Stages
                    .Where(s => s.Id == hr.HoldId && s.Status != null)
                    .Include(s => s.Status)
                    .FirstOrDefault();
                if (stage != null)
                {
                    var dto = new StagesDataDto
                    {
                        Id = hr.HoldId,
                        Rights = hr.Rights,
                        User = hr.User,
                        Group = hr.Group,
                        Title = stage.Title,
                        Addenable = stage.Addenable,
                        CreatedAt = stage.CreatedAt,
                        SignedAt = stage.SignedAt,
                        Status = stage.Status.Title,
                        Signed = stage.Signed,
                        CustomField = stage.CustomField,
                    };
                    res.Add(dto);
                }
            }
            return res;
        }

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


    }
}
