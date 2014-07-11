using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
   public class Currency
    {

       public Currency()
       {
           ContributionDetails=new List<ContributionDetail>();
       }

       public int CurrencyID { get; set; }
       public string Code { get; set; }
       public string Name { get; set; }

       public virtual ICollection<ContributionDetail> ContributionDetails { get; set; }


    }
}
