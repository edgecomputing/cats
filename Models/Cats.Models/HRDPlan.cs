using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
   public class HRDPlan
    {
       public HRDPlan()
       {
           Hrds=new List<HRD>();
       }

       public int PlanID { get; set; }
       public string PlanName { get; set; }
       public DateTime StartDate { get; set; }
       public DateTime EndDate { get; set; }
       public int ProgramID { get; set; }

       public virtual ICollection<HRD> Hrds { get; set; }
       public virtual Program Program { get; set; }
    }
}
