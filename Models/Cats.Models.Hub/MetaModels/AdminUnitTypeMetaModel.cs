using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hub.MetaModels
{

		public sealed class AdminUnitTypeMetaModel
		{
		
			[Required(ErrorMessage="Admin Unit Type is required")]
    		public Int32 AdminUnitTypeID { get; set; }

			[Required(ErrorMessage="Name is required")]
			[StringLength(50)]
    		public String Name { get; set; }

			[StringLength(50)]
    		public String NameAM { get; set; }

			[Required(ErrorMessage="Sort Order is required")]
    		public Int32 SortOrder { get; set; }

    		public EntityCollection<AdminUnit> AdminUnits { get; set; }

	   }
}

