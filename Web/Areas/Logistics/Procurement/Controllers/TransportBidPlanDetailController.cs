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
    public class TransportBidPlanDetailController : Controller
    {
        private readonly ITransportBidPlanDetailService _transportBidPlanDetailService;
        private CatsContext db = new CatsContext();
        public TransportBidPlanDetailController(ITransportBidPlanDetailService transportBidPlanDetailServiceParam)
        {
            this._transportBidPlanDetailService = transportBidPlanDetailServiceParam;
        }
        //
        // GET: /Procurement/TransportBidPlanDetail/

        public ActionResult Index()
        {
            var transportbidplandetails = db.TransportBidPlanDetails.Include(t => t.BidPlan).Include(t => t.Destination).Include(t => t.Source).Include(t => t.Program);
            return View(transportbidplandetails.ToList());
        }

        //
        // GET: /Procurement/TransportBidPlanDetail/Details/5

        public ActionResult Details(int id = 0)
        {
            TransportBidPlanDetail transportbidplandetail = db.TransportBidPlanDetails.Find(id);
            if (transportbidplandetail == null)
            {
                return HttpNotFound();
            }
            return View(transportbidplandetail);
        }

        //
        // GET: /Procurement/TransportBidPlanDetail/Create

        public ActionResult Create()
        {
          /*  ViewBag.BidPlanID = new SelectList(db.TransportBidPlans, "TransportBidPlanID", "TransportBidPlanID");
            ViewBag.DestinationID = new SelectList(db.AdminUnits, "AdminUnitID", "Name");
            ViewBag.SourceID = new SelectList(db.Hubs, "HubId", "Name");
            ViewBag.ProgramID = new SelectList(db.Programs, "ProgramID", "Name");*/
            return View();
        }

        //
        // POST: /Procurement/TransportBidPlanDetail/Create

        [HttpPost]
        public ActionResult Create(TransportBidPlanDetail transportbidplandetail)
        {
            if (ModelState.IsValid)
            {
                db.TransportBidPlanDetails.Add(transportbidplandetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BidPlanID = new SelectList(db.TransportBidPlans, "TransportBidPlanID", "TransportBidPlanID", transportbidplandetail.BidPlanID);
            ViewBag.DestinationID = new SelectList(db.AdminUnits, "AdminUnitID", "Name", transportbidplandetail.DestinationID);
            ViewBag.SourceID = new SelectList(db.Hubs, "HubId", "Name", transportbidplandetail.SourceID);
            ViewBag.ProgramID = new SelectList(db.Programs, "ProgramID", "Name", transportbidplandetail.ProgramID);
            return View(transportbidplandetail);
        }

        //
        // GET: /Procurement/TransportBidPlanDetail/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TransportBidPlanDetail transportbidplandetail = db.TransportBidPlanDetails.Find(id);
            if (transportbidplandetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.BidPlanID = new SelectList(db.TransportBidPlans, "TransportBidPlanID", "TransportBidPlanID", transportbidplandetail.BidPlanID);
            ViewBag.DestinationID = new SelectList(db.AdminUnits, "AdminUnitID", "Name", transportbidplandetail.DestinationID);
            ViewBag.SourceID = new SelectList(db.Hubs, "HubId", "Name", transportbidplandetail.SourceID);
            ViewBag.ProgramID = new SelectList(db.Programs, "ProgramID", "Name", transportbidplandetail.ProgramID);
            return View(transportbidplandetail);
        }

        //
        // POST: /Procurement/TransportBidPlanDetail/Edit/5

        [HttpPost]
        public ActionResult Edit(TransportBidPlanDetail transportbidplandetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transportbidplandetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BidPlanID = new SelectList(db.TransportBidPlans, "TransportBidPlanID", "TransportBidPlanID", transportbidplandetail.BidPlanID);
            ViewBag.DestinationID = new SelectList(db.AdminUnits, "AdminUnitID", "Name", transportbidplandetail.DestinationID);
            ViewBag.SourceID = new SelectList(db.Hubs, "HubId", "Name", transportbidplandetail.SourceID);
            ViewBag.ProgramID = new SelectList(db.Programs, "ProgramID", "Name", transportbidplandetail.ProgramID);
            return View(transportbidplandetail);
        }

        //
        // GET: /Procurement/TransportBidPlanDetail/Delete/5

        public ActionResult Delete(int id = 0)
        {
            _transportBidPlanDetailService.DeleteById(id);
            return RedirectToAction("Index");
            TransportBidPlanDetail transportbidplandetail = db.TransportBidPlanDetails.Find(id);
            if (transportbidplandetail == null)
            {
                return HttpNotFound();
            }
            return View(transportbidplandetail);
        }

        //
        // POST: /Procurement/TransportBidPlanDetail/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            TransportBidPlanDetail transportbidplandetail = db.TransportBidPlanDetails.Find(id);
            db.TransportBidPlanDetails.Remove(transportbidplandetail);
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