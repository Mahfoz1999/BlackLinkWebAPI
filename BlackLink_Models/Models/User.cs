using BlackLink_Models.Models.Files;
using BlackLink_SharedKernal.Enum.Personality;
using Microsoft.AspNetCore.Identity;

namespace BlackLink_Models.Models;

public class User : IdentityUser
{
    public required string NickName { get; set; } = string.Empty;
    public string AboutMe { get; set; } = string.Empty;
    public ICollection<UserPhoto> UserPhotos { get; set; } = new List<UserPhoto>();
    public string? Country { get; set; } = string.Empty;
    public string? City { get; set; } = string.Empty;
    public string? FacebookLink { get; set; } = string.Empty;
    public string? InstagramLink { get; set; } = string.Empty;
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public Gender Gender { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public DateTimeOffset Birthdate { get; set; }
    public ICollection<Story> Stories { get; set; } = new List<Story>();
    public ICollection<InterestUser> InterestUsers { get; set; } = new List<InterestUser>();
    public ICollection<Blog> Blogs { get; set; } = new List<Blog>();
}
