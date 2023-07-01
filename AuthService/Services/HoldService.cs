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

        public Task<List<HoldDto>> GetHolds(UserHoldTypeDto data)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == data.Id);
            var type = _context.Types.FirstOrDefault(t => t.Title == data.HoldType);

            if (user == null || type == null)
            {
                return Task.FromResult<List<HoldDto>>(null);
            }

            var UserHoldIds = _context.UserHoldMappers
                .Include(u => u.Hold)
                .Where(u => u.UserId == user.Id && u.Hold.TypeId == type.Id)
                .Select(u => u.Id)
                .ToList();

            var groupsIds = _context.UserGroupMappers
                .Where(ug => ug.UserId == user.Id)
                .Select(ug => ug.GroupId)
                .ToList();

            var GroupHoldIds = _context.GroupHoldMappers
                .Include(u => u.Hold)
                .Where(u => groupsIds.Contains(u.GroupId) && u.Hold.TypeId == type.Id)
                .Select(u => u.Id)
                .ToList();

            HashSet<int> HoldIds = new HashSet<int>()
                .Concat(UserHoldIds)
                .Concat(GroupHoldIds)
                .ToHashSet();

            var res = new List<HoldDto>();
            foreach (int holdId in HoldIds)
            {
                var hold = _context.Holds
                    .Include(u => u.Type)
                    .Where(u => u.Id == holdId)
                    .FirstOrDefault();

                var users = _context.Users
                    .Include(u => u.Holds).Include(u => u.Roles)
                    .Where(u => u.Holds.Select(u => u.Id).Contains(holdId))
                    .ToList();

                var userDtos = new List<UserDto>();
                foreach (var iUser in users)
                {
                    if (!userDtos.Any(ud => ud.Id == iUser.Id))
                    {
                        var userDto = new UserDto
                        {
                            Id = iUser.Id,
                            Email = iUser.Email,
                            LongName = iUser.LongName,
                            ShortName = iUser.ShortName,
                            Roles = iUser.Roles.Select(u => u.Title).ToList(),
                        };
                        userDtos.Add(userDto);
                    }
                }

                var groups = _context.Groups
                    .Include(u => u.Holds)
                    .Where(u => u.Holds.Select(u => u.Id).Contains(holdId))
                    .ToList();

                var groupDtos = new List<GroupDto>();
                foreach (var iGroup in groups)
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

                var rights = _context.RightHoldMappers
                    .Include(u => u.Right)
                    .Where(u => u.HoldId == holdId)
                    .Select(u => u.Right.Title)
                    .ToList();

                var holdDto = new HoldDto
                {
                    Id = holdId,
                    DestId = hold.DestId,
                    Type = hold.Type.Title,
                    Rights = rights,
                    Users = userDtos,
                    Groups = groupDtos,
                };
                res.Add(holdDto);
            }

            return Task.FromResult(res);
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
