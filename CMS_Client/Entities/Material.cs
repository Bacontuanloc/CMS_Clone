using System;
using System.Collections.Generic;

#nullable disable

namespace CMS_Client.Entities
{
    public partial class Material
    {
        public int MaterialId { get; set; }
        public int ClassId { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }

        public virtual Class Class { get; set; }
    }
}
