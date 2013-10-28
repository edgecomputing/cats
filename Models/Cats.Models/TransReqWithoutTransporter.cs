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
       public int TransportRequisitionID { get; set; }
       public bool IsAssigned { get; set; }

       public virtual TransportRequisition TransportRequisition { get; set; } 
    }
}
