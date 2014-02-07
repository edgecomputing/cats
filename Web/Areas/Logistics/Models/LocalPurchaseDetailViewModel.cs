using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Logistics.Models
{
    public class LocalPurchaseDetailViewModel
    {
        public int LocalPurchaseDetailID { get; set; }
        public int LocalPurchaseID { get; set; }
        public string HubName { get; set; }
        public int HubID { get; set; }
        public decimal AllocatedAmonut { get; set; }
        public decimal RecievedAmonut { get; set; }
        public Decimal RemainingAmonut
        {
            get { return AllocatedAmonut - RecievedAmonut; }
        }
    }
}