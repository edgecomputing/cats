using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;

namespace Cats.Areas.Procurement.Controllers
{
    public class TransporterController : Controller
    {
        private CatsContext2 db = new CatsContext2();
        private List<Transporter> transcoll = new List<Transporter>();
        public Transporter getTransporter(int id)
        {
            transcoll = GetAllTransporter();
            return transcoll[id - 1];
        }
        public List<Transporter> GetAllTransporter()
        {
            Transporter t1 = new Transporter 
                { 
                    TransporterID = 1 
                    ,Region="Addis"
                    ,SubCity="Yeka"
                    ,TelephoneNo="0911608166"
                    ,MobileNo="0911608166"
                    ,LiftCapacityTotal=2000	 
                   ,Capital=125000
                   ,EmployeeCountMale=20	
                };
            Transporter t2 = new Transporter 
                { 
                    TransporterID = 2
                    ,Region="Addis"
                    ,SubCity="Kera"
                    ,TelephoneNo="0911000000"
                    ,MobileNo="0911000000"
                    ,LiftCapacityTotal=8000	 
                   ,Capital=225000
                   ,EmployeeCountMale=40	
                };
           
            transcoll.Add(t1);
            transcoll.Add(t2);
            return transcoll;
        }

        //
        // GET: /Procurement/Default1/

        public ActionResult Index()
        {

            return View(GetAllTransporter());
        }

        //
        // GET: /Procurement/Default1/Details/5

        public ActionResult Details(int id = 0)
        {
            Transporter transporter = getTransporter(id);

            if (transporter == null)
            {
                return HttpNotFound();
            }
            return View(transporter);
        }

        //
        // GET: /Procurement/Default1/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Procurement/Default1/Create

        [HttpPost]
        public ActionResult Create(Transporter transporter)
        {
            if (ModelState.IsValid)
            {
                db.Transporters.Add(transporter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transporter);
        }

        //
        // GET: /Procurement/Default1/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Transporter transporter = getTransporter(id);
            if (transporter == null)
            {
                return HttpNotFound();
            }
            return View(transporter);
        }

        //
        // POST: /Procurement/Default1/Edit/5

        [HttpPost]
        public ActionResult Edit(Transporter transporter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transporter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transporter);
        }

        //
        // GET: /Procurement/Default1/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Transporter transporter = db.Transporters.Find(id);
            if (transporter == null)
            {
                return HttpNotFound();
            }
            return View(transporter);
        }

        //
        // POST: /Procurement/Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Transporter transporter = db.Transporters.Find(id);
            db.Transporters.Remove(transporter);
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