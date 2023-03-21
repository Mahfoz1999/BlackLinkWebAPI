namespace BlackLink_Models.Models;

public class InterestUser : BaseModel
{
    public required User User { get; set; }
    public required Interest Interest { get; set; }
}
