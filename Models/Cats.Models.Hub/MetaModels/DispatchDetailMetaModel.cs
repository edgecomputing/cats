using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hub.MetaModels
{

		public sealed class DispatchDetailMetaModel
		{
		
			[Required(ErrorMessage="Dispatch Detail is required")]
    		public Guid DispatchDetailID { get; set; }

			[Required(ErrorMessage="Partition is required")]
    		public Int32 PartitionID { get; set; }

    		public Guid TransactionGroupID { get; set; }

    		public Guid DispatchID { get; set; }

			[Required(ErrorMessage="Commodity is required")]
    		public Int32 CommodityID { get; set; }

			[Required(ErrorMessage="Requested Qunatity In Unit is required")]
    		public Decimal RequestedQunatityInUnit { get; set; }

			[Required(ErrorMessage="Unit is required")]
    		public Int32 UnitID { get; set; }

			[Required(ErrorMessage="Requested Quantity In M T is required")]
    		public Decimal RequestedQuantityInMT { get; set; }

			[StringLength(500)]
    		public String Description { get; set; }

    		public EntityCollection<Commodity> Commodity { get; set; }

    		public EntityCollection<Dispatch> Dispatch { get; set; }

    		public EntityCollection<Unit> Unit { get; set; }

    		public EntityCollection<TransactionGroup> TransactionGroup { get; set; }

	   }
}

