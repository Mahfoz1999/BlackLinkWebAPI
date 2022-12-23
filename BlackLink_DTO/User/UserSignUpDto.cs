using BlackLink_SharedKernal.Enum.Personality;
using Microsoft.AspNetCore.Http;

namespace BlackLink_DTO.User
{
    public record UserSignUpDto
    {
        public string NickName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AboutMe { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string City { get; set; }
        public Gender Gender { get; set; }
        public GenderPrefere GenderPrefere { get; set; }
        public DateTimeOffset Birthdate { get; set; }
        public string Password { get; set; } = string.Empty;
        public List<Guid> InterestsIds { get; set; } = new();
        public IFormFileCollection Files { get; set; }
    }
}
