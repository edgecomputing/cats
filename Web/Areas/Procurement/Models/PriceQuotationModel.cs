using System;
using System.Collections.Generic;

namespace Cats.Areas.Procurement.Models
{
    public class PriceQuotationFilterViewModel
    {
        public int BidID { get; set; }
        public int TransporterID { get; set; }
        public int RegionID { get; set; }
    }

    public class PriceQuotationFilterOfferlessViewModel
    {
        public int BidID { get; set; }
        public int RegionID { get; set; }
        public int HubID { get; set; }
    }

    public class WinnersGeneratorParameters
    {
        public int RegionID { get; set; }
        public int BidID { get; set; }
    }

    public class GoodsMovementDetailViewModel
    {
        //public string SourceWarehouse { get; set; }
        public string SourceName { get; set; }
        public int SourceID { get; set; }
        //public Hub SourceWarehouse { get; set; }
        public string DestinationZone { get; set; }
        //public AdminUnit DestinationWoreda { get; set; }
        public string DestinationName { get; set; }
        public int DestinationID { get; set; }
       
        public string RegionName { get; set; }
        public int RegionID { get; set; }
    }

    public class PriceQuotationDetailViewModel : GoodsMovementDetailViewModel
    {
        public int QuotationID { get; set; }
        public int Tariff { get; set; }
        public string Remark { get; set; }
        public int Rank { get; set; }
        public bool IsWinner { get; set; }
        public int TransportBidQuotationID { get; set; }
        public int BidID { get; set; }
        public int TransporterID { get; set; }
    }

    public class PriceQuotationDetail
    {
        public int TransportBidQuotationID { get; set; }
        public int HeaderId { get; set; }
        public string SourceWarehouse { get; set; }
        public string Zone { get; set; }
        public string Woreda { get; set; }
        public decimal Tariff { get; set; }
        public string Remark { get; set; }
        public int BidID { get; set; }
        public int TransporterID { get; set; }
        public int SourceID { get; set; }
        public int DestinationID { get; set; }
    }
    public class SelectedBidWinnerViewModel
    {
        public List<BidWinnerViewModel> Bidwinners { get; set; }
    }
    public class BidWinnerViewModel  
    {
        public int BidWinnnerID { get; set; }
        public string SourceWarehouse { get; set; }
        public int SourceId { get; set; }
        public int DestinationId { get; set; }
        public string Zone { get; set; }
        public string Woreda { get; set; }
        public int TransporterID { get; set; }
        public string TransporterName { get; set; }
        public int Rank { get; set; }
        public Nullable<decimal> WinnerTariff { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> StatusID { get; set; }
        public int BidID { get; set; }
        public int BidPlanID { get; set; }
        public string BidMumber { get; set; }
        public string BidStartDate { get; set; }
        public string BidEndDate { get; set; }
        public string BidOpeningDate { get; set; }
        public bool Selected { get; set; }
    }
    public class BidWinnerViewingModel
    {
        public BidWinnerViewingModel()
        {
            TransporterID = new List<string>();
            TransporterName = new List<string>();
        }
        public string BidID { get; set; }
        public string SourceWarehouse { get; set; }
        public string SourceId { get; set; }
        public string DestinationId { get; set; }
        public string Zone { get; set; }
        public string Woreda { get; set; }
        public List<string> TransporterID { get; set; }
        public List<string> TransporterName { get; set; }
        public string Rank { get; set; }
        public string WinnerTariff { get; set; }
        public string Quantity { get; set; }
        public string Status { get; set; }
        public string StatusID { get; set; }

    }
    public class BidWithWinnerViewModel
    {
        public int BidWinnerID { get; set; }
        public int BidID { get; set; }
        public string OpeningDate { get; set; }
        public string BidNumber { get; set; }
        public int Year { get; set; }
    }
}