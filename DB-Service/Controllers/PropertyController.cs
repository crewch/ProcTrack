﻿using DB_Service.Dtos;
using DB_Service.Models;
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

        [Route("ProcessStatuses")]
        [HttpGet]
        public async Task<ActionResult<List<string>>> GetProcessStatuses()
        {
            var res = await _propertyService.GetProcessStatuses();
            return Ok(res);
        }

        [Route("StageStatuses")]
        [HttpGet]
        public async Task<ActionResult<List<string>>> GetStageStatuses()
        {
            var res = await _propertyService.GetStageStatuses();
            return Ok(res);
        }

        [Route("Types")]
        [HttpGet]
        public async Task<ActionResult<List<string>>> GetTypes()
        {
            var res = await _propertyService.GetTypes();
            return Ok(res);
        }
    }
}
