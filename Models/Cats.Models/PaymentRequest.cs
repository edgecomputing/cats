using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

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
        public string ReferenceNo {get;set;}
        public int BusinessProcessID { get; set; }
        public Nullable<decimal> LabourCostRate { get; set; }
        public Nullable<decimal> LabourCost { get; set; }
    }

}
