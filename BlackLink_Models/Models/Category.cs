using System.ComponentModel.DataAnnotations;

namespace BlackLink_Models.Models
{
    public class Category : BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Blog> Blogs { get; set; } = new List<Blog>();
    }
}
