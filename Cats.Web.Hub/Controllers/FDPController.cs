using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models.Hub;
using Cats.Services.Hub;

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
            return View(fdps);
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

        public virtual ActionResult Create()
        {
            return PartialView();
        }

        //
        // POST: /FDP/Create

        [HttpPost]
        public virtual ActionResult Create(FDP fdp)
        {
            if (ModelState.IsValid)
            {
                _FDPService.AddFDP(fdp);
                return Json(new { success = true });
            }

            return PartialView(fdp);
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

        protected override void Dispose(bool disposing)
        {
           // _FDPService.Dispose();
            base.Dispose(disposing);
        }
    }
}