using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class ReliefRequisitionDetail
    {
        public ReliefRequisitionDetail()
        {
            this.RequisitionDetailLines = new List<RequisitionDetailLine>();
            this.AllocationDetailLines = new List<AllocationDetailLine>();
        }

        public int ReliefRequisitionDetailId { get; set; }
        public int ReliefRequistionId { get; set; }
        public int Fdpid { get; set; }
        public int CommodityId { get; set; }
        public int Beneficiaries { get; set; }

        #region Navigation Properties
        public virtual ReliefRequistion ReliefRequistion { get; set; }
        public virtual ICollection<RequisitionDetailLine> RequisitionDetailLines { get; set; }
        public virtual ICollection<AllocationDetailLine> AllocationDetailLines { get; set; }
        #endregion
    }
}
