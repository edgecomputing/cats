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
            this.Phrases = new List<Phrase>();
        }

        public int PageId { get; set; }
        public string PageKey { get; set; }
        public virtual ICollection<Phrase> Phrases { get; set; }
    }
}
