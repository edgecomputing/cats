using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
   public class LossReason
    {
       public int LossReasonId { get; set; }
       public string LossReasonEg { get; set; }
       public string LossReasonAm { get; set; }
       public string LossReasonCodeEg { get; set; }
       public string LossReasonCodeAm { get; set; }
       public string Description { get; set; }

    }
}
