using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models.Hubs;
using Cats.Services.Hub;

namespace Cats.Web.Hub.Controllers
{
     [Authorize]
    public class TransactionController : BaseController
     {
         private readonly ITransactionService _transactionService;
         private readonly ILedgerService _ledgerService;
         private readonly ICommodityService _commodityService;

         public TransactionController(ITransactionService transactionService,
             ILedgerService ledgerService, ICommodityService commodityService,
             IUserProfileService userProfileService)
             : base(userProfileService)
         {
             this._transactionService = transactionService;
             this._ledgerService = ledgerService;
             this._commodityService = commodityService;
         }
        //
        // GET: /Transaction/
      
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Journal()
        {
            
            return View(_transactionService.Get().OrderByDescending(o=>o.TransactionDate).ThenByDescending(o=> o.TransactionGroupID).ToList());
        }

        public ActionResult Ledger()
        {
            ViewBag.Ledgers =_ledgerService.GetAllLedger();
            ViewBag.Commodities = _commodityService.GetAllParents();

            return View();
        }

        public ActionResult LedgerPartial()
        {
            int account = Convert.ToInt32(Request["account"]);
            int commodity = Convert.ToInt32(Request["commodity"]);
            int ledger = Convert.ToInt32(Request["ledger"]);

            var transactions = (_transactionService.GetTransactionsForLedger(ledger,account,commodity).OrderByDescending(o => o.TransactionID).ToList());
            return PartialView(transactions);
        }


        public ActionResult GetAccounts(int LedgerID)
        {
            List<Account> accounts = _transactionService.GetActiveAccountsForLedger(LedgerID);

            return Json(new SelectList( accounts, "AccountID", "EntityName"), JsonRequestBehavior.AllowGet);
        }

    }
}
