using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Localization.Models
{
    public partial class LocalizedPagePhrase
    {
        public int PageId { get; set; }
        public string PageKey { get; set; }
        public int PhraseId { get; set; }
        public string PhraseText { get; set; }
        public string TranslatedText { get; set; }
        public string LanguageCode { get; set; }
    }
}
