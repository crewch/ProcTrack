using AuthService.Exceptions;
using AuthService.Services.Group;
using AuthService.Services.Hold;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuthService.Services.User;
using AuthService.Dtos.User;
using AuthService.Dtos.Group;

namespace AuthService.Controllers.HTTP
{
    [Route("[controller]/")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IUserService _userService;

        public GroupController(IGroupService groupService, IUserService userService)
        {
            _groupService = groupService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<GroupDto>> Create(CreateGroupRequestDto data)
        {
            try
            {
                var groupId = await _groupService.Create(data.Title, data.BossUserId);
                var group = await _groupService.Get(groupId);
                return Ok(group);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{GroupId}")]
        [HttpGet]
        public async Task<ActionResult<GroupDto>> Get(int GroupId)
        {
            try
            {
                var group = await _groupService.Get(GroupId);
                return Ok(group);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{GroupId}/user")]
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> Users(int GroupId)
        {
            try
            {
                var userIds = await _groupService.Users(GroupId);
                var users = new List<UserDto>();
                foreach (var user in userIds)
                {
                    users.Add(await _userService.Get(user));
                }
                return Ok(users);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("{GroupId}/user")]
        [HttpPost]
        public async Task<ActionResult<GroupDto>> AddUser(int GroupId, AddUserToGroupRequestDto data)
        {
            try
            {
                var groupId = await _groupService.AddUser(GroupId, data.UsertId);
                var group = await _groupService.Get(groupId);
                return Ok(group);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
