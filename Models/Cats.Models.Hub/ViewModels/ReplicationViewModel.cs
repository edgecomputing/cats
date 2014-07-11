using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs.ViewModels
{
    public class ReplicationViewModel
    {
        [Display(Name="Partition")]
        public int PartitionId { get; set; }
        [Display(Name="Hub")]
        public string HubName { get; set; }
        [Display(Name = "Partition Created")]
        public DateTime PartitionCreatedDate { get; set; }
        [Display(Name = "Last Updated")]
        public DateTime LastUpdated { get; set; }

        [Display(Name = "Last Synchronization")]
        public DateTime LastSyncTime { get; set; }

        [Display(Name="Conflict")]
        public bool HasConflict { get; set; }
        [Display(Name= "Active")]
        public bool IsActive { get; set; }

        [Display(Name="Last Action")]
        public string LastAction { get; set; }

        [Display(Name = "Action Time")]
        public DateTime ActionTime { get; set; }

    }
}
