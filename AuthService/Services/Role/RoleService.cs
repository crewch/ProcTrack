using AuthService.Data;
using AuthService.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Services.Role
{
    public class RoleService : IRoleService
    {
        private readonly AuthContext _context;
        public RoleService(AuthContext context) 
        {
            _context = context;
        }

        public async Task<Models.Role> Exist(int RoleId)
        {
            var role = await _context.Roles
                .Where(h => h.Id == RoleId)
                .FirstOrDefaultAsync();

            if (role == null)
            {
                throw new NotFoundException($"Role {RoleId} not exist");
            }

            return role;
        }

        public async Task<int> Find(string Title)
        {
            var role = await _context.Roles
                .Where(h => h.Title == Title)
                .FirstOrDefaultAsync();

            if (role == null)
            {
                throw new NotFoundException($"Role {Title} not exist");
            }

            return role.Id;
        }

        public async Task<List<string>> Rights(int RoleId)
        {
            try
            {
                var role = await Exist(RoleId);

                var rights = await _context.RightRoleMappers
                    .Include(m => m.Right)
                    .Where(m => m.RoleId == role.Id)
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
