using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hubs.MetaModels
{

		public sealed class AccountMetaModel
		{
		
			[Required(ErrorMessage="Account is required")]
    		public Int32 AccountID { get; set; }

			[Required(ErrorMessage="Entity Type is required")]
			[StringLength(50)]
    		public String EntityType { get; set; }

			[Required(ErrorMessage="Entity is required")]
    		public Int32 EntityID { get; set; }

    		public EntityCollection<Transaction> Transactions { get; set; }

	   }
}

