using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class UtilizationDetail
    {
        public int DistributionDetailId { get; set; }
        public int DistributionHeaderId { get; set; }
        public int FdpId { get; set; }
        public decimal DistributedQuantity { get; set; }
        public DateTime? DistributionSartDate { get; set; }
        public DateTime? DistributionEndDate { get; set; }
        public decimal? LossAmount { get; set; }
        public string LossReason { get; set; }
        public decimal? Transfered { get; set; }
        public virtual FDP FDP { get; set; }
        public virtual UtilizationHeader UtilizationHeader { get; set; }
    }
}
