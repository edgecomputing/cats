using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Models;
using Cats.Helpers;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Models.ViewModels;
using Cats.Services.EarlyWarning;
using Cats.Services.Security;
using Cats.ViewModelBinder;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.EarlyWarning.Controllers
{
    public class ReliefRequisitionController : Controller
    {
        //
        // GET: /EarlyWarning/ReliefRequisition/

        private readonly IReliefRequisitionService _reliefRequisitionService;
        private readonly IWorkflowStatusService _workflowStatusService;
        private readonly IReliefRequisitionDetailService _reliefRequisitionDetailService;
        private readonly IUserAccountService _userAccountService;
        private readonly IRationService  _rationService;


        public ReliefRequisitionController(IReliefRequisitionService reliefRequisitionService, IWorkflowStatusService workflowStatusService, 
            IReliefRequisitionDetailService reliefRequisitionDetailService,
            IUserAccountService userAccountService,
            IRationService rationService)
        {
            this._reliefRequisitionService = reliefRequisitionService;
            this._workflowStatusService = workflowStatusService;
            this._reliefRequisitionDetailService = reliefRequisitionDetailService;
            _userAccountService = userAccountService;
            _rationService = rationService;

        }

        public ViewResult Index(int id = 1)
        {
            ViewBag.Status = id;
            return View();
        }

        [HttpGet]
        public ActionResult CreateRequisiton(int id)
        {
            var input = _reliefRequisitionService.CreateRequisition(id);
            return RedirectToAction("NewRequisiton", "ReliefRequisition", new { id = id });
        }

        [HttpGet]
        public ViewResult NewRequisiton(int id)
        {
            var input = _reliefRequisitionService.GetRequisitionByRequestId(id);
            return View(input);


        }

        [HttpPost]
        public ActionResult NewRequisiton(List<DataFromGrid> input)
        {
            var requId = 0;
            if (ModelState.IsValid)
            {
                var requisitionNumbers = input.ToDictionary(t => t.Number, t => t.RequisitionNo);
                _reliefRequisitionService.AssignRequisitonNo(requisitionNumbers);

            }
            return RedirectToAction("Index", "ReliefRequisition");
        }

        [HttpGet]
        public ActionResult Allocation(int id)
        {
            var requisition =
                _reliefRequisitionService.Get(t => t.RequisitionID == id, null, "ReliefRequisitionDetails").
                    FirstOrDefault();
            if (requisition == null)
            {
                HttpNotFound();
            }
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var requisitionViewModel = RequisitionViewModelBinder.BindReliefRequisitionViewModel(requisition, _workflowStatusService.GetStatus(WORKFLOW.RELIEF_REQUISITION),datePref);

            return View(requisitionViewModel);
        }

        public ActionResult Allocation_Read([DataSourceRequest] DataSourceRequest request, int id)
        {

            var requisitionDetails = _reliefRequisitionDetailService.Get(t => t.RequisitionID == id, null, "ReliefRequisition.AdminUnit,FDP.AdminUnit,FDP,Donor,Commodity");
            var commodityID = requisitionDetails.FirstOrDefault().CommodityID;
            var RationAmount = GetCommodityRation(id, commodityID);
            var requisitionDetailViewModels = RequisitionViewModelBinder.BindReliefRequisitionDetailListViewModel(requisitionDetails,RationAmount);
            return Json(requisitionDetailViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Allocation_Create([DataSourceRequest] DataSourceRequest request, ReliefRequisitionDetailViewModel reliefRequisitionDetailViewModel)
        {
            if (reliefRequisitionDetailViewModel != null && ModelState.IsValid)
            {
                _reliefRequisitionDetailService.AddReliefRequisitionDetail(RequisitionViewModelBinder.BindReliefRequisitionDetail(reliefRequisitionDetailViewModel));
            }

            return Json(new[] { reliefRequisitionDetailViewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Allocation_Update([DataSourceRequest] DataSourceRequest request, ReliefRequisitionDetailViewModel reliefRequisitionDetailViewModel)
        {
            if (reliefRequisitionDetailViewModel != null && ModelState.IsValid)
            {
                var target = _reliefRequisitionDetailService.FindById(reliefRequisitionDetailViewModel.RequisitionDetailID);
                if (target != null)
                {
                    target.Amount = reliefRequisitionDetailViewModel.Amount;
                    target.BenficiaryNo = reliefRequisitionDetailViewModel.BenficiaryNo;

                    _reliefRequisitionDetailService.EditReliefRequisitionDetail(target);
                }
            }

            return Json(new[] { reliefRequisitionDetailViewModel }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Allocation_Destroy([DataSourceRequest] DataSourceRequest request,
                                                  ReliefRequisitionDetail reliefRequisitionDetail)
        {
            if (reliefRequisitionDetail != null)
            {
                _reliefRequisitionDetailService.DeleteById(reliefRequisitionDetail.RequisitionDetailID);
            }

            return Json(ModelState.ToDataSourceResult());
        }


        [HttpPost]
        public ActionResult RequistionDetailEdit(IEnumerable<ReleifRequisitionDetailEdit.ReleifRequisitionDetailEditInput> input)
        {
            // var requId = 0;
            if (ModelState.IsValid)
            {
                var allocaitons = input.ToDictionary(t => t.Number, t => t.Amount);

                _reliefRequisitionService.EditAllocatedAmount(allocaitons);

            }
            return RedirectToAction("Index", "ReliefRequisition");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var relifRequisition = _reliefRequisitionService.FindById(id);
            if (relifRequisition == null)
            {
                HttpNotFound();
            }
         return View(relifRequisition);
        }

        [HttpPost]
        public ActionResult Edit(ReliefRequisition reliefrequisition)
        {
            if (ModelState.IsValid)
            {
                _reliefRequisitionService.EditReliefRequisition(reliefrequisition);
                return RedirectToAction("Index", "ReliefRequisition");
            }
            return View(reliefrequisition);
        }

        [HttpGet]
        public ActionResult SendToLogistics(int id)
        {
            var requistion = _reliefRequisitionService.FindById(id);
            if (requistion == null)
            {
                HttpNotFound();
            }
            return View(requistion);
        }

        [HttpPost]
        public ActionResult ConfirmSendToLogistics(int requisitionid)
        {
            var requisition = _reliefRequisitionService.FindById(requisitionid);
            requisition.Status = (int)ReliefRequisitionStatus.Approved;
            _reliefRequisitionService.EditReliefRequisition(requisition);
            return RedirectToAction("Index", "ReliefRequisition");
        }

        public ActionResult Requisition_Read([DataSourceRequest] DataSourceRequest request, int id = 0)
        {


            var requests = _reliefRequisitionService.Get(t => t.Status == id);
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var requestViewModels = RequisitionViewModelBinder.BindReliefRequisitionListViewModel(requests,
                                                                                                  _workflowStatusService
                                                                                                      .GetStatus(
                                                                                                          WORKFLOW.
                                                                                                              RELIEF_REQUISITION),datePref);
            return Json(requestViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Requisition_Update([DataSourceRequest] DataSourceRequest request, ReliefRequisitionViewModel reliefRequisitionViewModel)
        {
            if (reliefRequisitionViewModel != null && ModelState.IsValid)
            {
                var target = _reliefRequisitionService.FindById(reliefRequisitionViewModel.RequisitionID);
                if (target != null)
                {

                    target.RequisitionNo = reliefRequisitionViewModel.RequisitionNo;

                    _reliefRequisitionService.EditReliefRequisition(target);
                }
            }

            return Json(new[] { reliefRequisitionViewModel }.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Requisition_Destroy([DataSourceRequest] DataSourceRequest request,
                                                  ReliefRequisition reliefRequisition)
        {
            if (reliefRequisition != null)
            {
                _reliefRequisitionDetailService.DeleteById(reliefRequisition.RequisitionID);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult Details(int id)
        {
            var requisition = _reliefRequisitionService.FindById(id);
            if (requisition == null)
            {
                return HttpNotFound();
            }
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var requisitionViewModel = RequisitionViewModelBinder.BindReliefRequisitionViewModel(requisition, _workflowStatusService.GetStatus(WORKFLOW.RELIEF_REQUISITION), datePref);
            return View(requisitionViewModel);
        }

        public decimal GetCommodityRation(int requisitionID, int commodityID)
        {
            var reliefRequisition = _reliefRequisitionService.FindById(requisitionID);
                var ration = _rationService.FindById(reliefRequisition.RegionalRequest.RationID);
                var rationModel = ration.RationDetails.FirstOrDefault(m => m.CommodityID == commodityID).Amount;

             return rationModel;

        }

    }
}