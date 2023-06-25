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
                            Rights = rights,
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
                                Rights = rights,
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
    }
}
