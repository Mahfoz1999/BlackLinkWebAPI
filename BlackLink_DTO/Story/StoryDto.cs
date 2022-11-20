namespace BlackLink_DTO.Story
{
    public record StoryDto
    {
        public Guid Id { get; set; }
        public string UserNiceName { get; set; }
        public string Content { get; set; } = string.Empty;
        public string FileUrl { get; set; }
    }
}
