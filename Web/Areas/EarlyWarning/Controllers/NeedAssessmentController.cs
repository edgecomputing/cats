using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Cats.Models;
using Cats.Services.EarlyWarning;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Cats.Models;
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

        public NeedAssessmentController(INeedAssessmentService needAssessmentService, 
                                        IAdminUnitService adminUnitService, 
                                        INeedAssessmentHeaderService needAssessmentHeaderService, 
                                        INeedAssessmentDetailService needAssessmentDetailService, 
                                        ISeasonService seasonService, ITypeOfNeedAssessmentService typeOfNeedAssessmentService)
        {
            _needAssessmentService = needAssessmentService;
            _adminUnitService = adminUnitService;
            _needAssessmentHeaderService = needAssessmentHeaderService;
            _needAssessmentDetailService = needAssessmentDetailService;
            _seasonService = seasonService;
            _typeOfNeedAssessmentService = typeOfNeedAssessmentService;
        }

        //
        // GET: /EarlyWarning/NeedAssessment/

        public ActionResult Index()
        {
            ViewData["zones"] = _adminUnitService.FindBy(t => t.AdminUnitTypeID == 3);
            ViewData["woredas"] = _adminUnitService.FindBy(t => t.AdminUnitTypeID == 4);
            return View();
        }

        public ActionResult _Index(int id)
        {
            ViewData["region"] = id;
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

             var region = collection["RegionID"].ToString(CultureInfo.InvariantCulture);
             int season = int.Parse(collection["SeasonID"].ToString(CultureInfo.InvariantCulture));
             int typeOfNeedID = int.Parse(collection["TypeOfNeedID"].ToString(CultureInfo.InvariantCulture));


            needAssessment.NeddACreatedBy = _needAssessmentHeaderService.GetUserProfileId(HttpContext.User.Identity.Name);
            needAssessment.NeedAApproved = false;
            needAssessment.NeedAApprovedBy = _needAssessmentHeaderService.GetUserProfileId(HttpContext.User.Identity.Name);
            needAssessment.Region = int.Parse(region.ToString(CultureInfo.InvariantCulture));
            needAssessment.Season = season;
            needAssessment.TypeOfNeedAssessment = typeOfNeedID;


         
            if (ModelState.IsValid)
            {
                _needAssessmentService.GenerateDefefaultData(needAssessment);
              
                    
            }
            int regionId = needAssessment.Region;
            return RedirectToAction("_Index", new {id = regionId});
        }

        public ActionResult AddZone()
        {

            
            

            ViewBag.Zones = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 3), "AdminUnitID","Name");

            var regionsInNeedAssessment = _needAssessmentService.GetRegionsFromNeedAssessment();
            var listOfRegions = _adminUnitService.FindBy(t => t.AdminUnitTypeID == 2);
            var filteredRegions = from region in listOfRegions
                        where regionsInNeedAssessment.Contains(region.Name)
                        select region;

            var seasonsInNeedAssessment = _seasonService.GetListOfSeasonsInRegion(regionsInNeedAssessment);
            ViewBag.Season = new SelectList(seasonsInNeedAssessment, "SeasonID",
                                               "Name");

            ViewBag.Regions = new SelectList(filteredRegions, "AdminUnitID","Name");
           

            return View();
        }

        [HttpPost]
        public ActionResult AddZone(NeedAssessmentHeader needAssessmentHeader,FormCollection collection)
        {
            var zone = int.Parse(collection["ZoneID"].ToString(CultureInfo.InvariantCulture));
            var region = collection["RegionID"].ToString(CultureInfo.InvariantCulture);
            int season = int.Parse(collection["SeasonID"].ToString(CultureInfo.InvariantCulture));

            needAssessmentHeader.Zone = zone;
            needAssessmentHeader.NeedAID = _needAssessmentHeaderService.GetRegionPrimeryId(int.Parse(region),season);


            if (ModelState.IsValid)
            {
                _needAssessmentHeaderService.AddNeedAssessmentHeader(needAssessmentHeader);
            }
            return RedirectToAction("Index");
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
            return Json(_needAssessmentService.ReturnNeedAssessmentDetailViewModel(region).ToDataSourceResult(request));
          

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
            return RedirectToAction("_Index", new { id = id });
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

            var regionsInNeedAssessment = _needAssessmentService.GetRegionsFromNeedAssessment();
            var listOfRegions = _adminUnitService.FindBy(t => t.AdminUnitTypeID == 2);
            var filteredRegions = from region in listOfRegions
                                  where regionsInNeedAssessment.Contains(region.Name)
                                  select region;

            var zonesInNeedAssessment = _needAssessmentService.GetZonesFromNeedAssessment();
            var listOfZones = _adminUnitService.FindBy(t => t.AdminUnitTypeID == 3);
            var filteredZones = from zone in listOfZones
                                 where zonesInNeedAssessment.Contains(zone.Name)
                                 select zone;

            var seasonsInNeedAssessment = _seasonService.GetListOfSeasonsInRegion(regionsInNeedAssessment);
            ViewBag.Season = new SelectList(seasonsInNeedAssessment, "SeasonID","Name");
            ViewBag.Regions = new SelectList(filteredRegions, "AdminUnitID", "Name");

            ViewBag.Zones = new SelectList(filteredZones, "AdminUnitID","Name");
            ViewBag.woredas = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 4), "AdminUnitID","Name");
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

              var woreda = collection["WoredaID"];
              var zone = int.Parse(collection["ZoneID"].ToString(CultureInfo.InvariantCulture));
              var region = int.Parse(collection["RegionID"].ToString(CultureInfo.InvariantCulture));

                if (ModelState.IsValid)
                {
                    
                     needDetail.Woreda = int.Parse(woreda.ToString(CultureInfo.InvariantCulture));
                     needDetail.NeedAId = _needAssessmentHeaderService.GetZonePrimeryId(zone, region);
                    
                    _needAssessmentDetailService.AddNeedAssessmentDetail(needDetail);
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
