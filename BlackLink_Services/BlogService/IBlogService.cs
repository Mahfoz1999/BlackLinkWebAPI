using BlackLink_Commends.Commend.BlogCommends.Commend;
using BlackLink_DTO.Blog;

namespace BlackLink_Services.BlogService;

public interface IBlogService
{
    public Task<AddBlogCommend> AddBlog(AddBlogCommend commend);
    public Task<UpdateBlogCommend> UpdateBlog(UpdateBlogCommend commend);
    public Task<IEnumerable<BlogDto>> GetAllBlogs();
    public Task<BlogDto> GetBlogById(Guid Id);
    public Task RemoveBlog(Guid Id);
}
