using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cats.Web.Adminstration.Controllers
{
    public class HubController : Controller
    {
        private readonly IHubService _hubService;
        private readonly IHubOwnerService _hubOwnerService;

        public HubController(IHubService hubService, IHubOwnerService hubOwnerService)
        {
            _hubService = hubService;
            _hubOwnerService = hubOwnerService;
        }
        //
        // GET: /Hub/

        public ActionResult Index()
        {

            return View();
        }

        //
        // GET: /Hub/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Hub/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Hub/Create

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
        // GET: /Hub/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Hub/Edit/5

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
        // GET: /Hub/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Hub/Delete/5

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
