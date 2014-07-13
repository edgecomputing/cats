using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs
{
    public partial class SessionHistory
    {
        [Key]
        public System.Guid SessionHistoryID { get; set; }
        public int? PartitionID { get; set; }
        public Nullable<int> UserProfileID { get; set; }
        public Nullable<int> RoleID { get; set; }
        public System.DateTime LoginDate { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string WorkstationName { get; set; }
        public string IPAddress { get; set; }
        public string ApplicationName { get; set; }
        public virtual Role Role { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
