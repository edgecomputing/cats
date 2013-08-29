using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
   public  class HRDPSNPPlanInfo
    {
       public HRDPSNPPlan HRDPSNPPlan { get; set; }
       public List<BeneficiaryInfo> BeneficiaryInfos { get; set; } 
    }
}
