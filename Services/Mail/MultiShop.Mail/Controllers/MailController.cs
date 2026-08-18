using Microsoft.AspNetCore.Mvc;
using MultiShop.Mail.Dtos;
using MultiShop.Mail.Services;

namespace MultiShop.Mail.Controllers
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

        [HttpPost]
        public IActionResult SendMail(SendMailDto sendMailDto)
        {
            _mailService.SendMail(sendMailDto);
            return Ok();
        }
    }
}
