using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hubs.MetaModels
{

		public sealed class ShippingInstructionMetaModel
		{
		
			[Required(ErrorMessage="Shipping Instruction is required")]
    		public Int32 ShippingInstructionID { get; set; }

			[Required(ErrorMessage="Value is required")]
			[StringLength(50)]
    		public String Value { get; set; }

    		public EntityCollection<DispatchAllocation> DispatchAllocations { get; set; }

    		public EntityCollection<OtherDispatchAllocation> OtherDispatchAllocations { get; set; }

    		public EntityCollection<Transaction> Transactions { get; set; }

	   }
}

