using BlackLink_SharedKernal.Enum.Personality;
using Microsoft.AspNetCore.Http;

namespace BlackLink_DTO.User;

public record UserSignUpDto
{
    public string NickName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public IFormFile File { get; set; }
    public DateTime Birthdate { get; set; }
    public string Password { get; set; } = string.Empty;
    public List<Guid> InterestsIds { get; set; } = new();
}
