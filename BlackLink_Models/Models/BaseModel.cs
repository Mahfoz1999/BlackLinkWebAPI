namespace BlackLink_Models.Models
{
    public class BaseModel
    {
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}
