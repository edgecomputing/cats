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
       
        public BidAdministrationController(IBidWinnerService bidWinnerService,IApplicationSettingService applicationSettingService,
                                           IWorkflowStatusService workflowStatusService,IUserAccountService userAccountService,
                                           IBidService bidService,ITransportBidPlanService bidPlanService)
                                  
                                          
        {
            _bidWinnerService = bidWinnerService;
            _applicationSettingService = applicationSettingService;
            _workflowStatusService = workflowStatusService;
            _userAccountService = userAccountService;
            _bidService = bidService;
            _bidPlanService = bidPlanService;

        }
        // GET: /Procurement/BidAdministration/

        public ActionResult Index(int id=0)
        {
            var currentBid = _applicationSettingService.FindBy(t => t.SettingName == "CurrentBid").FirstOrDefault();
            if (currentBid == null)
            {
                return HttpNotFound();
            }
            ViewBag.BidID=currentBid.SettingValue;
            ViewBag.BidAdminStatus = id;
            //ViewBag.RFQGenerated = _bidWinnerService.IsRfqGenerated(int.Parse(currentBid.SettingValue));
            var bidWinnerViewModel = GetListOfBidWinners(int.Parse(currentBid.SettingValue));
            if (bidWinnerViewModel == null || !bidWinnerViewModel.Bidwinners.Any())
                return RedirectToAction("WithoutRFQ","BidAdministration");
           
           return View(bidWinnerViewModel);
         
        }

        public ActionResult BidAdminDraft_Read([DataSourceRequest] DataSourceRequest request,int id=0)
        {

            var bids = _bidWinnerService.FindBy(m => m.BidID == id).Where(m=>m.Status==(int)BidWinnerStatus.Draft);
            var bidsToDisplay = GetBidWinners(bids).ToList();
            return Json(bidsToDisplay.ToDataSourceResult(request));
        }

        public ActionResult BidAdminSigned_Read([DataSourceRequest] DataSourceRequest request, int id = 0)
        {

            var bids = _bidWinnerService.Get(m => m.BidID == id).Where(m => m.Status == (int)BidWinnerStatus.Signed);
            var bidsToDisplay = GetBidWinners(bids).ToList();
            return Json(bidsToDisplay.ToDataSourceResult(request));
        }
        public ActionResult BidPlanDetail_Read([DataSourceRequest] DataSourceRequest request,int id=0)
        {
            var bidPlan = _bidPlanService.FindById(id);
            var planDetailsToDisplay = GetBidPlanDetail(bidPlan.TransportBidPlanID).ToList();
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
                        WinnerTariff = bidWinner.Tariff,
                        Quantity = bidWinner.Amount,
                        BidID = bidWinner.BidID,
                        BidPlanID = bidWinner.Bid.TransportBidPlanID,
                        BidMumber=bidWinner.Bid.BidNumber,
                        BidStartDate = bidWinner.Bid.StartDate.ToCTSPreferedDateFormat(datePref),
                        BidEndDate = bidWinner.Bid.EndDate.ToCTSPreferedDateFormat(datePref),
                        BidOpeningDate = bidWinner.Bid.OpeningDate.ToCTSPreferedDateFormat(datePref),
                        StatusID = bidWinner.Status,
                        Status = _workflowStatusService.GetStatusName(WORKFLOW.BidWinner, bidWinner.Status)

                    });
        }
        public SelectedBidWinnerViewModel GetListOfBidWinners(int id)
        {
            var selectedBidWinners = new SelectedBidWinnerViewModel();
            //req.Transporters = _transportOrderService.GetTransporter();
            var bidWinner = _bidWinnerService.FindBy(m => m.BidID == id);
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
            if(bidPlan!=null)
            {
                return (from bidplanDetail in bidPlan.TransportBidPlanDetails
                        select new BidPlanDetailViewModel()
                            {
                                BidPlanID=bidPlan.TransportBidPlanID,
                                BidPlanDetailID = bidplanDetail.TransportBidPlanDetailID,
                                SourceID = bidplanDetail.SourceID,
                                Warehouse = bidplanDetail.Source.Name,
                                DestinationID = bidplanDetail.DestinationID,
                                Woreda = bidplanDetail.Destination.Name,
                                Region=bidplanDetail.Destination.AdminUnit2.AdminUnit2.Name,
                                Quantity = bidplanDetail.Quantity,
                                ProgramID = bidplanDetail.ProgramID,
                                Program = bidplanDetail.Program.Name
                            });
            }
        return null;
        }
    
    }
}
