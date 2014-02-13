using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Cats.Models;
using Cats.Services.EarlyWarning;
namespace Cats.Areas.WorkflowManager.Controllers
{
    public class FlowTemplateController : Controller
    {

        private readonly IFlowTemplateService _FlowTemplateService;

        private readonly IStateTemplateService _StateTemplateService;

        


        public FlowTemplateController(IFlowTemplateService FlowTemplateServiceParam
                                       , IStateTemplateService StateTemplateServiceParam
                                       
                                       )
        {
            this._FlowTemplateService = FlowTemplateServiceParam;

            this._StateTemplateService = StateTemplateServiceParam;

            this._StateTemplateService = StateTemplateServiceParam;

        }
        public IEnumerable<FlowTemplatePOCO> toFlowTemplatePOCOList(IEnumerable<FlowTemplate> list)
        {
            return (from item in list
                    select new FlowTemplatePOCO()
                    {
                        FlowTemplateID = item.FlowTemplateID
                        ,
                        InitialStateID = item.InitialStateID
                        ,
                        FinalStateID = item.FinalStateID
                        ,
                        Name = item.Name
                    }
                    );
        }
        public void loadLookups()
        {

            

            ViewData["StateTemplateList"] = _StateTemplateService.GetAll();


        }
        public ActionResult Index()
        {
            loadLookups();
            IEnumerable<FlowTemplate> list = _FlowTemplateService.GetAll();
            return View(list);

        }
        public ActionResult ReadKendo([DataSourceRequest] DataSourceRequest request, int ProcessTemplateID=0)
        {
            IEnumerable<FlowTemplate> list;
            if(ProcessTemplateID==0)
            {
              list  = _FlowTemplateService.GetAll();
            }
            else
            {
                list = _FlowTemplateService.FindBy(t=>t.InitialState.ParentProcessTemplateID==ProcessTemplateID);
            }

                return Json(toFlowTemplatePOCOList(list).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /Workflow/FlowTemplate/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Workflow/FlowTemplate/Create

        [HttpPost]
        public ActionResult Create(FlowTemplate item)
        {
            if (ModelState.IsValid)
            {
                _FlowTemplateService.Add(item);

                return RedirectToAction("Index");
            }

            return View(item);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateKendo([DataSourceRequest] DataSourceRequest request, FlowTemplate item)
        {
            if (item != null && ModelState.IsValid)
            {
                _FlowTemplateService.Add(item);
            }

            return Json(new[] { item }.ToDataSourceResult(request, ModelState));
        }


        //
        // GET: /Workflow/FlowTemplate/Edit/5

        public ActionResult Edit(int id = 0)
        {
            FlowTemplate item = _FlowTemplateService.FindById(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }


        //
        // POST:  /Workflow/FlowTemplate/Edit/5

        [HttpPost]
        public ActionResult Edit(FlowTemplate item)
        {
            if (ModelState.IsValid)
            {
                _FlowTemplateService.Update(item);
                return RedirectToAction("Index");
            }
            return View(item);
        }

        [HttpPost]
        public ActionResult EditKendo([DataSourceRequest] DataSourceRequest request, FlowTemplate item)
        {
            if (ModelState.IsValid)
            {
                _FlowTemplateService.Update(item);
                return Json(ModelState.ToDataSourceResult());
            }
            return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult Delete(int id = 0)
        {
            FlowTemplate item = _FlowTemplateService.FindById(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        /*
        * POST: //Workflow/FlowTemplate/Delete/5
        */
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _FlowTemplateService.DeleteById(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeleteKendo([DataSourceRequest] DataSourceRequest request, FlowTemplate item)
        {
            if (item != null)
            {
                _FlowTemplateService.DeleteById(item.FlowTemplateID);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }
    }
}