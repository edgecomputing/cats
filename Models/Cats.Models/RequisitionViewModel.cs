using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class RequisitionViewModel
    {
        public string RequisitionNo { get; set; }
        public int RequisitionId { get; set; }
        public DateTime RequisitionDate { get; set; }
        public string Commodity { get; set; }
        public int BenficiaryNo { get; set; }
        public decimal Amount { get; set; }
        public int Status { get; set; } 
        public string Region { get; set; }
        public string Zone { get; set; }


    }
}
