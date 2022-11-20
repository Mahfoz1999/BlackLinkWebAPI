using Microsoft.AspNetCore.Http;

namespace BlackLink_DTO.Groub
{
    public record GroubFormDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IFormFile CoverFile { get; set; }
        public string Description { get; set; }
        public List<Guid> CategoryIds { get; set; }
    }
}
