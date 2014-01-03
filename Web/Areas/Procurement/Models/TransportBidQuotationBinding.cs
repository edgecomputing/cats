using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;

namespace Cats.Areas.Procurement.Models
{
    public class TransportBidQuotationBinding
    {
        public static List<TransportBidQuotationViewModel> TransportBidQuotationListViewModelBinder(List<TransportBidQuotation> transportBidQuotations)
        {
            return transportBidQuotations.Select(transportBidQuotation => new TransportBidQuotationViewModel
                {
                    //Bid = transportBidQuotation.Bid.BidNumber, 
                    BidID = transportBidQuotation.BidID, 
                    //Destination = transportBidQuotation.Destination.Name, 
                    DestinationID = transportBidQuotation.DestinationID, 
                    IsWinner = transportBidQuotation.IsWinner, 
                    Position = transportBidQuotation.Position, 
                    Remark = transportBidQuotation.Remark, 
                    //Source = transportBidQuotation.Source.Name, 
                    SourceID = transportBidQuotation.SourceID, 
                    Tariff = transportBidQuotation.Tariff, 
                    TransportBidQuotationID = transportBidQuotation.TransportBidQuotationID, 
                    //Transporter = transportBidQuotation.Transporter.Name, 
                    TransporterID = transportBidQuotation.TransporterID
                }).ToList();
        }
    }
}