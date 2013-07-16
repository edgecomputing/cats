using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models
{
    public class TransportBidQuotation
    {

        //TransportBidQuotationID
        [Display(Name = "ID")]
        public int TransportBidQuotationID { get; set; }

        //Bid
        [Display(Name = "Bid")]
        public virtual Bid Bid { get; set; }

        //BidID
        [Display(Name = "Bid ID")]
        public int BidID { get; set; }


        //Transporter
        [Display(Name = "Transporter")]
        public virtual Transporter Transporter { get; set; }

        //TransporterID
        [Display(Name = "Transporter ID")]
        public int TransporterID { get; set; }


        //Source
        [Display(Name = "Source")]
        public virtual Hub Source { get; set; }

        //SourceID
        [Display(Name = "Source ID")]
        public int SourceID { get; set; }


        //Destination
        [Display(Name = "Destination")]
        public virtual AdminUnit Destination { get; set; }

        //DestinationID
        [Display(Name = "Destination ID")]
        public int DestinationID { get; set; }


        //Tariff
        [Display(Name = "Tariff")]
        public decimal Tariff { get; set; }

        //IsWinner
        [Display(Name = "Is Winner")]
        public bool IsWinner { get; set; }

        //Position
        [Display(Name = "Position")]
        public int Position { get; set; }

        //Remark
        [Display(Name = "Remark")]
        public string Remark { get; set; }
    }
}