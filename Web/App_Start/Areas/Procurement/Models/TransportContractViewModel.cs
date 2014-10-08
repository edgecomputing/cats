using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cats.Areas.Procurement.Models
{
    public class TransportContractViewModel
    {
        public int TransportOrderID { get; set; }
        public string TransportOrderNo { get; set; }
        public string ContractNumber { get; set; }
        [Display(Name = "Order Date")]
        public string OrderDate { get; set; }
        [Display(Name = "Requested Dispatch Date")]
        public string RequestedDispatchDate { get; set; }
        public string OrderExpiryDate { get; set; }
        public string BidDocumentNo { get; set; }
        public string PerformanceBondReceiptNo { get; set; }
        public int TransporterID { get; set; }
        public string Transporter { get; set; }
        public string ConsignerName { get; set; }
        public string TransporterSignedName { get; set; }
        public string ConsignerDate { get; set; }
        public string TransporterSignedDate { get; set; }
        public int? StatusID { get; set; }
        public string Region { get; set; }
        public string Zone { get; set; }
        public string RequisitionNo { get; set; }
        
    }
}