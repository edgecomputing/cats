using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
   public class LoanReciptPlan
    {
       public int LoanReciptPlanID { get; set; }
       public int ShippingInstructionID { get; set; }
       public int HubID { get; set; }
       public int SourceHubID { get; set; }
       public int ProgramID { get; set; }
       public string ProjectCode { get; set; }
       public DateTime CreatedDate { get; set; }
       public string ReferenceNumber { get; set; }
       public int CommoditySourceID { get; set; }
       public int CommodityID { get; set; }
       public decimal Quantity { get; set; }

       public virtual Hub Hub  { get; set; }
       public virtual Hub OriginHub { get; set; }
       public virtual Program Program { get; set; }
       public virtual CommoditySource CommoditySource { get; set; }
       public virtual Commodity Commodity { get; set; }
       public virtual ShippingInstruction ShippingInstruction { get; set; }
    }
}
