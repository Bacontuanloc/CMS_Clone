using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CMS_API.Entities
{
    public partial class User
    {
        public User()
        {
            Assignments = new HashSet<Assignment>();
            Submissions = new HashSet<Submission>();
            UserClasses = new HashSet<UserClass>();
        }

        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string UserCode { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public virtual Role Role { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Assignment> Assignments { get; set; }
        [JsonIgnore]
        public virtual ICollection<Submission> Submissions { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserClass> UserClasses { get; set; }
    }
}
