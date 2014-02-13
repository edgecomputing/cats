using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Hub;
using Cats.Web.Hub;

namespace Cats.Areas.Hub.Controllers
{
    public class LedgerController : BaseController
    {
        //
        // GET: /Ledger/
        private readonly ILedgerService _ledgerService;
        private readonly ILedgerTypeService _ledgerTypeService;

        public LedgerController(ILedgerService ledgerService, ILedgerTypeService ledgerTypeService, IUserProfileService userProfileService)
            : base(userProfileService)
        { 
            _ledgerService = ledgerService;
            _ledgerTypeService = ledgerTypeService;
        }

        public ActionResult Index()
        {
           
            var ledgers = _ledgerService.GetAllLedger().OrderBy(l => l.Name).ToList();
            return View(ledgers);

        }


        public ActionResult Edit(int id) {
           
            var ledger = _ledgerService.FindById(id);
            ViewBag.ledgerTypes = new SelectList(_ledgerTypeService.GetAllLedgerType().OrderBy(l => l.Name), "LedgerTypeID", "Name", ledger.LedgerTypeID);
            return View(ledger);
        }

        
        [HttpPost]
        public ActionResult Edit(Cats.Models.Hubs.Ledger ledger) {

            if(ModelState.IsValid){
                _ledgerService.EditLedger(ledger);
                return Json(new { success = true });
            }
            ViewBag.hubOwnerID = new SelectList(_ledgerService.GetAllLedger().OrderBy(l => l.Name));
            return View();
        }

        public ActionResult Delete(int id) {
            var ledger = _ledgerService.FindById(id);
            return View(ledger);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id) {
            _ledgerService.DeleteById(id);
            return RedirectToAction("Index");
        }

        public ActionResult Create() {
            ViewBag.ledgerTypes = new SelectList(_ledgerTypeService.GetAllLedgerType().OrderBy(l => l.Name), "LedgerTypeID", "Name",1);
            return View();
        }

        [HttpPost]
        public virtual ActionResult Create(Cats.Models.Hubs.Ledger ledger)
        {
            if (ModelState.IsValid)
            {
                _ledgerService.AddLedger(ledger);
                return Json(new { success = true });
            }

            //ViewBag.HubOwnerID = new SelectList(_ledgerService.GetAllLedger().OrderBy(l=>l.Name), "Name", "LedgerType", ledger.LedgerID);
            return PartialView(ledger);
        }
    }
}
