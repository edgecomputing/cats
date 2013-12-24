using System;
using System.Collections.Generic;

namespace Cats.Models
{
    public partial class Distribution
    {
        public Distribution()
        {
            this.DistributionDetails = new List<DistributionDetail>();
        }

        public System.Guid DistributionID { get; set; }
        public string ReceivingNumber { get; set; }
        public Nullable<int> DonorID { get; set; }
        public int TransporterID { get; set; }
        public string PlateNoPrimary { get; set; }
        public string PlateNoTrailler { get; set; }
        public string DriverName { get; set; }
        public int FDPID { get; set; }
        public Nullable<System.Guid> DispatchID { get; set; }
        public string WayBillNo { get; set; }
        public string RequisitionNo { get; set; }
        public Nullable<int> HubID { get; set; }
        public string InvoiceNo { get; set; }
        public string DeliveryBy { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public string ReceivedBy { get; set; }
        public Nullable<System.DateTime> ReceivedDate { get; set; }
        public Nullable<System.DateTime> DocumentReceivedDate { get; set; }
        public Nullable<int> DocumentReceivedBy { get; set; }
        public virtual Donor Donor { get; set; }
        public virtual FDP FDP { get; set; }
        public virtual Hub Hub { get; set; }
        public int? Status { get; set; }
        public int? ActionType { get; set; }
        public string ActionTypeRemark { get; set; }
        public virtual ICollection<DistributionDetail> DistributionDetails { get; set; }
    }
}
