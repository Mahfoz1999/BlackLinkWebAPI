using System.ComponentModel.DataAnnotations;

namespace BlackLink_Models.Models
{
    public class Blog : BaseModel
    {
        public Blog()
        {
            UserLikes = new List<UserLike>();
            BlogComments = new List<BlogComment>();
            Blogs = new List<GroubBlog>();
            CategoryEntityRealteds = new List<CategoryEntityRealted>();
        }
        [Key]
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public required User User { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<UserLike> UserLikes { get; set; }
        public ICollection<BlogComment> BlogComments { get; set; }
        public ICollection<GroubBlog> Blogs { get; set; }
        public ICollection<CategoryEntityRealted> CategoryEntityRealteds { get; set; }

    }
}
