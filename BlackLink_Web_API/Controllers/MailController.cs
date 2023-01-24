using BlackLink_DTO.Mail;
using BlackLink_Services.MailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlackLink_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailController : Controller
    {
        private readonly IMailService mailService;
        public MailController(IMailService MailService)
        {
            mailService = MailService;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> SendEmail(MailRequest mail)
        {
            var result = await mailService.SendEmailAsync(mail);
            return Ok(result);
        }
    }
}
