using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class WoredaStockDistributionDetail
    {
        public int WoredaStockDistributionDetailID { get; set; }
        public int WoredaStockDistributionID { get; set; }
        public int FdpId { get; set; }
        public decimal DistributedAmount { get; set; }

        //public DateTime? DistributionSartDate { get; set; }
        //public DateTime? DistributionEndDate { get; set; }
        //public decimal? LossAmount { get; set; }
        //public string LossReason { get; set; }
        //public decimal? Transfered { get; set; }
        public virtual FDP FDP { get; set; }
        public virtual WoredaStockDistribution WoredaStockDistribution { get; set; }
    }
}
