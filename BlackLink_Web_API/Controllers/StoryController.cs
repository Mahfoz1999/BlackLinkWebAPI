using BlackLink_DTO.Story;
using BlackLink_Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlackLink_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoryController : Controller
    {
        readonly IStoryRepository repository;
        public StoryController(IStoryRepository storyRepository)
        {
            repository = storyRepository;
        }
        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> CreateStory([FromForm] StoryFormDto formDto)
        {
            var result = await repository.CreateStory(formDto);
            return Ok(result);
        }
        [HttpPut]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> StoryView(Guid storyId)
        {
            var result = await repository.StoryView(storyId);
            return Ok(result);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetAllStories()
        {
            var result = await repository.GetAllStories();
            return Ok(result);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetStory(Guid Id)
        {
            var result = await repository.GetStory(Id);
            return Ok(result);
        }
        [HttpDelete]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> RemoveStory(Guid Id)
        {
            var result = await repository.RemoveStory(Id);
            return Ok(result);
        }
    }
}
