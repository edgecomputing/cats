using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class AddressBook
    {
        public int ContactID { get; set; }
        public int FDPID { get; set; }
        public string ContactName { get; set; }
        public string phone { get; set; }

        public virtual FDP Fdp { get; set; }
    }
}
