using DB_Service.Data;
using DB_Service.Exceptions;
using DB_Service.Models;
using Microsoft.EntityFrameworkCore;

namespace DB_Service.Services.Log.CRUD
{
    public class LogService : ILogService
    {
        private readonly DataContext _context;

        public LogService(DataContext context)
        {
            _context = context;
        }

        public async Task<int> Create(
            string? table, 
            string? field, 
            string? operation, 
            string? logId, 
            string? oldField, 
            string? newField, 
            string? author)
        {
            var newLog = new Models.Log
            {
                Table = table,
                Field = field,
                Operation = operation,
                LogId = logId,
                Author = author,
                CreatedAt = DateTime.Now.AddHours(3),
                New = newField,
                Old = oldField,
            };

            await _context.Logs.AddAsync(newLog);
            await _context.SaveChangesAsync();

            return newLog.Id;
        }

        public async Task<bool> Delete(int logId)
        {
            try
            {
                var log = await Exist(logId);

                _context.Logs.Remove(log);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<Models.Log> Exist(int logId)
        {
            var log = await _context.Logs
                .Where(n => n.Id == logId)
                .FirstOrDefaultAsync() ??
                throw new NotFoundException($"Log with Id = {logId} not found");

            return log;
        }
    }
}
