using System.ComponentModel.DataAnnotations;

namespace BlackLink_Models.Models
{
    public class CategoryEntityRealted : BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        public required Category Category { get; set; }
        public Blog? Blog { get; set; }
        public Groub? Groub { get; set; }
    }
}
