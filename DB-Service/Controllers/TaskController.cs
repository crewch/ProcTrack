using DB_Service.Dtos;
using DB_Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DB_Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _service;

        public TaskController(ITaskService service)
        {
            _service = service;
        }

        [Route("{Id}/Assign")]
        [HttpGet]
        public async Task<ActionResult<TaskDto>> AssignTask(int UserId, int Id)
        {
            var res = await _service.AssignTask(UserId, Id);
            return Ok(res);
        }

        [Route("{Id}")]
        [HttpGet]
        public async Task<ActionResult<TaskDto>> GetTaskById(int Id)
        {
            var res = await _service.GetTaskById(Id);
            return Ok(res);
        }

        [Route("{Id}/Start")]
        [HttpGet]
        public async Task<ActionResult<TaskDto>> StartTask(int UserId, int Id)
        {
            var res = await _service.StartTask(UserId, Id);
            return Ok(res);
        }

        [Route("{Id}/Stop")]
        [HttpGet]
        public async Task<ActionResult<TaskDto>> StopTask(int UserId, int Id)
        {
            var res = await _service.StopTask(UserId, Id);
            return Ok(res);
        }

        [Route("{Id}/Comment/Get")]
        [HttpGet]
        public async Task<ActionResult<List<CommentDto>>> GetComments(int Id)
        {
            var res = await _service.GetComentsByTaskId(Id);
            return Ok(res);
        }

        [Route("{Id}/Comments/Create")]
        [HttpPost]
        public async Task<CommentDto> CreateComment(int Id, CommentDto data)
        {
            var res = await _service.CreateCommment(Id, data);
            return res;
        }
    }
}
