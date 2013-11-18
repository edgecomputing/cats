using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.EarlyWarning;
using Cats.Services.Security;
using hub = Cats.Services.Hub;
namespace Cats.Areas.Logistics.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Logistics/Home/
        private readonly hub.IDispatchAllocationService _dispatchAllocationService;
        private readonly IReliefRequisitionService _reliefRequisitionService;
        private readonly IUserAccountService _userAccountService;
       
        public HomeController(IReliefRequisitionService reliefRequisitionService,hub.IDispatchAllocationService dispatchAllocationService,IUserAccountService userAccountService)
        {
            this._reliefRequisitionService = reliefRequisitionService;
            _dispatchAllocationService = dispatchAllocationService;
            _userAccountService = userAccountService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetRecievedRequisitions()
        {
            return Json(_reliefRequisitionService.GetRequisitionsSentToLogistics(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDispatchAllocationByHub(int hubId)
        {
            var user = _userAccountService.GetUserInfo(User.Identity.Name);
           
            var fdpAllocations = _dispatchAllocationService.GetCommitedAllocationsByHubDetached(hubId, user.PreferedWeightMeasurment);

            return Json(fdpAllocations, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllocationSummary()
        {
            return new JsonResult();
        }
    }
}