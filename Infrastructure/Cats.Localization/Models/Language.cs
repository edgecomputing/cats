using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Localization.Models
{
    public partial class Language
    {
        public Language()
        {
            this.LocalizedPhrases = new List<LocalizedPhrase>();
        }

        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }
        public virtual ICollection<LocalizedPhrase> LocalizedPhrases { get; set; }
    }
}
