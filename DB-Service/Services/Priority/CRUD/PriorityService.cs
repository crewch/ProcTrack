using DB_Service.Data;
using DB_Service.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DB_Service.Services.Priority.CRUD
{
    public class PriorityService : IPriorityService
    {
        private readonly DataContext _context;

        public PriorityService(DataContext context)
        {
            _context = context;
        }

        public async Task<int> Create(string title)
        {
            var newPriority = new Models.Priority
            {
                Title = title,
            };

            await _context.Priorities.AddAsync(newPriority);
            await _context.SaveChangesAsync();

            return newPriority.Id;
        }

        public async Task<int> Update(int priorityId, string title)
        {
            try
            {
                var priority = await Exist(priorityId);

                priority.Title = title;
                
                await _context.SaveChangesAsync();

                return priority.Id;
            }
            catch (NotFoundException ex) 
            {
                throw ex;
            }  
        }

        public async Task<bool> Delete(int priorityId)
        {
            try
            {
                var priority = await Exist(priorityId);

                _context.Priorities.Remove(priority);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<Models.Priority> Exist(int priorityId)
        {
            var priority = await _context.Priorities
                .Where(p => p.Id == priorityId)
                .FirstOrDefaultAsync() ??
                throw new NotFoundException($"Priority with id = {priorityId} not found");

            return priority;
        }

        public async Task<int> Find(string title)
        {
            var priority = await _context.Priorities
                .Where(p => p.Title.ToLower() == title.ToLower())
                .FirstOrDefaultAsync() ??
                throw new NotFoundException($"Priority with title = {title} not found");

            return priority.Id;
        }

        public async Task<string> Get(int priorityId)
        {
            try
            {
                var priority = await Exist(priorityId);
                return priority.Title;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<List<string>> GetAll()
        {
            return await _context.Priorities
                .Select(p => p.Title)
                .ToListAsync();
        }
    }
}
