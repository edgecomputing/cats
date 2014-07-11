using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hubs.MetaModels
{

		public sealed class ReceiveMetaModel
		{
		
			[Required(ErrorMessage="Receive is required")]
    		public Guid ReceiveID { get; set; }

			[Required(ErrorMessage="Partition is required")]
    		public Int32 PartitionID { get; set; }

			[Required(ErrorMessage="Hub is required")]
    		public Int32 HubID { get; set; }

			[Required(ErrorMessage="G R N is required")]
			[StringLength(7)]
    		public String GRN { get; set; }

			[Required(ErrorMessage="Commodity Type is required")]
    		public Int32 CommodityTypeID { get; set; }

    		public Int32 SourceDonorID { get; set; }

    		public Int32 ResponsibleDonorID { get; set; }

			[Required(ErrorMessage="Transporter is required")]
    		public Int32 TransporterID { get; set; }

			[StringLength(50)]
    		public String PlateNo_Prime { get; set; }

			[StringLength(50)]
    		public String PlateNo_Trailer { get; set; }

			[StringLength(50)]
    		public String DriverName { get; set; }

			[StringLength(10)]
    		public String WeightBridgeTicketNumber { get; set; }

    		public Decimal WeightBeforeUnloading { get; set; }

    		public Decimal WeightAfterUnloading { get; set; }

			[Required(ErrorMessage="Receipt Date is required")]
			[DataType(DataType.DateTime)]
    		public DateTime ReceiptDate { get; set; }

			[Required(ErrorMessage="User Profile is required")]
    		public Int32 UserProfileID { get; set; }

			[Required(ErrorMessage="Created Date is required")]
			[DataType(DataType.DateTime)]
    		public DateTime CreatedDate { get; set; }

			[StringLength(50)]
    		public String WayBillNo { get; set; }

			[Required(ErrorMessage="Commodity Source is required")]
    		public Int32 CommoditySourceID { get; set; }

			[StringLength(4000)]
    		public String Remark { get; set; }

			[StringLength(50)]
    		public String VesselName { get; set; }

			[Required(ErrorMessage="Received By Store Man is required")]
			[StringLength(50)]
    		public String ReceivedByStoreMan { get; set; }

			[StringLength(50)]
    		public String PortName { get; set; }

			[StringLength(50)]
    		public String PurchaseOrder { get; set; }

			[StringLength(50)]
    		public String SupplierName { get; set; }

    		public Guid ReceiptAllocationID { get; set; }

    		public EntityCollection<CommoditySource> CommoditySource { get; set; }

    		public EntityCollection<CommodityType> CommodityType { get; set; }

            //public EntityCollection<Donor> Donor { get; set; }

            //public EntityCollection<Donor> Donor1 { get; set; }

    		public EntityCollection<Hub> Hub { get; set; }

    		public EntityCollection<ReceiptAllocation> ReceiptAllocation { get; set; }

    		public EntityCollection<Transporter> Transporter { get; set; }

    		public EntityCollection<UserProfile> UserProfile { get; set; }

    		public EntityCollection<ReceiveDetail> ReceiveDetails { get; set; }

	   }
}

