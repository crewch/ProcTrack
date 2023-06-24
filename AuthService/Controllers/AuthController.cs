using AuthService.Models;
using AuthService.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [EnableCors("cors")]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _service;

        public AuthController(ILoginService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<UserToken> Login(UserLogin userLogin)
        {
            var res = await _service.Login(userLogin);
            return res;
        }
    }
}
