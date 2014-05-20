using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
   public class LocalPurchaseDetail
    {
       public int LocalPurchaseDetailID { get; set; }
       public int LocalPurchaseID { get; set; }
       public int HubID { get; set; }
       public decimal AllocatedAmount { get; set; }
       public decimal RecievedAmount { get; set; }
       public int? PartitionId { get; set; }

       public virtual LocalPurchase LocalPurchase { get; set; }
       public virtual Hub Hub { get; set; }
      
    }
}
