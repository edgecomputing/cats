using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    class RegionalRequestDetailMeta
    {
        public int RegionalRequestDetailID { get; set; }
        public int RegionalRequestID { get; set; }
        public int Fdpid { get; set; }
      
        public int Beneficiaries { get; set; }
    }
}
