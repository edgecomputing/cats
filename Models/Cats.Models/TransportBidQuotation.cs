using System;

namespace Cats.Models
{
    public partial class TransportBidQuotation
    {
        public int TransportBidQuotationID { get; set; }
        public int TransportBidQuotationHeaderID { get; set; }
        //public Nullable<int> BidID { get; set; }
        //public Nullable<int> TransporterID { get; set; }
        //public Nullable<int> SourceID { get; set; }
        //public Nullable<int> DestinationID { get; set; }
        //public Nullable<decimal> Tariff { get; set; }
        //public Nullable<bool> IsWinner { get; set; }
        //public Nullable<int> Position { get; set; }
        public int BidID { get; set; }
        public int TransporterID { get; set; }
        public int SourceID { get; set; }
        public int DestinationID { get; set; }
        public decimal Tariff { get; set; }
        public bool IsWinner { get; set; }
        public int Position { get; set; }
        public string Remark { get; set; }
        public int? PartitionId { get; set; }

        public virtual AdminUnit AdminUnit { get; set; }
        public virtual Hub Hub { get; set; }
        public virtual TransportBidQuotationHeader TransportBidQuotationHeader { get; set; }
    }
}