using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DB_Service.Models;
using DB_Service.Services;
using DB_Service.Clients.Http;
using DB_Service.Dtos;
using Microsoft.AspNetCore.Cors;

namespace DB_Service.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    [EnableCors("cors")]
    public class TestController : ControllerBase
    {
        private readonly IAuthDataClient _client;

        public TestController(IAuthDataClient client)
        {
            _client = client;
        }

        [Route("GetHolds")]
        [HttpPost]

        public async Task<ActionResult<List<HoldDto>>> GetHolds(UserHoldTypeDto data)
        {
            var res = await _client.GetHolds(data);
            return Ok(res);
        }
        [Route("CreateHold")]
        [HttpPost]
        public async Task<ActionResult<CreateHoldResponceDto>> CreateHold(CreateHoldRequestDto data)
        {
            var res = await _client.CreateHold(data);
            return Ok(res);
        }
        [Route("Hold/{id}")]
        [HttpGet]
        public async Task<ActionResult<HoldDto>> GetHoldById(int id)
        {
            var res = await _client.GetHoldById(id);
            return Ok(res);
        }

    }
}
