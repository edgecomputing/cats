using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    [MetadataType(typeof(TransportRequisitionMeta))]
    public partial class TransportRequisition
    {
        public TransportRequisition()
        {
            this.TransportRequisitionDetails = new List<TransportRequisitionDetail>();
        }

        public int TransportRequisitionID { get; set; }
        public string TransportRequisitionNo { get; set; }
        public int RequestedBy { get; set; }
        public System.DateTime RequestedDate { get; set; }
        public int CertifiedBy { get; set; }
        public System.DateTime CertifiedDate { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual UserProfile UserProfile1 { get; set; }
        public virtual ICollection<TransportRequisitionDetail> TransportRequisitionDetails { get; set; }
    }
}
