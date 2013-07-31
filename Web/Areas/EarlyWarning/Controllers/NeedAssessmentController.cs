using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cats.Areas.EarlyWarning.Controllers
{
    public class NeedAssessmentController : Controller
    {
        //
        // GET: /EarlyWarning/NeedAssessment/

        public ActionResult Index()
        {
            return View();
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
            return View();
        }

        //
        // POST: /EarlyWarning/NeedAssessment/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

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
            return View();
        }

        //
        // POST: /EarlyWarning/NeedAssessment/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /EarlyWarning/NeedAssessment/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /EarlyWarning/NeedAssessment/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
