using System;
using System.Collections.Generic;

#nullable disable

namespace CMS_API.Entities
{
    public partial class Grade
    {
        public int GradeId { get; set; }
        public int SubmissionId { get; set; }
        public double Grade1 { get; set; }
        public string Feedback { get; set; }

        public virtual Submission Submission { get; set; }
    }
}
