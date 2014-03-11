using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Logistics.Models;
using Cats.Helpers;
using Cats.Models;
using Cats.Services.Procurement;
using Cats.Services.Security;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.Logistics.Controllers
{
    public class TransporterPerformanceController : Controller
    {
        //
        // GET: /Logistics/TransporterPerformance/
        private readonly ITransportOrderService _transportOrderService;
        private readonly IUserAccountService _userAccountService;
        public TransporterPerformanceController(ITransportOrderService transportOrderService,IUserAccountService userAccountService)
        {
            _transportOrderService = transportOrderService;
            _userAccountService = userAccountService;

        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TransporterPerformance_Read([DataSourceRequest] DataSourceRequest request, string searchIndex)
        {
            var transportOrder = _transportOrderService.Get(m => m.ContractNumber.Contains(searchIndex));
            var transportOrderToDisplay = GetTransportOrder(transportOrder);
            return Json(transportOrderToDisplay.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        private IEnumerable<TransporterPerformanceViewModel> GetTransportOrder(IEnumerable<TransportOrder> transportOrders)
        {
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            return (from transportOrder in transportOrders
                    select new TransporterPerformanceViewModel
                    {
                        TransporterID = transportOrder.TransporterID,
                        TransporterName = transportOrder.Transporter.Name,
                        TransporterOrderID = transportOrder.TransportOrderID,
                        ContractNumber = transportOrder.ContractNumber,
                        TransportOrderNumber = transportOrder.TransportOrderNo,
                        StartDate = transportOrder.StartDate.ToCTSPreferedDateFormat(datePref),
                        TotalQuantity = transportOrder.TransportOrderDetails.Sum(m => m.QuantityQtl),
                        NoOfDaysToComplete =(int) (transportOrder.EndDate.Subtract(transportOrder.StartDate)).TotalDays,
                        ElapsedDays =(int)(DateTime.Now.Subtract(transportOrder.StartDate)).TotalDays,
                        Percentage = (456 / (transportOrder.TransportOrderDetails.Sum(m => m.QuantityQtl)))*100
                    });
                
            
           
        }
        public JsonResult GetContractNumbers()
        {


            var contractNumbers = (from contractNumber in _transportOrderService.GetAllTransportOrder()
                                   select contractNumber.ContractNumber).Distinct().ToList();
            // .Except(
            //from allocated in _receiptAllocationService.GetAllReceiptAllocation()
            //select allocated.SINumber).ToList();

            return Json(contractNumbers, JsonRequestBehavior.AllowGet);
        }
    }
}
