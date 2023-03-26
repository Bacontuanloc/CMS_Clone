using System;
using System.Collections.Generic;

namespace CMS_API.Entities
{
    public partial class Submission
    {
        public int SubmissionId { get; set; }
        public int AssignmentId { get; set; }
        public int OwnerId { get; set; }
        public DateTime? SubmissionTime { get; set; }
        public string? FilePath { get; set; }
        public double? Grade { get; set; }
        public string? Feedback { get; set; }

        public virtual Assignment Assignment { get; set; } = null!;
        public virtual User Owner { get; set; } = null!;
    }
}
