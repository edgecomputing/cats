using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public class RequisitionMeta
    {
        public int RequisitionID { get; set; }
        [Display(Name="Commodity")]
        public Nullable<int> CommodityID { get; set; }
        [Display(Name="Region")]
        public Nullable<int> RegionID { get; set; }
        [Display(Name="Zone")]
        public Nullable<int> ZoneID { get; set; }
        public Nullable<int> Round { get; set; }
        [Display(Name="Requisition No")]
        public string RequisitionNo { get; set; }
        [Display(Name="Requested By")]
        public Nullable<int> RequestedBy { get; set; }
        [Display(Name="Request Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> RequestedDate { get; set; }
        public Nullable<int> ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public Nullable<int> Status { get; set; }
        [Display(Name="Program")]
        public Nullable<int> ProgramID { get; set; }

        public Nullable<int> RegionalRequestID { get; set; }
        

      
    }
}
