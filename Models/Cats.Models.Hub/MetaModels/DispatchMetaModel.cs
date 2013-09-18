using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hub.MetaModels
{

		public sealed class DispatchMetaModel
		{
		
			[Required(ErrorMessage="Dispatch is required")]
    		public Guid DispatchID { get; set; }

			[Required(ErrorMessage="Partition is required")]
    		public Int32 PartitionID { get; set; }

			[Required(ErrorMessage="Hub is required")]
    		public Int32 HubID { get; set; }

			[Required(ErrorMessage="G I N is required")]
			[StringLength(7)]
    		public String GIN { get; set; }

    		public Int32 FDPID { get; set; }

			[StringLength(50)]
    		public String WeighBridgeTicketNumber { get; set; }

			[Required(ErrorMessage="Requisition No is required")]
			[StringLength(50)]
    		public String RequisitionNo { get; set; }

			[Required(ErrorMessage="Bid Number is required")]
			[StringLength(50)]
    		public String BidNumber { get; set; }

			[Required(ErrorMessage="Transporter is required")]
    		public Int32 TransporterID { get; set; }

			[Required(ErrorMessage="Driver Name is required")]
			[StringLength(50)]
    		public String DriverName { get; set; }

			[Required(ErrorMessage="Plate No_ Prime is required")]
			[StringLength(50)]
    		public String PlateNo_Prime { get; set; }

			[StringLength(50)]
    		public String PlateNo_Trailer { get; set; }

			[Required(ErrorMessage="Period Year is required")]
    		public Int32 PeriodYear { get; set; }

			[Required(ErrorMessage="Period Month is required")]
    		public Int32 PeriodMonth { get; set; }

			[Required(ErrorMessage="Round is required")]
    		public Int32 Round { get; set; }

			[Required(ErrorMessage="User Profile is required")]
    		public Int32 UserProfileID { get; set; }

			[Required(ErrorMessage="Dispatch Date is required")]
			[DataType(DataType.DateTime)]
    		public DateTime DispatchDate { get; set; }

			[Required(ErrorMessage="Created Date is required")]
			[DataType(DataType.DateTime)]
    		public DateTime CreatedDate { get; set; }

			[StringLength(4000)]
    		public String Remark { get; set; }

			[Required(ErrorMessage="Dispatched By Store Man is required")]
			[StringLength(50)]
    		public String DispatchedByStoreMan { get; set; }

    		public Guid DispatchAllocationID { get; set; }

    		public Guid OtherDispatchAllocationID { get; set; }

    		public EntityCollection<DispatchAllocation> DispatchAllocation { get; set; }

    		public EntityCollection<FDP> FDP { get; set; }

    		public EntityCollection<Hub> Hub { get; set; }

    		public EntityCollection<OtherDispatchAllocation> OtherDispatchAllocation { get; set; }

    		public EntityCollection<Transporter> Transporter { get; set; }

    		public EntityCollection<DispatchDetail> DispatchDetails { get; set; }

	   }
}

