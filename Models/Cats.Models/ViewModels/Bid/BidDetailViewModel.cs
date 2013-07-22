using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.ViewModels.Bid
{
    public class BidDetailViewModel
    {
        public int BidDetailID { get; set; }
        public int BidID { get; set; }
        public int RegionID { get; set; }
        public string Region { get; set; }
        public decimal AmountForReliefProgram { get; set; }
        public decimal AmountForPSNPProgram { get; set; }
        public decimal BidDocumentPrice { get; set; }
        public decimal CPO { get; set; }
    }
}
