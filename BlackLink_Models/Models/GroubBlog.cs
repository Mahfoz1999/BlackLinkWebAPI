using System.ComponentModel.DataAnnotations;

namespace BlackLink_Models.Models
{
    public class GroubBlog : BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        public required Blog Blog { get; set; }
        public required Groub Groub { get; set; }
    }
}
