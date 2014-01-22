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
            ViewBag.RegionID = new SelectList(_commonService.GetAminUnits(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");
            ViewBag.ZoneID = new SelectList(_commonService.GetAminUnits(t => t.AdminUnitTypeID == 3), "AdminUnitID", "Name");
            ViewBag.WoredaID = new SelectList(_commonService.GetAminUnits(t => t.AdminUnitTypeID == 4), "AdminUnitID", "Name");
            return View();
        }


        public  ActionResult ReadRequestionNumbers([DataSourceRequest] DataSourceRequest request,  int regionId = -1, int programId =-1,int year =-1)
        {
            if (regionId == -1 || programId == -1 || year == -1)
                return null;
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var requisition = _utilizationService.GetRequisitions(regionId,5);
            var requisitionViewModel =UtilizationViewModelBinder.GetUtilizationViewModel(requisition);
            return Json(requisitionViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadRequisitionDetail([DataSourceRequest] DataSourceRequest request,   int requisitionId = -1 )
        {
            if (requisitionId == -1)
                return null;
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var requisition = _utilizationService.GetReliefRequisitions(requisitionId);
            var requisitionViewModel = UtilizationViewModelBinder.GetUtilizationDetailViewModel(requisition.ReliefRequisitionDetails.ToList());
            return Json(requisitionViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

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
