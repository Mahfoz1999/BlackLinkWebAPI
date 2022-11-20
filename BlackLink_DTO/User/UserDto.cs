namespace BlackLink_DTO.User
{
    public record UserDto
    {
        public Guid Id { get; set; }
        public string NickName { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PhotoUrl { get; set; }
        public bool IsOnline { get; set; } = false;
    }
}
