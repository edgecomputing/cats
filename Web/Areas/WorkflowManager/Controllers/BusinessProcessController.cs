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
    public class BusinessProcessController : Controller
    {

        private readonly IBusinessProcessService _BusinessProcessService;

        private readonly IProcessTemplateService _ProcessTemplateService;

        private readonly IStateTemplateService _StateTemplateService;
        private readonly IBusinessProcessStateService _BusinessProcessStateService;

        public BusinessProcessController(IBusinessProcessService BusinessProcessServiceParam
                                       , IProcessTemplateService ProcessTemplateServiceParam
                                       , IStateTemplateService StateTemplateServiceParam
                                        , IBusinessProcessStateService BusinessProcessStateServiceParam
                                       )
        {
            this._BusinessProcessService = BusinessProcessServiceParam;

            this._ProcessTemplateService = ProcessTemplateServiceParam;

            this._StateTemplateService = StateTemplateServiceParam;
            this._BusinessProcessStateService = BusinessProcessStateServiceParam;

        }
        public IEnumerable<BusinessProcessPOCO> toBusinessProcessPOCOList(IEnumerable<BusinessProcess> list)
        {
            return (from item in list
                    select new BusinessProcessPOCO()
                    {
                        BusinessProcessID = item.BusinessProcessID
                        ,
                        ProcessTypeID = item.ProcessTypeID
                        ,
                        DocumentID = item.DocumentID
                        ,
                        DocumentType = item.DocumentType
                        ,
                        CurrentStateID = item.CurrentStateID
                    }
                    );
        }
        public void loadLookups()
        {

            ViewData["ProcessTemplateList"] = _ProcessTemplateService.GetAll();

            ViewData["StateTemplateList"] = _StateTemplateService.GetAll();


        }
        public ActionResult Index()
        {
            loadLookups();
            IEnumerable<BusinessProcess> list = _BusinessProcessService.GetAll();
            return View(list);

        }
        public ActionResult Promote(BusinessProcessState st)
        {
            loadLookups();
            BusinessProcess item = _BusinessProcessService.FindById(st.ParentBusinessProcessID);
            item.ProcessTypeID = st.StateID;
            _BusinessProcessService.Update(item);
            _BusinessProcessStateService.Add(st);
            return View();

        }

        public ActionResult ReadKendo([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<BusinessProcess> list = _BusinessProcessService.GetAll();
            return Json(toBusinessProcessPOCOList(list).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /Cats/BusinessProcess/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Cats/BusinessProcess/Create

        [HttpPost]
        public ActionResult Create(BusinessProcess item)
        {
            if (ModelState.IsValid)
            {
                _BusinessProcessService.Add(item);

                return RedirectToAction("Index");
            }

            return View(item);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateKendo([DataSourceRequest] DataSourceRequest request, BusinessProcess item)
        {
            if (item != null && ModelState.IsValid)
            {
                _BusinessProcessService.Add(item);
            }

            return Json(new[] { item }.ToDataSourceResult(request, ModelState));
        }


        //
        // GET: /Cats/BusinessProcess/Edit/5

        public ActionResult Edit(int id = 0)
        {
            BusinessProcess item = _BusinessProcessService.FindById(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }


        //
        // POST:  /Cats/BusinessProcess/Edit/5

        [HttpPost]
        public ActionResult Edit(BusinessProcessState st)
        {
            _BusinessProcessService.PromotWorkflow(st);
            BusinessProcess item = _BusinessProcessService.FindById(st.ParentBusinessProcessID);
            /*if (ModelState.IsValid)
            {

                item.CurrentStateID = st.StateID;
                _BusinessProcessService.Update(item);
                _BusinessProcessStateService.Add(st);
                return View(item);
              //  _BusinessProcessService.Update(item);
             //   return RedirectToAction("Index");
            }*/
            return View(item);
        }

        [HttpPost]
        public ActionResult EditKendo([DataSourceRequest] DataSourceRequest request, BusinessProcess item)
        {
            if (ModelState.IsValid)
            {
                _BusinessProcessService.Update(item);
                return Json(ModelState.ToDataSourceResult());
            }
            return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult Delete(int id = 0)
        {
            BusinessProcess item = _BusinessProcessService.FindById(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        /*
        * POST: //Cats/BusinessProcess/Delete/5
        */
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _BusinessProcessService.DeleteById(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeleteKendo([DataSourceRequest] DataSourceRequest request, BusinessProcess item)
        {
            if (item != null)
            {
                _BusinessProcessService.DeleteById(item.BusinessProcessID);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }
    }
}