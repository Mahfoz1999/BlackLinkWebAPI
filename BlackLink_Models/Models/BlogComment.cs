namespace BlackLink_Models.Models;

public class BlogComment : BaseModel
{
    public required string Content { get; set; }
    public required Blog Blog { get; set; }
    public required User User { get; set; }
}
