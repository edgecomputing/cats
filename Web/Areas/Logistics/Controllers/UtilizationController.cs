using System.Linq;
using System.Web.Mvc;
using Cats.Models.Constant;
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
        public UtilizationController(IUtilizationHeaderSerivce utilizationService, IUtilizationDetailSerivce utilizationDetailSerivce, UserAccountService userAccountService, IWorkflowStatusService workflowStatusService)
        {
            _utilizationService = utilizationService;
            _utilizationDetailSerivce = utilizationDetailSerivce;
            _userAccountService = userAccountService;
            _workflowStatusService = workflowStatusService;
        }

        //
        // GET: /Logistics/Utilization/

        public ActionResult Index()
        {
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
    }
}
