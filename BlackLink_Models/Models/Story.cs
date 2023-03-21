namespace BlackLink_Models.Models;

public class Story : BaseModel
{
    public required User User { get; set; }
    public string Content { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
}
