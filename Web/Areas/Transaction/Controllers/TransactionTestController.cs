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
namespace Cats.Areas.Transaction.Controllers
{
    public class TransactionTestController : Controller
    {
       // private CatsContext db = new CatsContext();
      //  private 
        private readonly IAccountTransactionService _accountTransactionService;
        public TransactionTestController(IAccountTransactionService accountTransactionServiceParam)
        {
            _accountTransactionService = accountTransactionServiceParam;
        }
        //
        // GET: /Transaction/Transaction/

        public ActionResult Index()
        {
            IEnumerable<Cats.Models.AccountTransaction> list = (IEnumerable<Cats.Models.AccountTransaction>)_accountTransactionService.GetAllAccountTransaction();

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
        public ActionResult Create(Cats.Models.AccountTransaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.AccountTransactionID = Guid.NewGuid();
                _accountTransactionService.AddAccountTransaction(transaction);

                return RedirectToAction("Index");
              
                return RedirectToAction("Index");
            }

            return View(transaction);
        }

        //
        // GET: /Transaction/Transaction/Edit/5

        public ActionResult Edit(Guid id)
        {
            AccountTransaction transaction = _accountTransactionService.FindById(id);
            
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        //
        // POST: /Transaction/Transaction/Edit/5

        [HttpPost]
        public ActionResult Edit(Cats.Models.AccountTransaction transaction)
        {
            if (ModelState.IsValid)
            {

                _accountTransactionService.UpdateAccountTransaction(transaction);
                return RedirectToAction("Index");
            }
            return View(transaction);
        }

        //
        // GET: /Transaction/Transaction/Delete/5

        public ActionResult Delete(Guid id )
        {
            Cats.Models.AccountTransaction transaction = _accountTransactionService.FindById(id);
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