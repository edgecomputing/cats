using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Localization.Models
{
    public partial class Phrase
    {
        public Phrase()
        {
            this.LocalizedPhrases = new List<LocalizedPhrase>();
            this.Pages = new List<Page>();
        }

        public int PhraseId { get; set; }
        public string PhraseText { get; set; }
        public virtual ICollection<LocalizedPhrase> LocalizedPhrases { get; set; }
        public virtual ICollection<Page> Pages { get; set; }
    }
}
