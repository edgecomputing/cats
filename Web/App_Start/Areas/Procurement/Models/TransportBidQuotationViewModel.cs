using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Cats.Models;

namespace Cats.Areas.Procurement.Models
{
    public class TransportBidQuotationViewModel
    {

        public int TransportBidQuotationHeaddrID { get; set; }
        public Nullable<System.DateTime> BidQuotationDate { get; set; }
        public Nullable<float> BidBondAmount { get; set; }
        public Nullable<int> EnteredBy { get; set; }
        public Nullable<int> Status { get; set; }

        //TransportBidQuotationID
        [Display(Name = "ID")]
        public int TransportBidQuotationID { get; set; }

        //Bid
        [Display(Name = "Bid")]
        public string Bid { get; set; }

        //BidID
        [Display(Name = "Bid ID")]
        public int BidID { get; set; }


        //Transporter
        [Display(Name = "Transporter")]
        public string Transporter { get; set; }

        //TransporterID
        [Display(Name = "Transporter ID")]
        public int TransporterID { get; set; }


        //Source
        [Display(Name = "Source")]
        public string Source { get; set; }

        //SourceID
        [Display(Name = "Source ID")]
        public int SourceID { get; set; }


        //Destination
        [Display(Name = "Destination")]
        public string Destination { get; set; }

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