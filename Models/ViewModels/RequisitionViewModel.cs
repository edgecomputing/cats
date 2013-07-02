using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class RequisitionViewModel
    {
        public List<ReliefRequisition> _reliefRequisition { get; set; }
        public List<ReliefRequisitionDetail> _reliefRequisitionDetail { get; set; }

        public Donor _doner { get; set; }
        

      
    }
}
