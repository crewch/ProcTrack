using AuthService.Dtos;
using AuthService.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [EnableCors("cors")]
    public class UserController : Controller
    {
        private readonly IUserService _service;
        
        public UserController(IUserService service)
        {
            _service = service;
        }

        [Route("userWithRoles")]
        [HttpPost]
        public async Task<ActionResult<UserWithRoles>> GetUser(UserLoginDto data)
        {
            var res = await _service.GetUserByLogin(data);
            return res;
        }
    }
}
