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
    public class LocalPurchaseViewModel
    {
        public int LocalPurchaseID { get; set; }
        public string SiNumber { get; set; }
        public int ProgramID { get; set; }
        public string Program { get; set; }
        public int DonorID { get; set; }
        public string DonorName { get; set; }
        public string SupplierName { get; set; }
        public string ReferenceNumber { get; set; }
        public string CreatedDate { get; set; }
        public int CommodityID { get; set; }
        public string Commodity { get; set; }
        public decimal Quantity { get; set; }
        public string Status { get; set; }

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
    public class LocalPurchaseWithDetailViewModel
    {
        public int LocalPurchaseID { get; set; }
        public int GiftCertificateID { get; set; }
        public string SINumber { get; set; }
        public int CommodityID { get; set; }
        public int DonorID { get; set; }
        public int ProgramID { get; set; }
        public int ShippingInstractionID { get; set; }
        public int PurchaseOrder { get; set; }
        public string SupplierName { get; set; }
        public decimal Quantity { get; set; }
        public string CommoditySource { get; set; }
        public string Remark { get; set; }
        public int CommodityTypeID { get; set; }
        public string ReferenceNumber { get; set; }
        public IEnumerable<LocalPurchaseDetailViewModel> LocalPurchaseDetailViewModels { get; set; } 
    }
}