using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;

namespace Cats.Areas.Procurement.Models
{
    public class WinnersByBidViewModel
    {
        public int BidID { get; set; }
        public int TransporterID { get; set; }
        public IEnumerable<BidWinnerViewModel> BidWinners { get; set; }
    }
    public class WinnersTransportersViewModel
    {
        public int BidID { get; set; }
        public int TransporterID { get; set; }
        public IEnumerable<WinnerTransporterViewModel> Transporters { get; set; }
    }

    public class WinnerTransporterViewModel
    {
        
        public int TransporterID { get; set; }
        public string TransporterName { get; set; }
        //public int BIDID { get; set; }
       
    }
}