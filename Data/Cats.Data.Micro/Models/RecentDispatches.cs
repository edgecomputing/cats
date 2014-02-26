using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Cats.Data.Micro;

namespace Cats.Data.Micro.Models
{
    public class RecentDispatches : DynamicModel
    {
        public RecentDispatches()
            : base("CatsContext", "Dashboard_Regional_Dispatches")
        {

        }
    }
}