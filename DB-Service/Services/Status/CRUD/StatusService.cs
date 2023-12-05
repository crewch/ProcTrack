using DB_Service.Data;
using DB_Service.Exceptions;
using DB_Service.Models;
using Microsoft.EntityFrameworkCore;

namespace DB_Service.Services.Status.CRUD
{
    public class StatusService : IStatusService
    {
        private readonly DataContext _context;

        public StatusService(DataContext context)
        {
            _context = context;
        }

        public async Task<int> Create(string title)
        {
            var newStatus = new Models.Status
            {
                Title = title,
            };

            await _context.Statuses.AddAsync(newStatus);
            await _context.SaveChangesAsync();

            return newStatus.Id;
        }

        public async Task<int> Update(int statusId, string title)
        {
            try
            {
                var status = await Exist(statusId);

                status.Title = title;
                
                await _context.SaveChangesAsync();

                return status.Id;
            }
            catch (NotFoundException ex) 
            {
                throw ex;
            } 
        }

        public async Task<bool> Delete(int statusId)
        {
            try
            {
                var status = await Exist(statusId);

                _context.Statuses.Remove(status);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<Models.Status> Exist(int statusId)
        {
            var status = await _context.Statuses
                .Where(p => p.Id == statusId)
                .FirstOrDefaultAsync() ??
                throw new NotFoundException($"Status with id = {statusId} not found");

            return status;
        }

        public async Task<int> Find(string title)
        {
            var status = await _context.Statuses
                .Where(s => s.Title.ToLower() == title.ToLower())
                .FirstOrDefaultAsync() ??
                throw new NotFoundException($"Status with title = {title} not found");
            
            return status.Id;
        }

        public async Task<string> Get(int statusId)
        {
            try
            {
                var status = await Exist(statusId);
                return status.Title;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<List<string>> GetAll()
        {
            return await _context.Statuses
                .Select(s => s.Title)
                .ToListAsync();
        }

        public async Task<int> AcceptedForVerification()
        {
            return await Find("принят на проверку");
        }

        public async Task<int> Assigned()
        {
            return await Find("согласован");
        }

        public async Task<int> AssignedBlocked() //TODO:на фронте переделать 
        {
            return await Find("согласован-блокирован");
        }

        public async Task<int> InRework()
        {
            return await Find("в доработке");
        }

        public async Task<int> NotStarted()
        {
            return await Find("не начат");
        }

        public async Task<int> SentForVerification()
        {
            return await Find("отправлен на проверку");
        }

        public async Task<int> Stopped()
        {
            return await Find("принят на проверку");
        }
    }
}
