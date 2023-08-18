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
    // [EnableCors("cors")]
    [EnableCors]
    [Authorize]

    public class TaskController : ControllerBase
    {
        private readonly ITaskService _service;

        public TaskController(ITaskService service)
        {
            _service = service;
        }

        [Route("{Id}/Assign")]
        [HttpGet]
        public async Task<ActionResult<TaskDto>> AssignTask(int Id)
        {
            var token = Request.Headers["Authorization"].ToString().Split(' ')[1];

            var handler = new JwtSecurityTokenHandler();
            var parsedToken = handler.ReadToken(token) as JwtSecurityToken;

            bool validated = parsedToken.ValidTo > DateTime.Now;

            if (!validated)
            {
                return Unauthorized();
            }

            int UserId = int.Parse(parsedToken.Claims
                .Where(c => c.Type == ClaimTypes.Sid)
                .FirstOrDefault()?.ToString().Split(" ")[1]);

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
        public async Task<ActionResult<TaskDto>> StartTask(int Id)
        {
            var token = Request.Headers["Authorization"].ToString().Split(' ')[1];

            var handler = new JwtSecurityTokenHandler();
            var parsedToken = handler.ReadToken(token) as JwtSecurityToken;

            bool validated = parsedToken.ValidTo > DateTime.Now;

            if (!validated)
            {
                return Unauthorized();
            }

            int UserId = int.Parse(parsedToken.Claims
                .Where(c => c.Type == ClaimTypes.Sid)
                .FirstOrDefault()?.ToString().Split(" ")[1]);

            var res = await _service.StartTask(UserId, Id);
            return Ok(res);
        }

        [Route("{Id}/Stop")]
        [HttpGet]
        public async Task<ActionResult<TaskDto>> StopTask(int Id)
        {
            var token = Request.Headers["Authorization"].ToString().Split(' ')[1];

            var handler = new JwtSecurityTokenHandler();
            var parsedToken = handler.ReadToken(token) as JwtSecurityToken;

            bool validated = parsedToken.ValidTo > DateTime.Now;

            if (!validated)
            {
                return Unauthorized();
            }

            int UserId = int.Parse(parsedToken.Claims
                .Where(c => c.Type == ClaimTypes.Sid)
                .FirstOrDefault()?.ToString().Split(" ")[1]);

            var res = await _service.StopTask(UserId, Id);
            return Ok(res);
        }

        [Route("{Id}/EndVerification")]
        [HttpGet]
        public async Task<ActionResult<TaskDto>> UpdateEndVerification(int Id)
        {
            var token = Request.Headers["Authorization"].ToString().Split(' ')[1];

            var handler = new JwtSecurityTokenHandler();
            var parsedToken = handler.ReadToken(token) as JwtSecurityToken;

            bool validated = parsedToken.ValidTo > DateTime.Now;

            if (!validated)
            {
                return Unauthorized();
            }

            int UserId = int.Parse(parsedToken.Claims
                .Where(c => c.Type == ClaimTypes.Sid)
                .FirstOrDefault()?.ToString().Split(" ")[1]);

            var res = await _service.UpdateEndVerification(UserId, Id);
            return Ok(res);
        }

        [Route("{Id}/Comments/Get")]
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
