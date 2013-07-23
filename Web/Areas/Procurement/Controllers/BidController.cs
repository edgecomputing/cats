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
    public class BidController : Controller
    {
        // GET: /Procurement/Bid/
        private IBidService _bidService;
        private IBidDetailService _bidDetailService;
        private IAdminUnitService _adminUnitService;
        private IStatusService _statusService;
        private ITransportBidPlanService _transportBidPlanService;
        private ITransportBidPlanDetailService _transportBidPlanDetailService;

        public BidController(IBidService bidService, IBidDetailService bidDetailService,
                             IAdminUnitService adminUnitService,
                             IStatusService statusService,
                             ITransportBidPlanService transportBidPlanService,
                             ITransportBidPlanDetailService transportBidPlanDetailService)
        {
            this._bidService = bidService;
            this._bidDetailService = bidDetailService;
            this._adminUnitService = adminUnitService;
            this._statusService = statusService;
            this._transportBidPlanService = transportBidPlanService;
            this._transportBidPlanDetailService = transportBidPlanDetailService;
        }

        public ActionResult Index()
        {
            var bids = _bidService.Get(m => m.StatusID == 1);
            var bidsToDisplay = GetBids(bids).ToList();
            return View(bidsToDisplay);
        }
       
     
        public ActionResult Bid_Read([DataSourceRequest] DataSourceRequest request)
        {
            var bids = _bidService.Get(m => m.StatusID == 1);
            //var bids = _bidService.GetAllBid();
            var bidsToDisplay = GetBids(bids).ToList();
            return Json(bidsToDisplay.ToDataSourceResult(request));
        }
        public ActionResult ApprovedBids()
        {
            var bids = _bidService.Get(m => m.StatusID == 4);
            var bidsToDisplay = GetBids(bids).ToList();
            return View(bidsToDisplay);
        }
        public ActionResult Bid_Status([DataSourceRequest] DataSourceRequest request)
        {
            var bids = _bidService.Get(m => m.StatusID == 4);
            var StatusToDisplay = GetBids(bids).ToList();
            return Json(StatusToDisplay.ToDataSourceResult(request));
        }

        public ActionResult BidDetail_Read(int bidID,[DataSourceRequest] DataSourceRequest request)
        {
            var bidDetails = _bidDetailService.GetAllBidDetail();
            var bidsToDisplay = GetBidDetails(bidDetails).ToList();
            return Json(bidsToDisplay.Where(p => p.BidID == bidID).ToDataSourceResult(request));
        }

        private IEnumerable<BidViewModel> GetBids(IEnumerable<Bid> bids)
        {
            return (from bid in bids
                    select new BidViewModel()
                    {
                        BidID = bid.BidID,
                        BidNumber = bid.BidNumber,
                        StartDate = bid.StartDate,
                        EndDate = bid.EndDate,
                        OpeningDate = bid.OpeningDate,
                        Status = bid.Status.Name,
                        StatusID = bid.StatusID
                    });
        }
        private IEnumerable<BidDetailViewModel> GetBidDetails(IEnumerable<BidDetail> bidDetails)
        {
            return (from bidDetail in bidDetails
                    select new BidDetailViewModel()
                    {
                        BidDetailID =bidDetail.BidDetailID,
                        BidID = bidDetail.BidID,
                        Region= bidDetail.AdminUnit.Name,
                        RegionID=bidDetail.AdminUnit.AdminUnitID,
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


        //[HttpGet]
        //public ActionResult Index(string bidNumber="", bool open = true, bool closed = false, bool canceled = false, bool approved = false)
        //{
        //    var allStatusTypes = _statusService.GetAllStatus().Select(m => m.Name).ToList();
        //    ViewBag.BidStatusTypes = allStatusTypes;

        //    var statusTypesList = "";
        //    if (open) statusTypesList += "Open;";
        //    if (closed) statusTypesList += "Closed;";
        //    if (canceled) statusTypesList += "Canceled;";
        //    if (approved) statusTypesList += "Approved;";

        //    var listOfStatusTypes = statusTypesList.Split(';');

        //    var filteredBids = _bidService.Get(b => b.BidNumber.StartsWith(bidNumber) &&(listOfStatusTypes.Contains(b.Status.Name)));
        //    ViewData["bids"] = filteredBids;
        //    return View(filteredBids.ToList());
        //}

        public ActionResult Create(int id = 0)
        {
            var bid = new Bid();
            var regions = _adminUnitService.FindBy(t => t.AdminUnitTypeID == 2);
            ViewBag.StatusID = new SelectList(_statusService.GetAllStatus(), "StatusID", "Name", bid.StatusID = 1);
            var bidDetails = (from detail in regions
                              select new BidDetail()
                              {
                                  RegionID = detail.AdminUnitID,
                                  AmountForReliefProgram = 0,
                              }).ToList();
            bid.BidDetails = bidDetails;
            ViewBag.BidPlanID = id;
            ViewBag.TransportBidPlanID = new SelectList(_transportBidPlanService.GetAllTransportBidPlan(), "TransportBidPlanID", "ShortName", id);
            return View(bid);
        }

        //[HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Bid bid)
        {

            //DateTime startingdate=DateTime.Now;
            //DateTime EndDate= DateTime.Now;
            //DateTime OpeningDate=DateTime.Now;
            //try
            //{
            //    startingdate = DateTime.Parse(start);
            //    EndDate = DateTime.Parse(End);
            //    OpeningDate = DateTime.Parse(Opening);
            //}
            //catch (Exception)
            //{
            //    var strEth = new getGregorianDate();
            //    startingdate = strEth.ReturnGregorianDate(start);
            //    EndDate = strEth.ReturnGregorianDate(End);
            //    OpeningDate = strEth.ReturnGregorianDate(Opening);
            //    //throw;
            //}
            //bid.StartDate = startingdate.Date;
            //bid.EndDate = EndDate.Date;
            //bid.OpeningDate = OpeningDate.Date;

            if (ModelState.IsValid)
            {
                var regions = _adminUnitService.FindBy(t => t.AdminUnitTypeID == 2);
                bid.StatusID = 1;
                var bidDetails = (from detail in regions
                                  select new BidDetail()
                                      {
                                          RegionID = detail.AdminUnitID,
                                          AmountForReliefProgram = (decimal)_transportBidPlanDetailService.GetRegionPlanTotal(bid.TransportBidPlanID, detail.AdminUnitID, 1),
                                          AmountForPSNPProgram = (decimal)_transportBidPlanDetailService.GetRegionPlanTotal(bid.TransportBidPlanID, detail.AdminUnitID, 2),
                                          BidDocumentPrice = 0,
                                          CPO = 0,

                                      }).ToList();
                bid.BidDetails = bidDetails;
                _bidService.AddBid(bid);

                RouteValueDictionary routeValues = this.GridRouteValues();
                //return RedirectToAction("Edit","Bid", routeValues);
                //return RedirectToAction("Edit", "Bid", new { id = bid.BidID });
                return RedirectToAction("Index");
            }
            ViewBag.StatusID = new SelectList(_statusService.GetAllStatus(), "StatusID", "Name");
            ViewBag.BidPlanID = bid.TransportBidPlanID;
            ViewBag.TransportBidPlanID = new SelectList(_transportBidPlanService.GetAllTransportBidPlan(), "TransportBidPlanID", "ShortName", bid.TransportBidPlanID);

            return View(bid);

            //return View("Index", _bidService.GetAllBid());
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
                                 Region = detail.AdminUnit.Name,
                                 Edit = new BidDetailsViewModel.BidDetailEdit()
                                     {
                                         Number = detail.BidDetailID,
                                         AmountForReliefProgram = detail.AmountForReliefProgram,
                                         AmountForPSNPProgram = detail.AmountForPSNPProgram,
                                         BidDocumentPrice = detail.BidDocumentPrice,
                                         CPO = detail.CPO,
                                         AmountForReliefProgramPlanned = (decimal)_transportBidPlanDetailService.GetRegionPlanTotal(bid.TransportBidPlanID, detail.AdminUnit.AdminUnitID, 1),
                                         AmountForPSNPProgramPlanned = (decimal)_transportBidPlanDetailService.GetRegionPlanTotal(bid.TransportBidPlanID, detail.AdminUnit.AdminUnitID, 2)

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
            //var bidDetails = _bidDetailService.Get(t => t.BidID == id, null,null).FirstOrDefault();
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
            return View(bid);
        }
        [HttpPost]
        //[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditBidStatus(Bid bid)
        {
            //    DateTime startingdate = GetGregorianDate(start);
            //    DateTime EndDate = GetGregorianDate(end);
            //    DateTime OpeningDate = GetGregorianDate(open);

            //bid.StartDate = startingdate.Date;
            //bid.EndDate = EndDate.Date;
            //bid.OpeningDate = OpeningDate.Date;
            if (ModelState.IsValid)
            {
                _bidService.EditBid(bid);
                RouteValueDictionary routeValues = this.GridRouteValues();
                //return RedirectToAction("Index", routeValues);
                return RedirectToAction("Index");
            }
            ViewBag.StatusID = new SelectList(_statusService.GetAllStatus(), "StatusID", "Name", bid.StatusID);
            ViewBag.TransportBidPlanID = new SelectList(_transportBidPlanService.GetAllTransportBidPlan(),
                                                        "TransportBidPlanID", "ShortName", bid.TransportBidPlanID);
           // return View("Index", _bidService.GetAllBid());
            return View(bid);
        }

        private DateTime GetGregorianDate(string ethiopianDate)
        {
            DateTime convertedGregorianDate;
            try
            {
                convertedGregorianDate = DateTime.Parse(ethiopianDate);
            }
            catch (Exception ex)
            {
                var strEth = new getGregorianDate();
                convertedGregorianDate = strEth.ReturnGregorianDate(ethiopianDate);
            }
            return convertedGregorianDate;
        }

        public ActionResult ApproveBid(int id)
        {
            var bid = _bidService.FindById(id);
            bid.StatusID = 4;
            _bidService.EditBid(bid);
            return RedirectToAction("Index");
        }
    }
}
