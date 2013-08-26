using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

using Cats.Models;

using Cats.Services.EarlyWarning;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.EarlyWarning.Controllers
{
    public class NeedAssessmentController : Controller
    {
        private readonly INeedAssessmentService _needAssessmentService;
        private readonly IAdminUnitService _adminUnitService;
        private readonly INeedAssessmentHeaderService _needAssessmentHeaderService;
        private readonly INeedAssessmentDetailService _needAssessmentDetailService;
        public NeedAssessmentController(INeedAssessmentService needAssessmentService, 
                                        IAdminUnitService adminUnitService, 
                                        INeedAssessmentHeaderService needAssessmentHeaderService, 
                                        INeedAssessmentDetailService needAssessmentDetailService)
        {
            _needAssessmentService = needAssessmentService;
            _adminUnitService = adminUnitService;
            _needAssessmentHeaderService = needAssessmentHeaderService;
            _needAssessmentDetailService = needAssessmentDetailService;
        }

        //
        // GET: /EarlyWarning/NeedAssessment/

        public ActionResult Index()
        {
           
           
            ViewData["zones"] = _adminUnitService.FindBy(t => t.AdminUnitTypeID == 3);
            ViewData["woredas"] = _adminUnitService.FindBy(t => t.AdminUnitTypeID == 4);
            return View();
        }



        public ActionResult NeedAssessmentRead([DataSourceRequest] DataSourceRequest request )
        {
           return Json( _needAssessmentService.ReturnViewModel().ToDataSourceResult(request));

        }
        public ActionResult NeedAssessmentHeaderRead([DataSourceRequest] DataSourceRequest request, int region)
        {
            return Json(_needAssessmentService.ReturnNeedAssessmentHeaderViewModel(region).ToDataSourceResult(request));

        }
        public ActionResult NeedAssessmentDetailRead([DataSourceRequest] DataSourceRequest request, int region)//, string season)
        {
            return Json(_needAssessmentService.ReturnNeedAssessmentDetailViewModel(region).ToDataSourceResult(request));

        }
        //
        // GET: /EarlyWarning/NeedAssessment/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /EarlyWarning/NeedAssessment/Create

        public ActionResult Create()
        {
            ViewBag.Regions = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID",
                                               "Name");
            ViewBag.Zones = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 3), "AdminUnitID",
                                             "Name");
            ViewBag.woredas = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 4), "AdminUnitID",
                                             "Name");
            return View();
        }

        //
        // POST: /EarlyWarning/NeedAssessment/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection,NeedAssessmentDetail needDetail,string season)
        {
            try
            {
                // TODO: Add insert logic here

              NeedAssessment needAssessment = new NeedAssessment();
              NeedAssessmentHeader needAssessmentHeader=new NeedAssessmentHeader();

              var dateCreated = DateTime.Now  ;
              var region = collection["RegionID"].ToString(CultureInfo.InvariantCulture);
              var zone = collection["ZoneID"];
              var woreda = collection["WoredaID"];
              



                if (ModelState.IsValid)
                {
                    needAssessment.Region = int.Parse(region.ToString(CultureInfo.InvariantCulture));
                    needAssessment.NeedADate = dateCreated;
                    needAssessment.Season = needDetail.NeedAssessmentHeader.NeedAssessment.Season;
                    needAssessment.NeddACreatedBy =
                        _needAssessmentHeaderService.GetUserProfileId(HttpContext.User.Identity.Name);
                    needAssessment.TypeOfNeedAssessment = needDetail.NeedAssessmentHeader.NeedAssessment.TypeOfNeedAssessment;
                    needAssessment.NeedAApproved = false;
                    needAssessment.NeedAApprovedBy = _needAssessmentHeaderService.GetUserProfileId(HttpContext.User.Identity.Name);

                    needAssessmentHeader.Zone = int.Parse(zone.ToString(CultureInfo.InvariantCulture));
                    needDetail.Woreda = int.Parse(woreda.ToString(CultureInfo.InvariantCulture));


                    needDetail.NeedAssessmentHeader = needAssessmentHeader;
                    needDetail.NeedAssessmentHeader.NeedAssessment = needAssessment;
                    _needAssessmentService.AddNeedAssessment(needDetail);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /EarlyWarning/NeedAssessment/Edit/5

        public ActionResult Edit(int id)
        {
            
            var needAssessment = _needAssessmentService.FindBy(e => e.NeedAID == id).Single();
            ViewBag.Regions = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID",
                                              "Name",needAssessment.Region);
        
           
            return View(needAssessment);
        }
        public ActionResult EditDetail(int id)
        {
            return View();
        }
        //
        // POST: /EarlyWarning/NeedAssessment/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection,NeedAssessment needAssessment)
        {
            try
            {
                // TODO: Add update logic here

                NeedAssessment _needAssessment = _needAssessmentService.FindBy(e => e.NeedAID == id).Single();

                var region = collection["RegionID"];

                _needAssessment.Region = int.Parse(region.ToString(CultureInfo.InvariantCulture));
                _needAssessment.Season = needAssessment.Season;
                _needAssessment.TypeOfNeedAssessment = needAssessment.TypeOfNeedAssessment;
                _needAssessment.Remark = needAssessment.Remark;
                _needAssessment.NeedADate = needAssessment.NeedADate;

                _needAssessmentService.EditNeedAssessment(_needAssessment);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult NeedAssessmentUpdate([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<NeedAssessmentDetail> needAssessmentlDetails)
        {
           // IEnumerable<NeedAssessmentDetail> needAssessmentDetails = _needAssessmentService.GetDetail(needAssessmentViewModelDetails);
            if (needAssessmentlDetails != null && ModelState.IsValid)
            {
                foreach (var details in needAssessmentlDetails)
                {
                    _needAssessmentDetailService.EditNeedAssessmentDetail(details);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }


        //
        // GET: /EarlyWarning/NeedAssessment/Delete/5

        public ActionResult Delete(int id)
        {
            var needAssessment = _needAssessmentService.FindBy(e => e.NeedAID == id).Single();
            AdminUnit region = _adminUnitService.FindBy(t => t.AdminUnitTypeID == 2 && t.AdminUnitID == needAssessment.Region).Single();
            string createdBy = null;
            if (needAssessment.NeddACreatedBy != null)
            {
               
                createdBy = _needAssessmentHeaderService.GetUserProfileName((int) needAssessment.NeddACreatedBy);
            }

            ViewData["regionToDelete"] = region.Name;
            ViewData["createdBy"] = createdBy;


            return View(needAssessment);
        }

        //
        // POST: /EarlyWarning/NeedAssessment/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var needAssessment = _needAssessmentService.FindBy(e => e.NeedAID == id).Single();
                _needAssessmentService.DeleteNeedAssessment(needAssessment);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteDetail([DataSourceRequest] DataSourceRequest request, NeedAssessmentViewModel needAssessmentViewModel)
        {
            try
            {
                // TODO: Add delete logic here
                var needAssessment = _needAssessmentService.FindBy(e => e.NeedAID == needAssessmentViewModel.NAId).Single();
                _needAssessmentService.DeleteNeedAssessment(needAssessment);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
