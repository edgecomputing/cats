using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.ViewModels
{
   public  class BeneficiaryAllocation
    {
       public int CommodityID { get; set; }
       public string Commodity { get; set; }
       public int RequisitionID { get; set; }
       public string RequisitionNo { get; set; }
       public Nullable<int>  DonorID { get; set; }
       public string Donor { get; set; }
       public int BeneficiaryNo { get; set; }
       public decimal Amount { get; set; }
       public Nullable<int> RegionID { get; set; }
       public string Region { get; set; }
       public Nullable<int> ZoneID { get; set; }
       public string Zone { get; set; }
       public int WoredaID { get; set; }
       public string Woreda { get; set; }
       public int FDPID { get; set; }
       public string FDP { get; set; }
       public Nullable<int> ProgramID { get; set; }
       public string Program { get; set; }
       public string Month { get; set; }
       public int Year { get; set; }
       public int Round { get; set; }
    }
}
