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

namespace Cats.Areas.Procurement.Controllers
{
    public class BidWinnerController : Controller
    {
        // GET: /Procurement/Bid/
        private readonly IBidWinnerService _bidWinnerService;
        private readonly IUserAccountService _userAccountService;
        private readonly IWorkflowStatusService _workflowStatusService;
        public BidWinnerController(IBidWinnerService bidWinnerService,IUserAccountService userAccountService,IWorkflowStatusService workflowStatusService)
        {
           _bidWinnerService = bidWinnerService;
            _userAccountService = userAccountService;
            _workflowStatusService = workflowStatusService;

        }

        public ActionResult Index()
        {
            var bidWinner = _bidWinnerService.GetBidsWithWinner();
            return View();
        }
        public ActionResult Bid_Read([DataSourceRequest] DataSourceRequest request)
        {

            var bid = _bidWinnerService.GetBidsWithWinner().OrderByDescending(m => m.BidID);
            var winnerToDisplay = GetBids(bid).ToList();
            return Json(winnerToDisplay.ToDataSourceResult(request));
        }
        private IEnumerable<BidWithWinnerViewModel> GetBids(IEnumerable<Bid> bids)
        {
             var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            return (from bid in bids
                    select new BidWithWinnerViewModel()
                        {
                            BidID = bid.BidID,
                            BidNumber = bid.BidNumber,
                            Year = bid.OpeningDate.Year,
                            OpeningDate = bid.OpeningDate.ToCTSPreferedDateFormat(datePref)
                            
                        });
        }

        public ActionResult Details(int id)
        {
            var bidWinners = _bidWinnerService.FindBy(m => m.BidID == id);
            ViewBag.BidNumber = bidWinners.First().Bid.BidNumber;
            if (bidWinners == null)
            {
                return HttpNotFound();
            }
            var bidWinnersViewModel = new WinnersByBidViewModel
                {
                    BidID = id,
                    BidWinners = GetBidWinner(bidWinners)
                };

            return View(bidWinnersViewModel);
        }

        public ActionResult BidWinner_Read([DataSourceRequest] DataSourceRequest request,int id=0)
        {

            var bidWinner = _bidWinnerService.FindBy(m=>m.BidID==id).OrderByDescending(m => m.BidWinnerID);
            var winnerToDisplay = GetBidWinner(bidWinner).ToList();
            return Json(winnerToDisplay.ToDataSourceResult(request));
        }

        private  IEnumerable<BidWinnerViewModel> GetBidWinner(IEnumerable<BidWinner> bidWinners)
        {
            return (from bidWinner in bidWinners
                    select new BidWinnerViewModel()
                        {
                            BidWinnnerID = bidWinner.BidWinnerID,
                            TransporterID = bidWinner.TransporterID,
                            TransporterName = bidWinner.Transporter.Name,
                            SourceWarehouse = bidWinner.Hub.Name,
                            Woreda = bidWinner.AdminUnit.Name,
                            WinnerTariff = bidWinner.Tariff,
                            Quantity = bidWinner.Amount,
                            StatusID = bidWinner.Status,
                            Status =_workflowStatusService.GetStatusName(WORKFLOW.BidWinner,bidWinner.Status)

                        });
        }
     
        public ActionResult Edit(int id)
        {
            var bidWinner = _bidWinnerService.FindById(id);
            if (bidWinner==null)
            {
                return HttpNotFound();
            }
            ViewBag.Status = new SelectList(_workflowStatusService.GetStatus(WORKFLOW.BidWinner),"WorkflowID","Description");
            return View(bidWinner);
        }
        [HttpPost]
        public ActionResult Edit(BidWinner bidWinner)
        {
            if (ModelState.IsValid)
            {
                _bidWinnerService.EditBidWinner(bidWinner);
                return RedirectToAction("Index");
            }
            return View(bidWinner);
        }

        public ActionResult SignedContract(int id)
        {
            var bidWinner = _bidWinnerService.FindById(id);
            if(bidWinner!=null)
            {
                _bidWinnerService.SignContract(bidWinner);
                return RedirectToAction("Details", "BidWinner", new {id = bidWinner.BidID});
            }
            return RedirectToAction("Index");
        }

        public ActionResult DisqualifiedWinner(int id)
        {
            var bidWinner = _bidWinnerService.FindById(id);
            if (bidWinner != null)
            {
                _bidWinnerService.Disqualified(bidWinner);
                return RedirectToAction("Details", "BidWinner", new { id = bidWinner.BidID });
            }
            return RedirectToAction("Index");
        }

    }
}
