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
    public class RFQController : Controller
    {

        private readonly ITransportBidPlanService _transportBidPlanService;
        private readonly IAdminUnitService _adminUnitService;
        private readonly IProgramService _programService;
        private readonly ITransportBidPlanDetailService _transportBidPlanDetailService;
        private readonly IHubService _hubService;
        private readonly ITransportBidQuotationService _bidQuotationService;
        private readonly IBidService _bidService;
        private readonly ITransporterService _transporterService;

        public RFQController(ITransportBidPlanService transportBidPlanServiceParam
                                            , IAdminUnitService adminUnitServiceParam
                                            , IProgramService programServiceParam
                                            , ITransportBidPlanDetailService transportBidPlanDetailServiceParam
                                            ,IHubService hubServiceParam
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
       
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.BidPlanID = new SelectList(_transportBidPlanService.GetAllTransportBidPlan(), "TransportBidPlanID", "ShortName");
            ViewBag.RegionID = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");

            List<Cats.Models.TransportBidQuotation> list = this._bidQuotationService.GetAllTransportBidQuotation();
            return View(list);
        }
 

        public ActionResult Details(int BidPlanID = 0, int RegionID = 0)
        {
            //ViewBag.RegionID = new SelectList(_adminUnitService.GetAllAdminUnit(), "AdminUnitID", "Name", RegionID);
            ViewBag.SelectedRegion = _adminUnitService.FindById(RegionID);
            ViewBag.BidPlanID = new SelectList(_transportBidPlanService.GetAllTransportBidPlan(), "TransportBidPlanID", "ShortName", BidPlanID);
            ViewBag.RegionID = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name", RegionID);
            TransportBidPlan bidPlan = _transportBidPlanService.FindById(BidPlanID);

            List<TransportBidPlanDetail> regionalPlan = _transportBidPlanDetailService.FindBy(t => t.BidPlanID == BidPlanID && t.Destination.AdminUnit2.AdminUnit2.AdminUnitID == RegionID);

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
            ViewBag.region = RegionID;
            return View(bidPlan);
        }

        List<RfqViewModel> getViewModelList(int BidPlanID,int RegionID)
        {
            TransportBidPlan bidPlan = _transportBidPlanService.FindById(BidPlanID);

            List<TransportBidPlanDetail> regionalPlan = _transportBidPlanDetailService.FindBy(t => t.BidPlanID == BidPlanID && t.Destination.AdminUnit2.AdminUnit2.AdminUnitID == RegionID);

            var regionalPlanSorted =
                (from planDetail in regionalPlan
                 orderby planDetail.Source.Name, planDetail.Destination.AdminUnit2.Name, planDetail.DestinationID, planDetail.ProgramID
                 select planDetail

                 ).ToList();


            List<RfqViewModel> regionPlanDistinct = (from rg in regionalPlanSorted

                                      select new RfqViewModel
                                      {
                                          SourceWarehouse = rg.Source.Name,
                                          DestinationZone = rg.Destination.AdminUnit2.Name,
                                          RegionName = rg.Destination.AdminUnit2.AdminUnit2.Name,
                                          DestinationWoreda = rg.Destination.Name
                                      })

             .GroupBy(rg => new { rg.SourceWarehouse, rg.DestinationZone, rg.DestinationWoreda })

             .Select(s => s.FirstOrDefault()).ToList();

            return regionPlanDistinct;

        }
       
    }

    }
