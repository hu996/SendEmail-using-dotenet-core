using DevCreedSendEmail.Dtos;
using DevCreedSendEmail.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCreedSendEmail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;
        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }  
        [HttpPost("send")]
        public IActionResult SendMail([FromForm]MailRequestDto Dto)
        {
            _mailService.SendEmail(Dto.ToEmail,Dto.Subject,Dto.Body);
            return Ok();
        }
    }
}
