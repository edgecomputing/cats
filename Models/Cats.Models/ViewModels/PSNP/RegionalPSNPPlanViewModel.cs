using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.ViewModels.PSNP
{
    public class RegionalPSNPPlanViewModel
    {
        public int RegionalPSNPPlanID { get; set; }
        public int Year { get; set; }
        public int Duration { get; set; }
        public int RegionID { get; set; }
        public string Region { get; set; }
        public int RationID { get; set; }
        public int StatusID { get; set; }
        public List<RegionalPSNPPlanDetail> RegionalPSNPPlanDetails { get; set; }
    }
}
