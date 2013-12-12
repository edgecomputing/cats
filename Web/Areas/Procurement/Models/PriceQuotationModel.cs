using System;

namespace Cats.Areas.Procurement.Models
{
    public class PriceQuotationFilterViewModel
    {
        public int BidPlanID { get; set; }
        public int TransporterID { get; set; }
        public int RegionID { get; set; }
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
        public decimal WinnerTariff { get; set;}
        public decimal Quantity { get; set; }
        public string Status { get; set; }
        public int StatusID { get; set; }
       
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