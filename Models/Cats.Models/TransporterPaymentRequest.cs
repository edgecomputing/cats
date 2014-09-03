using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class TransporterPaymentRequest
    {
        public int TransporterPaymentRequestID { get; set; }
        public string ReferenceNo { get; set; }
        public int TransportOrderID { get; set; }
        public System.Guid DeliveryID { get; set; }
        public Nullable<decimal> ShortageBirr { get; set; }
        public Nullable<decimal> LabourCostRate { get; set; }
        public Nullable<decimal> LabourCost { get; set; }
        public Nullable<decimal> RejectedAmount { get; set; }
        public string RejectionReason { get; set; }
        public DateTime RequestedDate { get; set; }
        public int BusinessProcessID { get; set; }
        public Nullable<int> PartitionId { get; set; }
        public virtual BusinessProcess BusinessProcess { get; set; }
        public virtual Delivery Delivery { get; set; }
        public virtual TransportOrder TransportOrder { get; set; }
    }
}
