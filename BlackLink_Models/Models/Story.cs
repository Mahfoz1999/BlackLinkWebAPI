using System.ComponentModel.DataAnnotations;

namespace BlackLink_Models.Models
{
    public class Story : BaseModel
    {
        public Story()
        {
            StoryViews = new List<StoryView>();
        }
        [Key]
        public Guid Id { get; set; }
        public required User User { get; set; }
        public string Content { get; set; } = string.Empty;
        public string FileUrl { get; set; }
        public ICollection<StoryView> StoryViews { get; set; }
    }
}
