using System.ComponentModel.DataAnnotations;

namespace BlackLink_Models.Models
{
    public class BlogComment : BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public required Blog Blog { get; set; }
        public required User User { get; set; }
    }
}
