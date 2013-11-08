using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cats.Areas.Logistics.Models
{
    public class TransportRequisitionDetailViewModel
    {


        public int HubID { get; set; }
        public int RequisitionID { get; set; }
        public string RequisitionNo { get; set; }
        public string RequisitionStatus { get; set; }
        public int ZoneID { get; set; }
        [Display(Name="Amount")]
        public decimal QuanityInQtl { get; set; }
        [Display(Name="Warehouse")]
        public string OrignWarehouse { get; set; }
        public int CommodityID { get; set; }
        [Display(Name="Commodity")]
        public string CommodityName { get; set; }
        public string Zone { get; set; }
        public int RegionID { get; set; }
        public string  Region { get; set; }
    }
}