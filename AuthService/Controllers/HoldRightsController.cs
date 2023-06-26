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

        [Route("CreateHold")]
        [HttpPost]
        public async Task<ActionResult<HoldRightsDto>> CreateHold(CreateHoldDto data)
        {
            var res = _service.CreateHold(data);
            return Ok(res.Result);
        }

        [Route("UsersGroups")]
        [HttpPost]
        public async Task<ActionResult<UsersGroupsDto>> GetUsersGroups(GetUserByHoldDto data)
        {
            var res = _service.GetUsersGroupsByHold(data);
            return Ok(res.Result);
        }
    }
}
