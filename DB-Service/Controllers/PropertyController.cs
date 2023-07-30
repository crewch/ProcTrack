using DB_Service.Dtos;
using DB_Service.Models;
using DB_Service.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DB_Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors("cors")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly ILogService _logService;

        public PropertyController(IPropertyService propertyService,
                                  ILogService logService)
        {
            _propertyService = propertyService;
            _logService = logService;
        }

        [Route("Templates")]
        [HttpGet]
        public async Task<ActionResult<List<ProcessDto>>> GetTemplates()
        {
            var res = await _propertyService.GetTemplates();
            return Ok(res);
        }

        [Route("Priorities")]
        [HttpGet]
        public async Task<ActionResult<List<string>>> GetPriorities()
        {
            var res = await _propertyService.GetPriorities();
            return Ok(res);
        }

        [Route("Logs")]
        [HttpGet]
        public async Task<ActionResult<List<Log>>> GetLogs()
        {
            var res = await _logService.GetLogs();
            return Ok(res);
        } 
    }
}
