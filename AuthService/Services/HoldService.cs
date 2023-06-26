using AuthService.Data;
using AuthService.Dtos;
using AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Services
{
    public class HoldService : IHoldService
    {
        private readonly AuthContext _context;

        public HoldService(AuthContext context)
        {
            _context = context;
        }

        public async Task<HoldRightsDto> CreateHold(CreateHoldDto data)
        {
            var type = _context.Types
                .Where(t => t.Title == data.Type)
                .FirstOrDefault();

            User? user = new User();
            Group? group = new Group();

            bool dataHolderType = data.HolderType.ToLower() == "user";
            if (dataHolderType)
            {
                user = _context.Users
                    .Where(u => u.Id == data.HolderTypeId)
                    .FirstOrDefault();
            } else
            {
                group = _context.Groups
                    .Where(g => g.Id == data.HolderTypeId)
                    .FirstOrDefault();
            }
            
            var rights = new List<Right>();
            bool dataType = data.Type.ToLower() == "process";

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
            return new HoldRightsDto
            {
                HoldId = data.DestId,
                Rights = rights.Select(r => r.Title).ToList(),
                User = user.LongName,
                Group = group.Title,
            };
        }

        public Task<List<HoldRightsDto>> GetHoldIdsAndRights(LoginTypeDto loginType)
        {
            var dict = new HashSet<int?>();
            var res = new HashSet<HoldRightsDto>();
            var user = _context.Users.FirstOrDefault(u => u.Email == loginType.Email);
            var type = _context.Types.FirstOrDefault(t => t.Title == loginType.Type);
            if (user != null && type != null)
            {
                var holdsIds = _context.UserHoldMappers
                    .Where(uh => uh.UserId == user.Id && uh.Hold != null && uh.Hold.TypeId == type.Id)
                    .Select(uh => uh.Hold.DestId)
                    .ToList();


                foreach (var id in holdsIds)
                {
                    if (!dict.Contains(id))
                    {
                        var rights = _context.RightHoldMappers
                        .Where(hr => hr.Hold != null && hr.Hold.DestId == id && hr.Right != null)
                        .Select(hr => hr.Right.Title)
                        .ToList();
                        dict.Add(id);
                        var dto = new HoldRightsDto
                        {
                            HoldId = (int)id,
                            Rights = rights.Distinct().ToList(),
                            User = user.LongName,
                        };
                        res.Add(dto);
                    }
                }

                var groupsIds = _context.UserGroupMappers
                    .Where(ug => ug.UserId == user.Id)
                    .Select(ug => ug.GroupId)
                    .ToList();

                foreach (var gid in groupsIds)
                {
                    var holdIds = _context.GroupHoldMappers
                        .Where(gh => gh.GroupId == gid && gh.Hold != null && gh.Hold.TypeId == type.Id)
                        .Select(gh => gh.Hold.DestId) 
                        .ToList();
                    //Console.WriteLine(gid);
                    var group = _context.Groups
                        .Where(g => g.Id == gid)
                        .FirstOrDefault();
                    foreach (var id in holdIds)
                    {
                        if (!dict.Contains(id))
                        {
                            var rights = _context.RightHoldMappers
                                .Where(hr => hr.Hold != null && hr.Hold.DestId == id && hr.Right != null)
                                .Select(hr => hr.Right.Title)
                                .ToList();
                            dict.Add(id);
                            var dto = new HoldRightsDto
                            {
                                HoldId = (int)id,
                                Rights = rights.Distinct().ToList(),
                                Group = group.Title,
                            };
                            if (loginType.Type == "Stage" && user.Id == group.BossId)
                            {
                                dto.Rights.Add("signing");   
                            }
                            res.Add(dto);
                        }
                    }
                }

                return Task.FromResult(res.ToList());
            }
            return Task.FromResult<List<HoldRightsDto>>(null);
        }

        public Task<UsersGroupsDto> GetUsersGroupsByHold(GetUserByHoldDto data)
        {
            var holds = _context.Holds
                .Where(h => h.DestId == data.DestId && h.Type.Title.ToLower() == data.Type.ToLower())
                .ToList();

            var users = new List<UserDto>();
            var groups = new List<GroupDto>();
            Console.WriteLine($"Count : {holds.Count}");
            foreach (var h in holds)
            {
                var iUsers = _context.UserHoldMappers
                    .Where(uh => uh.Hold == h)
                    .Select(uh => uh.User)
                    .ToList();
                Console.WriteLine($"Count users: {iUsers.Count}");
                foreach (var user in iUsers)
                {
                    var iDto = new UserDto
                    {
                        Id = user.Id,
                        Email = user.Email,
                    };
                    if (users.Find(i => i.Id == user.Id) == null)
                    {
                        users.Add(iDto);
                    }
                }
                var iGroups = _context.GroupHoldMappers
                    .Where(uh => uh.Hold == h)
                    .Select(uh => uh.Group)
                    .ToList();
                Console.WriteLine($"Count group: {iGroups.Count}");
                foreach (var group in iGroups)
                {
                    var iDto = new GroupDto
                    {
                        Id = group.Id,
                        Title = group.Title
                    };
                    if (groups.Find(i => i.Id == group.Id) == null)
                    {
                        groups.Add(iDto);
                    }
                }
            }
            var dto = new UsersGroupsDto
            {
                Users = users,
                Groups = groups,
            };
            return Task.FromResult(dto);
        }
    }
}
