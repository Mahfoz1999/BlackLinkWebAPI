namespace BlackLink_Models.Models
{
    public class Interest : BaseModel
    {
        public Interest()
        {
            InterestUsers = new List<InterestUser>();
        }
        public Guid Id { get; set; }
        public required string InterestName { get; set; } = string.Empty;
        public ICollection<InterestUser> InterestUsers { get; set; }
    }
}
