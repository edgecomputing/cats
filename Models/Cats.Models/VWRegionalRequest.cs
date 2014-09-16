using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
   public class VWRegionalRequest
    {
       public int RegionalRequestID { get; set; }
       public int RegionalRequestDetailID { get; set; }
       public DateTime RequestDate { get; set; }
       public int ProgramID { get; set; }
       public string Program { get; set; }
       public int RationID { get; set; }
       public string RationName { get; set; }
       public int Month { get; set; }
       //public string MonthName { get; set; }
       //public int RegionID { get; set; }
       public string RegionName { get; set; }
       public string RequestNumber { get; set; }
       public int Year { get; set; }
       public int FDPID { get; set; }
       public string FDPName { get; set; }
       public string Woreda { get; set; }
       public string ZoneName { get; set; }
       public int Beneficiaries { get; set; }
       public int Status { get; set; }
       public string Commodity { get; set; }

    }
}
