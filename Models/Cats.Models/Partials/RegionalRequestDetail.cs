using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
   public partial  class RegionalRequestDetail
    {
       [NotMapped]
       public string GrainName {get { return "Grain"; } }
       [NotMapped]
       public string CSBName { get { return "CSB"; } }
       [NotMapped]
       public string OilName { get { return "Oil"; } }
       [NotMapped]
       public string PulseName { get { return "Pulse"; } }
    }
}
