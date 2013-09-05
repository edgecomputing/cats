using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Localization.Models
{
    public partial class LocalizedPhrase
    {
        public int LocalizationId { get; set; }
        public string LanguageCode { get; set; }
        public int PhraseId { get; set; }
        public string TranslatedText { get; set; }
        public virtual Language Language { get; set; }
        public virtual Phrase Phrase { get; set; }
    }
}
