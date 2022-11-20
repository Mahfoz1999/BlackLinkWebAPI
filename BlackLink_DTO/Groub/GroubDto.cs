namespace BlackLink_DTO.Groub
{
    public record GroubDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CoverUrl { get; set; }
        public string Description { get; set; }

    }
}
