using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Localization.Models
{
    public partial class Language
    {
        //public TimeSpan? Name;
        public Language()
        {
            this.LocalizedTextes = new List<LocalizedText>();
        }

        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }        
        public virtual ICollection<LocalizedText> LocalizedTextes { get; set; }
    }
}