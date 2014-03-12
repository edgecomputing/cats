using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Localization.Models
{
    public partial class LocalizedText
    {
        public int LocalizedTextId { get; set; }
        public string LanguageCode { get; set; }
        public Nullable<int> PageId { get; set; }
        public string TextKey { get; set; }
        public string TranslatedText { get; set; }
        public virtual Page Page { get; set; }
    }
}
