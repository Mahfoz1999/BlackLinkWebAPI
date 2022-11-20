namespace BlackLink_DTO.Blog
{
    public record CommentDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; }
    }
}
