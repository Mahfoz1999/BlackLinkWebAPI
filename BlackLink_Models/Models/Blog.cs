using System.ComponentModel.DataAnnotations;

namespace BlackLink_Models.Models;

public class Blog : BaseModel
{
    [Key]
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public required User User { get; set; }
    public string? ImageUrl { get; set; }
    public Category Category { get; set; }
}
