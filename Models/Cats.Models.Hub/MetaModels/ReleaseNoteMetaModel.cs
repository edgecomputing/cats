using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hub.MetaModels
{

		public sealed class ReleaseNoteMetaModel
		{
		
			[Required(ErrorMessage="Release Note is required")]
    		public Int32 ReleaseNoteID { get; set; }

			[StringLength(50)]
    		public String ReleaseName { get; set; }

    		public Int32 BuildNumber { get; set; }

			[Required(ErrorMessage="Date is required")]
			[DataType(DataType.DateTime)]
    		public DateTime Date { get; set; }

			[Required(ErrorMessage="Notes is required")]
    		public String Notes { get; set; }

    		public String Details { get; set; }

			[Required(ErrorMessage="D Build Quality is required")]
    		public Int32 DBuildQuality { get; set; }

    		public String Comments { get; set; }

	   }
}

