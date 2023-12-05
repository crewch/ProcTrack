using AuthService.Data;
using AuthService.Dtos.User;
using AuthService.Exceptions;
using AuthService.Services.Role;
using AuthService.Services.Type;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AuthService.Services.User
{
    public class UserService : IUserService
    {
        private readonly AuthContext _context;
        private readonly ITypeService _typeService;
        private readonly IRoleService _roleService;
        public UserService(AuthContext context, ITypeService typeService, IRoleService roleService)
        {
            _context = context;
            _typeService = typeService;
            _roleService = roleService;
        }

        public async Task<int> AddRole(int UserId, int RoleId)
        {
            try
            {
                var user = await Exist(UserId);
                var role = await _roleService.Exist(RoleId);

                var userRole = new Models.UserRoleMapper()
                {
                    RoleId = role.Id,
                    UserId = user.Id,
                };
                await _context.UserRoleMappers.AddAsync(userRole);
                await _context.SaveChangesAsync();

                return user.Id;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<int> Create(string Email, string LongName, string ShortName)
        {
            try
            {
                var user = new Models.User()
                {
                    Email = Email,
                    LongName = LongName,
                    ShortName = ShortName
                };
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return user.Id;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<Models.User> Exist(int UserId)
        {
            var user = await _context.Users
                .Where(h => h.Id == UserId)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new NotFoundException($"User {UserId} not exist");
            }

            return user;
        }

        public async Task<UserDto> Get(int UserId)
        {
            try
            {
                var user = await Exist(UserId);
                var roles = await Roles(user.Id);
                return new UserDto()
                {
                    Id = user.Id,
                    Email = user.Email,
                    LongName = user.LongName,
                    Roles = roles,
                    ShortName = user.ShortName
                };
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<List<int>> Groups(int UserId)
        {
            try
            {
                var user = await Exist(UserId);

                var groupIds = await _context.UserGroupMappers
                    .Where(m => m.UserId == user.Id)
                    .Select(m => m.GroupId.Value)
                    .Distinct()
                    .ToListAsync();

                return groupIds;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<List<int>> Holds(string Type, int UserId)
        {
            try
            {
                var user = await Exist(UserId);
                var typeId = await _typeService.Find(Type);
                var groups = await Groups(user.Id);

                var holdsUser = await _context.UserHoldMappers
                    .Include(m => m.Hold)
                    .Where(m => m.UserId == user.Id && m.Hold.TypeId == typeId)
                    .Select(m => m.HoldId.Value)
                    .Distinct()
                    .ToListAsync();

                var holdsGroup = await _context.GroupHoldMappers
                    .Include(m => m.Hold)
                    .Where(m => groups.Contains(m.GroupId.Value) && m.Hold.TypeId == typeId)
                    .Select(m => m.HoldId.Value)
                    .Distinct()
                    .ToListAsync();

                return holdsUser.Union(holdsGroup).ToList();
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<List<string>> Roles(int UserId)
        {
            try
            {
                var user = await Exist(UserId);
                var roles = await _context.UserRoleMappers
                    .Include(m => m.Role)
                    .Where(m => m.UserId == user.Id)
                    .Select(m => m.Role.Title)
                    .Distinct()
                    .ToListAsync();

                return roles;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
