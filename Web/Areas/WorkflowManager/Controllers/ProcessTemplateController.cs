using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Cats.Models;
using Cats.Services.EarlyWarning;
namespace Cats.Areas.WorkflowManager.Controllers
{
    public class ProcessTemplateController : Controller
    {

        private readonly IProcessTemplateService _ProcessTemplateService;


        public ProcessTemplateController(IProcessTemplateService ProcessTemplateServiceParam
                                       )
        {
            this._ProcessTemplateService = ProcessTemplateServiceParam;

        }
        public IEnumerable<ProcessTemplatePOCO> toProcessTemplatePOCOList(IEnumerable<ProcessTemplate> list)
        {
            return (from item in list
                    select new ProcessTemplatePOCO()
                    {
                        ProcessTemplateID = item.ProcessTemplateID
                        ,
                        Name = item.Name
                        ,
                        Description = item.Description
                    }
                    );
        }
        public void loadLookups()
        {


        }
        public ActionResult Index()
        {
            loadLookups();
            IEnumerable<ProcessTemplate> list = _ProcessTemplateService.GetAll();
            return View(list);

        }
        public ActionResult Detail(int id=0)
        {
            ProcessTemplate item = _ProcessTemplateService.FindById(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);

        }

        public ActionResult ReadKendo([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<ProcessTemplate> list = _ProcessTemplateService.GetAll();
            return Json(toProcessTemplatePOCOList(list).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /Workflow/ProcessTemplate/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Workflow/ProcessTemplate/Create

        [HttpPost]
        public ActionResult Create(ProcessTemplate item)
        {
            if (ModelState.IsValid)
            {
                _ProcessTemplateService.Add(item);

                return RedirectToAction("Index");
            }

            return View(item);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateKendo([DataSourceRequest] DataSourceRequest request, ProcessTemplate item)
        {
            if (item != null && ModelState.IsValid)
            {
                _ProcessTemplateService.Add(item);
            }

            return Json(new[] { item }.ToDataSourceResult(request, ModelState));
        }


        //
        // GET: /Workflow/ProcessTemplate/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ProcessTemplate item = _ProcessTemplateService.FindById(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        public ActionResult Design(int id = 0)
        {
            ProcessTemplate item = _ProcessTemplateService.FindById(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        //
        // POST:  /Workflow/ProcessTemplate/Edit/5

        [HttpPost]
        public ActionResult Edit(ProcessTemplate item)
        {
            if (ModelState.IsValid)
            {
                _ProcessTemplateService.Update(item);
                return RedirectToAction("Index");
            }
            return View(item);
        }

        [HttpPost]
        public ActionResult EditKendo([DataSourceRequest] DataSourceRequest request, ProcessTemplate item)
        {
            if (ModelState.IsValid)
            {
                _ProcessTemplateService.Update(item);
                return Json(ModelState.ToDataSourceResult());
            }
            return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult Delete(int id = 0)
        {
            ProcessTemplate item = _ProcessTemplateService.FindById(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        /*
        * POST: //Workflow/ProcessTemplate/Delete/5
        */
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _ProcessTemplateService.DeleteById(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeleteKendo([DataSourceRequest] DataSourceRequest request, ProcessTemplate item)
        {
            if (item != null)
            {
                _ProcessTemplateService.DeleteById(item.ProcessTemplateID);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }
    }
}