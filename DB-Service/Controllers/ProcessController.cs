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

    public class ProcessController : ControllerBase
    {
        private readonly IProcessService _service;

        public ProcessController(IProcessService service)
        {
            _service = service;
        }

        [Route("Get")]
        [HttpPost]
        public async Task<ActionResult<List<ProcessDto>>> GetProcessesByUserId(FilterProcessDto filter, int limit, int offset)
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

            var res = await _service.GetProcesesByUserId(UserId, filter, limit, offset);
            return Ok(res);
        }

        [Route("Create")]
        [HttpPost]
        public async Task<ActionResult<ProcessDto>> CreateProcess(CreateProcessDto data)
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

            var res = await _service.CreateProcess(data, UserId);
            return Ok(res);
        }

        [Route("{Id}/Update")]
        [HttpPut]
        public async Task<ActionResult<ProcessDto>> UpdateProcess(ProcessDto data, int Id)
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

            var res = await _service.UpdateProcess(data, UserId, Id);
            return Ok(res);
        }

        [Route("{Id}")]
        [HttpGet]
        public async Task<ActionResult<ProcessDto>> GetProcessById(int Id)
        {
            var res = await _service.GetProcessById(Id);
            return Ok(res);
        }

        [Route("{Id}/Stage")]
        [HttpGet]
        public async Task<ActionResult<List<StageDto>>> GetStagesByProcessId(int Id)
        {
            var res = await _service.GetStagesByProcessId(Id);
            return Ok(res);
        }

        [Route("{Id}/Links")]
        [HttpGet]
        public async Task<ActionResult<List<LinkDto>>> GetLinksByProcessId(int Id)
        {
            var res = await _service.GetLinksByProcessId(Id);
            return Ok(res);
        }

        [Route("{Id}/Start")]
        [HttpGet]
        public async Task<ActionResult<ProcessDto>> StartProcess(int Id)
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

            var res = await _service.StartProcess(UserId, Id);
            return Ok(res);
        }

        [Route("{Id}/Stop")]
        [HttpGet]
        public async Task<ActionResult<ProcessDto>> StopProcess(int Id)
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

            var res = await _service.StopProcess(UserId, Id);
            return Ok(res);
        }

        [Route("{Id}/Passport")]
        [HttpPost]
        public async Task<ActionResult<PassportDto>> CreatePassport(CreatePassportDto data, int Id)
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

            var res = await _service.CreatePassport(data, UserId, Id);
            return Ok(res);
        }

        [Route("{Id}/Passport")]
        [HttpGet]
        public async Task<ActionResult<List<PassportDto>>> GetPassports(int Id)
        {
            var res = await _service.GetPassports(Id);
            return Ok(res);
        }
        [Route("template/create")]
        [HttpPost]
        public async Task<ActionResult<ProcessDto>> CreateTemplate(TemplateDto data)
        {
            var res = await _service.CreateTemplate(data);
            return Ok(res);
        }

        [Route("{StageId}/Process")]
        [HttpGet]
        public async Task<ActionResult<ProcessDto>> GetProcessByStageId(int StageId)
        {
            var res = await _service.GetProcessByStageId(StageId);
            return Ok(res);
        }

        [Route("Count")]
        [HttpPost]
        public async Task<ActionResult<int>> ProcessCount(FilterProcessDto filter)
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

            var res = await _service.GetProcessCount(UserId, filter);
            return Ok(res);
        }
    }
}
