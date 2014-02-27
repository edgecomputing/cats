using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Cats.Data.Micro;

namespace Cats.Data.Micro.Models
{
    public class RequisitionPercentage: DynamicModel
    {
        public RequisitionPercentage()
            : base("CatsContext", "Dashboard_regional_ReqPercentage","Status")
        {
            
        }
    }
}