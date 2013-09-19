using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hub
{
    public partial class Partition
    {
        [Key]
        public int PartitionID { get; set; }
        public int HubID { get; set; }
        public string ServerUserName { get; set; }
        public System.DateTime PartitionCreatedDate { get; set; }
        public Nullable<System.DateTime> LastUpdated { get; set; }
        public Nullable<System.DateTime> LastSyncTime { get; set; }
        public bool HasConflict { get; set; }
        public bool IsActive { get; set; }
    }
}
