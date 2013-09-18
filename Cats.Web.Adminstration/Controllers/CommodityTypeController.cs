using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cats.Web.Adminstration.Controllers
{
    public class CommodityTypeController : Controller
    {
        //
        // GET: /CommodityType/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /CommodityType/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /CommodityType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CommodityType/Create

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
        // GET: /CommodityType/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /CommodityType/Edit/5

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
        // GET: /CommodityType/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /CommodityType/Delete/5

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
