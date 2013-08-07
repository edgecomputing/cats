using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageHelpers.Localization.Models
{
   public class TranslationViewModel
    {
       public int LocalizedTextId { get; set; }
       public string LanguageCode { get; set; }
       public string TextKey { get; set; }
       public string Value { get; set; }
    }
}
