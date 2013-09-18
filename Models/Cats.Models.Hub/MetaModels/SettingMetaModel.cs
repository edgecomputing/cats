using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hub.MetaModels
{

		public sealed class SettingMetaModel
		{
		
			[Required(ErrorMessage="Setting is required")]
    		public Int32 SettingID { get; set; }

			[Required(ErrorMessage="Category is required")]
			[StringLength(100)]
    		public String Category { get; set; }

			[Required(ErrorMessage="Key is required")]
			[StringLength(100)]
    		public String Key { get; set; }

			[Required(ErrorMessage="Value is required")]
			[StringLength(100)]
    		public String Value { get; set; }

			[StringLength(100)]
    		public String Option { get; set; }

			[Required(ErrorMessage="Type is required")]
			[StringLength(100)]
    		public String Type { get; set; }

	   }
}

