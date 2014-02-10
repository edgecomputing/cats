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
    public class LocalPurchaseFromGiftCertificateInfo
    {
        public int GiftCertificateID { get; set; }
        public int CommodityID { get; set; }
        public string CommodityName { get; set; }
        public int DonorID { get; set; }
        public string DonorName { get; set; }
        public int ProgramID { get; set; }
        public string ProgramName { get; set; }
        public decimal QuantityInMT { get; set; }
        public string CommoditySource { get; set; }

    }
}