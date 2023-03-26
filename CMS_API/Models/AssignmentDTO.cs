namespace CMS_API.Models
{
    public class AssignmentDTO
    {
        public int AssignmentId { get; set; }
        public int ClassId { get; set; }
        public int OwnerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
    }
}
