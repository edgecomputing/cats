using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cats.Areas.Procurement.Models
{
    public class TransportOrderDetailViewModel
    {
        public int TransportOrderDetailID { get; set; }
        public int TransportOrderID { get; set; }
        public int FdpID { get; set; }
        [Display(Name="Destination")]
        public string FDP { get; set; }
        public string Woreda { get; set; }
        public int SourceWarehouseID { get; set; }
        public string OriginWarehouse { get; set; }
        public decimal QuantityQtl { get; set; }
        public Nullable<decimal> DistanceFromOrigin { get; set; }
        public decimal TariffPerQtl { get; set; }
        public int RequisitionID { get; set; }
        public string RequisitionNo { get; set; }
        public int CommodityID { get; set; }
        public string Commodity { get; set; }
        public Nullable<int> ZoneID { get; set; }
        public string Zone { get; set; }
        public Nullable<int> DonorID { get; set; }
        public string Donor { get; set; }

        public decimal TotalAmount
        {
            get { return QuantityQtl*TariffPerQtl; }
        }
    }
}