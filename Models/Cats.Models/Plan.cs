using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
           RegionalPSNPPlans=new List<RegionalPSNPPlan>();
       }

       public int PlanID { get; set; }
       [Required(ErrorMessage = "Enter Plan Name")]
       public string PlanName { get; set; }
        [Required(ErrorMessage = "Enter Start Date")]
       public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Enter End Date")]
       public DateTime EndDate { get; set; }
       public int ProgramID { get; set; }
       public string Remark { get; set; }
       public int Status { get; set; }
       [Required(ErrorMessage = "Enter Duration")]
       public int Duration { get; set; }

       public int? PartitionId { get; set; }

       public virtual ICollection<HRD> Hrds { get; set; }
       public virtual ICollection<NeedAssessment> NeedAssessments { get; set;} 
       public virtual Program Program { get; set; }
       public virtual ICollection<RegionalPSNPPlan> RegionalPSNPPlans { get; set; }
       public virtual ICollection<RegionalRequest> RegionalRequests { get; set; }
    }
}
