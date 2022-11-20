using System.ComponentModel.DataAnnotations;

namespace BlackLink_Models.Models
{
    public class InterestUser : BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        public required User User { get; set; }
        public required Interest Interest { get; set; }
    }
}
