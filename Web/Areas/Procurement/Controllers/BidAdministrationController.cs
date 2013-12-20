using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Procurement.Models;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Services.Common;
using Cats.Services.EarlyWarning;
using Cats.Services.Procurement;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.Procurement.Controllers
{
    public class BidAdministrationController : Controller
    {
        private IBidWinnerService _bidWinnerService;
        private IApplicationSettingService _applicationSettingService;
        private IWorkflowStatusService _workflowStatusService;

        public BidAdministrationController(IBidWinnerService bidWinnerService,IApplicationSettingService applicationSettingService,IWorkflowStatusService workflowStatusService)
        {
            _bidWinnerService = bidWinnerService;
            _applicationSettingService = applicationSettingService;
            _workflowStatusService = workflowStatusService;

        }
        // GET: /Procurement/BidAdministration/

        public ActionResult Index()
        {
            var currentBid = _applicationSettingService.FindBy(t => t.SettingName == "CurrentBid").FirstOrDefault();
            if (currentBid != null)
                ViewBag.BidID=currentBid.SettingValue;
            return View();
        }

        public ActionResult BidAdminDraft_Read([DataSourceRequest] DataSourceRequest request,int id=0)
        {

            var bids = _bidWinnerService.Get(m => m.BidID == id).Where(m=>m.Status==(int)BidWinnerStatus.Draft);
            var bidsToDisplay = GetBidWinners(bids).ToList();
            return Json(bidsToDisplay.ToDataSourceResult(request));
        }

        public ActionResult BidAdminSigned_Read([DataSourceRequest] DataSourceRequest request, int id = 0)
        {

            var bids = _bidWinnerService.Get(m => m.BidID == id).Where(m => m.Status == (int)BidWinnerStatus.Signed);
            var bidsToDisplay = GetBidWinners(bids).ToList();
            return Json(bidsToDisplay.ToDataSourceResult(request));
        }

        private IEnumerable<BidWinnerViewModel> GetBidWinners(IEnumerable<BidWinner> bidWinners)
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
                        Status = _workflowStatusService.GetStatusName(WORKFLOW.BidWinner, bidWinner.Status)

                    });
        }
    }
}
