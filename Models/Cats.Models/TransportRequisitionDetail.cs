using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    [MetadataType(typeof(TransportRequisitionDetailMeta))]
    public partial class TransportRequisitionDetail
    {
        public int TransportRequisitionDetailID { get; set; }
        public int TransportRequisitionID { get; set; }
        public int RequisitionID { get; set; }
        public virtual ReliefRequisition ReliefRequisition { get; set; }
        public virtual TransportRequisition TransportRequisition { get; set; }
        public virtual ICollection<TransReqWithoutTransporter> TransReqWithoutTransporters { get; set; } 
    }
}
