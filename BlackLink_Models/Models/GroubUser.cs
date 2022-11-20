using System.ComponentModel.DataAnnotations;

namespace BlackLink_Models.Models
{
    public class GroubUser : BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        public required User User { get; set; }
        public required Groub Groub { get; set; }
    }
}
