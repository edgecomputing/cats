using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs
{
    public partial class Adjustment
    {
        [Key]
        public System.Guid AdjustmentID { get; set; }
        public int? PartitionID { get; set; }
        public Nullable<System.Guid> TransactionGroupID { get; set; }
        public int HubID { get; set; }
        public int AdjustmentReasonID { get; set; }
        public string AdjustmentDirection { get; set; }
        public System.DateTime AdjustmentDate { get; set; }
        public string ApprovedBy { get; set; }
        public string Remarks { get; set; }
        public int UserProfileID { get; set; }
        public string ReferenceNumber { get; set; }
        public string StoreManName { get; set; }
        public virtual AdjustmentReason AdjustmentReason { get; set; }
        public virtual TransactionGroup TransactionGroup { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
