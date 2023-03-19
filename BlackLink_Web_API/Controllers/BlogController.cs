using BlackLink_Commends.Commend.BlogCommends.Commend;
using BlackLink_Services.BlogService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlackLink_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogController : Controller
{
    private IBlogService service { get; }
    public BlogController(IBlogService service)
    {
        this.service = service;
    }
    [HttpPost]
    [Authorize]
    [Route("[action]")]
    public async Task<IActionResult> CreateBlog([FromForm] AddBlogCommend commend)
    {
        var blog = await service.AddBlog(commend);
        return Ok(blog);
    }
    [HttpPut]
    [Authorize]
    [Route("[action]")]
    public async Task<IActionResult> UpdateBlog([FromForm] UpdateBlogCommend commend)
    {
        var blog = await service.UpdateBlog(commend);
        return Ok(blog);
    }
    [HttpGet]
    [AllowAnonymous]
    [Route("[action]")]
    public async Task<IActionResult> GetAllBlogs()
    {
        var blog = await service.GetAllBlogs();
        return Ok(blog);
    }
    [HttpGet]
    [AllowAnonymous]
    [Route("[action]")]
    public async Task<IActionResult> GetBlog(Guid Id)
    {
        var blog = await service.GetBlogById(Id);
        return Ok(blog);
    }
    [HttpDelete]
    [AllowAnonymous]
    [Route("[action]")]
    public async Task<IActionResult> RemoveBlog(Guid Id)
    {
        await service.RemoveBlog(Id);
        return Ok();
    }
}
