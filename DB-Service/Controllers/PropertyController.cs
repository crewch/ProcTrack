using DB_Service.Dtos;
using DB_Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DB_Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _service;

        public PropertyController(IPropertyService service)
        {
            _service = service;
        }

        [Route("Templates")]
        [HttpGet]
        public async Task<ActionResult<List<ProcessDto>>> GetTemplates()
        {
            var res = await _service.GetTemplates();
            return Ok(res);
        }

        [Route("Priorities")]
        [HttpGet]
        public async Task<ActionResult<List<string>>> GetPriorities()
        {
            var res = await _service.GetPriorities();
            return Ok(res);
        }
    }
}
