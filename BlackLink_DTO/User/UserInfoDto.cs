using BlackLink_DTO.Interest;
using BlackLink_SharedKernal.Enum.Personality;

namespace BlackLink_DTO.User
{
    public record UserInfoDto
    {
        public UserInfoDto()
        {
            Interests = new List<InterestUserDto>();
            UserPhotos = new List<string>();
        }
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string AboutMe { get; set; } = string.Empty;
        public string? Country { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? FacebookLink { get; set; } = string.Empty;
        public string? InstagramLink { get; set; } = string.Empty;
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTimeOffset Birthdate { get; set; }
        public ICollection<InterestUserDto> Interests { get; set; }
        public ICollection<string> UserPhotos { get; set; }
        public int Followers { get; set; } = 0;
        public int Friends { get; set; } = 0;
    }
}
