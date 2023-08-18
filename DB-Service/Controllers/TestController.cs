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
using Microsoft.AspNetCore.Authorization;

namespace DB_Service.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    // [EnableCors("cors")]
    [EnableCors]

    public class TestController : ControllerBase
    {
        private readonly IMailDataClient _client;

        private readonly ITestDataService _testDataService;

        public TestController(IMailDataClient client, ITestDataService testDataService)
        {
            _client = client;
            _testDataService = testDataService;
        }

        [Route("AddTestData")]
        [HttpGet]
        public async Task<ActionResult> CreateTestData()
        {
            await _testDataService.CreateTestData();
            return Ok();
        }

        [Route("SendMail")]
        [HttpPost]
        [Authorize]

        public async Task<ActionResult<MailDto>> SendMail(MailDto data)
        {
            var res = await _client.SendMail(data);
            return Ok(res);
        }

    }
}
