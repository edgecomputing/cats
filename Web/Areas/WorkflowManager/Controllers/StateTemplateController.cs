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
    public class StateTemplateController : Controller
    {

        private readonly IStateTemplateService _StateTemplateService;

        private readonly IProcessTemplateService _ProcessTemplateService;


        public StateTemplateController(IStateTemplateService StateTemplateServiceParam
                                       , IProcessTemplateService ProcessTemplateServiceParam
                                       )
        {
            this._StateTemplateService = StateTemplateServiceParam;

            this._ProcessTemplateService = ProcessTemplateServiceParam;

        }
        public IEnumerable<StateTemplatePOCO> toStateTemplatePOCOList(IEnumerable<StateTemplate> list)
        {
            return (from item in list
                    select new StateTemplatePOCO()
                    {
                        StateTemplateID = item.StateTemplateID
                        ,
                        ParentProcessTemplateID = item.ParentProcessTemplateID
                        ,
                        Name = item.Name
                        ,AllowedAccessLevel = item.AllowedAccessLevel
                        ,StateType = item.StateType  
                    }
                    );
        }
        public void loadLookups()
        {

            ViewData["ProcessTemplateList"] = _ProcessTemplateService.GetAll();
            var StateTypes = new List<LookupData>  { 
                                                            new LookupData{ ID = 0, Name = "Start" }
                                                             ,new LookupData{ ID = 1, Name = "Intermediat" } 
                                                             ,new LookupData{ ID = 2, Name = "End" }
                                };
            ViewData["StateTypeList"] = StateTypes;
            var AccessLevels = new List<LookupData>  { 
                                                            new LookupData{ ID = 0, Name = "None" }
                                                             ,new LookupData{ ID = 1, Name = "View" } 
                                                             ,new LookupData{ ID = 2, Name = "Edit" }
                                                              ,new LookupData{ ID = 3, Name = "Delete" }
                                };
            ViewData["AccessLevelList"] = AccessLevels;
        }
        public ActionResult Index()
        {
            loadLookups();
            IEnumerable<StateTemplate> list = _StateTemplateService.GetAll();
            return View(list);

        }
        public ActionResult ReadKendo([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<StateTemplate> list = _StateTemplateService.GetAll();
            return Json(toStateTemplatePOCOList(list).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /Workflow/StateTemplate/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Workflow/StateTemplate/Create

        [HttpPost]
        public ActionResult Create(StateTemplate item)
        {
            if (ModelState.IsValid)
            {
                _StateTemplateService.Add(item);

                return RedirectToAction("Index");
            }

            return View(item);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateKendo([DataSourceRequest] DataSourceRequest request, StateTemplate item)
        {
            if (item != null && ModelState.IsValid)
            {
                _StateTemplateService.Add(item);
            }

            return Json(new[] { item }.ToDataSourceResult(request, ModelState));
        }


        //
        // GET: /Workflow/StateTemplate/Edit/5

        public ActionResult Edit(int id = 0)
        {
            StateTemplate item = _StateTemplateService.FindById(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }


        //
        // POST:  /Workflow/StateTemplate/Edit/5

        [HttpPost]
        public ActionResult Edit(StateTemplate item)
        {
            if (ModelState.IsValid)
            {
                _StateTemplateService.Update(item);
                return RedirectToAction("Index");
            }
            return View(item);
        }

        [HttpPost]
        public ActionResult EditKendo([DataSourceRequest] DataSourceRequest request, StateTemplate item)
        {
            if (ModelState.IsValid)
            {
                _StateTemplateService.Update(item);
                return Json(ModelState.ToDataSourceResult());
            }
            return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult Delete(int id = 0)
        {
            StateTemplate item = _StateTemplateService.FindById(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        /*
        * POST: //Workflow/StateTemplate/Delete/5
        */
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _StateTemplateService.DeleteById(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeleteKendo([DataSourceRequest] DataSourceRequest request, StateTemplate item)
        {
            if (item != null)
            {
                _StateTemplateService.DeleteById(item.StateTemplateID);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }
    }
}