using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;

namespace Cats.Areas.Procurement.Models
{
    public class SubstituteTransporterOrder
    {
        public int WoredaID { get; set; }
        public string Woreda { get; set; }
        public List<TransportOrderDetail> TransportOrderDetails { get; set; }
        public List<TransportBidQuotationViewModel> TransportersStandingList { get; set; }
        

    }

  
}