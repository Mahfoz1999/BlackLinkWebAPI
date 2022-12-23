using BlackLink_Models.Models.Files;
using BlackLink_SharedKernal.Enum.Personality;
using Microsoft.AspNetCore.Identity;

namespace BlackLink_Models.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            Blogs = new List<Blog>();
            InterestUsers = new List<InterestUser>();
            Stories = new List<Story>();
            UserLikes = new List<UserLike>();
            StoryViews = new List<StoryView>();
            BlogComments = new List<BlogComment>();
            Followers = new List<User>();
            BlockUsers = new List<User>();
            Friends = new List<User>();
            GroubUsers = new List<GroubUser>();
            Groubs = new List<Groub>();
            UserPhotos = new List<UserPhoto>();
        }
        public required string NickName { get; set; } = string.Empty;
        public string AboutMe { get; set; } = string.Empty;
        public List<UserPhoto> UserPhotos { get; set; }
        public string? Country { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? FacebookLink { get; set; } = string.Empty;
        public string? InstagramLink { get; set; } = string.Empty;
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public Gender Gender { get; set; }
        public GenderPrefere GenderPrefere { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTimeOffset Birthdate { get; set; }
        public ICollection<User> BlockUsers { get; set; }
        public ICollection<User> Followers { get; set; }
        public ICollection<User> Friends { get; set; }
        public ICollection<Story> Stories { get; set; }
        public ICollection<InterestUser> InterestUsers { get; set; }
        public ICollection<Blog> Blogs { get; set; }
        public ICollection<BlogComment> BlogComments { get; set; }
        public ICollection<UserLike> UserLikes { get; set; }
        public ICollection<StoryView> StoryViews { get; set; }
        public ICollection<Groub> Groubs { get; set; }
        public ICollection<GroubUser> GroubUsers { get; set; }
    }
}
