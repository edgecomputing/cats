using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Cats.Areas.Procurement.Models;
using Cats.Helpers;
using Cats.Models.ViewModels.Bid;
using Cats.Services.EarlyWarning;
using Cats.Services.Procurement;
using System;
using Cats.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.Procurement.Controllers
{
    public class BidWinnerController : Controller
    {
        // GET: /Procurement/Bid/
        private IBidService _bidService;
        private IApplicationSettingService _applicationSettingService;
        private ITransportBidQuotationService _bidQuotationService;
        private ITransporterService _transporterService;

        public BidWinnerController(IBidService bidService, IApplicationSettingService applicationSettingService,
                             ITransportBidQuotationService bidQuotationService, ITransporterService transporterService)
        {
            this._bidService = bidService;
            this._applicationSettingService = applicationSettingService;
            this._bidQuotationService = bidQuotationService;
            this._transporterService = transporterService;
        }

        [HttpGet]
        public ActionResult BidWinner(int sourceID,int DestinationID)
        {
            var bidId = _applicationSettingService.FindValue("CurrentBid");
            ViewBag.currentBidId = bidId;
            /*int currentBidId=int.Parse(bidId);
            ViewBag.CurrentBid = _bidService.FindById(currentBidId);

            List<TransportBidQuotation> Winners = _bidQuotationService.FindBy(q => q.BidID==currentBidId &&  q.SourceID == sourceID && q.DestinationID == DestinationID && q.IsWinner == true);
            Winners.OrderBy(t=>t.Position);*/
            List<TransportBidQuotation> Winners = _transporterService.GetBidWinner(sourceID, DestinationID);
            ViewBag.Winners=Winners; 
            return View();
        }
       
     

    }
}
