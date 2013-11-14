using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
   public class Plan
    {
       public Plan()
       {
           Hrds=new List<HRD>();
           NeedAssessments=new List<NeedAssessment>();
       }

       public int PlanID { get; set; }
       public string PlanName { get; set; }
       public DateTime StartDate { get; set; }
       public DateTime EndDate { get; set; }
       public int ProgramID { get; set; }
       public string Remark { get; set; }
       public int Status { get; set; }

       public virtual ICollection<HRD> Hrds { get; set; }
       public virtual ICollection<NeedAssessment> NeedAssessments { get; set;} 
       public virtual Program Program { get; set; }
       public virtual ICollection<RegionalPSNPPlan> RegionalPSNPPlans { get; set; }
    }
}
