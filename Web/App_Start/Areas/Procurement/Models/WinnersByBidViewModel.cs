using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

    public class WoredaBidWinnerViewModel
    {
        public int BidID { get; set; }
        public string BidNumber { get; set; }
        public string SourceWarehouse { get; set; }
        public int SourceId { get; set; }
        public int DestinationId { get; set; }
        public string Region { get; set; }
        public string Zone { get; set; }
        public string Woreda { get; set; }
        public int LeavingTransporterID { get; set; }
        public SelectList Transporters { get; set; }
    }

    public class WoredaCancelBidWinnerViewModel
    {
        public int BidID { get; set; }
        public string BidNumber { get; set; }
        public string SourceWarehouse { get; set; }
        public int SourceId { get; set; }
        public int DestinationId { get; set; }
        public string Region { get; set; }
        public string Zone { get; set; }
        public string Woreda { get; set; }
        public List<string> CanceledTransporters { get; set; }
        public List<string> PromotedTransporters { get; set; }
    }
}