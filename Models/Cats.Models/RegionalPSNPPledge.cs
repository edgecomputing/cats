using System;
using System.Collections.Generic;

namespace Cats.Models
{
    public partial class RegionalPSNPPledge
    {
        public int RegionalPSNPPledgeID { get; set; }
        public int RegionalPSNPPlanDetailID { get; set; }
        public int DonorID { get; set; }
        public int CommodityID { get; set; }
        public decimal Quantity { get; set; }
        public int UnitID { get; set; }
        public System.DateTime PledgeDate { get; set; }
        public virtual Commodity Commodity { get; set; }
        public virtual Donor Donor { get; set; }
        public virtual RegionalPSNPPlanDetail RegionalPSNPPlanDetail { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
