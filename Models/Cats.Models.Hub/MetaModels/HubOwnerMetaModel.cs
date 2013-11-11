using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hubs.MetaModels
{

		public sealed class HubOwnerMetaModel
		{
		
			[Required(ErrorMessage="Hub Owner is required")]
    		public Int32 HubOwnerID { get; set; }

			[Required(ErrorMessage="Name is required")]
			[StringLength(50)]
    		public String Name { get; set; }

			[StringLength(500)]
    		public String LongName { get; set; }

    		public EntityCollection<Hub> Hubs { get; set; }

    		public EntityCollection<Transaction> Transactions { get; set; }

	   }
}

