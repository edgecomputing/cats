using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hub.MetaModels
{

		public sealed class CommoditySourceMetaModel
		{
		
			[Required(ErrorMessage="Commodity Source is required")]
    		public Int32 CommoditySourceID { get; set; }

			[Required(ErrorMessage="Name is required")]
			[StringLength(50)]
    		public String Name { get; set; }

    		public EntityCollection<ReceiptAllocation> ReceiptAllocations { get; set; }

    		public EntityCollection<Receive> Receives { get; set; }

	   }
}

