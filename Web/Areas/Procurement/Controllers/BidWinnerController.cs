using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Cats.Areas.Procurement.Models;
using Cats.Helpers;
using Cats.Models.ViewModels.Bid;
using Cats.Services.EarlyWarning;
using Cats.Services.Procurement;
using Cats.Services.Common;
using System;
using Cats.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.Procurement.Controllers
{
    public class BidWinnerController : Controller
    {
        // GET: /Procurement/Bid/
        private readonly IBidWinnerService _bidWinnerService;
        
        public BidWinnerController(IBidWinnerService bidWinnerService)
        {
            this._bidWinnerService = bidWinnerService;
           
        }

        public ActionResult Index()
        {
            var bidWinner = _bidWinnerService.GetAllBidWinner();
            return View();
        }
        public ActionResult Bid_Read([DataSourceRequest] DataSourceRequest request)
        {

            var bid = _bidWinnerService.GetAllBidWinner().OrderByDescending(m => m.BidWinnerID);
            var winnerToDisplay = GetBids(bid).ToList();
            return Json(winnerToDisplay.ToDataSourceResult(request));
        }
        private IEnumerable<BidWithWinnerViewModel> GetBids(IEnumerable<BidWinner> bids)
        {
            return (from bid in bids
                    select new BidWithWinnerViewModel()
                        {
                            BidID = bid.BidID,
                            BidNumber = bid.Bid.BidNumber,
                            Year = bid.Bid.OpeningDate.Year,
                            BidWinnerID = bid.BidWinnerID,
                        });
        }

        public ActionResult Details(int id)
        {
            var biwWinners = _bidWinnerService.FindBy(m => m.BidID == id).FirstOrDefault();
            if (biwWinners == null)
            {
                return HttpNotFound();
            }

            return View(biwWinners);
        }

        public ActionResult BidWinner_Read([DataSourceRequest] DataSourceRequest request,int id=0)
        {

            var bidWinner = _bidWinnerService.FindBy(m=>m.BidID==id).OrderByDescending(m => m.BidWinnerID);
            var winnerToDisplay = GetBidWinner(bidWinner).ToList();
            return Json(winnerToDisplay.ToDataSourceResult(request));
        }

        private IEnumerable<BidWinnerViewModel> GetBidWinner(IEnumerable<BidWinner> bidWinners)
        {
            return (from bidWinner in bidWinners
                    select new BidWinnerViewModel()
                        {
                            BidWinnnerID = bidWinner.BidWinnerID,
                            TransporterID = bidWinner.TransporterID,
                            TransporterName = bidWinner.Transporter.Name,
                            SourceWarehouse = bidWinner.Hub.Name,
                            Woreda = bidWinner.AdminUnit.AdminUnit2.Name,
                            WinnerTariff = bidWinner.Amount,

                        });
        }
     public ActionResult DispatchLocation(int bidID,int transporterID)
     {
         var winnerWithLocation = _bidWinnerService.FindBy(m => m.BidID==bidID && m.TransporterID==transporterID);
         if (winnerWithLocation == null)
         {
             return HttpNotFound();
         }
         return View(winnerWithLocation);
     }

      public ActionResult WinnerWithLocation_Read([DataSourceRequest] DataSourceRequest request,int id=0,int bidID=0)
      {
          var winnerWithLocation = _bidWinnerService.FindBy(m => m.BidID == bidID && m.TransporterID == id);
          var winnerToDisplay = GetBidWinner(winnerWithLocation).ToList();
          return Json(winnerToDisplay.ToDataSourceResult(request));
      }
        public ActionResult EditWinnerStatus(int id)
        {
            var bidWinner = _bidWinnerService.FindBy(m => m.BidWinnerID == id);
            if (bidWinner==null)
            {
                return HttpNotFound();
            }
            return View(bidWinner);
        }
        [HttpPost]
        public ActionResult EditWinnerStatus(BidWinner bidWinner)
        {
            if (ModelState.IsValid)
            {
                _bidWinnerService.EditBidWinner(bidWinner);
                return RedirectToAction("Index");
            }
            return View(bidWinner);
        }

    }
}
