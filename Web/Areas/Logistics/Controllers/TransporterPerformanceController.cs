using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Logistics.Models;
using Cats.Helpers;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Services.Hub;
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
        private readonly IDispatchAllocationService _dispatchAllocationService;
        public TransporterPerformanceController(ITransportOrderService transportOrderService,IUserAccountService userAccountService,
                                              IDispatchAllocationService dispatchAllocationService)
        {
            _transportOrderService = transportOrderService;
            _userAccountService = userAccountService;
            _dispatchAllocationService = dispatchAllocationService;

        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TransporterPerformance_Read([DataSourceRequest] DataSourceRequest request, string searchIndex)
        {
            var transportOrder = _transportOrderService.Get(m => m.ContractNumber.Contains(searchIndex))
                               .Where(m=>m.StatusID==(int)TransportOrderStatus.Closed && m.TransportOrderDetails.Sum(s=>s.QuantityQtl)>0)
                               .OrderByDescending(m=>m.TransportOrderID);
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
                        PickedUpSofar = GetDispatchAllocation(transportOrder.TransportOrderID),
                        ElapsedDays =(int)(DateTime.Now.Subtract(transportOrder.StartDate)).TotalDays,
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

        private Decimal GetDispatchAllocation(int transportOrderID)
        {
            var dispatchAllocations = _dispatchAllocationService.FindBy(m => m.TransportOrderID == transportOrderID);
            var dispatchedSoFar = dispatchAllocations.Sum(m => m.Amount);
            return dispatchedSoFar;
        }
    }
}
