using AuthService.Dtos;
using AuthService.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace AuthService.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    [EnableCors("cors")]
    public class HoldController : ControllerBase
    {
        private readonly IHoldService _service;

        public HoldController(IHoldService service)
        {
            _service = service;
        }

        [Route("get")]
        [HttpPost]
        public async Task<ActionResult<List<HoldDto>>> GetHolds(UserHoldTypeDto data)
        {
            var res = await _service.GetHolds(data);
            return Ok(res);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<HoldDto>> GetHoldById(int id)
        {
            var res = await _service.GetHoldById(id);
            return Ok(res);
        }

        [Route("create")]
        [HttpPost]
        public async Task<ActionResult<CreateHoldResponceDto>> CreateHold(CreateHoldRequestDto data)
        {
            var res = await _service.CreateHold(data);
            return Ok(res);
        }

        //[HttpPost]
        //public async Task<ActionResult<List<HoldRightsDto>>> GetRightsHolds(LoginTypeDto loginType)
        //{
        //    var res = _service.GetHoldIdsAndRights(loginType);
        //    if (res == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(res.Result);
        //}

        //[Route("CreateHold")]
        //[HttpPost]
        //public async Task<ActionResult<HoldRightsDto>> CreateHold(HoldDto data)
        //{
        //    var res = _service.CreateHold(data);
        //    return Ok(res.Result);
        //}

        //[Route("UsersGroups")]
        //[HttpPost]
        //public async Task<ActionResult<UsersGroupsDto>> GetUsersGroups(GetUserByHoldDto data)
        //{
        //    var res = _service.GetUsersGroupsByHold(data);
        //    return Ok(res.Result);
        //}

    }
}
