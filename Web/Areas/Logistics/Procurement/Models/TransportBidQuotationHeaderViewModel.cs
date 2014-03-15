using Cats.Models;

namespace Cats.Areas.Procurement.Models
{
    public class TransportBidQuotationHeaderViewModel
    {
        public int TransportBidQuotationHeaderID { get; set; }
        public string BidNumber { get; set; }
        public string Region { get; set; }
        public string Transporter { get; set; }
        public float BidBondAmount { get; set; }
        public string Status { get; set; }
        public int OffersCount { get; set; }
        public string EnteredBy { get; set; }
        public int BidID { get; set; }
        public int TransporterId { get; set; }
        public int RegionId { get; set; }
    }
}