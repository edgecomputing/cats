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
        public virtual FDP FDP { get; set; }
        public virtual UtilizationHeader HeaderDistribution { get; set; }
    }
}
