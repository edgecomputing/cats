using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Procurement;
using Cats.Services.Security;

namespace Cats.Areas.Procurement.Controllers
{
    public class HomeController : Controller
    {
       // private readonly IPaymentRequestService _paymentRequestService;
        private readonly IBidService _bidService;
        private readonly ITransporterService _transporterService;
        private readonly ITransportBidPlanService _transportBidPlanService;
        //private readonly ITransportBidQuotationService _priceQuotataion;
        
        //
        // GET: /Procurement/FetchData/

        public HomeController(IBidService bidService,ITransporterService transporterService, ITransportBidPlanService transportBidPlanService)
        {
            _bidService = bidService;
            _transporterService = transporterService;
            _transportBidPlanService = transportBidPlanService;
        }

        //
        // GET: /Procurement/Home/

        public ActionResult Index()
        {
            ViewBag.ActiveBids = _bidService.FindBy(t => t.StatusID == 5).Count;
            ViewBag.Transporters = _transporterService.GetAllTransporter().Count;
            ViewBag.CurrentBidPlan =
                _transportBidPlanService.GetAllTransportBidPlan().OrderByDescending(t => t.TransportBidPlanID).First().ShortName;
            return View();
        }

    }
}
