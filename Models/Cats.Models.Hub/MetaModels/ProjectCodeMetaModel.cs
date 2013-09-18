using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hub.MetaModels
{

		public sealed class ProjectCodeMetaModel
		{
		
			[Required(ErrorMessage="Project Code is required")]
    		public Int32 ProjectCodeID { get; set; }

			[Required(ErrorMessage="Value is required")]
			[StringLength(50)]
    		public String Value { get; set; }

    		public EntityCollection<DispatchAllocation> DispatchAllocations { get; set; }

    		public EntityCollection<OtherDispatchAllocation> OtherDispatchAllocations { get; set; }

    		public EntityCollection<Transaction> Transactions { get; set; }

	   }
}

