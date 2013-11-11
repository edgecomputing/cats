using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hubs.MetaModels
{

		public sealed class AdjustmentReasonMetaModel
		{
		
			[Required(ErrorMessage="Adjustment Reason is required")]
    		public Int32 AdjustmentReasonID { get; set; }

			[StringLength(50)]
    		public String Name { get; set; }

			[Required(ErrorMessage="Direction is required")]
			[StringLength(1)]
    		public String Direction { get; set; }

    		public EntityCollection<Adjustment> Adjustments { get; set; }

	   }
}

