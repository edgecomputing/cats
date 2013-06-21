using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class ReliefRequistion
    {
        public ReliefRequistion()
        {
            this.ReliefRequisitionDetails = new List<ReliefRequisitionDetail>();
        }

        public int ReliefRequistionId { get; set; }
        public DateTime RequistionDate { get; set; }
        public int ProgramId { get; set; }
        public int RoundId { get; set; }
        public int RequestedByUserId { get; set; }
        public int CertifiedByUserId { get; set; }
        public int AuthorisedByUserId { get; set; }
        public string Remark { get; set; }

        public virtual ICollection<ReliefRequisitionDetail> ReliefRequisitionDetails { get; set; }
        public virtual Round Round { get; set; }
    }
}
