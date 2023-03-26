using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CMS_API.Entities
{
    public partial class UserClass
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ClassId { get; set; }
        public virtual Class Class { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
