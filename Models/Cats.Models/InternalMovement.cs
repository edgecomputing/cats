using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class InternalMovement
    {
        public System.Guid InternalMovementID { get; set; }
        public int? PartitionId { get; set; }
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
