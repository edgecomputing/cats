using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hubs.MetaModels
{

		public sealed class TransactionGroupMetaModel
		{
		
			[Required(ErrorMessage="Transaction Group is required")]
    		public Guid TransactionGroupID { get; set; }

			[Required(ErrorMessage="Partition is required")]
    		public Int32 PartitionID { get; set; }

    		public EntityCollection<Adjustment> Adjustments { get; set; }

    		public EntityCollection<DispatchDetail> DispatchDetails { get; set; }

    		public EntityCollection<InternalMovement> InternalMovements { get; set; }

    		public EntityCollection<ReceiveDetail> ReceiveDetails { get; set; }

    		public EntityCollection<Transaction> Transactions { get; set; }

	   }
}

