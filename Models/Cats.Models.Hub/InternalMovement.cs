using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs
{
    public partial class InternalMovement
    {
        [Key]
        public System.Guid InternalMovementID { get; set; }
        public int PartitionID { get; set; }
        public int HubID { get; set; }
        public Nullable<System.Guid> TransactionGroupID { get; set; }
        public System.DateTime TransferDate { get; set; }
        public string ReferenceNumber { get; set; }
        public int DReason { get; set; }
        public string Notes { get; set; }
        public string ApprovedBy { get; set; }
        public virtual Detail Detail { get; set; }
        public virtual TransactionGroup TransactionGroup { get; set; }
    }
}
