using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hubs.MetaModels
{

		public sealed class CommodityTypeMetaModel
		{
		
			[Required(ErrorMessage="Commodity Type is required")]
    		public Int32 CommodityTypeID { get; set; }

			[Required(ErrorMessage="Name is required")]
			[StringLength(50)]
    		public String Name { get; set; }

    		public EntityCollection<Commodity> Commodities { get; set; }

    		public EntityCollection<Receive> Receives { get; set; }

	   }
}

