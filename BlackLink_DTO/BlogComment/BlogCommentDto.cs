using BlackLink_DTO.User;

namespace BlackLink_DTO.BlogComment;

public class BlogCommentDto
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public UserBlogDto User { get; set; }
    public DateTime CreationDate { get; set; }
}
