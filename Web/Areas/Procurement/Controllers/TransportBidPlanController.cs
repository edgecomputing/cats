using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;
using Cats.Data;
using Cats.Services.Procurement;

namespace Cats.Areas.Procurement.Controllers
{
    public class TransportBidPlanController : Controller
    {
        private readonly ITransportBidPlanService transportBidPlanService;
        public TransportBidPlanController(ITransportBidPlanService transportBidPlanServiceParam)
        {
            this.transportBidPlanService = transportBidPlanServiceParam;
        }
        private CatsContext db = new CatsContext();

        //
        // GET: /Procurement/TransportBidPlan/

        public ActionResult Index()
        {
            List<Cats.Models.TransportBidPlan> list = transportBidPlanService.GetAllTransportBidPlan();
            return View(list);
        }

        //
        // GET: /Procurement/TransportBidPlan/Details/5

        public ActionResult Details(int id = 0)
        {
            TransportBidPlan transportbidplan = transportBidPlanService.FindById(id);
            if (transportbidplan == null)
            {
                return HttpNotFound();
            }
            return View(transportbidplan);
        }

        //
        // GET: /Procurement/TransportBidPlan/Create

        public ActionResult Create()
        {
            ViewBag.ProgramID = new SelectList(db.Programs, "ProgramID", "Name");
            return View();
        }

        //
        // POST: /Procurement/TransportBidPlan/Create

        [HttpPost]
        public ActionResult Create(TransportBidPlan transportbidplan)
        {
            if (ModelState.IsValid)
            {
                db.TransportBidPlans.Add(transportbidplan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProgramID = new SelectList(db.Programs, "ProgramID", "Name", transportbidplan.ProgramID);
            return View(transportbidplan);
        }

        //
        // GET: /Procurement/TransportBidPlan/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TransportBidPlan transportbidplan = transportBidPlanService.FindById(id);
            if (transportbidplan == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProgramID = new SelectList(db.Programs, "ProgramID", "Name", transportbidplan.ProgramID);
            return View(transportbidplan);
        }

        //
        // POST: /Procurement/TransportBidPlan/Edit/5

        [HttpPost]
        public ActionResult Edit(TransportBidPlan transportbidplan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transportbidplan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProgramID = new SelectList(db.Programs, "ProgramID", "Name", transportbidplan.ProgramID);
            return View(transportbidplan);
        }

        //
        // GET: /Procurement/TransportBidPlan/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TransportBidPlan transportbidplan = db.TransportBidPlans.Find(id);
            if (transportbidplan == null)
            {
                return HttpNotFound();
            }
            return View(transportbidplan);
        }

        //
        // POST: /Procurement/TransportBidPlan/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            TransportBidPlan transportbidplan = db.TransportBidPlans.Find(id);
            db.TransportBidPlans.Remove(transportbidplan);
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