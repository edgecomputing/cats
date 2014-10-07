using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Hub;
using Cats.Models.Hubs.ViewModels;
using Cats.Web.Hub;

namespace Cats.Areas.Hub.Controllers.Reports
{
     [Authorize]
    public class TransportationReportController : BaseController
     {
         private readonly ITransactionService _transactionService;

         public TransportationReportController(ITransactionService transactionService, IUserProfileService userProfileService)
             : base(userProfileService)
         {
             this._transactionService = transactionService;
         }
        //
        // GET: /TransportationReport/
        

        public ActionResult Index()
        {
            PopulateDateFormat();
            return View();
        }

        public ActionResult ReceiveTrend(int Operation, string from, string to)
        {

            DateTime fromDate;
            DateTime toDate;

            bool fromResult = DateTime.TryParse(from, Thread.CurrentThread.CurrentCulture, DateTimeStyles.None, out fromDate);
            bool toResult = DateTime.TryParse(to, Thread.CurrentThread.CurrentCulture, DateTimeStyles.None, out toDate);

            OperationMode mode = (OperationMode)Operation;
            DateTime? f = (fromResult)? fromDate : (DateTime?) null;
            DateTime? t = (toResult) ? toDate : (DateTime?)null;
            var list = new List<TransporationReport>();
            if (f < t)
                list = _transactionService.GetTransportationReports(mode, f, t);
            else
                return PartialView("DateRangeErrorPage");
            return list.Any() ? PartialView("PartialGrid", list) : PartialView("EmptyPage");
        }

         private void PopulateDateFormat()
         {
             if (Thread.CurrentThread.CurrentCulture.Name == "en-US")
             {
                 ViewBag.DateFormat = "dd-M-yy";
             }
             else if (Thread.CurrentThread.CurrentCulture.Name == "en-GB")
             {
                 ViewBag.DateFormat = "dd-M-yy";
             }
             else
             {
                 ViewBag.DateFormat = "dd-M-yy";
             }
         }
    }
}
