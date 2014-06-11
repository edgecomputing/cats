    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Cats.Helpers;
    using Cats.Infrastructure;
    using Cats.Models;
    using Cats.Data;
    using Cats.Services.Procurement;
    using Cats.Services.EarlyWarning;
using Cats.Areas.Procurement.Models;
    using Cats.Services.Security;
    using Cats.ViewModelBinder;

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
        private readonly IUserAccountService _userAccountService;

        public RFQController(ITransportBidPlanService transportBidPlanServiceParam
                                            , IAdminUnitService adminUnitServiceParam
                                            , IProgramService programServiceParam
                                            , ITransportBidPlanDetailService transportBidPlanDetailServiceParam
                                            ,IHubService hubServiceParam
                                            , ITransportBidQuotationService bidQuotationServiceParam
                                            , ITransporterService transporterServiceParam
                                            , IBidService bidServiceParam
                                            ,IUserAccountService userAccountService)
                                        
            {
                this._transportBidPlanService = transportBidPlanServiceParam;
                this._adminUnitService = adminUnitServiceParam;
                this._programService = programServiceParam;
                this._transportBidPlanDetailService = transportBidPlanDetailServiceParam;
                this._hubService = hubServiceParam;
                this._bidQuotationService = bidQuotationServiceParam;
                this._bidService = bidServiceParam;
                this._transporterService = transporterServiceParam;
                this._userAccountService = userAccountService;
            }
       
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.BidID = new SelectList(_bidService.GetAllBid(), "BidID", "BidNumber");
            ViewBag.RegionID = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");

            List<Cats.Models.TransportBidQuotation> list = this._bidQuotationService.GetAllTransportBidQuotation();
            return View(list);
        }

        public FileResult Print(int bidID, int regionID)
        {
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var reportPath = Server.MapPath("~/Report/Procurment/RFQ.rdlc");
            
            var bid = _bidService.FindById(bidID);
            var planID = bid.TransportBidPlanID;


            TransportBidPlan bidPlan = _transportBidPlanService.FindById(planID);

            List<TransportBidPlanDetail> regionalPlan = _transportBidPlanDetailService.FindBy(t => t.BidPlanID == planID && t.Destination.AdminUnit2.AdminUnit2.AdminUnitID == regionID);

            var totalReliefAmount = regionalPlan.Where(m => m.ProgramID == 1).Sum(m => m.Quantity);
            var totalPsnpAmount = regionalPlan.Where(m => m.ProgramID == 2).Sum(m => m.Quantity);
            var rfqDetail = (from transportBidPlanDetail in regionalPlan
                     select new RfqPrintViewModel
                         {
                             Source = transportBidPlanDetail.Source.Name,
                             Zone = transportBidPlanDetail.Destination.AdminUnit2.Name,
                             Woreda = transportBidPlanDetail.Destination.Name,
                             WoredaID = transportBidPlanDetail.DestinationID,
                             Region = transportBidPlanDetail.Destination.AdminUnit2.AdminUnit2.Name,
                             BidReference = bid.BidNumber,
                             ProgramID = transportBidPlanDetail.ProgramID,
                             Quantity = transportBidPlanDetail.Quantity/10,
                             BidOpeningdate = bid.OpeningDate.ToCTSPreferedDateFormat(datePref),
                         }
                    ).Where(m=>m.Quantity>0)
                   .GroupBy(ac => new
                   {
                       ac.Woreda,
                       ac.Source,
                       ac.Region,
                       ac.Zone
                   })
                    .Select(ac => new RfqPrintViewModel
                    {
                        Source = ac.Key.Source,
                        Zone = ac.Key.Zone,
                        Woreda = ac.Key.Woreda,
                        Region = ac.Key.Region,
                        BidReference = bid.BidNumber,
                        Quantity = ac.Sum(m=>m.Quantity),
                        ReliefAmount = totalReliefAmount/10,
                        PsnpAmount = totalPsnpAmount/10,
                        BidOpeningdate = bid.OpeningDate.ToCTSPreferedDateFormat(datePref),
                    });
            var reportData = rfqDetail;

            var dataSourceName = "RFQDataset";
            var result = ReportHelper.PrintReport(reportPath, reportData, dataSourceName);

            return File(result.RenderBytes, result.MimeType);
        }
       
        public ActionResult Details(int BidID = 0, int RegionID = 0)
        {
            //ViewBag.RegionID = new SelectList(_adminUnitService.GetAllAdminUnit(), "AdminUnitID", "Name", RegionID);
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var r = _adminUnitService.FindById(RegionID);
            ViewBag.SelectedRegion = r.Name;
           

            ViewBag.BidID = new SelectList(_bidService.GetAllBid(), "BidID", "BidNumber", BidID);
            ViewBag.RegionID = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name", RegionID);

            var bid = _bidService.FindById(BidID);
            var bidPlanID = bid.TransportBidPlanID;
            ViewBag.BidReference = bid.BidNumber;
            ViewBag.OpeningDate = bid.OpeningDate.ToCTSPreferedDateFormat(datePref);
            ViewBag.bid = BidID;
            ViewBag.reg = RegionID;
            
            TransportBidPlan bidPlan = _transportBidPlanService.FindById(bidPlanID);

            List<TransportBidPlanDetail> regionalPlan = _transportBidPlanDetailService.FindBy(t => t.BidPlanID == bidPlanID && t.Destination.AdminUnit2.AdminUnit2.AdminUnitID == RegionID);

            var regionalPlanSorted =
                (from planDetail in regionalPlan
                 orderby planDetail.Source.Name, planDetail.Destination.AdminUnit2.Name, planDetail.DestinationID, planDetail.ProgramID
                 select planDetail
                 
                 ).ToList();
           

           var regionPlanDistinct = (from rg in regionalPlanSorted

                                     select new RfqViewModel()
                                     {
                                         SourceWarehouse = rg.Source.Name,
                                         DestinationZone = rg.Destination.AdminUnit2.Name,
                                         DestinationWoreda = rg.Destination.Name,
                                         RegionName = rg.Destination.AdminUnit2.AdminUnit2.Name,
                                         Quantity = rg.Quantity / 10,
                                        
                                     }
                    ).Where(m => m.Quantity > 0)
                   .GroupBy(ac => new
                   {
                       ac.DestinationWoreda,
                       ac.SourceWarehouse,
                       ac.RegionName,
                       ac.DestinationZone
                   })
                    .Select(ac => new RfqViewModel
                    {
                        SourceWarehouse = ac.Key.SourceWarehouse,
                        DestinationZone = ac.Key.DestinationZone,
                        DestinationWoreda = ac.Key.DestinationWoreda,
                        RegionName = ac.Key.RegionName,
                        Quantity = ac.Sum(m => m.Quantity)
                    });

            ViewBag.regionalPlanSorted = regionalPlanSorted;
            ViewBag.regionPlanDistinct = regionPlanDistinct; 
            
            ViewBag.BidPlan = bidPlan;
            ViewBag.region = RegionID;
            ViewBag.total = regionPlanDistinct.Sum(m => m.Quantity);
           // ViewBag.regionName= 
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
                                          DestinationWoreda = rg.Destination.Name,
                                          Quantity = rg.Quantity
                                      })

             .GroupBy(rg => new { rg.SourceWarehouse, rg.DestinationZone, rg.DestinationWoreda })

             .Select(s => s.FirstOrDefault()).ToList();

            return regionPlanDistinct;

        }
       
    }

    }
