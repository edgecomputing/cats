using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.ViewModels
{
    public class SINumber
    {

        public Dictionary<string, string> _SINumber()
        {
            Dictionary<string, string> SI_List = new Dictionary<string, string>();
            SI_List.Add("89", "00017044.");
            SI_List.Add("93", "00017512B");
            SI_List.Add("95", "CHINA AID");
            return SI_List;
        }
    }
}
