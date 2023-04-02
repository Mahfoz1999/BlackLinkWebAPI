namespace BlackLink_Models.Models;

public class Blog : BaseModel
{
    public string Content { get; set; } = string.Empty;
    public required User User { get; set; }
    public string? ImageUrl { get; set; }
    public ICollection<BlogComment> BlogComments { get; set; } = new List<BlogComment>();
}
