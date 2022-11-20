using BlackLink_DTO.Blog;

namespace BlackLink_Repository.IRepository
{
    public interface IBlogRepository
    {
        public Task<BlogFormDto> CreateBlog(BlogFormDto formDto);
        public Task<BlogFormDto> UpdateBlog(BlogFormDto formDto);
        public Task<bool> LikeBlog(Guid blogId);
        public Task<bool> UnLikeBlog(Guid blogId);
        public Task<IEnumerable<BlogDto>> GetAllBlogs();
        public Task<BlogDto> GetBlog(Guid blogId);
        public Task<bool> RemoveBlog(Guid blogId);
    }
}
