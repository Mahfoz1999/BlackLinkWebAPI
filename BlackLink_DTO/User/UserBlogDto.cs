namespace BlackLink_DTO.User
{
    public record UserBlogDto
    {
        public Guid Id { get; set; }
        public string NickName { get; set; }
        public string PhotoUrl { get; set; }
    }
}
