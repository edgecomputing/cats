using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class DonationPlanHeader
    {
        public DonationPlanHeader()
        {
            this.DonationPlanDetails = new List<DonationPlanDetail>();
        }

        public int DonationHeaderPlanID { get; set; }
        public int ShippingInstructionId { get; set; }
        public Nullable<int> GiftCertificateID { get; set; }
        public int CommodityID { get; set; }
        public int? CommodityTypeID { get; set; }
        public decimal DonatedAmount { get; set; }
        public int DonorID { get; set; }
        public int ProgramID { get; set; }
        public System.DateTime ETA { get; set; }
        public string Vessel { get; set; }
        public string ReferenceNo { get; set; }
        public Nullable<int> ModeOfTransport { get; set; }
        public Nullable<System.Guid> TransactionGroupID { get; set; }
        public Nullable<bool> IsCommited { get; set; }
        public Nullable<int> EnteredBy { get; set; }
        public Nullable<System.DateTime> AllocationDate { get; set; }
        public string Remark { get; set; }
        public int? Status { get; set; }
        public int? PartitionId { get; set; }

        public virtual Commodity Commodity { get; set; }
        public virtual CommodityType CommodityType { get; set; }
        public virtual ICollection<DonationPlanDetail> DonationPlanDetails { get; set; }
        public virtual ShippingInstruction ShippingInstruction { get; set; }
        public virtual Donor Donor { get; set; }
        public virtual Program Program { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
