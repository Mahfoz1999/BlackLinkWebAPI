namespace BlackLink_DTO.User
{
    public record TokenModel
    {
        public string token { get; set; }
        public DateTime expiration { get; set; }
    }
}
