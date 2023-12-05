using AuthService.Data;
using AuthService.Dtos.Group;
using AuthService.Exceptions;
using AuthService.Models;
using AuthService.Services.User;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;

namespace AuthService.Services.Group
{
    public class GroupService : IGroupService
    {
        private readonly AuthContext _context;
        private readonly IUserService _userService;
        public GroupService(AuthContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<int> AddUser(int GroupId, int UserId)
        {
            try
            {
                var group = await Exist(GroupId);
                var user = await _userService.Exist(UserId);

                var exist = await _context.UserGroupMappers
                    .Where(m => m.GroupId == group.Id && m.UserId == user.Id)
                    .Select(m => m.GroupId == group.Id && m.UserId == user.Id)
                    .FirstOrDefaultAsync();

                if (exist)
                {
                    return group.Id;
                }

                var userGroup = new Models.UserGroupMapper()
                {
                    GroupId = group.Id,
                    UserId = user.Id,
                };

                await _context.UserGroupMappers.AddAsync(userGroup);
                await _context.SaveChangesAsync();

                return group.Id;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<int> Create(string Title, int BossUserId)
        {
            try
            {
                var user = await _userService.Exist(BossUserId);

                var group = new Models.Group()
                {
                    BossId = user.Id,
                    Title = Title,
                };
                await _context.Groups.AddAsync(group);
                await _context.SaveChangesAsync();

                return group.Id;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<Models.Group> Exist(int GroupId)
        {
            var group = await _context.Groups
                .Where(h => h.Id == GroupId)
                .FirstOrDefaultAsync();

            if (group == null)
            {
                throw new NotFoundException($"Group {GroupId} not exist");
            }

            return group;
        }

        public async Task<GroupDto> Get(int GroupId)
        {
            try
            {
                var group = await Exist(GroupId);
                var groupDto = new GroupDto()
                {
                    Id = group.Id,
                    Title = group.Title,
                    BossUserId = group.BossId
                };
                return groupDto;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
            throw new NotImplementedException();
        }

        public async Task<List<int>> Users(int GroupId)
        {
            try
            {
                var group = await Exist(GroupId);

                return await _context.UserGroupMappers
                    .Where(m => m.GroupId == group.Id)
                    .Select(m => m.UserId ?? 0)
                    .ToListAsync();
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
