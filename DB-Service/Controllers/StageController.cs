using DB_Service.Dtos;
using DB_Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DB_Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors("cors")]
    // [EnableCors]
    [Authorize]

    public class StageController : ControllerBase
    {
        private readonly IStageService _service;
        
        public StageController(IStageService service)
        {
            _service = service;
        }

        [Route("Get")]
        [HttpPost]
        public async Task<ActionResult<List<StageDto>>> GetStages(FilterStageDto filter, int limit, int offset)
        {
            var token = Request.Headers["Authorization"].ToString().Split(' ')[1];

            int UserId = Tools.TokenHandler.GetIdFromToken(token);

            if (UserId < 0)
            {
                return Unauthorized();
            }

            var res = await _service.GetStagesByUserId(UserId, filter, limit, offset);
            return Ok(res);
        }

        [Route("{Id}/Assign")]
        [HttpGet]
        public async Task<ActionResult<StageDto>> AssignStage(int Id)
        {
            var token = Request.Headers["Authorization"].ToString().Split(' ')[1];

            int UserId = Tools.TokenHandler.GetIdFromToken(token);

            if (UserId < 0)
            {
                return Unauthorized();
            }

            var res = await _service.AssignStage(UserId, Id);
            return Ok(res);
        }
        [Route("{Id}/Cancel")]
        [HttpGet]
        public async Task<ActionResult<StageDto>> CancleStage(int Id)
        {
            var token = Request.Headers["Authorization"].ToString().Split(' ')[1];

            int UserId = Tools.TokenHandler.GetIdFromToken(token);

            if (UserId < 0)
            {
                return Unauthorized();
            }

            var res = await _service.CancelStageById(Id, UserId);
            return Ok(res);
        }

        [Route("{Id}/Update")]
        [HttpPut]
        public async Task<ActionResult<StageDto>> UpdateStage(int Id, StageDto data)
        {
            var token = Request.Headers["Authorization"].ToString().Split(' ')[1];

            int UserId = Tools.TokenHandler.GetIdFromToken(token);

            if (UserId < 0)
            {
                return Unauthorized();
            }

            var res = await _service.UpdateStage(UserId, Id, data);
            return Ok(res);
        }

        [Route("{Id}")]
        [HttpGet]
        public async Task<ActionResult<StageDto>> GetStageById(int Id)
        {
            var token = Request.Headers["Authorization"].ToString().Split(' ')[1];

            int UserId = Tools.TokenHandler.GetIdFromToken(token);

            if (UserId < 0)
            {
                return Unauthorized();
            }

            var res = await _service.GetStageById(Id);
            return Ok(res);
        }

        [Route("{Id}/Tasks")]
        [HttpGet]
        public async Task<ActionResult<List<TaskDto>>> GetTasksByStageId(int Id)
        {
            var token = Request.Headers["Authorization"].ToString().Split(' ')[1];

            int UserId = Tools.TokenHandler.GetIdFromToken(token);

            if (UserId < 0)
            {
                return Unauthorized();
            }

            var res = await _service.GetTasksByStageId(Id);
            return Ok(res);
        }

        [Route("Count")]
        [HttpPost]
        public async Task<ActionResult<int>> StageCount(FilterStageDto filter)
        {
            var token = Request.Headers["Authorization"].ToString().Split(' ')[1];

            int UserId = Tools.TokenHandler.GetIdFromToken(token);

            if (UserId < 0)
            {
                return Unauthorized();
            }

            var res = await _service.GetStageCount(UserId, filter);
            return Ok(res);
        }
    }
}
