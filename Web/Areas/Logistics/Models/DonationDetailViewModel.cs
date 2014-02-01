using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Logistics.Models
{
    public class DonationDetailViewModel
    {
        public int ReceiptDetailId { get; set; }
        public int ReceiptHeaderId { get; set; }
        public int HubId { get; set; }
        public string Hub { get; set; }
        public decimal Allocated { get; set; }
        public Nullable<decimal> Received { get; set; }
        public Nullable<decimal> Balance { get; set; }
        
       
    }
}