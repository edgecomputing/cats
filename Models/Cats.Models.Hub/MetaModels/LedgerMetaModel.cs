using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hubs.MetaModels
{

		public sealed class LedgerMetaModel
		{
		
			[Required(ErrorMessage="Ledger is required")]
    		public Int32 LedgerID { get; set; }

			[Required(ErrorMessage="Name is required")]
			[StringLength(50)]
    		public String Name { get; set; }

			[Required(ErrorMessage="Ledger Type is required")]
    		public Int32 LedgerTypeID { get; set; }

    		public EntityCollection<LedgerType> LedgerType { get; set; }

    		public EntityCollection<Transaction> Transactions { get; set; }

	   }
}

