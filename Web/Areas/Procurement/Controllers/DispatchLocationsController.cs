using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cats.Areas.Procurement.Models;
using Cats.Services.EarlyWarning;
using Cats.Services.Procurement;
using System;
using Cats.Models;
namespace Cats.Areas.Procurement.Controllers
{
    public class DispatchLocationsController : Controller
    {
        //
        // GET: /Procurement/DispatchLocations/
        private IBidWinnerService _bidWinnerService;
        private IAdminUnitService _adminUnitService;
        public DispatchLocationsController(IBidWinnerService bidWinnerService,IAdminUnitService adminUnitService)
        {
            this._bidWinnerService = bidWinnerService;
            this._adminUnitService = adminUnitService;
        }

        public ActionResult Index()
        {
            var bidWinner = _bidWinnerService.Get(m=>m.Position<2);
            return View(bidWinner);
        }
        public ActionResult Details(int id=0)
        {
            var totalAmount=0;
            var totalTariff = 0;
            BidWinner bidWinner = _bidWinnerService.Get(t => t.BidWinnerID == id, null,"").FirstOrDefault();
            foreach (var winners in bidWinner.Bid.TransportBidPlan.TransportBidPlanDetails)
            {
                totalAmount = (int) (totalAmount + winners.Quantity);

            }
            ViewBag.TotalAmount = totalAmount;
            ViewBag.Transporter = bidWinner.Transporter.Name;
            ViewBag.Region = bidWinner.AdminUnit.Name;
            ViewBag.BidNumber = bidWinner.Bid.BidNumber;
            return View(bidWinner);
        }

    }
}
