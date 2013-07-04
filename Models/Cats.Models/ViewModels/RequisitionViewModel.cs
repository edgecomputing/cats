using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public class RequisitionViewModel
    {
        public int RequisitionID { get; set; }
        public Nullable<int> CommodityID { get; set; }
        public Nullable<int> RegionID { get; set; }
        public Nullable<int> ZoneID { get; set; }
        public Nullable<int> Round { get; set; }
        public string RequisitionNo { get; set; }
        public Nullable<int> RequestedBy { get; set; }
        public Nullable<System.DateTime> RequestedDate { get; set; }
        public Nullable<int> ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> ProgramID { get; set; }
        public Nullable<int> RegionalRequestID { get; set; }
        

      
    }
}
