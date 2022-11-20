using Microsoft.AspNetCore.Http;

namespace BlackLink_DTO.Story
{
    public record StoryFormDto
    {
        public string Content { get; set; } = string.Empty;
        public IFormFile File { get; set; }
    }
}
