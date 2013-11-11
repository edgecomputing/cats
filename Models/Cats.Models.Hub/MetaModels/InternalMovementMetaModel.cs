using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hubs.MetaModels
{

		public sealed class InternalMovementMetaModel
		{
		
			[Required(ErrorMessage="Internal Movement is required")]
    		public Guid InternalMovementID { get; set; }

			[Required(ErrorMessage="Partition is required")]
    		public Int32 PartitionID { get; set; }

			[Required(ErrorMessage="Hub is required")]
    		public Int32 HubID { get; set; }

    		public Guid TransactionGroupID { get; set; }

			[Required(ErrorMessage="Transfer Date is required")]
			[DataType(DataType.DateTime)]
    		public DateTime TransferDate { get; set; }

			[Required(ErrorMessage="Reference Number is required")]
			[StringLength(50)]
    		public String ReferenceNumber { get; set; }

			[Required(ErrorMessage="D Reason is required")]
    		public Int32 DReason { get; set; }

			[StringLength(4000)]
    		public String Notes { get; set; }

			[Required(ErrorMessage="Approved By is required")]
			[StringLength(50)]
    		public String ApprovedBy { get; set; }

    		public EntityCollection<Detail> Detail { get; set; }

    		public EntityCollection<TransactionGroup> TransactionGroup { get; set; }

	   }
}

