using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Localization.Models
{
    public partial class Page
    {
        public Page()
        {
            //this.LocalizedTexts = new List<LocalizedText>();
        }

        public int PageId { get; set; }
        public string PageKey { get; set; }
        public string PageDescription { get; set; }
        public virtual ICollection<LocalizedText> LocalizedTexts { get; set; }
    }
}
