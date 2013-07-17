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
            ViewBag.BidPlanID = new SelectList(_bidService.GetAllBid(), "BidID", "BidNumber");
            ViewBag.RegionID = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");
            ViewBag.TransporterID = new SelectList(_transporterService.GetAllTransporter(), "TransporterID", "Name");
            PriceQuotationModel model = new PriceQuotationModel();
            // return RedirectToAction("Edit");
            return View(model);
            //return View();
        }
        [HttpPost]
        public ActionResult Edit(TransportBidQuotation transportQuote)
        {
           // if (ModelState.IsValid)
            {
                List<TransportBidQuotation> existing = 
                    _bidQuotationService.FindBy(t => t.BidID == transportQuote.BidID  
                                               && t.TransporterID == transportQuote.TransporterID
                                               && t.SourceID == transportQuote.SourceID
                                               && t.DestinationID == transportQuote.DestinationID
                                               );
                if (existing.Count == 1)
                {
                    TransportBidQuotation edited = existing[0];
//                    transportQuote.TransportBidQuotationID = edited.TransportBidQuotationID;
                    edited.Tariff = transportQuote.Tariff;
                    edited.Remark = transportQuote.Remark;
                    edited.Position = transportQuote.Position;
                    edited.IsWinner = transportQuote.IsWinner;
                    _bidQuotationService.UpdateTransportBidQuotation(edited);
                }
                else
                {
                    _bidQuotationService.AddTransportBidQuotation(transportQuote);
                }
                return View(transportQuote);
               // _transportBidPlanService.UpdateTransportBidPlan(transportbidplan);
               // return RedirectToAction("EditStart",PriceQuotationModel { BidPlanID = transportQuote.BidID, TransporterID = transportQuote.TransporterID, RegionID = 2 });
              //  return EditStart(new PriceQuotationModel { BidPlanID = transportQuote.BidID, TransporterID = transportQuote.TransporterID, RegionID = 2 });
            }
           // return RedirectToAction("Index");
        }
        public Dictionary<string, TransportBidQuotation> organizeList(List<TransportBidQuotation> quoteList)
        {
            System.Collections.Generic.Dictionary<string, TransportBidQuotation> ret = new Dictionary<string, TransportBidQuotation>();

            foreach (var i in quoteList)
            {
                string hash = i.BidID + "_" + i.TransporterID + "_" + i.SourceID + "_" + i.DestinationID;
                ret.Add(hash, i);// = i;
            }
            return ret;
        }
        //
        // GET: /Procurement/RFQ/EditStart
        [HttpPost]
        public ActionResult EditStart(PriceQuotationModel model)
        {

            ViewBag.ModelFilter = model;
            int bidID = model.BidPlanID;
            
            ViewBag.BidPlanID = new SelectList(_bidService.GetAllBid(), "TransportBidPlanID", "BidNumber");
            ViewBag.RegionID = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");
            ViewBag.TransporterID = new SelectList(_transporterService.GetAllTransporter(), "TransporterID", "Name");
           // PriceQuotationModel model = new PriceQuotationModel();
            // return RedirectToAction("Edit");
            ViewBag.SelectedTransporter = _transporterService.FindById(model.TransporterID);
            Bid SelectedBid = _bidService.FindById(bidID);
            ViewBag.SelectedBid =SelectedBid;
            int bidPlanID = SelectedBid.TransportBidPlanID;

            List<TransportBidQuotation> transporterQuote = _bidQuotationService.FindBy(t => t.BidID == bidID && t.TransporterID == model.TransporterID);
            Dictionary<string, TransportBidQuotation> transporterQuoteDictionary = organizeList(transporterQuote);

            ViewBag.transporterQuoteHash = transporterQuoteDictionary;
            
            foreach (var i in transporterQuoteDictionary.Values)
            {
            }

            {
                //ViewBag.RegionID = new SelectList(_adminUnitService.GetAllAdminUnit(), "AdminUnitID", "Name", RegionID);
                ViewBag.SelectedRegion = _adminUnitService.FindById(model.RegionID);
                TransportBidPlan bidPlan = _transportBidPlanService.FindById(bidPlanID);

                List<TransportBidPlanDetail> regionalPlan = _transportBidPlanDetailService.FindBy(t => t.BidPlanID == model.BidPlanID && t.Destination.AdminUnit2.AdminUnit2.AdminUnitID == model.RegionID);

                var regionalPlanSorted =
                    (from planDetail in regionalPlan
                     orderby planDetail.Source.Name, planDetail.Destination.AdminUnit2.Name, planDetail.DestinationID, planDetail.ProgramID
                     select planDetail

                     ).ToList();


                var regionPlanDistinct = (from rg in regionalPlanSorted

                                          select new PriceQuotationModelDetailModel
                                          {
                                             
                                              SourceWarehouse=rg.Source,

                                              DestinationZone = rg.Destination.AdminUnit2.Name,
                                              RegionName = rg.Destination.AdminUnit2.AdminUnit2.Name,
                                              DestinationWoreda = rg.Destination,
                                              DestinationWoredaName = rg.Destination.Name

                                          })

                 .GroupBy(rg => new { rg.SourceWarehouse.Name, rg.DestinationZone, rg.DestinationWoredaName })

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
        /*[HttpPost]
        public ActionResult Edit(PriceQuotationModel model)
        {
            ViewBag.BidPlanID = new SelectList(_bidService.GetAllBid(), "TransportBidPlanID", "BidNumber");
            ViewBag.RegionID = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");
            ViewBag.TransporterID = new SelectList(_transporterService.GetAllTransporter(), "TransporterID", "Name");
          //  PriceQuotationModel model = new PriceQuotationModel();
            // return RedirectToAction("Edit");
            return View(model);
            //return View();
        }*/
    }
}
