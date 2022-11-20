using BlackLink_DTO.BlogComment;

namespace BlackLink_Repository.IRepository
{
    public interface IBlogCommnetRepository
    {
        public Task<BlogCommentFormDto> AddBlogComment(BlogCommentFormDto formDto);
        public Task<BlogCommentFormDto> UpdateBlogComment(BlogCommentFormDto formDto);
        public Task<IEnumerable<BlogCommentDto>> GetAllBlogComment(Guid Id);
        public Task<BlogCommentDto> GetBlogComment(Guid Id);
        public Task<bool> RemoveBlogComment(Guid Id);
    }
}
