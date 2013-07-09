using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.ViewModels
{
    class RegionalRequestVM
    {
        public int RegionalRequestID { get; set; }
        public string Region { get; set; }
        public string Program { get; set; }
        public int Round { get; set; }
        public DateTime RequistionDate { get; set; }
        public int Year { get; set; }
        public String ReferenceNumber { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }

        
       
    }
}
