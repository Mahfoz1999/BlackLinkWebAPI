using BlackLink_DTO.BlogComment;
using BlackLink_Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlackLink_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogCommentController : Controller
    {
        private readonly IBlogCommnetRepository blogCommnetRepository;
        public BlogCommentController(IBlogCommnetRepository blogCommnetRepository)
        {
            this.blogCommnetRepository = blogCommnetRepository;
        }

        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> AddBlogComment(BlogCommentFormDto formDto)
        {
            var blog = await blogCommnetRepository.AddBlogComment(formDto);
            return Ok(blog);
        }
        [HttpPut]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> UpdateBlogComment(BlogCommentFormDto formDto)
        {
            var blog = await blogCommnetRepository.UpdateBlogComment(formDto);
            return Ok(blog);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetAllBlogComment(Guid blogId)
        {
            var blog = await blogCommnetRepository.GetAllBlogComment(blogId);
            return Ok(blog);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetBlogComment(Guid Id)
        {
            var blog = await blogCommnetRepository.GetBlogComment(Id);
            return Ok(blog);
        }
        [HttpDelete]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> RemoveBlogComment(Guid Id)
        {
            var blog = await blogCommnetRepository.RemoveBlogComment(Id);
            return Ok(blog);
        }
    }
}
