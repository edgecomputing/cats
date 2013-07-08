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
    public class BidController : Controller
    {
        //
        // GET: /Procurement/Bid/
        private IBidService _bidService;
        private IBidDetailService _bidDetailService;
        private IAdminUnitService _adminUnitService;
        private IStatusService _statusService;

        public BidController(IBidService bidService, IBidDetailService bidDetailService,
                             IAdminUnitService adminUnitService,
                             IStatusService statusService)
        {
            this._bidService = bidService;
            this._bidDetailService = bidDetailService;
            this._adminUnitService = adminUnitService;
            this._statusService = statusService;
        }

        public ActionResult Index()
        {
            //var bids = _bidService.Get(null, null,null);
            var bids = _bidService.Get(null, null, "Status");
            return View(bids.ToList());
        }
        [HttpPost]
        public ActionResult Index(string bidNumber,DateTime startDate,DateTime endDate)
        {
            var filteredBid = _bidService.Get(b =>b.BidNumber==bidNumber || (b.StartDate >= startDate && b.EndDate<=endDate), null, "BidDetails");
            return View(filteredBid.ToList());
            //return View("Index");
        }

        public ActionResult Create()
        {
           // var bid = new Bid();
            // return View(bid);
            var bid = new Bid();
            var regions = _adminUnitService.FindBy(t => t.AdminUnitTypeID == 2);
            ViewBag.StatusID = new SelectList(_statusService.GetAllStatus(),"StatusID","Name");
            var bidDetails = (from detail in regions
                              select new BidDetail()
                              {
                                  RegionID=detail.AdminUnitID,
                                  AmountForReliefProgram=0,
                              }).ToList();
            bid.BidDetails = bidDetails;
            return View(bid);
        }

        [HttpPost]
        public ActionResult Create(Bid bid)
        {
            if (bid != null)
            {
                var regions = _adminUnitService.FindBy(t => t.AdminUnitTypeID == 2);
                var bidDetails = (from detail in regions
                                  select new BidDetail()
                                      {
                                          RegionID = detail.AdminUnitID,
                                          AmountForReliefProgram =0,
                                          AmountForPSNPProgram = 0,
                                          BidDocumentPrice = 0,
                                          CPO = 0,
                                          
                                      }).ToList();
                bid.BidDetails = bidDetails;
                _bidService.AddBid(bid);
                return RedirectToAction("Edit", "Bid", new {id = bid.BidID});
            }
            return View(new Bid());
            // _bidService.AddBid(bid);
            //return Redirect(string.Format("Edit/{0}", bid.BidID));
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //var viewModel = new BidViewModel();
            
            var bid = _bidService.Get(m => m.BidID == id, null, "BidDetails").FirstOrDefault();
            ViewBag.BidNumber = bid.BidNumber;
            ViewBag.StartDate = bid.StartDate;
            ViewBag.EndDate = bid.EndDate;
            ViewBag.OpeningDate = bid.OpeningDate;
           
            //var bid = _bidService.FindById(id);
            //var regions = _adminUnitService.FindBy(m => m.AdminUnitTypeID == 2);
            var bidDetails = bid.BidDetails;
            var input = ( from detail in bidDetails
                          select new BidDetailsViewModel
                              {
                                  BidDetailID=detail.BidDetailID,
                                  BidID = detail.BidID,
                                  Region=detail.AdminUnit.Name,
                                  Edit=new BidDetailsViewModel.BidDetailEdit()
                                      {
                                          Number=detail.BidDetailID,
                                          AmountForReliefProgram=detail.AmountForReliefProgram,
                                          AmountForPSNPProgram=detail.AmountForPSNPProgram,
                                          BidDocumentPrice=detail.BidDocumentPrice,
                                          CPO=detail.CPO,
                                          
                                      }
                                  
                              }
                        );
            
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
           // return Redirect("Index");
            return RedirectToAction("Edit", "Bid", new {id = bidId});
        }

        public ActionResult Details(int id=0)
        {
            Bid bid = _bidService.Get(t => t.BidID == id, null, "BidDetails").FirstOrDefault();
            ViewBag.BidStatus = new SelectList(_statusService.GetAllStatus(), "StatusID", "Name",bid.StatusID);
            if (bid == null)
            {
                return HttpNotFound();
            }
            return View(bid);
            
        }
        public ActionResult EditBidStatus(int id)
        {
            Bid bid = _bidService.FindById(id);
            ViewBag.BidStatusID = new SelectList(_statusService.GetAllStatus(), "StatusID", "Name", bid.StatusID);
            return View(bid);
        }
        [HttpPost]
        public ActionResult EditBidStatus(Bid bid)
        {
            //var statusID = _bidService.FindById(bid.StatusID);
            _bidService.EditBid(bid);
            return RedirectToAction("Index");
        }
    }
}
