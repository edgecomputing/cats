using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class vwPSNPAnnualPlan
    {
        public Nullable<int> FoodRatio { get; set; }
        public Nullable<int> CashRatio { get; set; }
        public Nullable<int> BeneficiaryCount { get; set; }
        public Nullable<int> PlanedFDPID { get; set; }
        public Nullable<int> Duration { get; set; }
        //public Nullable<int> RegionID { get; set; }
        public Nullable<int> Year { get; set; }
        public int RegionalPSNPPlanID { get; set; }
        //public string RegionName { get; set; }
        public string FDPName { get; set; }
        public string WoredaName { get; set; }
        public int WoredaID { get; set; }
        public int ZoneID { get; set; }
        public string ZoneName { get; set; }
    }
}
