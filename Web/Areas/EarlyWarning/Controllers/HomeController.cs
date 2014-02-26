using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.EarlyWarning;
using Cats.ViewModelBinder;

namespace Cats.Areas.EarlyWarning.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /EarlyWarning/
        private readonly IHRDService _hrdService;
        private readonly IHRDDetailService _hrdDetailService;
        private readonly IRationDetailService _rationDetailService;
        private readonly IRegionalRequestService _regionalRequestService;
        public HomeController(IHRDService hrdService,IHRDDetailService hrdDetailService,
                              IRationDetailService rationDetailService,IRegionalRequestService regionalRequestService)
        {
            _hrdService = hrdService;
            _hrdDetailService = hrdDetailService;
            _rationDetailService = rationDetailService;
            _regionalRequestService = regionalRequestService;

        }
        public ActionResult Index()
        {
            //ModelState.AddModelError("Success", "Sample Error Message. Use in Your Controller: ModelState.AddModelError('Errors', 'Your Error Message.')");
            var hrd = _hrdService.FindBy(m => m.Status == 4).FirstOrDefault();
            if (hrd==null)
            {
                return HttpNotFound();
            }
            var summary = GetHRDSummary(hrd.HRDID);
            return View(summary);
        }
        private DataTable GetHRDSummary(int id)
        {
            var hrd = _hrdService.FindById(id);
            var hrdDetails = _hrdDetailService.Get(t => t.HRDID == id, null, "AdminUnit,AdminUnit.AdminUnit2,AdminUnit.AdminUnit2.AdminUnit2").ToList();
            var rationDetails = _rationDetailService.Get(t => t.RationID == hrd.RationID, null, "Commodity");
            var dt = HRDViewModelBinder.TransposeDataSummary(hrdDetails, rationDetails);
            return dt;
        }
        //public JsonResult GetRation()
        //{
        //    var currentHrd = _hrdService.FindBy(m => m.Status == 4).FirstOrDefault();
        //    if (currentHrd != null)
        //    {
        //        var ration = _rationDetailService.FindBy(m => m.RationID == currentHrd.RationID);
        //    }

        //}
      public JsonResult GetRegionalRequests()
      {
          var currentHrd = _hrdService.FindBy(m => m.Status == 4).FirstOrDefault();
          
              var requests = _regionalRequestService.FindBy(m => m.PlanID == currentHrd.PlanID);

          return Json(requests, JsonRequestBehavior.AllowGet);
      }
    }
}
