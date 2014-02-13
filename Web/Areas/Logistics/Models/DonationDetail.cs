using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Logistics.Models
{
    public class DonationDetail
    {
        public int DonationDetailPlanID { get; set; }
        public int DonationHeaderPlanID { get; set; }
        public int HubID { get; set; }
        public string Hub { get; set; }
        public decimal AllocatedAmount { get; set; }
        public decimal ReceivedAmount { get; set; }
        public decimal Balance { get; set; }
        
        
    }
}