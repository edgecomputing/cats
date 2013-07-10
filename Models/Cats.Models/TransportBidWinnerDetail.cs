using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
   public  class TransportBidWinnerDetail
    {
       public int TransporterID { get; set; }
       public int WoredaID { get; set; }
       public int HubID { get; set; }
       public decimal TariffPerQtl { get; set; }
      
    }
}
