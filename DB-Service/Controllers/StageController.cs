using DB_Service.Dtos;
using DB_Service.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DB_Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    // [EnableCors("cors")]
    [EnableCors]
    public class StageController : ControllerBase
    {
        private readonly IStageService _service;
        
        public StageController(IStageService service)
        {
            _service = service;
        }

        [Route("Get")]
        [HttpGet]
        public async Task<ActionResult<List<StageDto>>> GetStages(int UserId)
        {
            var res = await _service.GetStagesByUserId(UserId);
            return Ok(res);
        }

        [Route("{Id}/Assign")]
        [HttpGet]
        public async Task<ActionResult<StageDto>> AssignStage(int UserId, int Id)
        {
            var res = await _service.AssignStage(UserId, Id);
            return Ok(res);
        }
        [Route("{Id}/Cancel")]
        [HttpGet]
        public async Task<ActionResult<StageDto>> CancleStage(int UserId, int Id)
        {
            var res = await _service.CancelStageById(Id, UserId);
            return Ok(res);
        }

        [Route("{Id}/Update")]
        [HttpPut]
        public async Task<ActionResult<StageDto>> UpdateStage(int UserId, int Id, StageDto data)
        {
            var res = await _service.UpdateStage(UserId, Id, data);
            return Ok(res);
        }

        [Route("{Id}")]
        [HttpGet]
        public async Task<ActionResult<StageDto>> GetStageById(int Id)
        {
            var res = await _service.GetStageById(Id);
            return Ok(res);
        }

        [Route("{Id}/Tasks")]
        [HttpGet]
        public async Task<ActionResult<List<TaskDto>>> GetTasksByStageId(int Id)
        {
            var res = await _service.GetTasksByStageId(Id);
            return Ok(res);
        }
    }
}
