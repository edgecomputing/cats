using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs
{
    public partial class Dispatch
    {
        public Dispatch()
        {
            this.DispatchDetails = new List<DispatchDetail>();
        }
        [Key]
        public System.Guid DispatchID { get; set; }
        public int? PartitionID { get; set; }
        public int HubID { get; set; }
        public string GIN { get; set; }
        public Nullable<int> FDPID { get; set; }
        public string WeighBridgeTicketNumber { get; set; }
        public string RequisitionNo { get; set; }
        public string BidNumber { get; set; }
        public int TransporterID { get; set; }
        public string DriverName { get; set; }
        public string PlateNo_Prime { get; set; }
        public string PlateNo_Trailer { get; set; }
        public int PeriodYear { get; set; }
        public int PeriodMonth { get; set; }
        public int Round { get; set; }
        public int UserProfileID { get; set; }
        public System.DateTime DispatchDate { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string Remark { get; set; }
        public string DispatchedByStoreMan { get; set; }
        public Nullable<System.Guid> DispatchAllocationID { get; set; }
        public Nullable<System.Guid> OtherDispatchAllocationID { get; set; }
        public virtual DispatchAllocation DispatchAllocation { get; set; }
        public virtual FDP FDP { get; set; }
        public virtual Hub Hub { get; set; }
        public virtual OtherDispatchAllocation OtherDispatchAllocation { get; set; }
        public virtual Transporter Transporter { get; set; }
        public virtual ICollection<DispatchDetail> DispatchDetails { get; set; }
    }
}
