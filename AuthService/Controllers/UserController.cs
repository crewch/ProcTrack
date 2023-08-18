using AuthService.Dtos;
using AuthService.Exceptions;
using AuthService.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    [EnableCors("cors")]
    // [EnableCors]
    public class UserController : Controller
    {
        private readonly IUserService _service;
        
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            try
            {
                var res = await _service.GetUserById(id);
                return Ok(res);
            } catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Groups")]
        [HttpGet]
        public async Task<ActionResult<List<GroupDto>>> GetGroups()
        {
            try
            {
                var res = await _service.GetGroups();
                return Ok(res);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Groups/Create")]
        [HttpPost]
        public async Task<ActionResult<GroupDto>> CreateGroup(CreateGroupDto data)
        {
            try
            {
                var res = await _service.AddGroup(data);
                return Ok(res);
            } catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Roles/Create")]
        [HttpPost]
        public async Task<ActionResult<string>> CreateRole(string data)
        {
            try
            {
                var res = await _service.AddRole(data);
                return Ok(res);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 

        [Route("Create")]
        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(UserDto data)
        {
            try
            {
                var res = await _service.AddUser(data);
                return Ok(res);
            } catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Groups/{GroupId}/AddUser")]
        [HttpPost]
        public async Task<ActionResult<List<UserDto>>> AddUsersToGroup(int GroupId, List<UserDto> data)
        {
            try
            {
                var res = await _service.AddUsersToGroup(GroupId, data);
                return Ok(res);
            } catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Route("Groups/{GroupId}/Users")]
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsersByGroupId(int GroupId)
        {
            try
            {
                var res = await _service.GetUsersByGroupId(GroupId);
                return Ok(res);
            } catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
