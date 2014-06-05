using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.ViewModels.PSNP
{
    public class RegionalPSNPPlanDetailViewModel
    {
        public int RegionalPSNPPlanDetailID { get; set; }
        public int RegionalPSNPPlanID { get; set; }
        public int PlanedWoredaID { get; set; }
        public string PlanedWoreda { get; set; }
        public int BeneficiaryCount { get; set; }
        public int FoodRatio { get; set; }
        public int CashRatio { get; set; }
        public int Item3Ratio { get; set; }
        public int Item4Ratio { get; set; }
    }
}
