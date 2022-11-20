namespace BlackLink_DTO.BlogComment
{
    public record BlogCommentFormDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid BlogId { get; set; }
    }
}
