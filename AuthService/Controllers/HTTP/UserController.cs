using AuthService.Dtos.Hold;
using AuthService.Dtos.User;
using AuthService.Exceptions;
using AuthService.Models;
using AuthService.Services.Hold;
using AuthService.Services.Role;
using AuthService.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers.HTTP
{
    [Route("[controller]/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUserService _userService;
        readonly IHoldService _holdService;
        readonly IRoleService _roleService;

        public UserController(IUserService userService, IHoldService holdService, IRoleService roleService)
        {
            _userService = userService;
            _holdService = holdService;
            _roleService = roleService;
        }

        [Route("{UserId}/hold/{Type}")]
        [HttpGet]
        public async Task<ActionResult<List<HoldDto>>> Holds(int UserId, string Type) 
        {
            try
            {
                var holdIds = await _userService.Holds(Type, UserId);
                var holds = await Task.WhenAll(holdIds.Select(async id => await _holdService.Get(id)));
                return Ok(holds.ToList());
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
        [HttpPost]
        public async Task<ActionResult<UserDto>> Create(CreateUserRequestDto data)
        {
            try
            {
                var userId = await _userService.Create(data.Email, data.LongName, data.ShortName);
                var user = await _userService.Get(userId);
                return Ok(user);
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
        [Route("{UserId}")]
        [HttpGet]
        public async Task<ActionResult<UserDto>> Get(int UserId)
        {
            try
            {
                var user = await _userService.Get(UserId);
                return Ok(user);
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
        [Route("{UserId}/role")]
        [HttpPost]
        public async Task<ActionResult<UserDto>> AddRole(AddRoleToUserRequestDto data, int UserId)
        {
            try
            {
                var roleId = await _roleService.Find(data.Role);
                var userId = await _userService.AddRole(UserId, roleId);
                var user = await _userService.Get(userId);

                return Ok(user);
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
