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
      
        private ITransportOrderService _transportOrderService;
    
        public DispatchLocationsController(IBidWinnerService bidWinnerService,ITransportOrderService transportOrderService)
        {
            this._bidWinnerService = bidWinnerService;
            //this._adminUnitService = adminUnitService;
            this._transportOrderService = transportOrderService;
        }
       
        public ActionResult Index(string transporter="")
        {
            var bidWinner = _bidWinnerService.Get(m => m.Transporter.Name.StartsWith(transporter));
            return View(bidWinner);
        }
        
        public ActionResult Details(int id=0)
        {


            TransportOrder transportOrder = _transportOrderService.Get(t => t.TransportOrderID == id, null, "TransportOrderDetails,TransportOrderDetails.FDP.AdminUnit.AdminUnit2,Transporter").FirstOrDefault();
            var bidWinner = _bidWinnerService.Get(m => m.TransporterID == transportOrder.TransporterID).FirstOrDefault();
            if (transportOrder != null)
            {
                var totalAmount = transportOrder.TransportOrderDetails.Sum(m => m.QuantityQtl);
                var totalTariff = transportOrder.TransportOrderDetails.Sum(m => m.TariffPerQtl);
                var region = transportOrder.TransportOrderDetails.FirstOrDefault().FDP.AdminUnit.AdminUnit2.AdminUnit2.Name;
                ViewBag.Transporter = transportOrder.Transporter.Name;
                ViewBag.TotalAmount = totalAmount;
                ViewBag.BidNumber = bidWinner.Bid.BidNumber;
                ViewBag.Region = region;
                return View(transportOrder);
            }
            return RedirectToAction("Index");
            

        }

    }
}
