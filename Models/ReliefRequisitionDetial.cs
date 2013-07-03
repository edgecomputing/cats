using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class ReliefRequisitionDetail
    {


        public int RequisitionDetailID { get; set; }
        public int RequisitionID { get; set; }
        public int CommodityID { get; set; }
        public int BenficiaryNo { get; set; }
        public decimal Amount { get; set; }
        public int FDPID { get; set; }
        public Nullable<int> DonorID { get; set; }
        public virtual Commodity Commodity { get; set; }
        public virtual Donor Donor { get; set; }
        public virtual FDP FDP { get; set; }
        public virtual ReliefRequisition ReliefRequisition { get; set; }
    }
}
