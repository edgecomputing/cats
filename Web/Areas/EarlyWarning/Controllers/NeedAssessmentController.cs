﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Cats.Models;
using Cats.Services.EarlyWarning;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using log4net;
using log4net.Config;
using Cats.Services.Common;

namespace Cats.Areas.EarlyWarning.Controllers
{
    public class NeedAssessmentController : Controller
    {
        private readonly INeedAssessmentService _needAssessmentService;
        private readonly IAdminUnitService _adminUnitService;
        private readonly INeedAssessmentHeaderService _needAssessmentHeaderService;
        private readonly INeedAssessmentDetailService _needAssessmentDetailService;
        private readonly ISeasonService _seasonService;
        private readonly ITypeOfNeedAssessmentService _typeOfNeedAssessmentService;
        public ILog _log;

     

        public NeedAssessmentController(INeedAssessmentService needAssessmentService, 
                                        IAdminUnitService adminUnitService, 
                                        INeedAssessmentHeaderService needAssessmentHeaderService, 
                                        INeedAssessmentDetailService needAssessmentDetailService, 
                                        ISeasonService seasonService, ITypeOfNeedAssessmentService typeOfNeedAssessmentService,ILog log)
        {
            _needAssessmentService = needAssessmentService;
            _adminUnitService = adminUnitService;
            _needAssessmentHeaderService = needAssessmentHeaderService;
            _needAssessmentDetailService = needAssessmentDetailService;
            _seasonService = seasonService;
            _typeOfNeedAssessmentService = typeOfNeedAssessmentService;
            _log = log;
        }

        //
        // GET: /EarlyWarning/NeedAssessment/

        public ActionResult Index()
        {
         
            ViewData["zones"] = _adminUnitService.FindBy(t => t.AdminUnitTypeID == 3);
            ViewData["woredas"] = _adminUnitService.FindBy(t => t.AdminUnitTypeID == 4);
            return View();
        }

        public ActionResult Edit(int id, int typeOfNeed)
        {
            ViewData["TypeOfNeedAssessment"] =
                _typeOfNeedAssessmentService.FindBy(t => t.TypeOfNeedAssessmentID == typeOfNeed).Select(
                    a => a.TypeOfNeedAssessment1).SingleOrDefault();

            ViewData["region"] = id;
            ViewData["RegionName"] = _adminUnitService.FindBy(r => r.AdminUnitID == id).Select(n=>n.Name).Single();
            
            ViewBag.Zones = _adminUnitService.GetZones(id).ToList();

            return View();
        }
        public ActionResult Approved()
        {
            return View();
        }
        public ActionResult GetRegions()
        {
          IOrderedEnumerable<RegionsViewModel> regions = _needAssessmentService.GetRegions();
            return Json(regions,JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetZones(int region)
        {
           
            var zones = _needAssessmentService.GetZoness(region);
            return Json(zones, JsonRequestBehavior.AllowGet);

        }
        public ActionResult AddRegion()
        {
            ViewBag.Regions = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID","Name");
            ViewBag.Season = new SelectList(_seasonService.GetAllSeason(), "SeasonID","Name");
            ViewBag.TypeOfNeed = new SelectList(_typeOfNeedAssessmentService.GetAllTypeOfNeedAssessment(), "TypeOfNeedAssessmentID","TypeOfNeedAssessment1");
            return View();
        }

       
        [HttpPost]
        public ActionResult AddRegion(NeedAssessment needAssessment,FormCollection collection)
        {
            try
            {


             ViewBag.Error = "";
             var region = collection["RegionID"].ToString(CultureInfo.InvariantCulture);
             int season = int.Parse(collection["SeasonID"].ToString(CultureInfo.InvariantCulture));
             int typeOfNeedID = int.Parse(collection["TypeOfNeedID"].ToString(CultureInfo.InvariantCulture));


            needAssessment.NeddACreatedBy = _needAssessmentHeaderService.GetUserProfileId(HttpContext.User.Identity.Name);
            needAssessment.NeedAApproved = false;
            needAssessment.NeedAApprovedBy = _needAssessmentHeaderService.GetUserProfileId(HttpContext.User.Identity.Name);
            needAssessment.Region = int.Parse(region.ToString(CultureInfo.InvariantCulture));
            needAssessment.Season = season;
            needAssessment.Year = needAssessment.NeedADate.Value.Year;
            needAssessment.TypeOfNeedAssessment = typeOfNeedID;


         
            if (ModelState.IsValid)
            {
                _needAssessmentService.GenerateDefefaultData(needAssessment);
              
                    
            }
            int regionId = needAssessment.Region;
            int typeOfNeedAsseessment = (int) needAssessment.TypeOfNeedAssessment;

            return RedirectToAction("Edit", new { id = regionId, typeOfNeed = typeOfNeedAsseessment });
            }

            catch (Exception exception)
            {
                ViewBag.Regions = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");
                ViewBag.Season = new SelectList(_seasonService.GetAllSeason(), "SeasonID", "Name");
                ViewBag.TypeOfNeed = new SelectList(_typeOfNeedAssessmentService.GetAllTypeOfNeedAssessment(), "TypeOfNeedAssessmentID", "TypeOfNeedAssessment1");
                ViewBag.Error = "An error has occured: This region has already been registered with the information you are trying to input. Please choose a different Region, Seasnon, Year or Type of Need Assessment.";
                return View();
            }
        }

      
       

        public ActionResult NeedAssessmentRead([DataSourceRequest] DataSourceRequest request )
        {
           return Json( _needAssessmentService.ReturnViewModel().ToDataSourceResult(request));

        }
        public ActionResult NeedAssessmentHeaderRead([DataSourceRequest] DataSourceRequest request)
        {
            
            return Json(_needAssessmentService.GetListOfZones().ToDataSourceResult(request));

        }
        public ActionResult NeedAssessmentDetailRead([DataSourceRequest] DataSourceRequest request, int region)//, string season)
        {
            return Json(_needAssessmentService.ReturnNeedAssessmentDetailViewModel(region).ToDataSourceResult(request),JsonRequestBehavior.AllowGet);
          

        }

        public ActionResult NeedAssessmentReadApproved([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_needAssessmentService.ReturnViewModelApproved().ToDataSourceResult(request));

        }

        public ActionResult DisapproveNeedAssessment(int id)
        {
            var needAssessment = _needAssessmentService.FindById(id);
            needAssessment.NeedAApproved = false;
            _needAssessmentService.EditNeedAssessment(needAssessment);
            return RedirectToAction("Index");
        }

        public ActionResult ApproveNeedAssessment(int id)
        {
            var needAssessment = _needAssessmentService.FindById(id);
            needAssessment.NeedAApproved = true;
            _needAssessmentService.EditNeedAssessment(needAssessment);
            return RedirectToAction("Index");
        }
        public ActionResult EditNeedAssessment(int id)
        {
            try
            {
                var needAssessment = _needAssessmentService.FindBy(r => r.Region == id).Single();
                int typeOfNeedAsseessment = (int)needAssessment.TypeOfNeedAssessment;
                return RedirectToAction("Edit", new { id = id, typeOfNeed = typeOfNeedAsseessment });
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
            
           
        }

        public ActionResult DeleteNeedAssessment(int id)
        {
            try
            {
               
             _needAssessmentService.DeleteById(id);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }


        }

       

       

       

       
       


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult NeedAssessmentUpdate([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<NeedAssessmentDetail> needAssessmentlDetails)
        {
         
            if (needAssessmentlDetails != null && ModelState.IsValid)
            {
                foreach (var details in needAssessmentlDetails)
                {
                    _needAssessmentDetailService.EditNeedAssessmentDetail(details);
                }
            }
           
            return Json(ModelState.ToDataSourceResult());
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
