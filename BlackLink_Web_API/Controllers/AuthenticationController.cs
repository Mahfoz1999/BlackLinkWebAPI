
using BlackLink_DTO.User;
using BlackLink_IRepository.IRepository.Authentication;
using BlackLink_Models.Models;
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
        private IAuthenticationRepository Manager { get; }
        public AuthenticationController(IMailService MaileService, IAuthenticationRepository manager, UserManager<User> userManager)
        {
            Manager = manager;
            this.MaileService = MaileService;
            _userManager = userManager;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SignUp([FromForm] UserSignUpDto userDto)
        {
            var result = await Manager.SignUp(userDto);
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

        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword)
        {
            var result = await Manager.ChangePassword(oldPassword, newPassword);
            return result.Succeeded ? Ok() : BadRequest(result.Errors);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> ResetPasswordConfirmation(string email, string token, string password)
        {
            User? user = await _userManager.FindByEmailAsync(email);
            IdentityResult resetPassResult = await _userManager.ResetPasswordAsync(user!, token, password);
            return resetPassResult.Succeeded ? Ok() : BadRequest(resetPassResult.Errors);
        }
        [HttpPut]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> UpdateUserAsync([FromForm] UserSignUpDto userDto)
        {
            var userResult = await Manager.UpdateUserAsync(userDto);
            return !userResult.Succeeded ? new BadRequestObjectResult(userResult) : StatusCode(201);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> LogIn([FromBody] UserLoginDto user)
        {
            return Ok(await Manager.Login(user));

        }


    }
}
