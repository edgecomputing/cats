using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Cats.Areas.Procurement.Models;
using Cats.Helpers;
using Cats.Models.Constant;
using Cats.Models.ViewModels.Bid;
using Cats.Services.EarlyWarning;
using Cats.Services.Procurement;
using Cats.Services.Common;
using System;
using Cats.Models;
using Cats.Services.Security;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Cats.Helpers;

namespace Cats.Areas.Procurement.Controllers
{
    public class BidController : Controller
    {
        // GET: /Procurement/Bid/
        private readonly IBidService _bidService;
        private readonly IBidDetailService _bidDetailService;
        private readonly IAdminUnitService _adminUnitService;
        private readonly IStatusService _statusService;
        private readonly ITransportBidPlanService _transportBidPlanService;
        private readonly ITransportBidPlanDetailService _transportBidPlanDetailService;
        private readonly IApplicationSettingService _applicationSettingService;
        private readonly IUserAccountService _userAccountService;
        private readonly ITransportBidQuotationService _transportBidQuotationService;
        private readonly IBidWinnerService _bidWinnerService;
        private readonly ITransporterService _transporterService;
        private readonly IHubService _hubService;
        private readonly IWorkflowStatusService _workflowStatusService;

        public BidController(IBidService bidService, IBidDetailService bidDetailService,
                             IAdminUnitService adminUnitService,
                             IStatusService statusService,
                             ITransportBidPlanService transportBidPlanService,
                             ITransportBidPlanDetailService transportBidPlanDetailService,
                             IApplicationSettingService applicationSettingService,IUserAccountService userAccountService,
                             ITransportBidQuotationService transportBidQuotationService, IBidWinnerService bidWinnerService,
                            ITransporterService transporterService, IHubService hubService,IWorkflowStatusService workflowStatusService)
        {
            _bidService = bidService;
            _bidDetailService = bidDetailService;
            _adminUnitService = adminUnitService;
            _statusService = statusService;
            _transportBidPlanService = transportBidPlanService;
            _transportBidPlanDetailService = transportBidPlanDetailService;
            _applicationSettingService = applicationSettingService;
            _userAccountService = userAccountService;
            _transportBidQuotationService = transportBidQuotationService;
            _bidWinnerService = bidWinnerService;
            _transporterService = transporterService;
            _hubService = hubService;
            _workflowStatusService = workflowStatusService;
        }

        public ActionResult Index(int id=0)
        {
            ViewBag.BidStatus = id;
            ViewBag.BidTitle = id == 0
                                             ? "Open"
                                             : _workflowStatusService.GetStatusName(WORKFLOW.BID, id);
            return View();
        }
        [HttpGet]
        public ActionResult WoredasBidStatus()
        {
            var filter = new PriceQuotationFilterViewModel();
            ViewBag.filter = filter;
            ViewBag.BidID = new SelectList(_bidService.GetAllBid(), "BidID", "BidNumber");
            ViewBag.RegionID = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");
            ViewBag.HubID = new SelectList(_hubService.GetAllHub(), "HubID", "Name", "Select Hub");
            return View("WoredaWithOutBidOfferFilterParial", filter);
        }

        [HttpPost]
        public ActionResult WoredasBidStatus(PriceQuotationFilterOfferlessViewModel filter)
        {
            ViewBag.WoredaFirstBidWinners = ReadWoredasWithBidWinners(filter.BidID, filter.RegionID, 1, filter.HubID);
            ViewBag.WoredaSecondBidWinners = ReadWoredasWithBidWinners(filter.BidID, filter.RegionID, 2, filter.HubID);
            ViewBag.filter = filter;
            ViewBag.BidID = new SelectList(_bidService.GetAllBid(), "BidID", "BidNumber");
            ViewBag.RegionID = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");
            ViewBag.HubID = new SelectList(_hubService.GetAllHub(), "HubID", "Name", "Select Hub");
            return View("WoredasWithOutBidOffer",filter);
        }

       

        public ActionResult ReadWoredasWithOutBidOffer([DataSourceRequest] DataSourceRequest request, int bidID, int regionID)
        {
            var planID = _bidService.FindById(bidID).TransportBidPlanID;

            var bidPlanDetail =
                _transportBidPlanDetailService.FindBy(t => t.Destination.AdminUnit2.AdminUnit2.AdminUnitID == regionID
                                                           && t.BidPlanID == planID);
            var df = (from planDetail in bidPlanDetail
                      group planDetail by new
                      {
                          planDetail.DestinationID,
                          planDetail.SourceID
                      }
                          into gr
                          select gr
                      );

            var detailPlans = df.Select(d => d.ToList()).Select(er => er.FirstOrDefault()).ToList();

            var result = new List<PriceQuotationDetail>();

            foreach (var transportBidPlanDetail in detailPlans)
            {
                var pdetail = transportBidPlanDetail;

                var detail = _transportBidQuotationService.FindBy(t => t.BidID == bidID
                                                                && t.SourceID == pdetail.SourceID
                                                                && t.DestinationID == pdetail.DestinationID).FirstOrDefault();
                if(detail==null)
                {
                    var n = new PriceQuotationDetail()
                    {
                        SourceWarehouse = pdetail.Source.Name,
                        Zone = pdetail.Destination.AdminUnit2.Name,
                        Woreda = pdetail.Destination.Name,
                        Tariff = 0,
                        Remark = String.Empty,
                        BidID = bidID,
                        DestinationID = pdetail.DestinationID,
                        SourceID = pdetail.SourceID
                    };
                    result.Add(n);
                }
            }
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult NoOfferWoredas(int bidID)
        {
            var planID = _bidService.FindById(bidID).TransportBidPlanID;

            var bidPlanDetail =
                _transportBidPlanDetailService.FindBy(t => t.BidPlanID == planID);
            var df = (from planDetail in bidPlanDetail
                      group planDetail by new
                      {
                          planDetail.DestinationID,
                          planDetail.SourceID
                      }
                          into gr
                          select gr
                      );

            var detailPlans = df.Select(d => d.ToList()).Select(er => er.FirstOrDefault()).ToList();

            var result = new List<NoOfferWoreda>();

            foreach (var transportBidPlanDetail in detailPlans)
            {
                var pdetail = transportBidPlanDetail;

                var detail = _transportBidQuotationService.FindBy(t => t.BidID == bidID
                                                                && t.SourceID == pdetail.SourceID
                                                                && t.DestinationID == pdetail.DestinationID).FirstOrDefault();
                if (detail == null)
                {
                    var n = new NoOfferWoreda()
                    {
                        SourceWarehouse = pdetail.Source.Name,
                        Zone = pdetail.Destination.AdminUnit2.Name,
                        Woreda = pdetail.Destination.Name,
                        Region = pdetail.Destination.AdminUnit2.AdminUnit2.Name
                    };
                    result.Add(n);
                }
            }

            var re =  (from planDetail in result
                      group planDetail by planDetail.Region
                      into gr select new
                          {
                              gr.Key,
                              Count = gr.Count()
                          }
                     );
           
            return Json(re, JsonRequestBehavior.AllowGet);
        }

        public List<BidWinnerViewingModel> ReadWoredasWithBidWinners(int bidID, int regionID, int rank, int hubID = 0)
        {
            List<BidWinner> bidWinners;
            if(hubID == 0)
            {
                bidWinners = _bidWinnerService.Get(t => t.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID == regionID && t.BidID == bidID
                                && t.Position == rank && t.Status == 1, null, "Transporter,AdminUnit, AdminUnit.AdminUnit2").ToList();
            }
            else
            {
                bidWinners = _bidWinnerService.Get(t => t.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID == regionID && t.BidID == bidID
                                && t.Position == rank && t.Status == 1 && t.SourceID == hubID, null, "Transporter,AdminUnit, AdminUnit.AdminUnit2").ToList();
            }
            
            //var enumerable = bidWinners as List<BidWinner> ?? bidWinners.ToList();
            var bw = (from bidWinner in bidWinners
                      group bidWinner by new
                      {
                          Woreda = bidWinner.AdminUnit.Name,
                          Zone = bidWinner.AdminUnit.AdminUnit2.Name,
                          bidWinner.DestinationID,
                          Hub = bidWinner.Hub.Name,
                          bidWinner.SourceID,
                          bidWinner.Tariff,
                          bidWinner.BidID,
                          bidWinner.Position
                      }
                          into gr
                          select new
                              {
                                  DestinationID = gr.Key.DestinationID,
                                  Zone = gr.Key.Zone,
                                  Woreda = gr.Key.Woreda,
                                  SourceID = gr.Key.SourceID, 
                                  Hub = gr.Key.Hub,
                                  Tariff = gr.Key.Tariff,
                                  BidID = gr.Key.BidID,
                                  Position = gr.Key.Position,
                                  TransporterIDs = gr.Select(t => t.TransporterID).ToList(),
                              }
                      );

            var bidWinnersList = bw.ToList();

            var result = new List<BidWinnerViewingModel>();

            foreach (var bidWinner in bidWinnersList)
            {
                var n = new BidWinnerViewingModel();
                n.SourceWarehouse = bidWinner.Hub.ToString();
                n.SourceId = bidWinner.SourceID.ToString();
                n.Zone = bidWinner.Zone.ToString();
                n.Woreda = bidWinner.Woreda.ToString();
                n.DestinationId = bidWinner.DestinationID.ToString();
                n.WinnerTariff = bidWinner.Tariff.ToString();
                n.Rank = bidWinner.Position.ToString();
                n.BidID = bidWinner.BidID.ToString();
                foreach (var transporter in bidWinner.TransporterIDs)
                {
                    n.TransporterID.Add(transporter.ToString());
                    n.TransporterName.Add(_transporterService.FindById(transporter).Name);
                }
                result.Add(n);
            }
            return result;
        }
        public ActionResult Bid_Read([DataSourceRequest] DataSourceRequest request,int id=0)
        {
            
            var bids = id==0?_bidService.Get(m => m.StatusID == (int)BidStatus.Open).OrderByDescending(m => m.BidID).ToList():_bidService.Get(m=>m.StatusID==id).ToList();
            var bidsToDisplay = GetBids(bids).ToList();
            return Json(bidsToDisplay.ToDataSourceResult(request));
        }
       
        public ActionResult BidDetail_Read(int bidID,[DataSourceRequest] DataSourceRequest request)
        {
            var bidDetails = _bidDetailService.GetAllBidDetail();
            var bidsToDisplay = GetBidDetails(bidDetails).ToList();
            return Json(bidsToDisplay.Where(p => p.BidID == bidID).ToDataSourceResult(request));
        }

        private IEnumerable<BidViewModel> GetBids(IEnumerable<Bid> bids)
        {
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            return (from bid in bids
                    select new BidViewModel()
                    {
                        BidID = bid.BidID,
                        BidNumber = bid.BidNumber,
                        BidBondAmount = bid.BidBondAmount,
                        StartDate = bid.StartDate,
                        EndDate = bid.EndDate,
                        OpeningDate = bid.OpeningDate,
                        Status = _workflowStatusService.GetStatusName(WORKFLOW.BID, bid.StatusID),
                        StatusID = bid.StatusID,
                        StartDatePref = bid.StartDate.ToCTSPreferedDateFormat(datePref),
                        OpeningDatePref = bid.OpeningDate.ToCTSPreferedDateFormat(datePref),
                        EndDatePref = bid.EndDate.ToCTSPreferedDateFormat(datePref)
                    });
        }
        private IEnumerable<BidDetailViewModel> GetBidDetails(IEnumerable<BidDetail> bidDetails)
        {
            return (from bidDetail in bidDetails
                    select new BidDetailViewModel()
                    {
                        BidDetailID =bidDetail.BidDetailID,
                        BidID = bidDetail.BidID,
                        Region= bidDetail.Bid.AdminUnit.Name,
                        RegionID=bidDetail.Bid.AdminUnit.AdminUnitID,
                        AmountForReliefProgram = bidDetail.AmountForReliefProgram,
                        AmountForPSNPProgram = bidDetail.AmountForPSNPProgram,
                        BidDocumentPrice = bidDetail.BidDocumentPrice,
                        CPO = bidDetail.CPO
                        
                    });
        }
        //update bid detail information
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult BidDetail_Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<BidDetail> bidDetails)
        {
            if (bidDetails != null && ModelState.IsValid)
            {
                foreach (var details in bidDetails)
                {
                    _bidDetailService.EditBidDetail(details);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult Create(int id = 0)
        {

            ViewBag.RegionID = new SelectList(_adminUnitService.GetRegions(), "AdminUnitID", "Name");
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var bid = new CreateBidViewModel();
            bid.StartDate = DateTime.Now;
            bid.EndDate = DateTime.Now.AddDays(10);
            bid.OpeningDate = DateTime.Now.AddDays(11);
            var regions = _adminUnitService.FindBy(t => t.AdminUnitTypeID == 2);
            ViewBag.StatusID = new SelectList(_statusService.GetAllStatus(), "StatusID", "Name", bid.StatusID = 1);
            bid.BidNumber = _bidService.AutogenerateBidNo();
            ViewBag.BidPlanID = id;
            
            ViewBag.TransportBidPlanID = new SelectList(_transportBidPlanService.GetAllTransportBidPlan(), "TransportBidPlanID", "ShortName", id);
            return View(bid);
        }

        //[HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(CreateBidViewModel bidViewModel)
        {
            var bid = GetBid(bidViewModel);
            if (!IsBidMadeForThisRegion(bid.RegionID, bid.TransportBidPlanID))
            {
                ModelState.AddModelError("Errors","This Region is already registered with this Bid Plan. Please choose another Region or Plan!");
                ViewBag.StatusID = new SelectList(_statusService.GetAllStatus(), "StatusID", "Name");
                ViewBag.BidPlanID = bid.TransportBidPlanID;
                ViewBag.TransportBidPlanID = new SelectList(_transportBidPlanService.GetAllTransportBidPlan(), "TransportBidPlanID", "ShortName", bid.TransportBidPlanID);
                ViewBag.RegionID = new SelectList(_adminUnitService.GetRegions(), "AdminUnitID", "Name");
                return View(bidViewModel);
            }
            
            if (ModelState.IsValid)
            {
                //var regions = _adminUnitService.FindBy(t => t.AdminUnitTypeID == 2);
                var regions = _adminUnitService.FindBy(t => t.AdminUnitID == bid.RegionID);
                bid.StatusID = (int)BidStatus.Open;
             
                var bidDetails = (from detail in regions
                                  select new BidDetail()
                                      {
                                          AmountForReliefProgram = (decimal)_transportBidPlanDetailService.GetRegionPlanTotal(bid.TransportBidPlanID, detail.AdminUnitID, 1),
                                          AmountForPSNPProgram = (decimal)_transportBidPlanDetailService.GetRegionPlanTotal(bid.TransportBidPlanID, detail.AdminUnitID, 2),
                                          BidDocumentPrice = 0,
                                          CPO = 0
                                          //RegionID = bid.RegionID
                                      }).ToList();
                bid.BidDetails = bidDetails;
                bid.RegionID = bid.RegionID;
                var user = (UserIdentity)System.Web.HttpContext.Current.User.Identity;
                bid.UserProfileId = user.Profile.UserProfileID;
                _bidService.AddBid(bid);

                return RedirectToAction("Index");
            }

            ViewBag.StatusID = new SelectList(_statusService.GetAllStatus(), "StatusID", "Name");
            ViewBag.BidPlanID = bid.TransportBidPlanID;
            ViewBag.TransportBidPlanID = new SelectList(_transportBidPlanService.GetAllTransportBidPlan(), "TransportBidPlanID", "ShortName", bid.TransportBidPlanID);
            ViewBag.RegionID = new SelectList(_adminUnitService.GetRegions(), "AdminUnitID", "Name");
            return View(bidViewModel);

            //return View("Index", _bidService.GetAllBid());
        }

        private Bid GetBid(CreateBidViewModel bidViewModel)
        {
           

            var bid = new Bid()
                {
                    

                    RegionID = bidViewModel.RegionID,
                    StartDate = bidViewModel.StartDate,
                    startTime = bidViewModel.StartTime,
                    EndDate = bidViewModel.EndDate,
                    endTime = bidViewModel.EndTime,
                    BidNumber = bidViewModel.BidNumber,
                    BidBondAmount = bidViewModel.BidBondAmount,
                    OpeningDate = bidViewModel.OpeningDate,
                    BidOpeningTime = bidViewModel.BidOpningTime,
                    StatusID = bidViewModel.StatusID,
                    TransportBidPlanID = bidViewModel.TransportBidPlanID

                };
            return bid;
        }

        private  Boolean IsBidMadeForThisRegion(int regionId,int bidPlanId)
        {
            var bidForThisRegion = _bidService.FindBy(b => b.RegionID == regionId && b.TransportBidPlanID == bidPlanId).ToList();
            if (bidForThisRegion.Count > 0)
            {
                return false;
            }
            return true;
        }



        public ActionResult Edit(int id)
        {

            var bid = _bidService.Get(m => m.BidID == id, null, "BidDetails").FirstOrDefault();
            ViewBag.BidNumber = bid.BidNumber;
            ViewBag.StartDate = bid.StartDate;
            ViewBag.EndDate = bid.EndDate;
            ViewBag.OpeningDate = bid.OpeningDate;
            var bidDetails = bid.BidDetails;
            var input = (from detail in bidDetails
                         select new BidDetailsViewModel
                             {
                                 BidDetailID = detail.BidDetailID,
                                 BidID = detail.BidID,
                                 Region = detail.Bid.AdminUnit.Name,
                                 Edit = new BidDetailsViewModel.BidDetailEdit()
                                     {
                                         Number = detail.BidDetailID,
                                         AmountForReliefProgram = detail.AmountForReliefProgram,
                                         AmountForPSNPProgram = detail.AmountForPSNPProgram,
                                         BidDocumentPrice = detail.BidDocumentPrice,
                                         CPO = detail.CPO,
                                         AmountForReliefProgramPlanned = (decimal)_transportBidPlanDetailService.GetRegionPlanTotal(bid.TransportBidPlanID, detail.Bid.AdminUnit.AdminUnitID, 1),
                                         AmountForPSNPProgramPlanned = (decimal)_transportBidPlanDetailService.GetRegionPlanTotal(bid.TransportBidPlanID, detail.Bid.AdminUnit.AdminUnitID, 2)

                                     }
                             }
                        );
            ViewData["BidDetail"] = input;
            return View(input);
        }

        [HttpPost]
        public ActionResult Edit(List<BidDetailsViewModel.BidDetailEdit> input)
        {
            var bidId = 0;
            foreach (var bidDetailEdit in input)
            {
                var bidDetail = _bidDetailService.FindById(bidDetailEdit.Number);
                bidId = bidDetail.BidID;

                bidDetail.AmountForReliefProgram = bidDetailEdit.AmountForReliefProgram;
                bidDetail.AmountForPSNPProgram = bidDetailEdit.AmountForPSNPProgram;
                bidDetail.BidDocumentPrice = bidDetailEdit.BidDocumentPrice;
                bidDetail.CPO = bidDetailEdit.CPO;
            }
            _bidDetailService.Save();
            return RedirectToAction("Edit", "Bid", new { id = bidId });
        }

        public ViewResult Details(int id = 0)
        {
            Bid bid = _bidService.Get(t => t.BidID == id, null, "BidDetails").FirstOrDefault();
            ViewBag.BidStatus = new SelectList(_statusService.GetAllStatus(), "StatusID", "Name", bid.StatusID);
            ViewData["BidDetails"] = bid;
            return View(bid);

            
        }
        public ActionResult EditBidStatus(int id)
        {
            Bid bid = _bidService.Get(t => t.BidID == id, null, "BidDetails").FirstOrDefault();
            ViewBag.StatusID = new SelectList(_statusService.GetAllStatus(), "StatusID", "Name", bid.StatusID);
            ViewBag.TransportBidPlanID = new SelectList(_transportBidPlanService.GetAllTransportBidPlan(),
                                                        "TransportBidPlanID", "ShortName", bid.TransportBidPlanID);
            ViewBag.RegionID = new SelectList(_adminUnitService.GetRegions(), "AdminUnitID", "Name",bid.RegionID);
            return View(bid);
        }
        [HttpPost]
        public ActionResult EditBidStatus(Bid bid)
        {
           

            if (ModelState.IsValid)
            {
                _bidService.EditBid(bid);
                return RedirectToAction("Index");
            }
            ViewBag.StatusID = new SelectList(_statusService.GetAllStatus(), "StatusID", "Name", bid.StatusID);
            ViewBag.TransportBidPlanID = new SelectList(_transportBidPlanService.GetAllTransportBidPlan(),
                                                        "TransportBidPlanID", "ShortName", bid.TransportBidPlanID);
            ViewBag.RegionID = new SelectList(_adminUnitService.GetRegions(), "AdminUnitID", "Name");
           // return View("Index", _bidService.GetAllBid());
            return View(bid);
        }

        public ActionResult MakeActive(int id)
        {
             _bidService.ActivateBid(id);
           // _applicationSettingService.SetValue("CurrentBid", ""+id);
            return RedirectToAction("Index","bid",new {id=(int)BidStatus.Active});
        }
        public ActionResult ApproveBid(int id)
        {
            var bid = _bidService.FindById(id);
            bid.StatusID = (int)BidStatus.Approved;
            _bidService.EditBid(bid);
            return RedirectToAction("Index", "bid", new { id = (int)BidStatus.Approved });
        } 
        public ActionResult CloseBid(int id)
        {
            var bid = _bidService.FindById(id);
            bid.StatusID = (int) BidStatus.Closed;
            _bidService.EditBid(bid);
            return RedirectToAction("Index", "Bid", new {id = (int) BidStatus.Closed});

        }
    }
}
