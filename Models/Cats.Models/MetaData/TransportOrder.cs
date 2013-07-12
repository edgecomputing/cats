using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class TransportOrderMeta
    {

        public int TransportOrderID { get; set; }
        [Display(Name = "Transport Order No")]
        public string TransportOrderNo { get; set; }
        [Display(Name = "Contract Number")]
        public string ContractNumber { get; set; }
        [Display(Name = "Order Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime OrderDate { get; set; }
        [Display(Name = "Requested Dispatch Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime RequestedDispatchDate { get; set; }
        [Display(Name = "Order Expiry Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime OrderExpiryDate { get; set; }
        [Display(Name = "Bid Document No")]
        public string BidDocumentNo { get; set; }
        [Display(Name = "Performance Bond Receipt No")]
        public string PerformanceBondReceiptNo { get; set; }

        public int TransporterID { get; set; }

        [Display(Name = "Consigner Name")]
        public string ConsignerName { get; set; }
        [Display(Name = "Transporter Signed Name")]
        public string TransporterSignedName { get; set; }
        [Display(Name = "Consigner Signed Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime ConsignerDate { get; set; }
        [Display(Name = "Transporter Signed Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime TransporterSignedDate { get; set; }



    }
}
