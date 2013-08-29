using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.ViewModels
{
    public class FDPsCoveredByDonors
    {
        public string Donor { get; set; }
        public string Region { get; set; }
        public string Zone { get; set; }
        public string Woreda { get; set; }
        public string FDP { get; set; }
        public string Commodity { get; set; }
        public string NeededQty { get; set; }
        public string NeededQtyUnit { get; set; }
        public string PledgedQty { get; set; }
        public string PledgedQtyUnit { get; set; }
        public string PledgeDate { get; set; }
    }
}
