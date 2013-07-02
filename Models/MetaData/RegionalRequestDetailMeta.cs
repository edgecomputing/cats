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
        public decimal Grain { get; set; }
        public decimal Pulse { get; set; }
        public decimal Oil { get; set; }
        public decimal CSB { get; set; }
        public int Beneficiaries { get; set; }
    }
}
