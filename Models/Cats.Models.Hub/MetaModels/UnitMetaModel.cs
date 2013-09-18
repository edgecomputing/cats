using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hub.MetaModels
{

		public sealed class UnitMetaModel
		{
		
			[Required(ErrorMessage="Unit is required")]
    		public Int32 UnitID { get; set; }

			[Required(ErrorMessage="Name is required")]
			[StringLength(50)]
    		public String Name { get; set; }

    		public EntityCollection<DispatchDetail> DispatchDetails { get; set; }

    		public EntityCollection<ReceiveDetail> ReceiveDetails { get; set; }

    		public EntityCollection<Transaction> Transactions { get; set; }

	   }
}

