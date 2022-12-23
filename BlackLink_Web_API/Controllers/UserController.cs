using BlackLink_Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlackLink_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;
        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await userRepository.GetAllUsers());
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetUser(Guid Id)
        {
            return Ok(await userRepository.GetUser(Id));
        }
        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> UpdateCurrentUserPhoto(IFormFile file)
        {
            await userRepository.UpdateCurrentUserPhoto(file);
            return Ok();
        }

    }
}
