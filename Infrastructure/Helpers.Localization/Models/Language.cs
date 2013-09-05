using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageHelpers.Localization.Models
{
   public class Language
    {

        [Key]
       public int LanguageID { get; set; }
       public string LanguageCode { get; set; }
       public string Name { get; set; }

    }
}
