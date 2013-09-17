using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hub.MetaModels
{

		public sealed class HubSettingMetaModel
		{
		
			[Required(ErrorMessage="Hub Setting is required")]
    		public Int32 HubSettingID { get; set; }

			[Required(ErrorMessage="Key is required")]
			[StringLength(50)]
    		public String Key { get; set; }

			[Required(ErrorMessage="Name is required")]
			[StringLength(50)]
    		public String Name { get; set; }

			[StringLength(50)]
    		public String ValueType { get; set; }

			[StringLength(500)]
    		public String Options { get; set; }

    		public EntityCollection<HubSettingValue> HubSettingValues { get; set; }

	   }
}

