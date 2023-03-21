using BlackLink_Commends.Commend.BlogCommentCommends.Commend;
using BlackLink_DTO.BlogComment;

namespace BlackLink_Services.BlogCommentService;

public interface IBlogCommentService
{
    public Task<AddBlogCommentCommend> AddBlogComment(AddBlogCommentCommend commend);
    public Task UpdateBlogComment(UpdateBlogCommentCommend commend);
    public Task RemoveBlogComment(Guid Id);
    public Task<IEnumerable<BlogCommentDto>> GetAllBlogComments(Guid blogId);
    public Task<BlogCommentDto> GetBlogComment(Guid Id);
}
