using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.ViewModels
{
   public class DistributionByAgeViewModel
    {
       public int RegionalRequestID { get; set; }
       public int FdpID { get; set; }
       public string FdpName { get; set; }
       public int ProgramID { get; set; }
       public string ProgramName { get; set; }
       public int PlanID { get; set; }
       public string PlanName { get; set; }
       public int MaleLessThan5Years { get; set; }
       public int FemaleLessThan5Years { get; set; }
       public int MaleBetween5And18Years { get; set; }
       public int FemaleBetween5And18Years { get; set; }
       public int MaleAbove18Years { get; set; }
       public int FemaleAbove18Years { get; set; }
       public int Total
       {
           get
           {
               return MaleLessThan5Years + MaleBetween5And18Years + MaleAbove18Years + 
                      FemaleLessThan5Years + FemaleBetween5And18Years + FemaleAbove18Years;
           } 
       }
    }
}
