using System;
using System.Collections.Generic;

#nullable disable

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
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }

        public virtual Class Class { get; set; }
        public virtual User Owner { get; set; }
        public virtual ICollection<Submission> Submissions { get; set; }
    }
}
