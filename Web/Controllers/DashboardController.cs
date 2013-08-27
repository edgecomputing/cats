using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Services.EarlyWarning;
using System.Web.Mvc;
using Cats.Models.ViewModels;


namespace Cats.Controllers
{
    public class DashboardController : Controller
    {
        public DashboardController()
        {
            this._IDashboardService = new Cats.Services.EarlyWarning.DashboardService();
        }
        private readonly IDashboardService _IDashboardService;

        public ActionResult Requests(int RegionId)
        {
            return Json(_IDashboardService.RegionalRequests(RegionId), JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult RegionalRequestsById(int RegionId)
        {
            return Json(_IDashboardService.RegionalRequestsByRegionID(RegionId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult PieRequests()
        {
            
           // var model = _IDashboardService.PieRegionalRequests();
           // return PartialView(model);
            return Json(_IDashboardService.PieRegionalRequests(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BarBeneficiaries()
        {

            return Json(_IDashboardService.BarNoOfBeneficiaries(), JsonRequestBehavior.AllowGet);
        }
    }
}