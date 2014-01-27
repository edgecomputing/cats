using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Procurement.Models;
using Cats.Helpers;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Services.Common;
using Cats.Services.EarlyWarning;
using Cats.Services.Security;
using Cats.Services.Procurement;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.Procurement.Controllers
{
    public class BidAdministrationController : Controller
    {
        private readonly IBidWinnerService _bidWinnerService;
        private readonly IApplicationSettingService _applicationSettingService;
        private readonly IWorkflowStatusService _workflowStatusService;
        private readonly IUserAccountService _userAccountService;
        private readonly IBidService _bidService;
        private readonly ITransportBidPlanService _bidPlanService;


        public BidAdministrationController(IBidWinnerService bidWinnerService, IApplicationSettingService applicationSettingService,
                                           IWorkflowStatusService workflowStatusService, IUserAccountService userAccountService,
                                           IBidService bidService, ITransportBidPlanService bidPlanService)
        {
            _bidWinnerService = bidWinnerService;
            _applicationSettingService = applicationSettingService;
            _workflowStatusService = workflowStatusService;
            _userAccountService = userAccountService;
            _bidService = bidService;
            _bidPlanService = bidPlanService;


        }
        // GET: /Procurement/BidAdministration/

        public ActionResult Index(int id = 0)
        {
            //var currentBid = _applicationSettingService.FindBy(t => t.SettingName == "CurrentBid").FirstOrDefault();
            //if (currentBid == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.BidID=currentBid.SettingValue;
            var bid = _bidService.FindBy(m => m.StatusID == (int)BidStatus.Active);

            //TODO: Dont we need to check if bid varable is null?
            if (null != bid)
                ViewBag.BIDID = new SelectList(bid, "BIDID", "BidNumber");
            //ViewBag.BidAdminStatus = id;
            //var bid=_bidService.FindById(int.Parse(currentBid.SettingValue));
            //ViewBag.BidPlanID = bid.TransportBidPlanID;
            //var bidWinnerViewModel = GetListOfBidWinners(int.Parse(currentBid.SettingValue));
            //var bidWinnerViewModel = GetListOfBidWinners(bid.);
            //if (bidWinnerViewModel==null || !bidWinnerViewModel.Bidwinners.Any())
            //    return RedirectToAction("WithoutRFQ","BidAdministration");

            //return View(bidWinnerViewModel);
            return View();

        }
        public ActionResult BidAdminDraft_Read([DataSourceRequest] DataSourceRequest request, int? BIDID)
        {

            int bidID = BIDID ?? 0;
            var bids = _bidWinnerService.FindBy(m => m.BidID == bidID);
            var bidsToDisplay = GetBidWinners(bids).ToList();
            return Json(bidsToDisplay.ToDataSourceResult(request));
        }

        public ActionResult BidAdminSigned_Read([DataSourceRequest] DataSourceRequest request, int id = 0)
        {

            var bids = _bidWinnerService.Get(m => m.BidID == id).Where(m => m.Status == (int)BidWinnerStatus.Signed);
            var bidsToDisplay = GetBidWinners(bids).ToList();
            return Json(bidsToDisplay.ToDataSourceResult(request));
        }
        public ActionResult WoredasWithoutOffer_Read([DataSourceRequest] DataSourceRequest request, int BIDID)
        {

            var bid = _bidService.FindById(BIDID);
            var bidPlanDetail = _bidPlanService.FindById(bid.TransportBidPlanID);
            var planned = (from planDetail in bidPlanDetail.TransportBidPlanDetails
                           group planDetail by new
                           {
                               planDetail.DestinationID,
                               planDetail.SourceID
                           }
                               into grouped
                               select grouped
                      );

            var detailPlans = planned.Select(d => d.ToList()).Select(er => er.FirstOrDefault()).ToList();

            var result = new List<BidWinnerViewModel>();
            foreach (TransportBidPlanDetail pdetail in detailPlans)
            {
                var detail = _bidWinnerService.FindBy(t => t.BidID == BIDID && t.SourceID == pdetail.SourceID &&
                                                     t.DestinationID == pdetail.DestinationID).FirstOrDefault();
                if (detail == null)
                    result.Add(new BidWinnerViewModel()
                    {
                        SourceWarehouse = pdetail.Source.Name,
                        Zone = pdetail.Destination.AdminUnit2.Name,
                        Region = pdetail.Destination.AdminUnit2.AdminUnit2.Name,
                        Woreda = pdetail.Destination.Name,
                        BidID = BIDID,
                        DestinationId = pdetail.DestinationID,
                        SourceId = pdetail.SourceID
                    });
            }

            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult BidPlanDetail_Read([DataSourceRequest] DataSourceRequest request, int id = 0)
        {
            //var bidPlan = _bidPlanService.FindById(id);
            var planDetailsToDisplay = GetBidPlanDetail(id).ToList();
            return Json(planDetailsToDisplay.ToDataSourceResult(request));
        }
        private IEnumerable<BidWinnerViewModel> GetBidWinners(IEnumerable<BidWinner> bidWinners)
        {
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            return (from bidWinner in bidWinners
                    select new BidWinnerViewModel()
                    {
                        BidWinnnerID = bidWinner.BidWinnerID,
                        TransporterID = bidWinner.TransporterID,
                        TransporterName = bidWinner.Transporter.Name,
                        SourceWarehouse = bidWinner.Hub.Name,
                        Woreda = bidWinner.AdminUnit.Name,
                        Region = bidWinner.AdminUnit.AdminUnit2.AdminUnit2.Name,
                        WinnerTariff = bidWinner.Tariff,
                        Quantity = bidWinner.Amount,
                        BidID = bidWinner.BidID,
                        BidPlanID = bidWinner.Bid.TransportBidPlanID,
                        BidMumber = bidWinner.Bid.BidNumber,
                        BidStartDate = bidWinner.Bid.StartDate.ToCTSPreferedDateFormat(datePref),
                        BidEndDate = bidWinner.Bid.EndDate.ToCTSPreferedDateFormat(datePref),
                        BidOpeningDate = bidWinner.Bid.OpeningDate.ToCTSPreferedDateFormat(datePref),
                        StatusID = bidWinner.Status,

                        //Status = _workflowStatusService.GetStatusName(WORKFLOW.BidWinner, int(bidWinner.Status.Value))

                    });
        }
        public SelectedBidWinnerViewModel GetListOfBidWinners(int id)
        {
            var selectedBidWinners = new SelectedBidWinnerViewModel();
            var bidWinner = _bidWinnerService.FindBy(m => m.BidID == id && m.Position == 1);
            if (bidWinner != null)
            {
                selectedBidWinners.Bidwinners = GetBidWinners(bidWinner).ToList();
            }
            return selectedBidWinners;
        }
        private IEnumerable<BidWinner> GetSelectedWinner(SelectedBidWinnerViewModel selectedWinners)
        {
            return (from selectedWinner in selectedWinners.Bidwinners
                    select new BidWinner()
                        {
                            BidWinnerID = selectedWinner.BidWinnnerID,
                            DestinationID = selectedWinner.DestinationId
                        });
        }
        public ActionResult WithoutRFQ()
        {
            var currentBid = _applicationSettingService.FindBy(t => t.SettingName == "CurrentBid").FirstOrDefault();
            if (currentBid == null)
            {
                return HttpNotFound();
            }
            var bid = _bidService.FindById(int.Parse(currentBid.SettingValue));
            ViewBag.BidNumber = bid.BidNumber;
            //ViewBag.BidPlanID = bid.TransportBidPlanID;
            var bidPlanDetailViewModel = new BidPlanDetailListViewModel()
                {
                    BidPlanID = bid.TransportBidPlanID,
                    BidPlanDetails = GetBidPlanDetail(bid.TransportBidPlanID)
                };

            return View(bidPlanDetailViewModel);
        }
        private IEnumerable<BidPlanDetailViewModel> GetBidPlanDetail(int planID)
        {
            var bidPlan = _bidPlanService.FindById(planID);
            if (bidPlan != null)
            {
                return (from bidplanDetail in bidPlan.TransportBidPlanDetails
                        select new BidPlanDetailViewModel()
                            {
                                BidPlanID = bidPlan.TransportBidPlanID,
                                BidPlanDetailID = bidplanDetail.TransportBidPlanDetailID,
                                SourceID = bidplanDetail.SourceID,
                                Warehouse = bidplanDetail.Source.Name,
                                DestinationID = bidplanDetail.DestinationID,
                                Woreda = bidplanDetail.Destination.Name,
                                Region = bidplanDetail.Destination.AdminUnit2.AdminUnit2.Name,
                                Quantity = bidplanDetail.Quantity,
                                ProgramID = bidplanDetail.ProgramID,
                                Program = bidplanDetail.Program.Name
                            });
            }
            return null;
        }

        public bool WinnerGenerated(int BIDID)
        {
            var bidWinner = _bidWinnerService.FindBy(m => m.BidID == BIDID);
            if (bidWinner.Count > 0)
                return true;
            return false;

        }



    }
}
