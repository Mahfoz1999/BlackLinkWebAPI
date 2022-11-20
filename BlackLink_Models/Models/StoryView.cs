using System.ComponentModel.DataAnnotations;

namespace BlackLink_Models.Models
{
    public class StoryView : BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        public required User User { get; set; }
        public required Story Story { get; set; }
    }
}
