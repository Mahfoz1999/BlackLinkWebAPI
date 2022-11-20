using BlackLink_DTO.Blog;
using BlackLink_DTO.User;

namespace BlackLink_DTO.Groub
{
    public record GroubInfoDto
    {
        public GroubInfoDto()
        {
            Blogs = new List<BlogDto>();
            Users = new List<UserDto>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CoverUrl { get; set; }
        public string Description { get; set; }
        public ICollection<BlogDto> Blogs { get; set; }
        public ICollection<UserDto> Users { get; set; }
    }
}
