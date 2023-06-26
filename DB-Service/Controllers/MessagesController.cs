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

namespace DB_Service.Controllers
{
    [Route("api/")]
    [ApiController]
    
    public class MessagesController : ControllerBase
    {
        private readonly IDataService _service;
        private readonly IAuthDataClient _client;

        public MessagesController(IDataService service, IAuthDataClient client)
        {
            _client = client;
            _service = service;
        }


        //[HttpGet]
        //public async Task<ActionResult<List<Message>>> GetMessages()
        //{
        //    var messages = await _messageService.GetMessages();

        //    return messages;
        //}

        //[HttpPost]
        //public async Task<ActionResult<Message>> AddMessage(Message message)
        //{
        //    var newMessage = await _messageService.AddMessage(message);

        //    return newMessage;
        //}

        //[Route("debug")]
        //[HttpGet]
        //public async Task<ActionResult<UserWithRoles>> GetUsersWithRoles()
        //{
        //    var res = await _client.GetUsersWithRoles();
        //    return Ok(res);
        //}

        [Route("RightsHolds")]
        [HttpPost]
        public async Task<ActionResult<HoldRightsDto>> GetRightsHolds(LoginTypeDto loginType)
        {
            var res = await _client.GetRightsHolds(loginType);
            return Ok(res);
        }

        [Route("Stages")]
        [HttpPost]
        public async Task<ActionResult<StagesDataDto>> GetStages(LoginTypeDto loginType)
        {
            var res = await _service.GetStageData(loginType);
            return Ok(res);
        }

        [Route("Processes")]
        [HttpPost]
        public async Task<ActionResult<StagesDataDto>> GetProcesses(LoginTypeDto loginType)
        {
            var res = await _service.GetProcessData(loginType);
            return Ok(res);
        }

        [Route("CreateProcess")]
        [HttpPost]
        public async Task<ActionResult<ProcessesDataDto>> CreateProcess(FromTemplateCreateDto data)
        {
            var res = await _service.CreateProcessFromTemplate(data);
            return Ok(res);
        }
    }
}
