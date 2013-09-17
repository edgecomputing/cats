using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hub.MetaModels
{

		public sealed class FDPMetaModel
		{
		
			[Required(ErrorMessage="FDP is required")]
    		public Int32 FDPID { get; set; }

			[Required(ErrorMessage="Name is required")]
			[StringLength(50)]
    		public String Name { get; set; }

			[StringLength(50)]
            [DisplayName("Name (Amharic)")]
            [UIHint("AmharicTextBox")]
    		public String NameAM { get; set; }

			[Required(ErrorMessage="Admin Unit is required")]
    		public Int32 AdminUnitID { get; set; }

    		public EntityCollection<AdminUnit> AdminUnit { get; set; }

    		public EntityCollection<Contact> Contacts { get; set; }

    		public EntityCollection<Dispatch> Dispatches { get; set; }

    		public EntityCollection<DispatchAllocation> DispatchAllocations { get; set; }

	   }
}

