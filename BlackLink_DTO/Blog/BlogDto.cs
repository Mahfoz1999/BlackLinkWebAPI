

using BlackLink_DTO.User;

namespace BlackLink_DTO.Blog
{
    public record BlogDto
    {
        public BlogDto()
        {
            UsersLikes = new List<string>();
            Comments = new List<CommentDto>();
            Categories = new List<string>();
        }
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public List<string> UsersLikes { get; set; }
        public int LikesCount { get; set; } = 0;
        public DateTime CreationDate { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public UserBlogDto User { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
        public ICollection<string> Categories { get; set; }
    }
}
