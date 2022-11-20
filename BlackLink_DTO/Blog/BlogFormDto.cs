using Microsoft.AspNetCore.Http;

namespace BlackLink_DTO.Blog
{
    public record BlogFormDto
    {
        public BlogFormDto()
        {
            CategoryIds = new List<Guid>();
        }
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public List<Guid> CategoryIds { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
