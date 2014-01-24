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
        public UtilizationController(IUtilizationHeaderSerivce utilizationService, IUtilizationDetailSerivce utilizationDetailSerivce, UserAccountService userAccountService, IWorkflowStatusService workflowStatusService, ICommonService commonService)
        {
            _utilizationService = utilizationService;
            _utilizationDetailSerivce = utilizationDetailSerivce;
            _userAccountService = userAccountService;
            _workflowStatusService = workflowStatusService;
            _commonService = commonService;
        }

        //
        // GET: /Logistics/Utilization/

        public ActionResult Index()
        {
            ViewBag.RegionCollection = _commonService.GetAminUnits(t => t.AdminUnitTypeID == 2);
            ViewBag.RegionID = new SelectList(_commonService.GetAminUnits(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");
            ViewBag.ZoneID = new SelectList(_commonService.GetAminUnits(t => t.AdminUnitTypeID == 3), "AdminUnitID", "Name");
            ViewBag.WoredaID = new SelectList(_commonService.GetAminUnits(t => t.AdminUnitTypeID == 4), "AdminUnitID", "Name");
            ViewBag.ProgramID = new SelectList(_commonService.GetPrograms().Take(2),"ProgramId","Name");
            return View();
        }


        public ActionResult ReadRequestionNumbers([DataSourceRequest] DataSourceRequest request, int zoneId, int programId = -1)
        {
            if (zoneId == -1 || programId ==-1)
                return null;
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var requisition = _utilizationService.GetRequisitions(zoneId,programId,5);
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
        public ActionResult DistributionByAge_Read([DataSourceRequest] DataSourceRequest request, int requisitionId = -1)
        {
            if (requisitionId == -1)
                return null;
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var requisitionDetail = _utilizationService.GetReliefRequisitions(requisitionId);
            var distributionByAgeDetailViewModel =UtilizationViewModelBinder.GetDistributionByAgeDetail(requisitionDetail);
            return Json(distributionByAgeDetailViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, IEnumerable<Models.UtilizationDetailViewModel> utilizationDetailViewModels)
        {
            var results = new List<Models.UtilizationDetailViewModel>();
            return Json(results.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<Models.UtilizationDetailViewModel> utilizationDetailViewModels)
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
                _utilizationDetailSerivce.AddDetailDistribution(utilizationDetail);
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }
                
        public JsonResult GetCasscadeAdminUnits()
        {
            var cascadeAdminUnitAllRegions = (from region in _commonService.GetAminUnits(m => m.AdminUnitTypeID == 2)

                                              select new
                                              {
                                                  RegionID = region.AdminUnitID,
                                                  RegionName = region.Name,

                                                  zones = from zone in _commonService.GetAminUnits(z => z.ParentID == region.AdminUnitID)
                                                          select new
                                                          {
                                                              ZoneID = zone.AdminUnitID,
                                                              ZoneName = zone.Name,


                                                              Woredas = from woreda in _commonService.GetAminUnits(m => m.ParentID == zone.AdminUnitID)
                                                                        select new
                                                                        {
                                                                            WoredaID = woreda.AdminUnitID,
                                                                            WoredaName = woreda.Name,
                                                                            fdps = from fdp in _commonService.GetFDPs(woreda.AdminUnitID)
                                                                                   select new
                                                                                   {
                                                                                       FDPID = fdp.FDPID,
                                                                                       FDPName = fdp.Name
                                                                                   }
                                                                        }

                                                          }
                                              }


                 );
            return Json(cascadeAdminUnitAllRegions, JsonRequestBehavior.AllowGet);
        }
    }
}
