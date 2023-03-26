using System;
using System.Collections.Generic;

namespace CMS_API.Entities
{
    public partial class Assignment
    {
        public Assignment()
        {
            Submissions = new HashSet<Submission>();
        }

        public int AssignmentId { get; set; }
        public int ClassId { get; set; }
        public int OwnerId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }

        public virtual Class Class { get; set; } = null!;
        public virtual User Owner { get; set; } = null!;
        public virtual ICollection<Submission> Submissions { get; set; }
    }
}
