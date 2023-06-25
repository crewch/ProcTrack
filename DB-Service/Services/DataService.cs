using DB_Service.Clients.Http;
using DB_Service.Data;
using DB_Service.Dtos;
using DB_Service.Models;
using Microsoft.EntityFrameworkCore;

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
