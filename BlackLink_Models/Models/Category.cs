namespace BlackLink_Models.Models;

public class Category : BaseModel
{
    public string Name { get; set; }
    public ICollection<Blog> Blogs { get; set; } = new List<Blog>();
}
