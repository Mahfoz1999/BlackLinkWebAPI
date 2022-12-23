using System.ComponentModel.DataAnnotations;

namespace BlackLink_Models.Models.Files;

public class UserPhoto
{
    [Key]
    public Guid Id { get; set; }
    public required string PhotoUrl { get; set; }
    public required User User { get; set; }
}
