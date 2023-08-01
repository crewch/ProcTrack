using DB_Service.Clients.Http;
using DB_Service.Data;
using DB_Service.Dtos;
using Microsoft.EntityFrameworkCore;
using DB_Service.Services;
using DB_Service.Tools;
using DB_Service.Models;

namespace DB_Service.Services
{
    public class StageService : IStageService
    {
        private readonly DataContext _context;
        private readonly IAuthDataClient _authClient;
        private readonly IMailDataClient _mailClient;
        private readonly ITaskService _taskService;
        private readonly ILogService _logService;

        public StageService(DataContext context, 
                            IAuthDataClient authClient,
                            IMailDataClient mailClient,
                            ITaskService taskService,
                            ILogService logService)
        {
            _context = context;
            _authClient = authClient;
            _mailClient = mailClient;
            _taskService = taskService;
            _logService = logService;
        }

        public async Task<StageDto> CancelStageById(int Id, int UserId)
        {
            var stage = await _context.Stages
                .Include(s => s.Status)
                .Where(s => s.Id == Id)
                .FirstOrDefaultAsync();

            if (stage == null)
            {
                return null;
            }

            string oldStatus = stage.Status?.Title;
            var oldSigned = stage.Signed;
            var oldSignedAt = stage.SignedAt;
            var oldSignId = stage.SignId;

            stage.Signed = null;
            stage.SignedAt = null;
            stage.SignId = null;

            stage.Status = await _context.Statuses
                .Where(s => s.Title.ToLower() == "отменен")
                .FirstOrDefaultAsync();

            await _context.SaveChangesAsync();

            var logUser = await _authClient.GetUserById(UserId);
            if (logUser != null)
            {
                await _logService.AddLog(new Log
                {
                    Table = "Stage",
                    Field = "Status, Signed, SignedAt, SignId",
                    Operation = "Update",
                    LogId = Id.ToString(),
                    Old = $"{oldStatus}, {oldSigned}, {oldSignedAt}, {oldSignId}",
                    New = "Отменен, null, null, null",
                    Author = logUser.ShortName.ToString(),
                    CreatedAt = DateTime.Now.AddHours(3)
                });
                await _context.SaveChangesAsync();
            }

            var processForNotification = await _context.Processes
                .Where(p => p.Id == stage.ProcessId)
                .FirstOrDefaultAsync();

            var notificatedReleaserHolds = await _authClient.FindHold(processForNotification.Id, "Process");
            var notificatedReleaser = notificatedReleaserHolds[0]?.Users[0];

            System.Threading.Tasks.Task.Run(async () => await _mailClient.SendMail(new MailDto
            {
                To = notificatedReleaser.Email,
                Body = $"Уважаемый(ая) {notificatedReleaser.LongName.Split(' ').ToList()[1]} " +
                       $"{notificatedReleaser.LongName.Split(' ').ToList()[2]},<br><br>" +
                       $"Процесс согласования КД \"{processForNotification.Title}\", находящийся на этапе согласования " +
                       $"\"{stage.Title}\" {stage.Status.Title} <br><br>" +
                       $"ProcTrack, Система отслеживания процессов согласования, <br>" +
                       $"{DateParser.Parse(DateTime.Now.AddHours(3))}",
                Subject = $"Процесс согласования КД {processForNotification.Title}"
            }));

            return await GetStageById(Id);
        }

        public async Task<StageDto> AssignStage(int UserId, int Id)
        {
            var logUser = await _authClient.GetUserById(UserId);

            var stage = await _context.Stages
                .Include(s => s.Status)
                .Where(s => s.Id == Id)
                .FirstOrDefaultAsync();

            if (stage == null)
            {
                return null;
            }

            var processForNotification = await _context.Processes
                .Where(p => p.Id == stage.ProcessId)
                .FirstOrDefaultAsync();

            var oldStatus = stage.Status?.Title;

            if (stage.Pass ?? false)
            {
                var nextStagesPass = await _context.Edges
                    .Include(e => e.EndStage.Status)
                    .Where(e => e.Start == stage.Id && e.EndStage.Status.Title.ToLower() == "не начат")
                    .Select(e => e.EndStage)
                    .ToListAsync();

                var newStatusPass = await _context.Statuses
                    .Where(s => s.Title.ToLower() == "отправлен на проверку")
                    .FirstOrDefaultAsync();
                
                foreach (var next in nextStagesPass)
                {
                    if (next.Pass == null ? false : (bool) next.Pass)
                    {
                        await AssignStage(UserId, next.Id);
                        continue;
                    }
                    next.Status = newStatusPass;

                    var holdsForNotificate = await _authClient.FindHold(next.Id, "Stage");
                    
                    if (holdsForNotificate.Count < 1)
                    {
                        continue;
                    }

                    foreach (var group in holdsForNotificate[0]?.Groups)
                    {
                        var NotificatedUsers = await _authClient.GetUsersByGroupId(group.Id);
                        foreach (var user in NotificatedUsers)
                        {
                            System.Threading.Tasks.Task.Run(async () => await _mailClient.SendMail(new MailDto
                            {
                                To = user.Email,
                                Body = $"Уважаемый(ая) {user.LongName.Split(' ').ToList()[1]} {user.LongName.Split(' ').ToList()[2]},<br><br>" +
                                       $"Процесс согласования КД \"{processForNotification.Title}\", находящийся на этапе согласования " +
                                       $"\"{stage.Title}\" <br> отправлен на проверку в Ваше подразделение \"{group.Title}\" <br><br>" +
                                       $"ProcTrack, Система отслеживания процессов согласования, <br>" +
                                       $"{DateParser.Parse(DateTime.Now.AddHours(3))}",
                                Subject = $"Процесс согласования КД {processForNotification.Title}"
                            }));
                        }
                    }

                    var notificatedReleaserHolds = await _authClient.FindHold(processForNotification.Id, "Process");
                    var notificatedReleaser = notificatedReleaserHolds[0]?.Users[0];

                    System.Threading.Tasks.Task.Run(async () => await _mailClient.SendMail(new MailDto
                    {
                        To = notificatedReleaser.Email,
                        Body = $"Уважаемый(ая) {notificatedReleaser.LongName.Split(' ').ToList()[1]} " +
                               $"{notificatedReleaser.LongName.Split(' ').ToList()[2]},<br><br>" +
                               $"Процесс согласования КД \"{processForNotification.Title}\", находящийся на этапе согласования " +
                               $"\"{stage.Title}\" {stage.Status.Title} <br><br>" +
                               $"ProcTrack, Система отслеживания процессов согласования, <br>" +
                               $"{DateParser.Parse(DateTime.Now.AddHours(3))}",
                        Subject = $"Процесс согласования КД {processForNotification.Title}"
                    }));
                }

                await _context.SaveChangesAsync();

                return await GetStageById(Id);
            }

            bool blockStage = stage.Status.Title.ToLower() == "отменено";

            stage.Signed = UserId.ToString();
            stage.SignedAt = DateTime.Now.AddHours(3);
            
            var canselledStages = await _context.Stages
                .Include(s => s.Status)
                .Where(s => s.ProcessId == stage.ProcessId && 
                        s.Id != stage.Id && (
                        s.Status.Title.ToLower() == "отменено" ||
                        s.Status.Title.ToLower() == "остановлен"
                    ))
                .Select(s => s.Status.Title)
                .ToListAsync();

            if (canselledStages.Count() > 0)
            {
                stage.Status = await _context.Statuses
                    .Where(s => s.Title.ToLower() == "согласовано-блокировано")
                    .FirstOrDefaultAsync();

                await _context.SaveChangesAsync();

                if (logUser != null)
                {
                    await _logService.AddLog(new Log
                    {
                        Table = "Stage",
                        Field = "Status",
                        Operation = "Update",
                        LogId = Id.ToString(),
                        Old = $"{oldStatus}",
                        New = "Cогласовано-блокировано",
                        Author = logUser.ShortName.ToString(),
                        CreatedAt = DateTime.Now.AddHours(3)
                    });
                    await _context.SaveChangesAsync();
                }

                return await GetStageById(Id);
            }

            var dependences = await _context.Dependences
                .Include(d => d.SecondStage.Status)
                .Where(d => d.First == stage.Id)
                .Select(d => d.SecondStage)
                .ToListAsync();
            
            if (dependences.All(d => 
                    d.Status.Title.ToLower() == "согласовано-блокировано" ||
                    d.Status.Title.ToLower() == "согласовано") || 
                dependences == null)
            {
                stage.Status = await _context.Statuses
                    .Where(s => s.Title.ToLower() == "согласовано")
                    .FirstOrDefaultAsync();
                
                await _context.SaveChangesAsync();
            } else {
                stage.Status = await _context.Statuses
                    .Where(s => s.Title.ToLower() == "согласовано-блокировано")
                    .FirstOrDefaultAsync();

                await _context.SaveChangesAsync();

                if (logUser != null)
                {
                    await _logService.AddLog(new Log
                    {
                        Table = "Stage",
                        Field = "Status",
                        Operation = "Update",
                        LogId = Id.ToString(),
                        Old = $"{oldStatus}",
                        New = "Cогласовано-блокировано",
                        Author = logUser.ShortName.ToString(),
                        CreatedAt = DateTime.Now.AddHours(3)
                    });
                    await _context.SaveChangesAsync();
                }

                return await GetStageById(Id);
            }
            
            var dependent = await _context.Dependences
                .Include(d => d.FirstStage.Status)
                .Where(d => d.Second == stage.Id && 
                        d.FirstStage.Status.Title.ToLower() == "согласовано-блокировано")
                .Select(d => d.FirstStage)
                .ToListAsync();
            
            foreach (var depStage in dependent)
            {
                await AssignStage(UserId, depStage.Id);
            }

            var nextStages = await _context.Edges
                .Include(e => e.EndStage.Status)
                .Where(e => e.Start == stage.Id && e.EndStage.Status.Title.ToLower() == "не начат")
                .Select(e => e.EndStage)
                .ToListAsync();

            var newStatus = await _context.Statuses
                .Where(s => s.Title.ToLower() == "отправлен на проверку")
                .FirstOrDefaultAsync();

            foreach (var next in nextStages)
            {
                if (next.Pass ?? false)
                {
                    await AssignStage(UserId, next.Id);
                    continue;
                }
                next.Status = newStatus;

                var holdsForNotificate = await _authClient.FindHold(next.Id, "Stage");
                    
                if (holdsForNotificate.Count < 1)
                {
                    continue;
                }

                foreach (var group in holdsForNotificate[0]?.Groups)
                {
                    var NotificatedUsers = await _authClient.GetUsersByGroupId(group.Id);
                    foreach (var user in NotificatedUsers)
                    {
                        System.Threading.Tasks.Task.Run(async () => await _mailClient.SendMail(new MailDto
                        {
                            To = user.Email,
                            Body = $"Уважаемый(ая) {user.LongName.Split(' ').ToList()[1]} {user.LongName.Split(' ').ToList()[2]},<br><br>" +
                                   $"Процесс согласования КД \"{processForNotification.Title}\", находящийся на этапе согласования " +
                                   $"\"{stage.Title}\" <br> отправлен на проверку в Ваше подразделение \"{group.Title}\" <br><br>" +
                                   $"ProcTrack, Система отслеживания процессов согласования, <br>" +
                                   $"{DateParser.Parse(DateTime.Now.AddHours(3))}",
                            Subject = $"Процесс согласования КД {processForNotification.Title}"
                        }));
                    }
                }
                var notificatedReleaserHolds = await _authClient.FindHold(processForNotification.Id, "Process");
                var notificatedReleaser = notificatedReleaserHolds[0]?.Users[0];

                System.Threading.Tasks.Task.Run(async () => await _mailClient.SendMail(new MailDto
                {
                    To = notificatedReleaser.Email,
                    Body = $"Уважаемый(ая) {notificatedReleaser.LongName.Split(' ').ToList()[1]} " +
                           $"{notificatedReleaser.LongName.Split(' ').ToList()[2]},<br><br>" +
                           $"Процесс согласования КД \"{processForNotification.Title}\", находящийся на этапе согласования " +
                           $"\"{stage.Title}\" {stage.Status.Title} <br><br>" +
                           $"ProcTrack, Система отслеживания процессов согласования, <br>" +
                           $"{DateParser.Parse(DateTime.Now.AddHours(3))}",
                    Subject = $"Процесс согласования КД {processForNotification.Title}"
                }));
            }

            await _context.SaveChangesAsync();

            if (blockStage) {
                var blockingStagesIds = await _context.Stages
                    .Include(s => s.Status)
                    .Where(s => s.ProcessId == stage.ProcessId &&
                                s.Status.Title.ToLower() == "согласовано-блокировано")
                    .Select(s => s.Id)
                    .ToListAsync();

                foreach (var blockingStageId in blockingStagesIds)
                {
                    await AssignStage(UserId, blockingStageId);
                }
            }

            await _context.SaveChangesAsync();

            string newStatusLog = stage.Status.Title;

            if (logUser != null)
            {
                await _logService.AddLog(new Log
                {
                    Table = "Stage",
                    Field = "Status",
                    Operation = "Update",
                    LogId = Id.ToString(),
                    Old = $"{oldStatus}",
                    New = $"{newStatusLog}",
                    Author = logUser.ShortName.ToString(),
                    CreatedAt = DateTime.Now.AddHours(3)
                });
                await _context.SaveChangesAsync();
            }

            return await GetStageById(Id);
        }

        public async Task<StageDto> GetStageById(int Id)
        {
            var stageModel = await _context.Stages
                .Include(s => s.Status)
                .Where(s => s.Id == Id)
                .FirstOrDefaultAsync();

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

            var status = await _context.Statuses
                .Where(s => s.Id == stageModel.StatusId)
                .FirstOrDefaultAsync();

            return new StageDto
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
        }

        public async Task<List<StageDto>> GetStagesByUserId(int UserId, FilterStageDto filter)
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

                var stageModel = await _context.Stages
                    .Include(s => s.Status)
                    .Where(s => s.Id == hold.DestId &&
                                s.Status != null && 
                                s.Status.Title.ToLower() != "не начат" &&
                                s.Status.Title.ToLower() != "остановлен" &&
                                (filter.Statuses == null || filter.Statuses.Count == 0 || filter.Statuses.Contains(s.Status.Title)) &&
                                (filter.Text == null || filter.Text.Length == 0 || s.Title.Contains(filter.Text)) &&
                                !(s.Pass ?? false) 
                    )
                    .FirstOrDefaultAsync();

                if (stageModel != null)
                {
                    var stageDto = await GetStageById(stageModel.Id);

                    if (stageDto != null)
                    {
                        res.Add(stageDto);
                    }
                }
            }

            // тут добавить пагинацию

            return res;
        }

        public async Task<List<TaskDto>> GetTasksByStageId(int Id)
        {
            var taskModels = await _context.Tasks
                .Where(s => s.StageId == Id)
                .ToListAsync();

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
            var logUser = await _authClient.GetUserById(UserId);

            var stage = await _context.Stages
                .Where(s => s.Id == Id)
                .FirstOrDefaultAsync();

            if (stage == null)
            {
                return null;
            }

            var oldTitle = stage.Title;
            var oldPass = stage.Pass;
            var oldStatus = stage.Status?.Title;

            if (data.Title != null)
            {
                stage.Title = data.Title;
            }

            if (data.Pass != null)
            {
                stage.Pass = data.Pass;
            }
            
            if (data.Status != null)
            {
                var status = await _context.Statuses
                    .Where(s => s.Title == data.Status)
                    .FirstOrDefaultAsync();

                stage.Status = status;

                var processForNotification = await _context.Processes
                    .Where(p => p.Id == stage.ProcessId)
                    .FirstOrDefaultAsync();

                var notificatedReleaserHolds = await _authClient.FindHold(processForNotification.Id, "Process");
                var notificatedReleaser = notificatedReleaserHolds[0]?.Users[0];

                System.Threading.Tasks.Task.Run(async () => await _mailClient.SendMail(new MailDto
                {
                    To = notificatedReleaser.Email,
                    Body = $"Уважаемый(ая) {notificatedReleaser.LongName.Split(' ').ToList()[1]} " +
                           $"{notificatedReleaser.LongName.Split(' ').ToList()[2]},<br><br>" +
                           $"Процесс согласования КД \"{processForNotification.Title}\", находящийся на этапе согласования " +
                           $"\"{stage.Title}\" {stage.Status.Title} <br><br>" +
                           $"ProcTrack, Система отслеживания процессов согласования, <br>" +
                           $"{DateParser.Parse(DateTime.Now.AddHours(3))}",
                    Subject = $"Процесс согласования КД {processForNotification.Title}"
                }));
            }
            
            _context.SaveChanges();

            if (logUser != null)
            {
                await _logService.AddLog(new Log
                {
                    Table = "Stage",
                    Field = "Title, Pass, Status",
                    Operation = "Update",
                    LogId = Id.ToString(),
                    Old = $"{oldTitle}, {oldPass}, {oldStatus}",
                    New = $"{stage.Title}, {stage.Pass}, {stage.Status?.Title}",
                    Author = logUser.ShortName.ToString(),
                    CreatedAt = DateTime.Now.AddHours(3)
                });
                await _context.SaveChangesAsync();
            }

            return await GetStageById(Id);
        }
    }
}
