using BlackLink_Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlackLink_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SocialController : Controller
    {
        private readonly ISocialRepository repository;
        public SocialController(ISocialRepository repository)
        {
            this.repository = repository;
        }
        [HttpPut]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> FolllowUser(Guid FolllowUser)
        {
            await repository.FolllowUser(FolllowUser);
            return Ok();
        }
        [HttpPut]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> UnFollowUser(Guid userId)
        {
            await repository.FolllowUser(userId);
            return Ok();
        }
        [HttpPut]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> AddFriend(Guid userId)
        {
            await repository.AddFriend(userId);
            return Ok();
        }
        [HttpPut]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> RemoveFriend(Guid userId)
        {
            await repository.RemoveFriend(userId);
            return Ok();
        }
        [HttpPut]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> BlockUser(Guid userId)
        {
            await repository.FolllowUser(userId);
            return Ok();
        }
        [HttpPut]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> UnBlockUser(Guid userId)
        {
            await repository.FolllowUser(userId);
            return Ok();
        }
    }
}
