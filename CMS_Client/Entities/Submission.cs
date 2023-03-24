using System;
using System.Collections.Generic;

#nullable disable

namespace CMS_Client.Entities
{
    public partial class Submission
    {
        public Submission()
        {
            Grades = new HashSet<Grade>();
        }

        public int SubmissionId { get; set; }
        public int AssignmentId { get; set; }
        public int OwnerId { get; set; }
        public DateTime? SubmissionTime { get; set; }
        public string FilePath { get; set; }

        public virtual Assignment Assignment { get; set; }
        public virtual User Owner { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
    }
}
