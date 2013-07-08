using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.ViewModels
{
   public class RequisitionToDispatch
    {
       public int RequisitionID { get; set; }
       public int ZoneID { get; set; }
       public int WoredaID { get; set; }
       public int HubID { get; set; }
       public decimal QuanityInQtl { get; set; }
       public string RequisitionNo { get; set; }
       public string Zone { get; set; }
       public string Woreda { get; set; }
       public string  OrignWarehouse { get; set; }
       public int RequisitionStatus { get; set; }
       public string RequisitionStatusName { get; set; }

    }
}
