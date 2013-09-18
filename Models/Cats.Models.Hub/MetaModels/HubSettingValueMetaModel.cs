using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hub.MetaModels
{

		public sealed class HubSettingValueMetaModel
		{
		
			[Required(ErrorMessage="Hub Setting Value is required")]
    		public Int32 HubSettingValueID { get; set; }

			[Required(ErrorMessage="Hub Setting is required")]
    		public Int32 HubSettingID { get; set; }

			[Required(ErrorMessage="Hub is required")]
    		public Int32 HubID { get; set; }

			[StringLength(4000)]
    		public String Value { get; set; }

    		public EntityCollection<Hub> Hub { get; set; }

    		public EntityCollection<HubSetting> HubSetting { get; set; }

	   }
}

