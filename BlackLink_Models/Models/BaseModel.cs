using System.ComponentModel.DataAnnotations;

namespace BlackLink_Models.Models;

public class BaseModel
{
    [Key]
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public Guid CreatedBy { get; set; }
    public Guid UpdatedBy { get; set; }
}
