using AuthService.Data;
using AuthService.Dtos;
using AuthService.Exceptions;
using AuthService.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AuthService.Services
{
    public class HoldService : IHoldService
    {
        private readonly AuthContext _context;

        public HoldService(AuthContext context)
        {
            _context = context;
        }

        public async Task<CreateHoldResponceDto> CreateHold(CreateHoldRequestDto data)
        {
            var type = await _context.Types
                .Where(t => t.Title == data.DestType)
                .FirstOrDefaultAsync();

            if (type == null)
            {
                // throw new NotFoundException($"type \"{data.DestType}\" not found");
                return null;
            }

            User? user = new User();
            Group? group = new Group();

            bool dataHolderType = data.HolderType.ToLower() == "user";
            if (dataHolderType)
            {
                user = await _context.Users
                    .Include(u => u.Roles)
                    .Where(u => u.Id == data.HolderId)
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    // throw new NotFoundException($"user with id {data.HolderId} not found");
                    return null;
                }

            } else
            {
                group = await _context.Groups
                    .Where(g => g.Id == data.HolderId)
                    .FirstOrDefaultAsync();

                if (group == null)
                {
                    // throw new NotFoundException($"group with id {data.HolderId} not found");
                    return null;
                }
            }

            var rights = new List<Right>();
            bool dataType = data.DestType.ToLower() == "process";

            if (dataType && dataHolderType)
            {
                rights = await _context.Rights
                    .Where(r => r.Title == "modification" || r.Title == "deletion")
                    .ToListAsync();
            } else if (dataType && !dataHolderType)
            {
                rights = await _context.Rights
                    .Where(r => r.Title == "reading")
                    .ToListAsync();
            } else
            {
                rights = await _context.Rights
                    .Where(r => r.Title == "matching" || r.Title == "commenting")
                    .ToListAsync();
            }

            var hold = new Hold
            {
                DestId = data.DestId,
                Type = type,
                Users = dataHolderType ? new List<User>() { user } : new List<User>(),
                Groups = dataHolderType ? new List<Group>() : new List<Group>() { group },
                Rights = rights,
            };

            await _context.Holds.AddAsync(hold);
            
            var userDto = new UserDto();
            var groupDto = new GroupDto();

            if (dataHolderType)
            {
                userDto = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    LongName = user.LongName,
                    ShortName = user.ShortName,
                    Roles = user.Roles.Select(r => r.Title).ToList(),
                };
            } else
            {
                var boss = await _context.Users
                    .Where(u => u.Id == group.BossId)
                    .FirstOrDefaultAsync();

                if (boss == null)
                {
                    // throw new NotFoundException($"user with id {group.BossId} not found");
                    return null;
                }

                var bossDto = new UserDto
                {
                    Id = boss.Id,
                    Email = boss.Email,
                    LongName = boss.LongName,
                    ShortName = boss.ShortName,
                    Roles = user.Roles.Select(b => b.Title).ToList(),
                };

                groupDto = new GroupDto
                {
                    Id = group.Id,
                    Title = group.Title,
                    Description = group.Description,
                    Boss = bossDto,
                };
            }

            var holderDto = new HolderDto
            {
                Id = dataHolderType ? user.Id : group.Id,
                Type = data.HolderType
            };

            var holdDto = new HoldDto
            {
                Id = hold.Id,
                DestId = hold.DestId,
                Type = hold.Type.Title,
                Users = new List<UserDto>() { userDto },
                Groups = new List<GroupDto>() { groupDto },
                Rights = rights.Select(r => r.Title).ToList(),
            };

            var dto = new CreateHoldResponceDto
            {
                Hold = holdDto,
                Holder = holderDto,
            };

            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<List<HoldDto>> FindHold(int destId, string type)
        {
            var TypeId = await _context.Types
                .Where(t => t.Title == type)
                .Select(t => t.Id)
                .ToListAsync();

            if (TypeId == null)
            {
                // throw new NotFoundException($"TypeId not found");
                return null;
            }

            var holdIds = await _context.Holds
                .Where(h => h.DestId == destId && h.TypeId == TypeId[0])
                .Select(h => h.Id)
                .ToListAsync();

            var res = new List<HoldDto>();
            foreach (var holdId in holdIds)
            {
                res.Add(await GetHoldById(holdId));
            }

            return res;
        }

        public async Task<HoldDto> GetHoldById(int id)
        {
            var hold = await _context.Holds
                .Include(h => h.Users)
                .Include(h => h.Groups)
                .Include(h => h.Rights)
                .Include(h => h.Type)
                .Where(h => h.Id == id)
                .FirstOrDefaultAsync();

            var rights = await _context.RightHoldMappers
                .Include(rh => rh.Right)
                .Where(hr => hr.Hold != null && hr.Hold.Id == id && hr.Right != null)
                .Select(hr => hr.Right.Title)
                .ToListAsync();

            var userModels = await _context.UserHoldMappers
                .Include(uh => uh.User)
                .Where(uh => uh.Hold != null && uh.Hold.Id == id && uh.User != null)
                .Select(uh => uh.User)
                .ToListAsync();

            var userDtos = new List<UserDto>();

            foreach (var iUser in userModels)
            {
                if (!userDtos.Any(ud => ud.Id == iUser.Id))
                {
                    var uDto = new UserDto
                    {
                        Id = iUser.Id,
                        Email = iUser.Email,
                        LongName = iUser.LongName,
                        ShortName = iUser.ShortName,
                    };
                    userDtos.Add(uDto);
                }
            }

            var groupModels = await _context.GroupHoldMappers
                .Include(gh => gh.Group)
                .Where(gh => gh.Hold != null && gh.Hold.Id == id && gh.Group != null)
                .Select(gh => gh.Group)
                .ToListAsync();

            var groupDtos = new List<GroupDto>();

            foreach (var iGroup in groupModels)
            {
                if (!groupDtos.Any(gd => gd.Id == iGroup.Id))
                {
                    var bossModel = await _context.Users
                        .Include(u => u.Roles)
                        .Where(u => u.Id == iGroup.BossId)
                        .FirstOrDefaultAsync();

                    var bossDto = new UserDto
                    {
                        Id = bossModel.Id,
                        Email = bossModel.Email,
                        LongName = bossModel.LongName,
                        ShortName = bossModel.ShortName,
                        Roles = bossModel.Roles.Select(r => r.Title).ToList()
                    };

                    var gDto = new GroupDto
                    {
                        Id = iGroup.Id,
                        Title = iGroup.Title,
                        Description = iGroup.Description,
                        Boss = bossDto,
                    };
                    groupDtos.Add(gDto);
                }
            }

            var dto = new HoldDto
            {
                Id = id,
                DestId = hold.DestId,
                Type = hold.Type.Title,
                Users = userDtos,
                Groups = groupDtos,
                Rights = rights.Distinct().ToList()
            };

            return dto;
        }

        public async Task<List<HoldDto>> GetHolds(UserHoldTypeDto data)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == data.Id);
            
            if (user == null)
            {
                // throw new NotFoundException($"user with id {data.Id} not found");
                return null;
            }
            
            var type = await _context.Types.FirstOrDefaultAsync(t => t.Title == data.HoldType);
    
            if (type == null)
            {
                // throw new NotFoundException($"type {data.HoldType} not found");
                return null;
            }

            var UserHoldIds = await _context.UserHoldMappers
                .Include(u => u.Hold)
                .Where(u => u.UserId == user.Id && u.Hold.TypeId == type.Id)
                .Select(u => u.Hold.Id)
                .ToListAsync();

            var groupsIds = await _context.UserGroupMappers
                .Where(ug => ug.UserId == user.Id)
                .Select(ug => ug.GroupId)
                .ToListAsync();

            var GroupHoldIds = await _context.GroupHoldMappers
                .Include(u => u.Hold)
                .Where(u => groupsIds.Contains(u.GroupId) && u.Hold != null && u.Hold.TypeId == type.Id)
                .Select(u => u.Hold.Id)
                .ToListAsync();

            HashSet<int> HoldIds = new HashSet<int>()
                .Concat(UserHoldIds)
                .Concat(GroupHoldIds)
                .ToHashSet();

            var res = new List<HoldDto>();
            foreach (int holdId in HoldIds)
            {
                var hold = await GetHoldById(holdId);
                res.Add(hold);
            }

            return res;
        }

        public async Task<GetRightResponseDto> GetRights(GetRightRequestDto data)
        {
            var holds = await _context.Holds
                .Include(h => h.GroupHolds)
                .ThenInclude(gh => gh.Group)
                .Include(h => h.UserHolds)
                .ThenInclude(uh => uh.User)
                .Include(h => h.Type)
                .Include(h => h.Rights)
                .Where(h => h.DestId == data.DestId && h.Type.Title == data.Type)
                .ToListAsync();

            var rightsRes = new List<string>();

            var user = await _context.Users
                .Where(u => u.Id == data.UserId)
                .FirstOrDefaultAsync();

            var groups = await _context.UserGroupMappers
                .Where(gh => gh.User == user)
                .Select(gh => gh.Group)
                .ToListAsync();

            foreach (var hold in holds)
            {
                bool flag = hold.Users.Contains(user);
                
                foreach (var g in groups)
                {
                    flag = flag || hold.Groups.Contains(g);
                }
                
                if (flag)
                {
                    var holdRights = await _context.RightHoldMappers
                        .Where(rh => rh.HoldId == hold.Id)
                        .Select(rh => rh.Right.Title)
                        .ToListAsync();
                    
                    rightsRes.AddRange(holdRights);
                }
            }
                
            var dto = new GetRightResponseDto
            {
                Rights = rightsRes.Distinct().ToList(),
                UserId = data.UserId,
                DestId = data.DestId,
            };
            return dto;
        }
    }
}
