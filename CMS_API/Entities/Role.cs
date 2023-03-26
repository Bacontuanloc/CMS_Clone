using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CMS_API.Entities
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}
