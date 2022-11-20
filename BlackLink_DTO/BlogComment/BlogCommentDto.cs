namespace BlackLink_DTO.BlogComment
{
    public record BlogCommentDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
