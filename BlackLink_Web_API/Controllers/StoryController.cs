using BlackLink_Commends.Commend.StoryCommends.Commend;
using BlackLink_Services.StoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlackLink_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StoryController : Controller
{
    readonly IStoryService service;
    public StoryController(IStoryService service)
    {
        this.service = service;
    }
    [HttpPost]
    [Authorize]
    [Route("[action]")]
    public async Task<IActionResult> CreateStory([FromForm] AddStoryCommend commend)
    {
        var result = await service.AddStory(commend);
        return Ok(result);
    }
    [HttpPut]
    [Authorize]
    [Route("[action]")]
    public async Task<IActionResult> UpdateStory([FromForm] UpdateStoryCommend commend)
    {
        await service.UpdateStory(commend);
        return Ok();
    }
    [HttpGet]
    [AllowAnonymous]
    [Route("[action]")]
    public async Task<IActionResult> GetAllStories()
    {
        var result = await service.GetAllStories();
        return Ok(result);
    }
    [HttpGet]
    [AllowAnonymous]
    [Route("[action]")]
    public async Task<IActionResult> GetStory(Guid Id)
    {
        var result = await service.GetStoryById(Id);
        return Ok(result);
    }
    [HttpDelete]
    [Authorize]
    [Route("[action]")]
    public async Task<IActionResult> RemoveStory(Guid Id)
    {
        await service.RemoveStory(Id);
        return Ok();
    }
}
