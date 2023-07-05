using AuthService.Data;
using AuthService.Dtos;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Services
{
    public class UserService : IUserService
    {
        private readonly AuthContext _context;

        public UserService(AuthContext context)
        {
            _context = context;
        }

        public Task<UserDto> GetUserById(int id)
        {
            var user = _context.Users
                .Include(u => u.Roles)
                .Where(u => u.Id == id)
                .FirstOrDefault();
            if (user == null)
            {
                return Task.FromResult<UserDto>(null);
            }
            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                LongName = user.LongName,
                ShortName = user.ShortName,
                Roles = user.Roles.Select(r => r.Title).ToList(),
            };
            return Task.FromResult(userDto);
        }

        public async Task<List<GroupDto>> GetGroups()
        {
            var groups = _context.Groups
                .ToList();
            
            var res = new List<GroupDto>();

            foreach (var group in groups)
            {
                res.Add(new GroupDto {
                    Id = group.Id,
                    Title = group.Title,
                    Description = group.Description,
                    Boss = await GetUserById(group.BossId),
                });
            }
            return res;
        }
    }
}
