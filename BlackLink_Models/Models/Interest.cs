namespace BlackLink_Models.Models;

public class Interest : BaseModel
{
    public required string InterestName { get; set; } = string.Empty;
    public ICollection<InterestUser> InterestUsers { get; set; } = new List<InterestUser>();
}
