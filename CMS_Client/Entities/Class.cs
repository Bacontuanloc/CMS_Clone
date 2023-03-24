using System;
using System.Collections.Generic;

#nullable disable

namespace CMS_Client.Entities
{
    public partial class Class
    {
        public Class()
        {
            Assignments = new HashSet<Assignment>();
            Materials = new HashSet<Material>();
        }

        public int ClassId { get; set; }
        public string ClassCode { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<Material> Materials { get; set; }
    }
}
