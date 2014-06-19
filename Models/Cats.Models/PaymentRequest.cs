using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Cats.Models;

namespace Cats.Models
{
    public class PaymentRequest : PaymentRequestPOCO
    {
        public virtual TransportOrder TransportOrder { get; set; }
        public virtual BusinessProcess BusinessProcess { get; set; }
        
    }
  

    public class PaymentRequestPOCO
    {
        
        public int PaymentRequestID {get;set;}
        public int TransportOrderID {get;set;}
        public decimal RequestedAmount { get; set; }
        public decimal TransportedQuantityInQTL { get; set; }
        public string ReferenceNo {get;set;}
        public DateTime RequestedDate { get; set; }
        public int BusinessProcessID { get; set; }
        public Nullable<decimal> LabourCostRate { get; set; }
        public Nullable<decimal> LabourCost { get; set; }
        public Nullable<decimal> RejectedAmount { get; set; }
        public string RejectionReason { get; set; }
        public virtual ICollection<TransporterCheque> TransporterCheques { get; set; }
        public int? PartitionId { get; set; }
    }

}
