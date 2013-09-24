using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LanguageHelpers.Localization.Models
{
    public sealed class LocalizedText
    {
        [Key]
        public int LocalizedTextId { get; set; }

        public string LanguageCode { get; set; }

        public string TextKey { get; set; }

        [UIHint("AmharicTextBox")]
        public string TranslatedText { get; set; }
    }
}