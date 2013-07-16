using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.ViewModels
{
   public  class RequestView
    {
       public string ReferenceNumber { get; set; }
       public string RequisitionDate { get; set; }
       public string Program { get; set; }
       public int ProgramID { get; set; }
       public int Round { get; set; }
       public string Region { get; set; }
       public int Year { get; set; }
       public string Status { get; set; }
       public int StatusID { get; set; }

       
    }
}
