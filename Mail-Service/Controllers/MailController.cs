using Mail_Service.Dtos;
using Mail_Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mail_Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mail;

        public MailController(IMailService mail)
        {
            _mail = mail;   
        }

        [HttpPost("sendmail")]
        public async Task<ActionResult<MailDto>> SendMailAsync(MailDto mailData)
        {
            var res = await _mail.SendAsync(mailData, new CancellationToken());

            return Ok(res);
        }
    }
}
