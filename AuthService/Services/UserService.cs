using AuthService.Data;
using AuthService.Dtos;
using AuthService.Models;
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

        public async Task<UserDto> AddUser(UserDto data)
        {
            var roles = new List<Role>();

            foreach (var role in data.Roles)
            {
                roles.Add(_context.Roles
                    .Where(r => r.Title == role)
                    .FirstOrDefault());
            }

            var user = new User
            {
                Email = data.Email,
                LongName= data.LongName,
                ShortName= data.ShortName,
                Roles = roles,
            };

            _context.Add(user);
            _context.SaveChanges();

            var res = await GetUserById(user.Id);
            return res;
        }

        public async Task<GroupDto> AddGroup(CreateGroupDto data)
        {
            var users = new List<User>();

            foreach (var user in data.Users)
            {
                users.Add(_context.Users
                    .Where(u => u.Email == user.Email)
                    .FirstOrDefault());
            }

            var group = new Group
            {
                Title = data.Title,
                Description = data.Description,
                BossId = data.Boss.Id,
                Users = users,
            };

            _context.Add(group);
            _context.SaveChanges();

            var res = new GroupDto
            {
                Id = group.Id,
                Title = group.Title,
                Description = group.Description,
                Boss = await GetUserById(group.BossId),
            };

            return res;
        }
    }
}
