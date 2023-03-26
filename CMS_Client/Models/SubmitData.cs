namespace CMS_Client.Models
{
    public class SubmitData
    {
        public int AssignmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime? Deadline { get; set; }

        public bool Submitted { get; set; }

    }
}
