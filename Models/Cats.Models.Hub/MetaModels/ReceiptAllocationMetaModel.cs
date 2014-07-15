using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hubs.MetaModels
{

		public sealed class ReceiptAllocationMetaModel
		{
		
			[Required(ErrorMessage="Receipt Allocation is required")]
    		public Guid ReceiptAllocationID { get; set; }

            //[Required(ErrorMessage="Partition is required")]
            public Int32? PartitionId { get; set; }

			[Required(ErrorMessage="Is Commited is required")]
    		public Boolean IsCommited { get; set; }

			[Required(ErrorMessage="E T A is required")]
			[DataType(DataType.DateTime)]
    		public DateTime ETA { get; set; }

			[Required(ErrorMessage="Project Number is required")]
			[StringLength(50)]
    		public String ProjectNumber { get; set; }

    		public Int32 GiftCertificateDetailID { get; set; }

			[Required(ErrorMessage="Commodity is required")]
    		public Int32 CommodityID { get; set; }

			[StringLength(50)]
    		public String SINumber { get; set; }

			[Required(ErrorMessage="Quantity In M T is required")]
    		public Decimal QuantityInMT { get; set; }

			[Required(ErrorMessage="Hub is required")]
    		public Int32 HubID { get; set; }

    		public Int32 DonorID { get; set; }

			[Required(ErrorMessage="Program is required")]
    		public Int32 ProgramID { get; set; }

			[Required(ErrorMessage="Commodity Source is required")]
    		public Int32 CommoditySourceID { get; set; }

			[Required(ErrorMessage="Is Closed is required")]
    		public Boolean IsClosed { get; set; }

			[StringLength(50)]
    		public String PurchaseOrder { get; set; }

			[StringLength(50)]
    		public String SupplierName { get; set; }

    		public Int32 SourceHubID { get; set; }

			[StringLength(50)]
    		public String OtherDocumentationRef { get; set; }

			[StringLength(50)]
    		public String Remark { get; set; }

    		public EntityCollection<Commodity> Commodity { get; set; }

    		public EntityCollection<CommoditySource> CommoditySource { get; set; }

    		public EntityCollection<Donor> Donor { get; set; }

    		public EntityCollection<GiftCertificateDetail> GiftCertificateDetail { get; set; }

    		public EntityCollection<Hub> Hub { get; set; }

    		public EntityCollection<Hub> Hub1 { get; set; }

    		public EntityCollection<Program> Program { get; set; }

    		public EntityCollection<Receive> Receives { get; set; }

	   }
}

