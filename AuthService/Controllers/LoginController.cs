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
            try
            {
                var res = await _service.Authorize(data);
                return Ok(res);
            } catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            } catch (UnauthorizedException ex)
            {
                return Unauthorized(ex.Message);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("refresh")]
        [HttpPost]
        public async Task<ActionResult<TokenDto>> RefreshToken(TokenDto data)
        {
            try
            {
                var res = await _service.RefreshToken(data);
                return Ok(res);
            } catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            } catch (UnauthorizedException ex)
            {
                return Unauthorized(ex.Message);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
