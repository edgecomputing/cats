using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models.Hub;
using Cats.Models.Hub.ViewModels;
using Cats.Services.Hub;
using Cats.Web.Hub.ViewModelBinder;

namespace Cats.Web.Hub.Controllers
{
    [Authorize]
    public partial class FDPController : BaseController
    {

        private readonly IFDPService _FDPService;
        private readonly IAdminUnitService _adminUnitService;
        public FDPController(IFDPService FDPServiceParam, 
            IAdminUnitService adminUnitService, 
            IUserProfileService userProfileService)
            : base(userProfileService)
        {
            this._FDPService = FDPServiceParam;
            _adminUnitService = adminUnitService;
        }

        //
        // GET: /FDP/

        public virtual ViewResult Index()
        
        {
            ViewBag.Regions = _adminUnitService.GetRegions();
            var fdps = _FDPService.GetAllFDP();
            var fdpsViewModel = FDPViewModelBinder.FDPListViewModelBinder(fdps);
            return View(fdpsViewModel);
        }

        public ActionResult GetFDPGrid(int regionId=0, int zoneId=0, int woredaId=0)
        {
            if(regionId>0)
            {
                var fdps = _FDPService.GetFDPsByRegion(regionId).OrderBy(o => o.Name);
                var fdpsViewModel = FDPViewModelBinder.FDPListViewModelBinder(fdps.ToList());
                return Json(fdpsViewModel, JsonRequestBehavior.AllowGet);
            }
            else if (zoneId>0)
            {
                var fdps = _FDPService.GetFDPsByZone(zoneId).OrderBy(o => o.Name);
                var fdpsViewModel = FDPViewModelBinder.FDPListViewModelBinder(fdps.ToList());
                return Json(fdpsViewModel, JsonRequestBehavior.AllowGet);
            }
            else if(woredaId>0)
            {
                var fdps =_FDPService.GetFDPsByWoreda(woredaId).OrderBy(o => o.Name);
                var fdpsViewModel = FDPViewModelBinder.FDPListViewModelBinder(fdps.ToList());
                return Json(fdpsViewModel, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return EmptyResult;
            }
        }

        protected ActionResult EmptyResult
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        //
        // GET: /FDP/Details/5

        public virtual ViewResult Details(int id)
        {
            FDP fdp = _FDPService.FindById(id);
            return View(fdp);
        }

        //
        // GET: /FDP/Create

        public virtual ActionResult Create(int? woredaId)
        {
            var adminUnit = _adminUnitService.Get(t=>t.AdminUnitID == woredaId, null, "AdminUnit2,AdminUnit2.AdminUnit2").FirstOrDefault();
            var adminUnitModel = new AdminUnitModel();
            if (adminUnit != null)
            {
                if (adminUnit.AdminUnit2.ParentID != null)
                    adminUnitModel = new AdminUnitModel
                        {
                            SelectedWoredaId = adminUnit.AdminUnitID,
                            SelectedZoneId = adminUnit.AdminUnit2.AdminUnitID,
                            SelectedRegionId = (int) adminUnit.AdminUnit2.ParentID,
                            SelectedWoredaName = adminUnit.Name,
                            SelectedZoneName = adminUnit.AdminUnit2.Name,
                            SelectedRegionName = adminUnit.AdminUnit2.AdminUnit2.Name
                        };
            }
            return PartialView(adminUnitModel);
        }

        //
        // POST: /FDP/Create

        [HttpPost]
        public virtual ActionResult Create(AdminUnitModel fdps)
        {
            var fdp = new FDP
                {
                    Name = fdps.UnitName,
                    NameAM = fdps.UnitNameAM,
                    AdminUnitID = fdps.SelectedWoredaId
                };
            if (ModelState.IsValid)
            {
                _FDPService.AddFDP(fdp);
                return Json(new { success = true });
            }
            return PartialView(fdps);
        }

        //
        // GET: /FDP/Edit/5

        public virtual ActionResult Edit(int id)
        {
            FDP fdp = _FDPService.FindById(id);
            var fdps = _FDPService.GetAllFDP();
            ViewBag.FDPID = new SelectList(fdps, "FDPID", "Name", fdp.FDPID);
            return PartialView(fdp);
        }

        //
        // POST: /FDP/Edit/5

        [HttpPost]
        public virtual ActionResult Edit(FDP fdp)
        {
            if (ModelState.IsValid)
            {

                _FDPService.EditFDP(fdp);


                //return RedirectToAction("Index");
                return Json(new { success = true });
            }

            ViewBag.FDPID = new SelectList(_FDPService.GetAllFDP(), "FDPID", "Name", fdp.FDPID);
            return PartialView(fdp);
        }

        //
        // GET: /FDP/Delete/5

        public virtual ActionResult Delete(int id)
        {
            FDP fdp = _FDPService.FindById(id);
            return View(fdp);
        }

        //
        // POST: /FDP/Delete/5

        [HttpPost, ActionName("Delete")]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            _FDPService.DeleteById(id);
            return RedirectToAction("Index");
        }
        public ActionResult GetFdps(int woredaId)
        {
            var fdps = from p in _FDPService.GetFDPsByWoreda(woredaId)
                       select new AdminUnitItem { Id = p.FDPID, Name = p.Name };

            return Json(new SelectList(fdps.OrderBy(o => o.Name), "Id", "Name"), JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
           // _FDPService.Dispose();
            base.Dispose(disposing);
        }
    }
}