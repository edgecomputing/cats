using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hubs.MetaModels
{

		public sealed class ReceiveDetailMetaModel
		{
		
			[Required(ErrorMessage="Receive Detail is required")]
    		public Guid ReceiveDetailID { get; set; }

			[Required(ErrorMessage="Partition is required")]
    		public Int32 PartitionID { get; set; }

    		public Guid ReceiveID { get; set; }

    		public Guid TransactionGroupID { get; set; }

			[Required(ErrorMessage="Commodity is required")]
    		public Int32 CommodityID { get; set; }

			[Required(ErrorMessage="Sent Quantity In Unit is required")]
    		public Decimal SentQuantityInUnit { get; set; }

			[Required(ErrorMessage="Unit is required")]
    		public Int32 UnitID { get; set; }

			[Required(ErrorMessage="Sent Quantity In M T is required")]
    		public Decimal SentQuantityInMT { get; set; }

			[StringLength(500)]
    		public String Description { get; set; }

    		public EntityCollection<Commodity> Commodity { get; set; }

    		public EntityCollection<Receive> Receive { get; set; }

    		public EntityCollection<Unit> Unit { get; set; }

    		public EntityCollection<TransactionGroup> TransactionGroup { get; set; }

	   }
}

