using System.ComponentModel.DataAnnotations;

namespace BlackLink_Models.Models
{
    public class Category : BaseModel
    {
        public Category()
        {
            CategoryEntityRealteds = new List<CategoryEntityRealted>();
        }
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<CategoryEntityRealted> CategoryEntityRealteds { get; set; }
    }
}
