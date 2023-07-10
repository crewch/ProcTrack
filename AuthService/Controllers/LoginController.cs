using AuthService.Dtos;
using AuthService.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    [EnableCors("cors")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _service;

        public LoginController(ILoginService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Authorize(AuthDto data)
        {
            var res = await _service.Authorize(data);
            return Ok(res);
        }

        [Route("GetEnv")]
        [HttpGet]
        public async Task<ActionResult<EnvDto>> GetEnv()
        {
            var res = await _service.GetEnv();
            return Ok(res);
        }

    }
}
