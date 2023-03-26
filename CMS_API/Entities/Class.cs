using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CMS_API.Entities
{
    public partial class Class
    {
        public Class()
        {
            Assignments = new HashSet<Assignment>();
            Materials = new HashSet<Material>();
            UserClasses = new HashSet<UserClass>();
        }

        public int ClassId { get; set; }
        public string ClassCode { get; set; } = null!;
        public string? Description { get; set; }
        [JsonIgnore]
        public virtual ICollection<Assignment> Assignments { get; set; }
        [JsonIgnore]
        public virtual ICollection<Material> Materials { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserClass> UserClasses { get; set; }
    }
}
