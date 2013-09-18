using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hub.MetaModels
{

		public sealed class AdjustmentMetaModel
		{
		
			[Required(ErrorMessage="Adjustment is required")]
    		public Guid AdjustmentID { get; set; }

			[Required(ErrorMessage="Partition is required")]
    		public Int32 PartitionID { get; set; }

    		public Guid TransactionGroupID { get; set; }

			[Required(ErrorMessage="Hub is required")]
    		public Int32 HubID { get; set; }

			[Required(ErrorMessage="Adjustment Reason is required")]
    		public Int32 AdjustmentReasonID { get; set; }

			[Required(ErrorMessage="Adjustment Direction is required")]
			[StringLength(1)]
    		public String AdjustmentDirection { get; set; }

			[Required(ErrorMessage="Adjustment Date is required")]
			[DataType(DataType.DateTime)]
    		public DateTime AdjustmentDate { get; set; }

			[Required(ErrorMessage="Approved By is required")]
			[StringLength(50)]
    		public String ApprovedBy { get; set; }

			[StringLength(500)]
    		public String Remarks { get; set; }

			[Required(ErrorMessage="User Profile is required")]
    		public Int32 UserProfileID { get; set; }

			[Required(ErrorMessage="Reference Number is required")]
			[StringLength(50)]
    		public String ReferenceNumber { get; set; }

			[Required(ErrorMessage="Store Man Name is required")]
			[StringLength(50)]
    		public String StoreManName { get; set; }

    		public EntityCollection<AdjustmentReason> AdjustmentReason { get; set; }

    		public EntityCollection<TransactionGroup> TransactionGroup { get; set; }

    		public EntityCollection<UserProfile> UserProfile { get; set; }

	   }
}

