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
        public int CommodityID { get; set; }
        public decimal DistributedAmount { get; set; }
        public decimal StartingBalance { get; set; }
        public decimal EndingBalance { get; set; }
        public decimal TotalIn { get; set; }
        public decimal TotoalOut { get; set; }
        public DateTime DistributionStartDate { get; set; }
        public DateTime DistributionEndDate { get; set; }
        public decimal LossAmount { get; set; }
        public int?  LossReason { get; set; }

        public virtual FDP FDP { get; set; }
        public virtual WoredaStockDistribution WoredaStockDistribution { get; set; }
    }
}
