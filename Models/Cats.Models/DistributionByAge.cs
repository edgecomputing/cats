using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
   public class DistributionByAge
    {
       public int DistributionByAgeID { get; set; }
       public int RegionalRequestID { get; set; }
       public int FDPID { get; set; }
       public int PlanID { get; set; }
       public int ProgramID { get; set; }
       public int MaleLessThan5Years { get; set; }
       public int FemaleLessThan5Years { get; set; }
       public int MaleBetween5And18Years { get; set; }
       public int FemaleBetween5And18Years { get; set; }
       public int MaleAbove18Years { get; set; }
       public int FemaleAbove18Years { get; set; }

       public virtual RegionalRequest RegionalRequest { get; set; }
       public virtual FDP  Fdp { get; set; }
       public virtual Program  Program { get; set; }
       public virtual Plan Plan { get; set; }
             
    }
}
