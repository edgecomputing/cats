using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;
using Cats.Data;

namespace Cats.Areas.PSNP.Controllers
{
    public class Default1Controller : Controller
    {
        private CatsContext db = new CatsContext();

        //
        // GET: /PSNP/Default1/

        public ActionResult Index()
        {
            var regionalpsnpplandetails = db.RegionalPSNPPlanDetails.Include(r => r.RegionalPSNPPlan).Include(r => r.PlanedFDP);
            return View(regionalpsnpplandetails.ToList());
        }

        //
        // GET: /PSNP/Default1/Details/5

        public ActionResult Details(int id = 0)
        {
            RegionalPSNPPlanDetail regionalpsnpplandetail = db.RegionalPSNPPlanDetails.Find(id);
            if (regionalpsnpplandetail == null)
            {
                return HttpNotFound();
            }
            return View(regionalpsnpplandetail);
        }

        //
        // GET: /PSNP/Default1/Create

        public ActionResult Create()
        {
            ViewBag.RegionalPSNPPlanID = new SelectList(db.RegionalPSNPPlans, "RegionalPSNPPlanID", "RegionalPSNPPlanID");
            ViewBag.PlanedFDPID = new SelectList(db.Fdps, "FDPID", "Name");
            return View();
        }

        //
        // POST: /PSNP/Default1/Create

        [HttpPost]
        public ActionResult Create(RegionalPSNPPlanDetail regionalpsnpplandetail)
        {
            if (ModelState.IsValid)
            {
                db.RegionalPSNPPlanDetails.Add(regionalpsnpplandetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RegionalPSNPPlanID = new SelectList(db.RegionalPSNPPlans, "RegionalPSNPPlanID", "RegionalPSNPPlanID", regionalpsnpplandetail.RegionalPSNPPlanID);
            ViewBag.PlanedFDPID = new SelectList(db.Fdps, "FDPID", "Name", regionalpsnpplandetail.PlanedFDPID);
            return View(regionalpsnpplandetail);
        }

        //
        // GET: /PSNP/Default1/Edit/5

        public ActionResult Edit(int id = 0)
        {
            RegionalPSNPPlanDetail regionalpsnpplandetail = db.RegionalPSNPPlanDetails.Find(id);
            if (regionalpsnpplandetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.RegionalPSNPPlanID = new SelectList(db.RegionalPSNPPlans, "RegionalPSNPPlanID", "RegionalPSNPPlanID", regionalpsnpplandetail.RegionalPSNPPlanID);
            ViewBag.PlanedFDPID = new SelectList(db.Fdps, "FDPID", "Name", regionalpsnpplandetail.PlanedFDPID);
            return View(regionalpsnpplandetail);
        }

        //
        // POST: /PSNP/Default1/Edit/5

        [HttpPost]
        public ActionResult Edit(RegionalPSNPPlanDetail regionalpsnpplandetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(regionalpsnpplandetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RegionalPSNPPlanID = new SelectList(db.RegionalPSNPPlans, "RegionalPSNPPlanID", "RegionalPSNPPlanID", regionalpsnpplandetail.RegionalPSNPPlanID);
            ViewBag.PlanedFDPID = new SelectList(db.Fdps, "FDPID", "Name", regionalpsnpplandetail.PlanedFDPID);
            return View(regionalpsnpplandetail);
        }

        //
        // GET: /PSNP/Default1/Delete/5

        public ActionResult Delete(int id = 0)
        {
            RegionalPSNPPlanDetail regionalpsnpplandetail = db.RegionalPSNPPlanDetails.Find(id);
            if (regionalpsnpplandetail == null)
            {
                return HttpNotFound();
            }
            return View(regionalpsnpplandetail);
        }

        //
        // POST: /PSNP/Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            RegionalPSNPPlanDetail regionalpsnpplandetail = db.RegionalPSNPPlanDetails.Find(id);
            db.RegionalPSNPPlanDetails.Remove(regionalpsnpplandetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}