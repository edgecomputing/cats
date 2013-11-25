using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Helpers;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Models.ViewModels;
using Cats.Services.EarlyWarning;
using Cats.Services.Procurement;
using Cats.Services.Security;
using Cats.ViewModelBinder;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.Hub.Controllers
{
    public class TransportOrderController : Controller
    {
        //
        // GET: /Hub/TransportOrder/
        private readonly ITransportOrderService _transportOrderService;
        private readonly IWorkflowStatusService _workflowStatusService;
        private readonly IUserAccountService _userAccountService;
        public TransportOrderController(ITransportOrderService transportOrderService, ITransReqWithoutTransporterService transReqWithoutTransporterService, IWorkflowStatusService workflowStatusService, IUserAccountService userAccountService)
        {
            _transportOrderService = transportOrderService;
            _workflowStatusService = workflowStatusService;
            _userAccountService = userAccountService;
        }


        public ActionResult NotificationIndex(int recordId)
        {

            NotificationHelper.MakeNotificationRead(recordId);
            return RedirectToAction("Index", new { id = 2 });//get approved transport orders

        }

        public ViewResult Index(int id = 0)
        {
           
            ViewBag.TransportOrdrStatus = id;
            ViewBag.TransportOrderTitle = id == 0
                                              ? "Draft"
                                              : _workflowStatusService.GetStatusName(WORKFLOW.TRANSPORT_ORDER, id);
         
            return View();
        }

        public ActionResult TransportOrder_Read([DataSourceRequest] DataSourceRequest request, int id = 0)
        {
            var transportOrders = id == 0 ? _transportOrderService.Get(t => t.StatusID == (int)TransportOrderStatus.Draft).OrderByDescending(m => m.TransportOrderID).ToList() : _transportOrderService.Get(t => t.StatusID == id).ToList();
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var statuses = _workflowStatusService.GetStatus(WORKFLOW.TRANSPORT_ORDER);
            var transportOrderViewModels = TransportOrderViewModelBinder.BindListTransportOrderViewModel(
                transportOrders, datePref, statuses);
            return Json(transportOrderViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}
