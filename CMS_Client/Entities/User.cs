using System;
using System.Collections.Generic;

#nullable disable

namespace CMS_Client.Entities
{
    public partial class User
    {
        public User()
        {
            Assignments = new HashSet<Assignment>();
            Submissions = new HashSet<Submission>();
        }

        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserCode { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<Submission> Submissions { get; set; }
    }
}
