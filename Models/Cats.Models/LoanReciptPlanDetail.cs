using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
   public class LoanReciptPlanDetail
    {
       public int LoanReciptPlanDetailID { get; set; }
       public int LoanReciptPlanID { get; set; }
       public int HubID { get; set; }
       public string MemoReferenceNumber { get; set; }
       public decimal RecievedQuantity { get; set; }
       public int ApprovedBy { get; set; }
       public DateTime RecievedDate { get; set; }


       public virtual LoanReciptPlan LoanReciptPlan { get; set; }
       public virtual Hub Hub { get; set; }
       public virtual UserProfile UserProfile { get; set; }

    }
}
