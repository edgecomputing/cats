using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class DonationPlanDetail
    {
        public int DonationDetailPlanID { get; set; }
        public int DonationHeaderPlanID { get; set; }
        public int HubID { get; set; }
        public decimal AllocatedAmount { get; set; }
        public decimal ReceivedAmount { get; set; }
        public decimal Balance { get; set; }
        public int? PartitionId { get; set; }
        public virtual DonationPlanHeader DonationPlanHeader { get; set; }
        public virtual Hub Hub { get; set; }
    }
}
