using BlackLink_SharedKernal.Enum.Personality;

namespace BlackLink_DTO.User
{
    public record UserSignUpDto
    {
        public string nickName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public GenderPrefere GenderPrefere { get; set; }
        public DateTimeOffset Birthdate { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
