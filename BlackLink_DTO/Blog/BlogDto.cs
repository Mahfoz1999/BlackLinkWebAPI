

using BlackLink_DTO.Category;
using BlackLink_DTO.User;

namespace BlackLink_DTO.Blog;

public record BlogDto
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public UserBlogDto User { get; set; }
    public ICollection<CommentDto> Comments { get; set; } = new HashSet<CommentDto>();
    public CategoryDto Category { get; set; }
}
