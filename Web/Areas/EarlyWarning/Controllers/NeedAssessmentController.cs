using System.Globalization;
using Cats.Models;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Cats.Services.EarlyWarning;

namespace Cats.Areas.EarlyWarning.Controllers
{
    public class NeedAssessmentController : Controller
    {
        //service declarations
        private readonly INeedAssessmentService _needAssessment;
        private readonly IAdminUnitService _adminUnitService;
        //service injection
        public NeedAssessmentController(INeedAssessmentService needAssessment, IAdminUnitService adminUnitService)
        {
            this._needAssessment = needAssessment;
            this._adminUnitService = adminUnitService;
        }


        //
        // GET: /EarlyWarning/NeedAssessment/

        public ActionResult Index()
        {
            var zone = _adminUnitService.FindBy(t => t.AdminUnitTypeID == 3);
            var woreda = _adminUnitService.FindBy(t => t.AdminUnitTypeID == 4);
            ViewData["zone"] = zone;
            ViewData["woreda"] = woreda;

            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var needAssessment = _needAssessment.GetAllNeedAssessment();
            return Json(needAssessment.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
           
        }

        //


        //
        // POST: /EarlyWarning/NeedAssessment/Create

        public ActionResult Create([DataSourceRequest] DataSourceRequest request, NeedAssement needAssessment)
        {
            if (ModelState.IsValid && needAssessment != null)
            {
                _needAssessment.AddNeedAssessment(needAssessment);

            }
            return Json(new[] {needAssessment}.ToDataSourceResult(request, ModelState));
        }


        // POST: /EarlyWarning/NeedAssessment/Edit/5
         [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit([DataSourceRequest] DataSourceRequest request, NeedAssement needAssessment)
        {
            if (ModelState.IsValid && needAssessment != null)
            {
                _needAssessment.EditNeedAssessment(needAssessment);

            }
            return Json(ModelState.ToDataSourceResult());
        }






        //
        // POST: /EarlyWarning/NeedAssessment/Delete/5

        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, NeedAssement needAssessment)
        {
            if (ModelState.IsValid && needAssessment != null)
            {
                _needAssessment.DeleteNeedAssessment(needAssessment);

            }
            return Json(ModelState.ToDataSourceResult());
        }


        public ActionResult NewNeedAssessment()
        {
            
            return View();
        }


        public ActionResult Save(NeedAssement needAssessment,FormCollection form)
        {
            string zone = form["Zone"].ToString(CultureInfo.InvariantCulture); //retrives Zone id from the view
            string woreda = form["Woreda"].ToString(CultureInfo.InvariantCulture); //retrives Woreda id from the view
            zone = zone.Substring(1);
            woreda = woreda.Substring(1);

            if (needAssessment != null && ModelState.IsValid)
            {

                needAssessment.Zone = int.Parse(zone);
                needAssessment.District = int.Parse(woreda);
                _needAssessment.AddNeedAssessment(needAssessment);

            }
            return null;
        }
    }
}
