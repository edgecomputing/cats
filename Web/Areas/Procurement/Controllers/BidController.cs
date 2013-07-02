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

        public BidController(IBidService bidService, IBidDetailService bidDetailService,
                             IAdminUnitService adminUnitService)
        {
            this._bidService = bidService;
            this._bidDetailService = bidDetailService;
            this._adminUnitService = adminUnitService;
        }

        public ActionResult Index()
        {
            //var bids = _bidService.Get(null,null,"BidNumber");
            var bids = _bidService.GetAllBid();
            return View(bids.ToList());
        }
        [HttpPost]
        public ActionResult Index(DateTime startDate, DateTime endDate)
        {
           // var bid = _bidService.Get(b => b.StartDate >= startDate && b.EndDate <= endDate, null, "StartDate");
            return View("Index");
        }
        
        public ActionResult Create()
        {
            var bid = new Bid();
            return View(bid);
        }

        [HttpPost]
        public ActionResult Create(Bid bid)
        {
            _bidService.AddBid(bid);
            return Redirect(string.Format("Edit/{0}", bid.BidID));
        }

        public ActionResult Edit(int id)
        {
            //var viewModel = new BidViewModel();
            var bid = _bidService.Get(m => m.BidID == id, null, "BidDetails").FirstOrDefault();
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
                                          CPO=detail.CBO,

                                      }
                                  
                              }
                        );
            
            return View(input);
        }


        [HttpPost]
        public ActionResult Edit(int id, BidViewModel bidViewModel)
        {
            foreach (var bidDetail in bidViewModel.BidDetails)
            {
                _bidDetailService.AddBidDetail(bidDetail);
            }
            return Redirect("Index");
        }
        public ActionResult Details(int id)
        {
            var viewModel = new BidViewModel();
            var bid = _bidService.FindById(id);
            var regions = _adminUnitService.FindBy(m => m.AdminUnitTypeID == 2);
            var bidDetails = new List<BidDetail>();
            foreach (var region in regions)
            {
                var bidDetail = new BidDetail();
                bidDetail.AdminUnit = region;
                bidDetail.Bid = bid;
                bidDetails.Add(bidDetail);
            }
            viewModel.BidID = bid.BidID;
            viewModel.BidNumber = bid.BidNumber;
            viewModel.StartDate = bid.StartDate;
            viewModel.EndDate = bid.EndDate;
            viewModel.BidDetails = bidDetails;
            return View(viewModel);
        }
    }
}
