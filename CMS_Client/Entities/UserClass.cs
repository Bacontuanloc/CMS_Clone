using Newtonsoft.Json;
using System;
using System.Collections.Generic;

#nullable disable

namespace CMS_Client.Entities
{
    public partial class UserClass
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ClassId { get; set; }
        public virtual Class Class { get; set; }
        public virtual User User { get; set; }
    }
}
