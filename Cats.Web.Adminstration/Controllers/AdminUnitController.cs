using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Administration;

namespace Cats.Web.Adminstration.Controllers
{
    public class AdminUnitController : Controller
    {

        private readonly IAdminUnitService _adminUnitService;

        public AdminUnitController(IAdminUnitService adminUnitService)
        {
            _adminUnitService = adminUnitService;
        }
        //
        // GET: /AdminUnit/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /AdminUnit/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /AdminUnit/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AdminUnit/Create

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
        // GET: /AdminUnit/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /AdminUnit/Edit/5

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
        // GET: /AdminUnit/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /AdminUnit/Delete/5

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
