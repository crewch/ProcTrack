using AuthService.Data;
using AuthService.Exceptions;
using AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Services.Status
{
    public class StatusService : IStatusService
    {
        private readonly AuthContext _context;
        public StatusService(AuthContext context) 
        { 
            _context = context;
        }

        public async Task<Models.Status> Exist(int StatusId)
        {
            var status = await _context.Statuses
                .Where(h => h.Id == StatusId)
                .FirstOrDefaultAsync();

            if (status == null)
            {
                throw new NotFoundException($"Status {StatusId} not exist");
            }

            return status;
        }

        public async Task<List<string>> Rights(int StatusId)
        {
            try
            {
                var status = await Exist(StatusId);

                var rights = await _context.RightStatusMappers
                    .Include(m => m.Right)
                    .Where(m => m.StatusId == status.Id)
                    .Select(m => m.Right.Title)
                    .ToListAsync();

                return rights;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
