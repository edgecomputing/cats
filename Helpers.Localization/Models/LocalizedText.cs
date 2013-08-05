using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Helpers.Localization.Models
{
    public class LocalizedText
    {

        [Key]
        public int LocalizedTextId { get; set; }


        public String LanguageCode { get; set; }


        public string TextKey { get; set; }


        public string Value { get; set; }
    }
}
