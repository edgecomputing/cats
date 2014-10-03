using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Helpers;
using Cats.Models.Constant;
using Cats.Services.EarlyWarning;
using Cats.Services.Security;
using Cats.ViewModelBinder;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.Regional.Controllers
{
    public class RegionRequisitionsController : Controller
    {
        //
        // GET: /Regional/RegionRequisitions/
        private readonly IReliefRequisitionService _reliefRequisitionService;
        private readonly IUserAccountService _userAccountService;
        private readonly IWorkflowStatusService _workflowStatusService;
        private readonly IDonorService _donorService;
        private readonly IRationService _rationService;

        private readonly IReliefRequisitionDetailService _reliefRequisitionDetailService;
        public RegionRequisitionsController(IUserAccountService userAccountService, IReliefRequisitionService reliefRequisitionService, IWorkflowStatusService workflowStatusService, IReliefRequisitionDetailService reliefRequisitionDetailService, IDonorService donorService, IRationService rationService)
        {
            _userAccountService = userAccountService;
            _reliefRequisitionService = reliefRequisitionService;
            _donorService = donorService;
            _rationService = rationService;
            _workflowStatusService = workflowStatusService;
            _reliefRequisitionDetailService = reliefRequisitionDetailService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Requisition_Read([DataSourceRequest] DataSourceRequest request)
        {
            var requests = _reliefRequisitionService.Get(t => t.Status == (int)Cats.Models.Constant.ReliefRequisitionStatus.Approved);
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var requestViewModels = RequisitionViewModelBinder.BindReliefRequisitionListViewModel(requests,
                                                                                                  _workflowStatusService
                                                                                                      .GetStatus(
                                                                                                          WORKFLOW.
                                                                                                              RELIEF_REQUISITION), datePref).OrderByDescending(m => m.RequisitionID);
            return Json(requestViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public decimal GetCommodityRation(int requisitionID, int commodityID)
        {
            var reliefRequisition = _reliefRequisitionService.FindById(requisitionID);
            var ration = _rationService.FindById(reliefRequisition.RegionalRequest.RationID);
            var rationModel = ration.RationDetails.FirstOrDefault(m => m.CommodityID == commodityID);

            return rationModel != null ? rationModel.Amount : 0;

        }

        [HttpGet]
        public ActionResult Allocation(int id)
        {
            var requisition =
                _reliefRequisitionService.Get(t => t.RequisitionID == id, null, "ReliefRequisitionDetails").
                    FirstOrDefault();
            ViewData["donors"] = _donorService.GetAllDonor();
            //ViewBag.HRDID = new SelectList(_donorService.GetAllDonor(), "HRDID", "Year", donor.HRDID);

            if (requisition == null)
            {
                HttpNotFound();
            }
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var requisitionViewModel = RequisitionViewModelBinder.BindReliefRequisitionViewModel(requisition, _workflowStatusService.GetStatus(WORKFLOW.RELIEF_REQUISITION), datePref);

            return View(requisitionViewModel);
        }

        public ActionResult Allocation_Read([DataSourceRequest] DataSourceRequest request, int id)
        {

            var requisitionDetails = _reliefRequisitionDetailService.Get(t => t.RequisitionID == id, null, "ReliefRequisition.AdminUnit,FDP.AdminUnit,FDP,Donor,Commodity").ToList();
            var commodityID = requisitionDetails.FirstOrDefault().CommodityID;
            var RationAmount = GetCommodityRation(id, commodityID);
            RationAmount = RationAmount.GetPreferedRation();

            var requisitionDetailViewModels = RequisitionViewModelBinder.BindReliefRequisitionDetailListViewModel(requisitionDetails, RationAmount);
            return Json(requisitionDetailViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

    }



}
