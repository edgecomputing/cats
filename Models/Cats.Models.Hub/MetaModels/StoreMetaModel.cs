using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hubs.MetaModels
{

		public sealed class StoreMetaModel
		{
		
			[Required(ErrorMessage="Store is required")]
    		public Int32 StoreID { get; set; }

			[Required(ErrorMessage="Number is required")]
    		public Int32 Number { get; set; }

			[Required(ErrorMessage="Name is required")]
			[StringLength(50)]
    		public String Name { get; set; }

			[Required(ErrorMessage="Hub is required")]
    		public Int32 HubID { get; set; }

			[Required(ErrorMessage="Is Temporary is required")]
    		public Boolean IsTemporary { get; set; }

			[Required(ErrorMessage="Is Active is required")]
    		public Boolean IsActive { get; set; }

			[Required(ErrorMessage="Stack Count is required")]
    		public Int32 StackCount { get; set; }

			[StringLength(50)]
    		public String StoreManName { get; set; }

    		public EntityCollection<Hub> Hub { get; set; }

    		public EntityCollection<Transaction> Transactions { get; set; }

	   }
}

