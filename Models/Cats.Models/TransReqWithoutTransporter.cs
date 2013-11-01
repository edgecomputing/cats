using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
   public class TransReqWithoutTransporter
    {
       public int TransReqWithoutTransporterID { get; set; }
       public int RequisitionDetailID { get; set; }
       public int TransportRequisitionDetailID { get; set; }
       public bool IsAssigned { get; set; }

       public virtual TransportRequisitionDetail TransportRequisitionDetail { get; set; }
       public virtual ReliefRequisitionDetail ReliefRequisitionDetail { get; set; }
    }
}
