using AuthService.Exceptions;
using AuthService.Services.Hold;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers.HTTP
{
    [Route("[controller]/")]
    [ApiController]
    // [EnableCors("cors")]
    [EnableCors]
    public class RightController : ControllerBase
    {
        private readonly IHoldService _holdService;

        public RightController(IHoldService holdService)
        {
            _holdService = holdService;
        }

        [Route("user/{UserId}/{Type}/{DestId}")]
        [HttpGet]
        public async Task<ActionResult<List<string>>> UserRights(int UserId, string Type, int DestId)
        {
            try
            {
                var holdId = await _holdService.Find(Type, DestId);
                var rights = await _holdService.UserRights(holdId, UserId);
                return rights;
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
