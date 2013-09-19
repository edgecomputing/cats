using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hub.MetaModels
{

		public sealed class CommodityGradeMetaModel
		{
		
			[Required(ErrorMessage="Commodity Grade is required")]
    		public Int32 CommodityGradeID { get; set; }

			[Required(ErrorMessage="Name is required")]
			[StringLength(50)]
    		public String Name { get; set; }

			[StringLength(50)]
    		public String Description { get; set; }

			[Required(ErrorMessage="Sort Order is required")]
    		public Int32 SortOrder { get; set; }

    		public EntityCollection<Transaction> Transactions { get; set; }

	   }
}

