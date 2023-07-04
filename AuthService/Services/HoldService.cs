using AuthService.Data;
using AuthService.Dtos;
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
            var type = _context.Types
                .Where(t => t.Title == data.DestType)
                .FirstOrDefault();

            User? user = new User();
            Group? group = new Group();

            bool dataHolderType = data.HolderType.ToLower() == "user";
            if (dataHolderType)
            {
                user = _context.Users
                    .Include(u => u.Roles)
                    .Where(u => u.Id == data.HolderId)
                    .FirstOrDefault();
            } else
            {
                group = _context.Groups
                    .Where(g => g.Id == data.HolderId)
                    .FirstOrDefault();
            }

            var rights = new List<Right>();
            bool dataType = data.DestType.ToLower() == "process";

            if (dataType && dataHolderType)
            {
                rights = _context.Rights
                    .Where(r => r.Title == "modification" || r.Title == "deletion")
                    .ToList();
            } else if (dataType && !dataHolderType)
            {
                rights = _context.Rights
                    .Where(r => r.Title == "reading")
                    .ToList();
            } else
            {
                rights = _context.Rights
                    .Where(r => r.Title == "matching" || r.Title == "commenting")
                    .ToList();
            }

            var hold = new Hold
            {
                DestId = data.DestId,
                Type = type,
                Users = dataHolderType ? new List<User>() { user } : new List<User>(),
                Groups = dataHolderType ? new List<Group>() : new List<Group>() { group },
                Rights = rights,
            };

            _context.Holds.Add(hold);
            _context.SaveChanges();
            
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
                var boss = _context.Users
                    .Where(u => u.Id == group.BossId)
                    .FirstOrDefault();

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

            return await Task.FromResult(dto);
        }

        public async Task<List<HoldDto>> FindHold(int destId, string type)
        {
            var TypeId = _context.Types
                .Where(t => t.Title == type)
                .Select(t => t.Id)
                .ToList();
            if (TypeId == null)
            {
                return null;
            }
            var holdIds = _context.Holds
                .Where(h => h.DestId == destId && h.TypeId == TypeId[0])
                .Select(h => h.Id)
                .ToList();

            var res = new List<HoldDto>();
            foreach ( var holdId in holdIds )
            {
                res.Add(await GetHoldById(holdId));
            }
            return res;
        }

        public Task<HoldDto> GetHoldById(int id)
        {
            var hold = _context.Holds
                .Include(h => h.Users)
                .Include(h => h.Groups)
                .Include(h => h.Rights)
                .Include(h => h.Type)
                .Where(h => h.Id == id)
                .FirstOrDefault();

            var rights = _context.RightHoldMappers
                .Include(rh => rh.Right)
                .Where(hr => hr.Hold != null && hr.Hold.Id == id && hr.Right != null)
                .Select(hr => hr.Right.Title)
                .ToList();

            var userModels = _context.UserHoldMappers
                .Include(uh => uh.User)
                .Where(uh => uh.Hold != null && uh.Hold.Id == id && uh.User != null)
                .Select(uh => uh.User)
                .ToList();

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

            var groupModels = _context.GroupHoldMappers
                .Include(gh => gh.Group)
                .Where(gh => gh.Hold != null && gh.Hold.Id == id && gh.Group != null)
                .Select(gh => gh.Group)
                .ToList();

            var groupDtos = new List<GroupDto>();

            foreach (var iGroup in groupModels)
            {
                if (!groupDtos.Any(gd => gd.Id == iGroup.Id))
                {
                    var bossModel = _context.Users
                        .Include(u => u.Roles)
                        .Where(u => u.Id == iGroup.BossId)
                        .FirstOrDefault();

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

            return Task.FromResult(dto);
        }

        public async Task<List<HoldDto>> GetHolds(UserHoldTypeDto data)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == data.Id);
            var type = _context.Types.FirstOrDefault(t => t.Title == data.HoldType);
    
            if (user == null || type == null)
            {
                return null;
            }

            var UserHoldIds = _context.UserHoldMappers
                .Include(u => u.Hold)
                .Where(u => u.UserId == user.Id && u.Hold.TypeId == type.Id)
                .Select(u => u.Hold.Id)
                .ToList();

            var groupsIds = _context.UserGroupMappers
                .Where(ug => ug.UserId == user.Id)
                .Select(ug => ug.GroupId)
                .ToList();

            var GroupHoldIds = _context.GroupHoldMappers
                .Include(u => u.Hold)
                .Where(u => groupsIds.Contains(u.GroupId) && u.Hold.TypeId == type.Id)
                .Select(u => u.Hold.Id)
                .ToList();

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

        public Task<GetRightResponseDto> GetRights(GetRightRequestDto data)
        {
            var holds = _context.Holds
                .Include(h => h.GroupHolds)
                .ThenInclude(gh => gh.Group)
                .Include(h => h.UserHolds)
                .ThenInclude(uh => uh.User)
                .Include(h => h.Type)
                .Include(h => h.Rights)
                .Where(h => h.DestId == data.DestId && h.Type.Title == data.Type)
                .ToList();

            var rightsRes = new List<string>();

            var user = _context.Users
                .Where(u => u.Id == data.UserId)
                .FirstOrDefault();

            var groups = _context.UserGroupMappers
                .Where(gh => gh.User == user)
                .Select(gh => gh.Group)
                .ToList();

            foreach (var hold in holds)
            {
                bool flag = hold.Users.Contains(user);
                
                foreach (var g in groups)
                {
                    flag = flag || hold.Groups.Contains(g);
                }
                
                if (flag)
                {
                    var holdRights = _context.RightHoldMappers
                        .Where(rh => rh.HoldId == hold.Id)
                        .Select(rh => rh.Right.Title)
                        .ToList();
                    
                    rightsRes.AddRange(holdRights);
                }
            }
                
            var dto = new GetRightResponseDto
            {
                Rights = rightsRes.Distinct().ToList(),
                UserId = data.UserId,
                DestId = data.DestId,
            };
            return Task.FromResult(dto);
        }

        //public async Task<HoldRightsDto> CreateHold(HoldDto data)
        //{
        //    var type = _context.Types
        //        .Where(t => t.Title == data.Type)
        //        .FirstOrDefault();

        //    User? user = new User();
        //    Group? group = new Group();

        //    bool dataHolderType = data.HolderType.ToLower() == "user";
        //    if (dataHolderType)
        //    {
        //        user = _context.Users
        //            .Where(u => u.Id == data.HolderTypeId)
        //            .FirstOrDefault();
        //    } else
        //    {
        //        group = _context.Groups
        //            .Where(g => g.Id == data.HolderTypeId)
        //            .FirstOrDefault();
        //    }

        //    var rights = new List<Right>();
        //    bool dataType = data.Type.ToLower() == "process";

        //    if (dataType && dataHolderType)
        //    {
        //        rights = _context.Rights
        //            .Where(r => r.Title == "modification" || r.Title == "deletion")
        //            .ToList();
        //    } else if (dataType && !dataHolderType)
        //    {
        //        rights = _context.Rights
        //            .Where(r => r.Title == "reading")
        //            .ToList();
        //    } else
        //    {
        //        rights = _context.Rights
        //            .Where(r => r.Title == "matching" || r.Title == "commenting")
        //            .ToList();
        //    }

        //    var hold = new Hold
        //    {
        //        DestId = data.DestId,
        //        Type = type,
        //        Users = dataHolderType ? new List<User>() { user } : new List<User>(),
        //        Groups = dataHolderType ? new List<Group>() : new List<Group>() { group },
        //        Rights = rights,
        //    };

        //    _context.Holds.Add(hold);
        //    _context.SaveChanges();

        //    var dto  = new HoldRightsDto
        //    {
        //        DestId = data.DestId,
        //        Rights = rights.Select(r => r.Title).ToList(),
        //        User = user.LongName,
        //        Group = group.Title,
        //    };

        //    return dto;
        //}

        //public Task<List<HoldRightsDto>> GetHoldIdsAndRights(GetHoldsRequestDto loginType)
        //{
        //    var dict = new HashSet<int?>();
        //    var res = new HashSet<HoldRightsDto>();
        //    var user = _context.Users.FirstOrDefault(u => u.Email == loginType.Email);
        //    var type = _context.Types.FirstOrDefault(t => t.Title == loginType.Type);
        //    if (user != null && type != null)
        //    {
        //        var holdsIds = _context.UserHoldMappers
        //            .Include(uh => uh.Hold)
        //            .Where(uh => uh.UserId == user.Id && uh.Hold != null && uh.Hold.TypeId == type.Id)
        //            .Select(uh => uh.Hold.DestId)
        //            .ToList();


        //        foreach (var id in holdsIds)
        //        {
        //            if (!dict.Contains(id))
        //            {
        //                var rights = _context.RightHoldMappers
        //                .Include(rh => rh.Right)
        //                .Where(hr => hr.Hold != null && hr.Hold.DestId == id && hr.Right != null)
        //                .Select(hr => hr.Right.Title)
        //                .ToList();
        //                dict.Add(id);
        //                var dto = new HoldRightsDto
        //                {
        //                    DestId = (int)id,
        //                    Rights = rights.Distinct().ToList(),
        //                    User = user.LongName,
        //                };
        //                res.Add(dto);
        //            }
        //        }

        //        var groupsIds = _context.UserGroupMappers
        //            .Where(ug => ug.UserId == user.Id)
        //            .Select(ug => ug.GroupId)
        //            .ToList();


        //        foreach (var gid in groupsIds)
        //        {
        //            var holdIds = _context.GroupHoldMappers
        //                .Include(gh => gh.Hold)
        //                .Where(gh => gh.GroupId == gid && gh.Hold != null && gh.Hold.TypeId == type.Id)
        //                .Select(gh => gh.Hold.DestId) 
        //                .ToList();

        //            //Console.WriteLine($"ЧИЧ \n\n\n\n{holdIds.Count}");

        //            var group = _context.Groups
        //                .Where(g => g.Id == gid)
        //                .FirstOrDefault();

        //            foreach (var id in holdIds)
        //            {

        //                if (!dict.Contains(id))
        //                {
        //                    var rights = _context.RightHoldMappers
        //                        .Where(hr => hr.Hold != null && hr.Hold.DestId == id && hr.Right != null)
        //                        .Select(hr => hr.Right.Title)
        //                        .ToList();
        //                    dict.Add(id);
        //                    var dto = new HoldRightsDto
        //                    {
        //                        DestId = (int)id,
        //                        Rights = rights.Distinct().ToList(),
        //                        Group = group.Title,
        //                    };
        //                    if (loginType.Type == "Stage" && user.Id == group.BossId)
        //                    {
        //                        dto.Rights.Add("signing");   
        //                    }
        //                    res.Add(dto);
        //                }
        //            }
        //        }

        //        return Task.FromResult(res.ToList());
        //    }
        //    return Task.FromResult<List<HoldRightsDto>>(null);
        //}

        //public Task<UsersGroupsDto> GetUsersGroupsByHold(GetUserByHoldDto data)
        //{
        //    var holds = _context.Holds
        //        .Where(h => h.DestId == data.DestId && h.Type.Title.ToLower() == data.Type.ToLower())
        //        .ToList();

        //    var users = new List<UserDto>();
        //    var groups = new List<GroupDto>();


        //    foreach (var h in holds)
        //    {
        //        var iUsers = _context.UserHoldMappers
        //            .Where(uh => uh.Hold == h)
        //            .Select(uh => uh.User)
        //            .ToList();


        //        foreach (var user in iUsers)
        //        {
        //            var iDto = new UserDto
        //            {
        //                Id = user.Id,
        //                Email = user.Email,
        //                LongName = user.LongName,
        //                ShortName = user.ShortName,
        //            };
        //            if (users.Find(i => i.Id == user.Id) == null)
        //            {
        //                users.Add(iDto);
        //            }
        //        }

        //        var iGroups = _context.GroupHoldMappers
        //            .Where(uh => uh.Hold == h)
        //            .Select(uh => uh.Group)
        //            .ToList();

        //        foreach (var group in iGroups)
        //        {
        //            var iDto = new GroupDto
        //            {
        //                Id = group.Id,
        //                Title = group.Title
        //            };
        //            if (groups.Find(i => i.Id == group.Id) == null)
        //            {
        //                groups.Add(iDto);
        //            }
        //        }
        //    }
        //    var dto = new UsersGroupsDto
        //    {
        //        Users = users,
        //        Groups = groups,
        //    };
        //    return Task.FromResult(dto);
        //}
    }
}
