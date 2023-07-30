using AuthService.Data;
using AuthService.Dtos;
using AuthService.Exceptions;
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

        public async Task<UserDto> GetUserById(int id)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                // throw new NotFoundException($"user with id {id} not found");
                return null;
            }
            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                LongName = user.LongName,
                ShortName = user.ShortName,
                Roles = user.Roles.Select(r => r.Title).ToList(),
            };
            return userDto;
        }

        public async Task<List<GroupDto>> GetGroups()
        {
            var groups = await _context.Groups
                .ToListAsync();
            
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
                var roleModel = await _context.Roles
                    .Where(r => r.Title == role)
                    .FirstOrDefaultAsync();

                if (roleModel == null)
                {
                    throw new NotFoundException($"role \"{role}\" not found");
                }

                roles.Add(roleModel);
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
                var userModel = await _context.Users
                    .Where(u => u.Email == user.Email)
                    .FirstOrDefaultAsync();

                if (userModel == null)
                {
                    throw new NotFoundException($"user \"{user.Email}\" not found");
                }
                
                users.Add(userModel);
            }

            var boss = await _context.Users
                .Where(u => u.Email == data.Boss.Email)
                .FirstOrDefaultAsync();

            if (boss == null)
            {
                throw new NotFoundException($"user \"{data.Boss.Email}\" not found");
            }

            var group = new Group
            {
                Title = data.Title,
                Description = data.Description,
                BossId = boss.Id,
                Users = users,
            };

            await _context.AddAsync(group);
            await _context.SaveChangesAsync();

            var res = new GroupDto
            {
                Id = group.Id,
                Title = group.Title,
                Description = group.Description,
                Boss = await GetUserById(group.BossId),
            };

            return res;
        }

        public async Task<string> AddRole(string data)
        {
            var role = new Role
            {
                Title = data,
            };

            await _context.AddAsync(role);
            await _context.SaveChangesAsync();

            return role.Title;
        }

        public async Task<List<UserDto>> AddUsersToGroup(int GroupId, List<UserDto> data)
        {
            var group = await _context.Groups
                .Include(g => g.Users)
                .Where(g => g.Id == GroupId)
                .FirstOrDefaultAsync();

            if (group == null)
            {
                throw new NotFoundException($"group with id {GroupId} not found");
            }
            
            var res = new List<UserDto>();

            foreach (var user in data)
            {
                User iUser = await _context.Users
                        .Where(u => u.Id == user.Id)
                        .FirstOrDefaultAsync();

                if (iUser == null)
                {
                    throw new NotFoundException($"user with id {user.Id} not found");
                }

                group.Users.Add(iUser);
                res.Add(await GetUserById(iUser.Id));
            }
            await _context.SaveChangesAsync();
            return res;
        }

        public async Task<List<UserDto>> GetUsersByGroupId(int Id)
        {
            var users = await _context.UserGroupMappers
                .Where(ug => ug.GroupId == Id)
                .Select(ug => ug.User)
                .ToListAsync();

            if (users == null)
            {
                return null;
            }
            
            var res = new List<UserDto>();

            foreach (var user in users)
            {
                var userDto = await GetUserById(user.Id);
                res.Add(userDto);
            }

            return res;
        }
    }
}
