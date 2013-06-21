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

        public int ReliefRequistionID { get; set; }
        public DateTime RequistionDate { get; set; }
        public int ProgramID { get; set; }
        public int RoundID { get; set; }
        public int RequestedByUserID { get; set; }
        public int CertifiedByUserID { get; set; }
        public int AuthorisedByUserId { get; set; }
        public string Remark { get; set; }

        public virtual ICollection<ReliefRequisitionDetail> ReliefRequisitionDetails { get; set; }
        public virtual Round Round { get; set; }
    }
}
