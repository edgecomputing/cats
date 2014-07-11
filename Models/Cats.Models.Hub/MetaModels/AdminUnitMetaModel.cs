using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hubs.MetaModels
{

		public sealed class AdminUnitMetaModel
		{
		
			[Required(ErrorMessage="Admin Unit is required")]
    		public Int32 AdminUnitID { get; set; }

			[StringLength(50)]
    		public String Name { get; set; }

			[StringLength(50)]
    		public String NameAM { get; set; }

    		public Int32 AdminUnitTypeID { get; set; }

    		public Int32 ParentID { get; set; }

    		public EntityCollection<AdminUnit> AdminUnit1 { get; set; }

    		public EntityCollection<AdminUnit> AdminUnit2 { get; set; }

    		public EntityCollection<AdminUnitType> AdminUnitType { get; set; }

    		public EntityCollection<FDP> FDPs { get; set; }

	   }
}

