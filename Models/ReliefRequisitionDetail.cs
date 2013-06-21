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
            
        }

        public int ReliefRequisitionDetailId { get; set; }
        public int ReliefRequistionId { get; set; }
        public int CommodityId { get; set; }
        public int DonorId { get; set; }
        public int NoOfBeneficiaries { get; set; }
        public decimal Amount { get; set; }
        public int Fdpid { get; set; }

        #region Navigation Properties
        public virtual ReliefRequistion ReliefRequistion { get; set; }
        #endregion
    }
}
