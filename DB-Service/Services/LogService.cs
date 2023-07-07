using DB_Service.Clients.Http;
using DB_Service.Data;
using DB_Service.Models;

namespace DB_Service.Services
{
    public class LogService : ILogService
    {
        private readonly DataContext _context;
        
        public LogService(DataContext context)
        {
            _context = context;
        }

        public async Task<Log> CreateLog(Log data)
        {
            var log = new Log
            {
                Table = data.Table,
                Author = data.Author,
                CreatedAt = DateTime.Now,
                Field = data.Field,
                Old = data.Old,
                New = data.New,
                LogId = data.LogId,
                Operation = data.Operation
            };

            _context.Logs.Add(log);
            _context.SaveChanges();

            return log;
        }
    }
}
