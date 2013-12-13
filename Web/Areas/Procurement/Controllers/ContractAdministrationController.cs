using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Procurement.Models;
using Cats.Helpers;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Services.Procurement;
using Cats.Services.Security;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.Procurement.Controllers
{
    public class ContractAdministrationController : Controller
    {
        private readonly IPaymentRequestService _paymentRequestService;
        private readonly ITransporterService _transporterService;
        private readonly ITransportOrderService _transportOrderService;
        private readonly IUserAccountService _userAccountService;

        public ContractAdministrationController(IPaymentRequestService paymentRequestService, ITransporterService transporterService,
            ITransportOrderService transportOrderService, IUserAccountService userAccountService)
        {
            _paymentRequestService = paymentRequestService;
            _transporterService = transporterService;
            _transportOrderService = transportOrderService;
            _userAccountService = userAccountService;
        }
        //
        // GET: /Procurement/ContractAdministration/

        public ActionResult Index(int transporterID)
        {
            var transporterObj = _transporterService.FindById(transporterID);
            ViewBag.TransporterID = transporterID;
            ViewBag.PaymentRequests= (IEnumerable<Cats.Models.PaymentRequest>)_paymentRequestService.GetAll().ToList();
            ViewBag.TransporterName = transporterObj.Name;
            ViewBag.TransporterAddress = "Region: " + transporterObj.Region
                                            + "  |  Sub-City: " + transporterObj.SubCity
                                            + "  |  Telephone: " + transporterObj.TelephoneNo;
            return View();
        }

        public ActionResult ActiveTO_Read([DataSourceRequest] DataSourceRequest request, int transporterID)
        {
            var activeTOs =
                _transportOrderService.Get(t => t.StatusID == 3 && t.TransporterID == transporterID).ToList();
            var activeTOsViewModel = GetActiveTOsListViewModel(activeTOs);
            return Json(activeTOsViewModel.ToDataSourceResult(request));
        }

        private IEnumerable<ActiveTransportOrderViewModel> GetActiveTOsListViewModel(IEnumerable<TransportOrder> transportOrders)
        {
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            return (from transportOrder in transportOrders
                    select new ActiveTransportOrderViewModel()
                    {
                        TransportOrderID = transportOrder.TransportOrderID,
                        TransportOderNo = transportOrder.TransportOrderNo,
                        StartedOn = transportOrder.StartDate.ToCTSPreferedDateFormat(datePref),
                        SignedDate = transportOrder.TransporterSignedDate.ToCTSPreferedDateFormat(datePref),
                        RemainingDays = (transportOrder.EndDate - transportOrder.StartDate).TotalDays.ToString(),
                        Progress = ((((DateTime.Now - transportOrder.StartDate).TotalDays) / ((transportOrder.EndDate - transportOrder.StartDate).TotalDays)) * 100).ToString() + "%"

                    });
        }
    }
}
