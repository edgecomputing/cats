using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;
using Cats.Data;
using Cats.Services.Transaction;
using Cats.Services.PSNP;
using Cats.Services.EarlyWarning;

namespace Cats.Areas.Transaction.Controllers
{
    public class TransactionTestController : Controller
    {
       // private CatsContext db = new CatsContext();
      //  private 
        private readonly ITransactionService _accountTransactionService;
        private readonly IRegionalPSNPPlanService _regionalPSNPPlanService;
        private readonly IRationService _rationService;

        public TransactionTestController(ITransactionService accountTransactionServiceParam
                                        , IRegionalPSNPPlanService regionalPSNPPlanServiceParam
                                        ,IRationService rationServiceParam)
        {
            _accountTransactionService = accountTransactionServiceParam;
            _regionalPSNPPlanService = regionalPSNPPlanServiceParam;
            _rationService = rationServiceParam;
        }
        public void loadLookups()
        {
            ViewBag.RegionalPSNPPlanID = new SelectList(_regionalPSNPPlanService.GetAllRegionalPSNPPlan(), "RegionalPSNPPlanID", "ShortName");
            ViewBag.RationID = new SelectList(_rationService.GetAllRation(), "RationID", "RefrenceNumber");

        }
        //
        // GET: /Transaction/Transaction/

        public ActionResult Index()
        {
            IEnumerable<Cats.Models.Transaction> list = (IEnumerable<Cats.Models.Transaction>)_accountTransactionService.GetAllTransaction();
            loadLookups();
            return View(list);
            
        }
        public ActionResult PostHRD(int RegionalPSNPPlanID, int RationID)
        {
           // IEnumerable<Cats.Models.Transaction> list = (IEnumerable<Cats.Models.Transaction>)_accountTransactionService.GetAllTransaction();
            loadLookups();

            
            RegionalPSNPPlan plan= _regionalPSNPPlanService.FindById(RegionalPSNPPlanID);
            Ration ration = _rationService.FindById(RationID);

            List<Cats.Models.Transaction> list = _accountTransactionService.PostPSNPPlan(plan, ration);
            ViewBag.SelectedPSNPPlan = plan;
            ViewBag.SelectedRation = ration;
           // IEnumerable<RationDetail> rationdetails = ration.RationDetails;
            return View(list);

        }

        //
        // GET: /Transaction/Transaction/Details/5

        public ActionResult Details(Guid id)
        {
            /* Cats.Models.Transaction transaction = db.Transaction.Find(id);
             if (transaction == null)
             {
                 return HttpNotFound();
             }* */
            return View();
            
        }

        //
        // GET: /Transaction/Transaction/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Transaction/Transaction/Create

        [HttpPost]
        public ActionResult Create(Cats.Models.Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.TransactionID = Guid.NewGuid();
                transaction.TransactionGroupID = Guid.NewGuid();

                _accountTransactionService.AddTransaction(transaction);

                return RedirectToAction("Index");
              
              
            }

            return View(transaction);
        }

        //
        // GET: /Transaction/Transaction/Edit/5

        public ActionResult Edit(Guid id)
        {
            Models.Transaction transaction = _accountTransactionService.FindById(id);
            
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        //
        // POST: /Transaction/Transaction/Edit/5

        [HttpPost]
        public ActionResult Edit(Cats.Models.Transaction transaction)
        {
            if (ModelState.IsValid)
            {

                _accountTransactionService.UpdateTransaction(transaction);
                return RedirectToAction("Index");
            }
            return View(transaction);
        }

        //
        // GET: /Transaction/Transaction/Delete/5

        public ActionResult Delete(Guid id )
        {
            Cats.Models.Transaction transaction = _accountTransactionService.FindById(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        //
        // POST: /Transaction/Transaction/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _accountTransactionService.DeleteById(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
          //  db.Dispose();
            base.Dispose(disposing);
        }
    }
}