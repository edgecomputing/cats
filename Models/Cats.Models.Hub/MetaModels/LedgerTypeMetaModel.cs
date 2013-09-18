using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hub.MetaModels
{

		public sealed class LedgerTypeMetaModel
		{
		
			[Required(ErrorMessage="Ledger Type is required")]
    		public Int32 LedgerTypeID { get; set; }

			[Required(ErrorMessage="Name is required")]
			[StringLength(50)]
    		public String Name { get; set; }

			[Required(ErrorMessage="Direction is required")]
			[StringLength(1)]
    		public String Direction { get; set; }

    		public EntityCollection<Ledger> Ledgers { get; set; }

	   }
}

