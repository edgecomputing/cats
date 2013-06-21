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

        public int ReliefRequisitionDetailID { get; set; }
        public int ReliefRequistionID { get; set; }
        public int CommodityID { get; set; }
        public int DonorID { get; set; }
        public int NoOfBeneficiaries { get; set; }
        public decimal Amount { get; set; }
        public int FDPID { get; set; }

        #region Navigation Properties
        public virtual ReliefRequistion ReliefRequistion { get; set; }
        #endregion
    }
}
