using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public class RequisitionViewModel
    {
        public string RequisitionNo { get; set; }
        public int RequisitionId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime RequisitionDate { get; set; }
        public string Commodity { get; set; }
        public int BenficiaryNo { get; set; }
        public decimal Amount { get; set; }
        public int Status { get; set; }
        public int RegionId { get; set; }
        public string Region { get; set; }
        public string Zone { get; set; }
        public int HubId { get; set; }
        public string Hub { get; set; }
        public decimal AmountAllocated { get; set; }
        public string StrRequisitionDate { get; set; }
       
    }
}
