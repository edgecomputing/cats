using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hubs.MetaModels
{

		public class TranslationMetaModel
		{
		
			[Required(ErrorMessage="Translation is required")]
    		public Int32 TranslationID { get; set; }

			[Required(ErrorMessage="Language Code is required")]
			[StringLength(4)]
    		public String LanguageCode { get; set; }

			[Required(ErrorMessage="Phrase is required")]
			[StringLength(4000)]
    		public String Phrase { get; set; }

			[Required(ErrorMessage="Translated Text is required")]
			[StringLength(4000)]
            [UIHint("AmharicTextBox")]
    		public String TranslatedText { get; set; }

	   }
}

