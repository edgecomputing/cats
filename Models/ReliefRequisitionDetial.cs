using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class ReliefRequisitionDetial
    {
        
        
        public int ReliefRequisitionDetialID { get; set; }
        public int RequisitionID { get; set; }
        public int Beneficiaries { get; set; }
        public int CommodityID { get; set; }
        public decimal Amount { get; set; }
        public int FDPID { get; set; }
        public int DonorID { get; set; }


        public virtual ReliefRequisition ReliefRequisition { get; set; }
        public virtual Commodity Commodity { get; set; }
        public virtual FDP Fdps { get; set; }
        public Donor Donor { get; set; }
    }
}
