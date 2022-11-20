namespace BlackLink_Models.Models
{
    public class Groub : BaseModel
    {
        public Groub()
        {
            Users = new List<GroubUser>();
            Blogs = new List<GroubBlog>();
            CategoryEntityRealteds = new List<CategoryEntityRealted>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CoverUrl { get; set; }
        public string Description { get; set; }
        public required User Admin { get; set; }
        public ICollection<GroubUser> Users { get; set; }
        public ICollection<GroubBlog> Blogs { get; set; }
        public ICollection<CategoryEntityRealted> CategoryEntityRealteds { get; set; }

    }
}
