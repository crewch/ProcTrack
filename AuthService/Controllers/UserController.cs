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
            return Ok(res);
        }

        //[Route("userWithRoles")]
        //[HttpPost]
        //public async Task<ActionResult<UserWithRoles>> GetUser(UserEmailDto data)
        //{
        //    var res = await _service.GetUserByLogin(data);
        //    return res;
        //}
    }
}
