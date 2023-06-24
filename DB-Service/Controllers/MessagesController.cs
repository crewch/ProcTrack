using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DB_Service.Models;
using DB_Service.Services;


namespace DB_Service.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    
    public class MessagesController : ControllerBase
    {
        private readonly IDataService _messageService;
        public MessagesController(IDataService messageService)
        {
            _messageService = messageService;
        }


        [HttpGet]
        public async Task<ActionResult<List<Message>>> GetMessages()
        {
            var messages = await _messageService.GetMessages();

            return messages;
        }

        [HttpPost]
        public async Task<ActionResult<Message>> AddMessage(Message message)
        {
            var newMessage = await _messageService.AddMessage(message);

            return newMessage;
        }
    }
}
