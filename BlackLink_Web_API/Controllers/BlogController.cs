using BlackLink_DTO.Blog;
using BlackLink_Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlackLink_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : Controller
    {
        private IBlogRepository repository { get; }
        public BlogController(IBlogRepository repository)
        {
            this.repository = repository;
        }
        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> CreateBlog([FromForm] BlogFormDto formDto)
        {
            var blog = await repository.CreateBlog(formDto);
            return Ok(blog);
        }
        [HttpPut]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> UpdateBlog([FromForm] BlogFormDto formDto)
        {
            var blog = await repository.UpdateBlog(formDto);
            return Ok(blog);
        }
        [HttpPut]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> LikeBlog(Guid blogId)
        {
            var blog = await repository.LikeBlog(blogId);
            return Ok(blog);
        }
        [HttpDelete]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> UnLikeBlog(Guid blogId)
        {
            var blog = await repository.UnLikeBlog(blogId);
            return Ok(blog);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetAllBlogs()
        {
            var blog = await repository.GetAllBlogs();
            return Ok(blog);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetBlog(Guid blogId)
        {
            var blog = await repository.GetBlog(blogId);
            return Ok(blog);
        }
        [HttpDelete]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> RemoveBlog(Guid blogId)
        {
            var blog = await repository.RemoveBlog(blogId);
            return Ok(blog);
        }
    }
}
