using AuthService.Data;
using Microsoft.EntityFrameworkCore;
using AuthService.Exceptions;
using AuthService.Services.Status;
using AuthService.Services.User;
using AuthService.Services.Group;
using AuthService.Models;
using AuthService.Services.Type;
using AuthService.Dtos.Hold;
using NuGet.Frameworks;
using NuGet.Packaging;
using AuthService.Services.Role;

namespace AuthService.Services.Hold
{
    public class HoldService : IHoldService
    {
        private readonly AuthContext _context;
        private readonly IStatusService _statusService;
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;
        private readonly ITypeService _typeService;
        private readonly IRoleService _roleService;

        public HoldService(
            AuthContext context, 
            IStatusService statusService, 
            IUserService userService, 
            IGroupService groupService, 
            ITypeService typeService,
            IRoleService roleService)
        {
            _context = context;
            _statusService = statusService;
            _userService = userService;
            _groupService = groupService;
            _typeService = typeService;
            _roleService = roleService;
        }

        public async Task<int> AddGroup(int HoldId, int GroupId, int StatusMemberId, int StatusBossId)
        {
            try
            {
                var hold = await Exist(HoldId);
                var group = await _groupService.Exist(GroupId);
                var statusMember = await _statusService.Exist(StatusMemberId);
                var statusBoss = await _statusService.Exist(StatusBossId);

                var GroupHold = new Models.GroupHoldMapper()
                {
                    Hold = hold,
                    Group = group,
                    StatusMember = statusMember,
                    StatusBoss = statusBoss
                };

                await _context.GroupHoldMappers.AddAsync(GroupHold);
                await _context.SaveChangesAsync();

                return hold.Id; // TODO: мб что то другое возвращать?
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<int> AddUser(int HoldId, int UserId, int StatusId)
        {
            try
            {
                var hold = await Exist(HoldId);
                var user = await _userService.Exist(UserId);
                var status = await _statusService.Exist(StatusId);

                var UserHold = new Models.UserHoldMapper()
                {
                    Hold = hold,
                    User = user,
                    Status = status
                };

                await _context.UserHoldMappers.AddAsync(UserHold);
                await _context.SaveChangesAsync();

                return hold.Id; // TODO: мб что то другое возвращать?
            } 
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<int> Copy(int HoldId, int NewHoldId)
        {
            try
            {
                var hold = await Exist(HoldId);
                var newHold = await Exist(NewHoldId);

                var users = await Users(hold.Id);
                var groups = await Groups(hold.Id);

                foreach (var user in users)
                {
                    await AddUser(newHold.Id, user.UserId, user.StatusId);
                }

                foreach (var group in groups)
                {
                    await AddGroup(newHold.Id, group.GroupId, group.StatusMemberId, group.StatusBossId);
                }

                return newHold.Id;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<int> Create(string Type, int DestId)
        {
            try
            {
                var type = await _typeService.Find(Type);
                var hold = await _context.Holds
                    .Where(h => h.TypeId == type && h.DestId == DestId)
                    .FirstOrDefaultAsync();

                if (hold != null)
                {
                    return hold.Id;
                }

                var newHold = new Models.Hold()
                {
                    DestId = DestId,
                    Type = await _typeService.Exist(type)
                };
                await _context.Holds.AddAsync(newHold);
                await _context.SaveChangesAsync();

                return newHold.Id;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<Models.Hold> Exist(int HoldId)
        {
            var hold = await _context.Holds
                .Where(h => h.Id == HoldId)
                .FirstOrDefaultAsync();

            if (hold == null)
            {
                throw new NotFoundException($"Hold {HoldId} not exist");
            }

            return hold;
        }

        public async Task<int> Find(string Type, int DestId)
        {
            try
            {
                var type = await _typeService.Find(Type);
                var hold = await _context.Holds
                    .Include(p => p.Type)
                    .Where(h => h.Type.Title == Type && h.DestId == DestId)
                    .FirstOrDefaultAsync();

                if (hold == null)
                {
                    throw new NotFoundException($"Hold {Type} {DestId} not exist");
                }

                return hold.Id;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<HoldDto> Get(int HoldId)
        {
            try
            {
                var hold = await Exist(HoldId);

                return new HoldDto()
                {
                    Id = hold.Id,
                    Type = await _typeService.GetTitle(hold.TypeId ?? 0),
                    DestId = hold.DestId
                };
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<List<GroupHoldDto>> Groups(int HoldId)
        {
            try
            {
                var hold = await Exist(HoldId);

                var groups = await _context.GroupHoldMappers
                    .Where(m => m.HoldId == hold.Id)
                    .Select(m => new GroupHoldDto()
                    {
                        GroupId = m.GroupId ?? 0,
                        StatusMemberId = m.StatusMemberId ?? 0,
                        StatusBossId = m.StatusBossId ?? 0,
                    })
                    .ToListAsync();

                return groups;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
            
        }

        public async Task<List<string>> UserRights(int HoldId, int UserId)
        {
            try
            {
                var hold = await Exist(HoldId);
                var user = await _userService.Exist(UserId);

                var rights = new List<string>();

                var roles = await _userService.Roles(user.Id);
                foreach (var role in roles)
                {
                    var roleId = await _roleService.Find(role);
                    rights.AddRange(await _roleService.Rights(roleId));
                }

                var users = await Users(hold.Id);
                foreach (var userHold in users)
                {
                    if (userHold.UserId == user.Id)
                    {
                        rights.AddRange(await _statusService.Rights(userHold.StatusId));
                    }
                }

                var groups = await Groups(hold.Id);
                foreach (var group in groups)
                {
                    var usersInGroup = await _groupService.Users(group.GroupId);
                    if (usersInGroup.Any(g => g == user.Id))
                    {
                        var rightsLoc = await _statusService.Rights(group.StatusMemberId);
                        rights.AddRange(rightsLoc);
                    }
                    if (
                        await _context.Groups
                            .Where(g => g.Id == group.GroupId)
                            .Select(g => g.BossId == user.Id)
                            .FirstOrDefaultAsync()
                       )
                    {
                        var rightsLoc = await _statusService.Rights(group.StatusBossId);
                        rights.AddRange(rightsLoc);
                    }
                }

                return rights.Distinct().ToList();
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<List<UserHoldDto>> Users(int HoldId)
        {
            try
            {
                var hold = await Exist(HoldId);

                var users = await _context.UserHoldMappers
                    .Where(m => m.HoldId == hold.Id)
                    .Select(m => new UserHoldDto()
                    {
                        UserId = m.UserId ?? 0,
                        StatusId = m.StatusId ?? 0,
                    })
                    .ToListAsync();

                return users;
            }
            catch (NotFoundException ex) 
            {
                throw ex;
            }
        }
    }
}
