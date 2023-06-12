using DB_Service.Data;
using DB_Service.Models;
using Microsoft.EntityFrameworkCore;

namespace DB_Service.Services
{
    public class DataService : IDataService
    {
        private readonly DataContext _context;

        public DataService(DataContext context)
        {
            _context = context;
        }

        public async Task<Message> AddMessage(Message message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task<List<Message>> GetMessages()
        {
            return await _context.Messages.ToListAsync();
        }
    }
}
