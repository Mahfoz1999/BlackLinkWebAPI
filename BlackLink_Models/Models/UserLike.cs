using System.ComponentModel.DataAnnotations;

namespace BlackLink_Models.Models
{
    public class UserLike : BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        public required User User { get; set; }
        public required Blog Blog { get; set; }
    }
}
