using BlackLink_Commends.Commend.BlogCommentCommends.Commend;
using BlackLink_Services.BlogCommentService;
using Microsoft.AspNetCore.Mvc;

namespace BlackLink_Web_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogCommentController : ControllerBase
{
    private readonly IBlogCommentService service;
    public BlogCommentController(IBlogCommentService service)
    {
        this.service = service;
    }
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> AddBlogComment(AddBlogCommentCommend commend)
    {
        var result = await service.AddBlogComment(commend);
        return Ok(result);
    }
    [HttpPut]
    [Route("[action]")]
    public async Task<IActionResult> UpdateBlogComment(UpdateBlogCommentCommend commend)
    {
        await service.UpdateBlogComment(commend);
        return Ok();
    }
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetAllBlogComments(Guid blogId)
    {
        var result = await service.GetAllBlogComments(blogId);
        return Ok(result);
    }
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetBlogComment(Guid Id)
    {
        var result = await service.GetBlogComment(Id);
        return Ok(result);
    }
    [HttpDelete]
    [Route("[action]")]
    public async Task<IActionResult> RemoveBlogComment(Guid Id)
    {
        await service.RemoveBlogComment(Id);
        return Ok();
    }
}
