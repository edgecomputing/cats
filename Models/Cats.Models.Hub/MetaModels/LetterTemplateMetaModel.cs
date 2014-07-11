using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hubs.MetaModels
{

		public sealed class LetterTemplateMetaModel
		{
		
			[Required(ErrorMessage="Letter Template is required")]
    		public Int32 LetterTemplateID { get; set; }

			[Required(ErrorMessage="Name is required")]
			[StringLength(50)]
            [UIHint("AmharicTextBox")]
    		public String Name { get; set; }

    		public String Parameters { get; set; }

			[Required(ErrorMessage="Template is required")]
    		public String Template { get; set; }

	   }
}

