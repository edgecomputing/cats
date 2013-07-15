    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Cats.Models;
    using Cats.Data;
    using Cats.Services.Procurement;
    using Cats.Services.EarlyWarning;
using Cats.Areas.Procurement.Models;

namespace Cats.Areas.Procurement.Controllers
{
    public class PriceQuotationController : Controller
    {

        private readonly ITransportBidPlanService _transportBidPlanService;
        private readonly IAdminUnitService _adminUnitService;
        private readonly IProgramService _programService;
        private readonly ITransportBidPlanDetailService _transportBidPlanDetailService;
        private readonly IHubService _hubService;
        private readonly ITransportBidQuotationService _bidQuotationService;
        private readonly IBidService _bidService;
        private readonly ITransporterService _transporterService;

        public PriceQuotationController(ITransportBidPlanService transportBidPlanServiceParam
                                            , IAdminUnitService adminUnitServiceParam
                                            , IProgramService programServiceParam
                                            , ITransportBidPlanDetailService transportBidPlanDetailServiceParam
                                            , IHubService hubServiceParam
                                            , ITransportBidQuotationService bidQuotationServiceParam
                                            , ITransporterService transporterServiceParam
                                            , IBidService bidServiceParam)
        {
            this._transportBidPlanService = transportBidPlanServiceParam;
            this._adminUnitService = adminUnitServiceParam;
            this._programService = programServiceParam;
            this._transportBidPlanDetailService = transportBidPlanDetailServiceParam;
            this._hubService = hubServiceParam;
            this._bidQuotationService = bidQuotationServiceParam;
            this._bidService = bidServiceParam;
            this._transporterService = transporterServiceParam;
        }
        //
        // GET: /Procurement/RFQ/EditStart
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.BidPlanID = new SelectList(_bidService.GetAllBid(), "TransportBidPlanID", "BidNumber");
            ViewBag.RegionID = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");
            ViewBag.TransporterID = new SelectList(_transporterService.GetAllTransporter(), "TransporterID", "Name");
            PriceQuotationModel model = new PriceQuotationModel();
            // return RedirectToAction("Edit");
            return View(model);
            //return View();
        }

        //
        // GET: /Procurement/RFQ/EditStart
        [HttpPost]
        public ActionResult EditStart(PriceQuotationModel model)
        {

            ViewBag.ModelFilter = model;

            ViewBag.BidPlanID = new SelectList(_bidService.GetAllBid(), "TransportBidPlanID", "BidNumber");
            ViewBag.RegionID = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");
            ViewBag.TransporterID = new SelectList(_transporterService.GetAllTransporter(), "TransporterID", "Name");
           // PriceQuotationModel model = new PriceQuotationModel();
            // return RedirectToAction("Edit");
            ViewBag.SelectedTransporter = _transporterService.FindById(model.TransporterID);
            {
                //ViewBag.RegionID = new SelectList(_adminUnitService.GetAllAdminUnit(), "AdminUnitID", "Name", RegionID);
                ViewBag.SelectedRegion = _adminUnitService.FindById(model.RegionID);
                TransportBidPlan bidPlan = _transportBidPlanService.FindById(model.BidPlanID);

                List<TransportBidPlanDetail> regionalPlan = _transportBidPlanDetailService.FindBy(t => t.BidPlanID == model.BidPlanID && t.Destination.AdminUnit2.AdminUnit2.AdminUnitID == model.RegionID);

                var regionalPlanSorted =
                    (from planDetail in regionalPlan
                     orderby planDetail.Source.Name, planDetail.Destination.AdminUnit2.Name, planDetail.DestinationID, planDetail.ProgramID
                     select planDetail

                     ).ToList();


                var regionPlanDistinct = (from rg in regionalPlanSorted

                                          select new RfqViewModel
                                          {
                                              SourceWarehouse = rg.Source.Name,
                                              DestinationZone = rg.Destination.AdminUnit2.Name,
                                              RegionName = rg.Destination.AdminUnit2.AdminUnit2.Name,
                                              DestinationWoreda = rg.Destination.Name
                                          })

                 .GroupBy(rg => new { rg.SourceWarehouse, rg.DestinationZone, rg.DestinationWoreda })

                 .Select(s => s.FirstOrDefault());

                ViewBag.regionalPlanSorted = regionalPlanSorted;
                ViewBag.regionPlanDistinct = regionPlanDistinct;

                ViewBag.BidPlan = bidPlan;
                ViewBag.region = model.RegionID;
                return View(bidPlan);
            }          
            
            
            
            
            
            return View(model);
            //return View();
        }
        [HttpPost]
        public ActionResult Edit(PriceQuotationModel model)
        {
            ViewBag.BidPlanID = new SelectList(_bidService.GetAllBid(), "TransportBidPlanID", "BidNumber");
            ViewBag.RegionID = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");
            ViewBag.TransporterID = new SelectList(_transporterService.GetAllTransporter(), "TransporterID", "Name");
          //  PriceQuotationModel model = new PriceQuotationModel();
            // return RedirectToAction("Edit");
            return View(model);
            //return View();
        }
    }
}
