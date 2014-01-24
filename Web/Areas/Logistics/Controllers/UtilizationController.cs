using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cats.Models.Constant;
using Cats.Services.Common;
using Cats.Services.Logistics;
using Cats.Services.EarlyWarning;
using Cats.Services.Security;
using Cats.ViewModelBinder;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace Cats.Areas.Logistics.Controllers
{
    public class UtilizationController : Controller
    {

        private readonly IUtilizationHeaderSerivce _utilizationService;
        private readonly IUtilizationDetailSerivce _utilizationDetailSerivce;
        private readonly IReliefRequisitionService _reliefRequisitionService;
        private readonly UserAccountService _userAccountService;
        private readonly IWorkflowStatusService _workflowStatusService;
        private readonly ICommonService _commonService;
        private readonly IRegionalRequestService _regionalRequestService;
        public UtilizationController(IUtilizationHeaderSerivce utilizationService, IUtilizationDetailSerivce utilizationDetailSerivce, UserAccountService userAccountService, IWorkflowStatusService workflowStatusService, ICommonService commonService, IRegionalRequestService regionalRequestService)
        {
            _utilizationService = utilizationService;
            _utilizationDetailSerivce = utilizationDetailSerivce;
            _userAccountService = userAccountService;
            _workflowStatusService = workflowStatusService;
            _commonService = commonService;
            _regionalRequestService = regionalRequestService;
        }

        //
        // GET: /Logistics/Utilization/

        public ActionResult Index()
        {
           
            ViewBag.RegionCollection = _commonService.GetAminUnits(t => t.AdminUnitTypeID == 2);
           
            ViewBag.ProgramID = new SelectList(_commonService.GetPrograms().Take(2),"ProgramId","Name");
            return View();
        }


        public ActionResult ReadRequestionNumbers([DataSourceRequest] DataSourceRequest request, int zoneId=-1, int programId = -1,int planId = -1)
        {
            if (zoneId == -1 || programId ==-1 || planId ==-1)
                return null;
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var requisition = _utilizationService.GetRequisitions(zoneId,programId,planId,5);
            var requisitionViewModel =UtilizationViewModelBinder.GetUtilizationViewModel(requisition);
            return Json(requisitionViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadRequisitionDetail([DataSourceRequest] DataSourceRequest request,   int requisitionId = -1 )
        {
            if (requisitionId == -1)
                return null;
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var requisition = _utilizationService.GetReliefRequisitions(requisitionId);
            var requisitionViewModel = UtilizationViewModelBinder.GetUtilizationDetailViewModel(requisition);
            return Json(requisitionViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, IEnumerable<Models.UtilizationDetailViewModel> utilizationDetailViewModels)
        {
            var results = new List<Models.UtilizationDetailViewModel>();
            return Json(results.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<Models.UtilizationDetailViewModel> utilizationDetailViewModels )
        {

            var userProfileId = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).UserProfileID;
            var results = new List<Models.UtilizationDetailViewModel>();

            var utilization = new Cats.Models.UtilizationHeader
                                  {DistributionDate = DateTime.Now, DistributedBy = userProfileId};


            foreach (var utilizationDetailViewModel in utilizationDetailViewModels)
            {
                var utilizationDetail = new Cats.Models.UtilizationDetail
                                            {
                                                DistributedQuantity = utilizationDetailViewModel.DistributedQuantity,
                                                FdpId = utilizationDetailViewModel.FdpId,
                                                UtilizationHeader = utilization
                                            };
                utilization.RequisitionId = utilizationDetailViewModel.RequisitionId;
                utilization.PlanId = utilizationDetailViewModel.PlanId;
                _utilizationDetailSerivce.AddDetailDistribution(utilizationDetail);
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }



        public JsonResult GetPlans(string id)
        {
            var programId = int.Parse(id);
            var plans = _commonService.GetPlan(programId);
            return Json(new SelectList(plans.ToList(), "PlanID", "PlanName"), JsonRequestBehavior.AllowGet);
        }
       
        public JsonResult GetMonth(string id)
        {
            try
            {
                var planid = int.Parse(id);
                var months = _regionalRequestService.FindBy(r => r.PlanID == planid).ToList();
                return Json(new SelectList(months, "Month", "Month"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return null;
            }
          
        }
        public JsonResult GetRound(string id)
        {
            try
            {
                var planid = int.Parse(id);
                var rounds = _regionalRequestService.FindBy(r => r.PlanID == planid).ToList();
                return Json(new SelectList(rounds, "Round", "Round"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return null;
            }
           
        }
    }
}
