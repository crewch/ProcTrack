using AuthService.Dtos;
using AuthService.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    [EnableCors("cors")]
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
            var res = await _service.GetUserById(id);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [Route("Groups")]
        [HttpGet]
        public async Task<ActionResult<List<GroupDto>>> GetGroups()
        {
            var res = await _service.GetGroups();
            return Ok(res);
        }
        
    }
}
