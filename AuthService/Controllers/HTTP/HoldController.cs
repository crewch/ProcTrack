using AuthService.Exceptions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using AuthService.Services.Hold;
using AuthService.Dtos.Hold;
using AuthService.Models;


namespace AuthService.Controllers.HTTP
{
    [Route("[controller]/")]
    [ApiController]
    // [EnableCors("cors")]
    [EnableCors]
    public class HoldController : ControllerBase
    {
        private readonly IHoldService _service;

        public HoldController(IHoldService service)
        {
            _service = service;
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<HoldDto>> GetHold(int id)
        {
            try
            {
                return Ok(await _service.Get(id));
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

        [HttpPost]
        public async Task<ActionResult<HoldDto>> CreateHold(CreateHoldRequestDto data)
        {
            try
            {
                var holdId = await _service.Create(data.Type, data.DestId);
                return Ok(await _service.Get(holdId));
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

        [Route("{id}/user")]
        [HttpPost]
        public async Task<ActionResult<HoldDto>> AddUserToHold(AddUserToHoldRequestDto data, int id)
        {
            try
            {
                var holdId = await _service.AddUser(id, data.UserId, data.StatusId);
                return Ok(await _service.Get(holdId));
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

        [Route("{id}/group")]
        [HttpPost]
        public async Task<ActionResult<HoldDto>> AddGroupToHold(AddGroupToHoldRequestDto data, int id)
        {
            try
            {
                var holdId = await _service.AddGroup(id, data.GroupId, data.StatusMemberId, data.StatusBossId);
                return Ok(await _service.Get(holdId));
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

        [Route("{id}/user")]
        [HttpGet]
        public async Task<ActionResult<List<UserHoldDto>>> Users(int id)
        {
            try
            {
                var userIds = await _service.Users(id);
                return Ok(userIds);
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

        [Route("{id}/group")]
        [HttpGet]
        public async Task<ActionResult<List<GroupHoldDto>>> Groups(int id)
        {
            try
            {
                var groupIds = await _service.Groups(id);
                return Ok(groupIds);
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

        [Route("{HoldId}/copy/{NewHoldId}")]
        [HttpGet] //TODO: Put
        public async Task<ActionResult<HoldDto>> Copy(int HoldId, int NewHoldId)
        {
            try
            {
                var holdId = await _service.Copy(HoldId, NewHoldId);
                return Ok(await _service.Get(holdId));
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

        [Route("{Type}/{DestId}")]
        [HttpGet]
        public async Task<ActionResult<HoldDto>> Find(string Type, int DestId)
        {
            try
            {
                var holdId = await _service.Find(Type, DestId);
                return Ok(await _service.Get(holdId));
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
