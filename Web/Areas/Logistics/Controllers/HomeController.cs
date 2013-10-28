using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.EarlyWarning;

namespace Cats.Areas.Logistics.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Logistics/Home/
        
        private readonly IReliefRequisitionService _reliefRequisitionService;
       
        public HomeController(IReliefRequisitionService reliefRequisitionService)
        {
            this._reliefRequisitionService = reliefRequisitionService;
        }


        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetRecievedRequisitions()
        {
            return Json(_reliefRequisitionService.GetRequisitionsSentToLogistics(), JsonRequestBehavior.AllowGet);
        }
    }
}