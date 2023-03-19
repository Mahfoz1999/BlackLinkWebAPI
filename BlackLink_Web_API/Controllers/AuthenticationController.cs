
using BlackLink_DTO.User;
using BlackLink_Models.Models;
using BlackLink_Services.AuthenticationService;
using BlackLink_Services.MailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlackLink_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IMailService MaileService;
        private IAuthenticationService Service { get; }
        public AuthenticationController(IMailService MaileService, IAuthenticationService Service, UserManager<User> userManager)
        {
            this.Service = Service;
            this.MaileService = MaileService;
            _userManager = userManager;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SignUp([FromForm] UserSignUpDto userDto)
        {
            var result = await Service.SignUp(userDto);
            return result.Succeeded ? StatusCode(201) : new BadRequestObjectResult(result);
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return View("Error");
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return Ok(result.Succeeded ? nameof(ConfirmEmail) : "Error");
        }

        [HttpPut]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> UpdateUser([FromForm] UserSignUpDto userDto)
        {
            var userResult = await Service.UpdateUser(userDto);
            return !userResult.Succeeded ? new BadRequestObjectResult(userResult) : StatusCode(201);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> LogIn([FromBody] UserLoginDto user)
        {
            return Ok(await Service.LogIn(user));

        }


    }
}
