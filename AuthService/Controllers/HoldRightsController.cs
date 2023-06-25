using AuthService.Dtos;
using AuthService.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace AuthService.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [EnableCors("cors")]
    public class HoldRightsController : ControllerBase
    {
        private readonly IHoldService _service;

        public HoldRightsController(IHoldService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<List<HoldRightsDto>>> GetRightsHolds(LoginTypeDto loginType)
        {
            var res = _service.GetHoldIdsAndRights(loginType);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res.Result);
        }
    }
}
