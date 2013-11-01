using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cats.Areas.Procurement.Models
{
    public class TransportRequisitionWithoutWinnerModel
    {
        
        public int TransReqWithoutTransporter { get; set; }
        public int TransportRequisitionID{ get; set; }
        public int RequisitionDetailID { get; set; }
        public int FdpID { get; set; }
        [Display(Name = "Destination")]
        public string FDP { get; set; }
        public string Woreda { get; set; }
        public int SourceWarehouseID { get; set; }
        public int beneficiaryNumber { get; set; }
        public string OriginWarehouse { get; set; }
        public decimal QuantityQtl { get; set; }
        public Nullable<decimal> DistanceFromOrigin { get; set; }
        public int CommodityID { get; set; }
        public string Commodity { get; set; }
        public string Zone { get; set; }
        public bool Select { get; set; }
        public decimal Amount
        {
            get { return QuantityQtl*beneficiaryNumber; }

        }
    }
}