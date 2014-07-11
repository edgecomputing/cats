using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs
{
    public partial class StackEvent
    {
        [Key]
        public System.Guid StackEventID { get; set; }
        public System.DateTime EventDate { get; set; }
        public int HubID { get; set; }
        public int StoreID { get; set; }
        public int StackEventTypeID { get; set; }
        public int StackNumber { get; set; }
        public Nullable<System.DateTime> FollowUpDate { get; set; }
        public bool FollowUpPerformed { get; set; }
        public string Description { get; set; }
        public string Recommendation { get; set; }
        public int UserProfileID { get; set; }
        public virtual StackEventType StackEventType { get; set; }
    }
}
