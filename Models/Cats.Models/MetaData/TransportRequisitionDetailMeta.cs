using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public  class TransportRequisitionDetailMeta
    {
        public int TransportRequisitionDetailID { get; set; }
        public int TransportRequisitionID { get; set; }
        [Display(Name="Requisition No")]
        public int RequisitionID { get; set; }
      
    }
}
