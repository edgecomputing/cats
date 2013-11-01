using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
   
        public partial class AllocationByRegion
        {
            public Nullable<int> Status { get; set; }
            public Nullable<int> RegionID { get; set; }
            public string Name { get; set; }
            public Nullable<decimal> Amount { get; set; }
            public Nullable<int> BenficiaryNo { get; set; }
            public string Hub { get; set; }
        }
    
}
