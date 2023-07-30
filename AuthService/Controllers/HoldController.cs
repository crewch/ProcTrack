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
            try
            {
                var res = await _service.GetHolds(data);
                return Ok(res);
            } catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<HoldDto>> GetHoldById(int id)
        {
            try
            {
                var res = await _service.GetHoldById(id);
                return Ok(res);
            } catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("create")]
        [HttpPost]
        public async Task<ActionResult<CreateHoldResponceDto>> CreateHold(CreateHoldRequestDto data)
        {
            try
            {
                var res = await _service.CreateHold(data);
                return Ok(res);
            } catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("find")]
        [HttpGet]
        public async Task<ActionResult<List<HoldDto>>> FindHold(int destId, string type)
        {
            try
            {
                var res = await _service.FindHold(destId, type);
                return Ok(res);
            } catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Rights/Get")]
        [HttpPost]
        public async Task<ActionResult<GetRightResponseDto>> GetRights(GetRightRequestDto data)
        {
            try
            {
                var res = await _service.GetRights(data);
                return Ok(res);
            } catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
