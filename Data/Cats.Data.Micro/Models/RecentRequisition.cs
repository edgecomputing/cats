using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Cats.Data.Micro;

namespace Cats.Data.Micro.Models
{
    public class RecentRequisition:  DynamicModel
    {
        public RecentRequisition()
            : base("CatsContext", "Dashborad_Regional_Requisitions.", "RequisitionID")
        {
            
        }
    }
}
