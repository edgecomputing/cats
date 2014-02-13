using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;

namespace Cats.Areas.Logistics.Models
{
    public class ReceiptAllocationViewModel
    {
      
        public Guid? ReceiptAllocationID { get; set; }
        public string CommodityName { get; set; }
        public decimal AllocatedQuantity { get; set; }
        public decimal DispatchedQuantity { get; set; }
        public decimal ReceivedQuantity { get; set; }
        public string Hub { get; set; }
        public Boolean IsCommited { get; set; }
        public DateTime ETA { get; set; }
        public String ProjectNumber { get; set; }
        public Int32 CommodityID { get; set; }
        public String SINumber { get; set; }
        public Decimal QuantityInMT { get; set; }
        public Decimal QuantityInUnit { get; set; }
        public Int32 HubID { get; set; }
        public Int32? DonorID { get; set; }
        public Int32? GiftCertificateDetailID { get; set; }
        public Int32 ProgramID { get; set; }
        public Int32 CommoditySourceID { get; set; }
        public String PurchaseOrder { get; set; }
        public Int32? SourceHubID { get; set; }
        public decimal Balance { get; set; }
        }
}