using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hub.MetaModels
{

		public sealed class MasterMetaModel
		{
		
			[Required(ErrorMessage="Master is required")]
    		public Int32 MasterID { get; set; }

			[Required(ErrorMessage="Name is required")]
			[StringLength(50)]
    		public String Name { get; set; }

			[Required(ErrorMessage="Sort Order is required")]
    		public Int32 SortOrder { get; set; }

    		public EntityCollection<Detail> Details { get; set; }

	   }
}

