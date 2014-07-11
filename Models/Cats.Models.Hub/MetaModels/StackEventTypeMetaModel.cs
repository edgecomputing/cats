using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hubs.MetaModels
{

		public sealed class StackEventTypeMetaModel
		{
		
			[Required(ErrorMessage="Stack Event Type is required")]
    		public Int32 StackEventTypeID { get; set; }

			[Required(ErrorMessage="Name is required")]
			[StringLength(50)]
    		public String Name { get; set; }

			[StringLength(500)]
    		public String Description { get; set; }

			[Required(ErrorMessage="Periodic is required")]
    		public Boolean Periodic { get; set; }

    		public Int32 DefaultFollowUpDuration { get; set; }

    		public EntityCollection<StackEvent> StackEvents { get; set; }

	   }
}

