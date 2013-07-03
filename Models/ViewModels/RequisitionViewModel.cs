using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public virtual AdminUnit AdminUnit { get; set; }
        public virtual AdminUnit AdminUnit1 { get; set; }
        public virtual Program Program { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual UserProfile UserProfile1 { get; set; }

        public int RequisitionDetailID { get; set; }
       
      
        public int BenficiaryNo { get; set; }
        public decimal Amount { get; set; }
        public int FDPID { get; set; }
        public Nullable<int> DonorID { get; set; }
        public virtual Commodity Commodity { get; set; }
        public virtual Donor Donor { get; set; }
        public virtual FDP FDP { get; set; }

        public bool IsSelected { get; set; }
     

      
    }
}
