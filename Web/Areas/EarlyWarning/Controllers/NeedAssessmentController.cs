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

        //service injection
        public NeedAssessmentController(INeedAssessmentService needAssessment)
        {
            _needAssessment = needAssessment;
        }


        //
        // GET: /EarlyWarning/NeedAssessment/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var needAssessment = _needAssessment.GetAllNeedAssessment();
            return Json(needAssessment.ToDataSourceResult(request));
        }
        //
      

        //
        // POST: /EarlyWarning/NeedAssessment/Create

        public ActionResult Create([DataSourceRequest] DataSourceRequest request, NeedAssessment needAssessment)
        {
            if (ModelState.IsValid && needAssessment != null)
            {
                _needAssessment.AddNeedAssessment(needAssessment);
                
            }
           return Json(new[] { needAssessment }.ToDataSourceResult(request, ModelState));
        }

      
        // POST: /EarlyWarning/NeedAssessment/Edit/5

        public ActionResult Edit([DataSourceRequest] DataSourceRequest request, NeedAssessment needAssessment)
        {
            if (ModelState.IsValid && needAssessment != null)
            {
                _needAssessment.EditNeedAssessment(needAssessment);
               
            }
            return Json(ModelState.ToDataSourceResult());
        }

        
      

       

        //
        // POST: /EarlyWarning/NeedAssessment/Delete/5

        public ActionResult Delete([DataSourceRequest]  DataSourceRequest request, NeedAssessment needAssessment)
        {
            if (ModelState.IsValid && needAssessment != null)
            {
                _needAssessment.DeleteNeedAssessment(needAssessment);
               
            }
            return Json(ModelState.ToDataSourceResult());
        }

      
    }
}
