using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
   public  class HubAllocationByRegionViewModel
    {
       public int RegionId { get; set; }
       public string Region { get; set; }
       public decimal AllocatedAmount { get; set; }
       public int HubId { get; set; }
       public string Hub { get; set; }
    }
}
