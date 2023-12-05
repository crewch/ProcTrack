
using DB_Service.Models;
using DB_Service.Clients.Http;
using DB_Service.Data;
using DB_Service.Models;
using Microsoft.EntityFrameworkCore;

namespace DB_Service.Services
{
    public class LogService : ILogService
    {
        private readonly DataContext _context;

        public LogService(DataContext context)
        {
            _context = context;
        }

        public async Task<Log> AddLog(Log data)
        {
            var res = await _context.Logs.AddAsync(data);
            return res.Entity;
        }

        public async Task<List<Log>> GetLogs()
        {
            var logs = await _context.Logs.ToListAsync();
            return logs;
        }
    }
}
