using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Procurement.Models;
using Cats.Helpers;
using Cats.Infrastructure;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Models.ViewModels;
using Cats.Models.ViewModels.HRD;
using Cats.Services.Logistics;
using Cats.Services.Procurement;
using Cats.Services.EarlyWarning;
using Cats.Services.Security;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Cats.Areas.Logistics.Models;
using log4net;
using Cats.ViewModelBinder;

namespace Cats.Areas.Procurement.Controllers
{
    public class TransportOrderController : Controller
    {
        //
        // GET: /Procurement/TransportOrder/
        private readonly ITransportOrderService _transportOrderService;
        private readonly ITransportOrderDetailService _transportOrderDetailService;
        private readonly ITransportRequisitionService _transportRequisitionService;
        private readonly IWorkflowStatusService _workflowStatusService;
        private readonly ILog _log;
        private readonly IUserAccountService _userAccountService;
        private readonly ITransReqWithoutTransporterService _transReqWithoutTransporterService;


        public TransportOrderController(
            ITransportOrderService transportOrderService,
            ITransportRequisitionService transportRequisitionService,
            IWorkflowStatusService workflowStatusService, 
            ILog log,
            IUserAccountService userAccountService,
            ITransReqWithoutTransporterService transReqWithoutTransporterService,
            ITransportOrderDetailService transportOrderDetailService
            )
        {
            this._transportOrderService = transportOrderService;
            this._transportRequisitionService = transportRequisitionService;
            this._workflowStatusService = workflowStatusService;
            _log = log;
            _userAccountService = userAccountService;
            _transReqWithoutTransporterService = transReqWithoutTransporterService;
            _transportOrderDetailService = transportOrderDetailService;
        }




        [HttpGet]
        public ViewResult TransportRequisitions()
        {
            var transportRequisitions = _transportRequisitionService.Get(t => t.Status == (int)TransportRequisitionStatus.Approved);
            var transportReqInput = (from item in transportRequisitions
                                     select new TransportRequisitionSelect
                                                {
                                                    CertifiedBy = item.CertifiedBy,
                                                    CertifiedDate = item.CertifiedDate,
                                                    RequestedBy = item.RequestedBy,
                                                    RequestedDate = item.RequestedDate,
                                                    StatusName = _workflowStatusService.GetStatusName(WORKFLOW.TRANSPORT_REQUISITION, item.Status),
                                                    Status = item.Status,
                                                    RequestDateET = EthiopianDate.GregorianToEthiopian(item.RequestedDate),
                                                    CertifiedDateET = EthiopianDate.GregorianToEthiopian(item.CertifiedDate),
                                                    TransportRequisitionID = item.TransportRequisitionID,
                                                    TransportRequisitionNo = item.TransportRequisitionNo,
                                                    Input = new TransportRequisitionSelect.TransportRequisitionSelectInput
                                                              {
                                                                  Number = item.TransportRequisitionID,
                                                                  IsSelected = false
                                                              }


                                                });


            return View(transportReqInput.ToList());
        }
        public FileResult Print(int id)
        {
            var reportPath = Server.MapPath("~/Report/Procurment/TransportOrder.rdlc");
            var reportData = _transportOrderService.GeTransportOrderRpt(id);
            var dataSourceName = "TransportOrders";
            var result = ReportHelper.PrintReport(reportPath, reportData, dataSourceName);

            return File(result.RenderBytes, result.MimeType);
        }

        [HttpPost]
        public ActionResult TransportRequisitions(IList<SelectFromGrid> input)
        {
            try
            {
                var requisionIds = (from item in input where (item.IsSelected != null ? ((string[])item.IsSelected)[0] : "off") == "on" select item.Number).ToList();
                return CreateTransportOrder(requisionIds);
            }
            catch (Exception exception)
            {
                var log = new Logger();
                log.LogAllErrorsMesseges(exception, _log);
                return View("TransportRequisitions", "TransportOrder");
            }


        }

        public ActionResult CreateTransportOrder(IEnumerable<int> requisitionToDispatches)
        {

            _transportOrderService.CreateTransportOrder(requisitionToDispatches);


            return RedirectToAction("Index", "TransportOrder");
        }

        public ViewResult Index(int id = 0)
        {
            ViewBag.Month = new SelectList(RequestHelper.GetMonthList(), "Id", "Name");
            ViewBag.TransportOrdrStatus = id;
            ViewBag.TransportOrderTitle = id == 0
                                              ? "Draft"
                                              : _workflowStatusService.GetStatusName(WORKFLOW.TRANSPORT_ORDER, id);
            var allTransporters = _transportOrderService.GetTransporter();
            ViewBag.TransporterID = new SelectList(allTransporters, "TransporterID", "Name");
            var viewModel = GetRequisitionsWithoutTransporter();
            //viewModel.Transporters = allTransporters;
            return View(viewModel);
        }

        public ActionResult TransportOrder_Read([DataSourceRequest] DataSourceRequest request, int id = 0)
        {
            var transportOrders = id == 0 ? _transportOrderService.Get(t => t.StatusID == (int)TransportOrderStatus.Draft).OrderByDescending(m=>m.TransportOrderID).ToList() : _transportOrderService.Get(t => t.StatusID == id).ToList();
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var statuses = _workflowStatusService.GetStatus(WORKFLOW.TRANSPORT_ORDER);
            var transportOrderViewModels = TransportOrderViewModelBinder.BindListTransportOrderViewModel(
                transportOrders, datePref, statuses);
            return Json(transportOrderViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public TransportRequisitionWithTransporter GetRequisitionsWithoutTransporter()
        {
            var req = new TransportRequisitionWithTransporter();
            //req.Transporters = _transportOrderService.GetTransporter();
            var transReqWithoutTransport = _transReqWithoutTransporterService.FindBy(m=>m.IsAssigned==false);
            if (transReqWithoutTransport != null)
            {
                req.TransReqwithOutTransporters = GetTransReqWithoutTransporter(transReqWithoutTransport).ToList();
            }
            return req;
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var transportOrder = _transportOrderService.FindById(id);
            if (transportOrder == null)
            {
                return HttpNotFound();
            }
            return View(transportOrder);

        }
        [HttpPost]
        public ActionResult Edit(TransportOrder transportOrder)
        {
            if (ModelState.IsValid)
            {
                var target = _transportOrderService.FindById(transportOrder.TransportOrderID);
                target.TransportOrderNo = transportOrder.TransportOrderNo;
                target.ContractNumber = transportOrder.ContractNumber;
                target.OrderDate = transportOrder.OrderDate;
                target.OrderExpiryDate = transportOrder.OrderExpiryDate;
                target.RequestedDispatchDate = transportOrder.RequestedDispatchDate;
                target.PerformanceBondReceiptNo = transportOrder.PerformanceBondReceiptNo;
                target.BidDocumentNo = transportOrder.BidDocumentNo;
                target.TransporterSignedDate = transportOrder.TransporterSignedDate;
                target.TransporterSignedName = transportOrder.TransporterSignedName;
                target.ConsignerDate = transportOrder.ConsignerDate;
                target.ConsignerName = transportOrder.ConsignerName;
                _transportOrderService.EditTransportOrder(target);
                return RedirectToAction("Index", "TransportOrder");
            }
            return View(transportOrder);
        }
        public ActionResult Details(int id)
        {
            var transportOrder = _transportOrderService.Get(t => t.TransportOrderID == id, null, "TransportOrderDetails.FDP,TransportOrderDetails.FDP.AdminUnit,TransportOrderDetails.Commodity,TransportOrderDetails.Hub,TransportOrderDetails.ReliefRequisition").FirstOrDefault();
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var statuses = _workflowStatusService.GetStatus(WORKFLOW.TRANSPORT_ORDER);
            var transportOrderViewModel = TransportOrderViewModelBinder.BindTransportOrderViewModel(transportOrder, datePref, statuses);
            ViewData["Transport.order.detail.ViewModel"] = transportOrder == null ? null :
                GetDetail(transportOrder.TransportOrderDetails);
            return View(transportOrderViewModel);
        }
        private IEnumerable<TransportOrderDetailViewModel> GetDetail(IEnumerable<TransportOrderDetail> transportOrderDetails)
        {

            var transportOrderDetailViewModels =
                (from itm in transportOrderDetails select BindTransportOrderDetailViewModel(itm));
            return transportOrderDetailViewModels;
        }
        private TransportOrderDetailViewModel BindTransportOrderDetailViewModel(TransportOrderDetail transportOrderDetail)
        {
            TransportOrderDetailViewModel transportOrderDetailViewModel = null;
            if (transportOrderDetail != null)
            {
                transportOrderDetailViewModel = new TransportOrderDetailViewModel();
                transportOrderDetailViewModel.FdpID = transportOrderDetail.FdpID;
                transportOrderDetailViewModel.FDP = transportOrderDetail.FDP.Name;
                transportOrderDetailViewModel.CommodityID = transportOrderDetail.CommodityID;
                transportOrderDetailViewModel.Commodity = transportOrderDetail.Commodity.Name;
                transportOrderDetailViewModel.DonorID = transportOrderDetail.DonorID;
                transportOrderDetailViewModel.OriginWarehouse = transportOrderDetail.Hub.Name;
                transportOrderDetailViewModel.QuantityQtl = transportOrderDetail.QuantityQtl;
                transportOrderDetailViewModel.RequisitionID = transportOrderDetail.RequisitionID;
                transportOrderDetailViewModel.RequisitionNo = transportOrderDetail.ReliefRequisition.RequisitionNo;
                transportOrderDetailViewModel.SourceWarehouseID = transportOrderDetail.SourceWarehouseID;
                transportOrderDetailViewModel.TariffPerQtl = transportOrderDetail.TariffPerQtl;
                transportOrderDetailViewModel.Woreda = transportOrderDetail.FDP.AdminUnit.Name;

            }
            return transportOrderDetailViewModel;
        }
        public ActionResult TransportOrder()
        {
            ViewBag.Months = new SelectList(RequestHelper.GetMonthList(), "Id", "Name");
            var reliefRequisitions = _transportOrderService.GetTransportOrderReleifRequisition(6);
            var transOrderDetailList = new List<TransportOrderDetail>();
            var reqItems = reliefRequisitions as ReliefRequisition[] ?? reliefRequisitions.ToArray();
            foreach (var reqItem in reqItems)
            {
                var result = _transportOrderService.GetTransportOrderDetail(reqItem.RequisitionID).ToList();
                transOrderDetailList.AddRange(result);
            }
            return View(transOrderDetailList);


        }

        public ActionResult TransportOrderDetail(int id)
        {
            var detailTransportOrders = _transportOrderService.GetTransportOrderDetailByTransportId(id);
            if (detailTransportOrders == null)
            {
                return HttpNotFound();
            }
            return View(detailTransportOrders.ToList());
        }

        public ActionResult TransportContract(int id)
        {
            var transportOrder = _transportOrderService.FindById(id);
            ViewBag.HubID = _transportOrderService.GetHubs();
            ViewBag.TransportOrderID = id;
            return View(transportOrder);
        }
        public ActionResult Contract_Read([DataSourceRequest] DataSourceRequest request,int id=0)
        {
            var transportOrder =_transportOrderService.Get(m => m.TransportOrderID == id, null, "TransportOrderDetails").OrderByDescending(m=>m.TransportOrderID).FirstOrDefault();
            if(transportOrder!=null)
            {
                var detailToDisplay = GetTransportContract(transportOrder).ToList();
                return Json(detailToDisplay.ToDataSourceResult(request));
            }
            return RedirectToAction("Index");
        }
        public ActionResult TransReqWithoutTransporter_Read([DataSourceRequest] DataSourceRequest request)
        {
            var transReqWithoutTransport = _transReqWithoutTransporterService.GetAllTransReqWithoutTransporter();
            if(transReqWithoutTransport!=null)
            {
                var withoutTransporterToDisplay = GetTransReqWithoutTransporter(transReqWithoutTransport).ToList();
                return Json(withoutTransporterToDisplay.ToDataSourceResult(request));
            }
            return RedirectToAction("Index");
        }
        private IEnumerable<TransportRequisitionWithoutWinnerModel> GetTransReqWithoutTransporter(IEnumerable<TransReqWithoutTransporter> transReqWithoutTransporter)
        {
            
            return (from detail in transReqWithoutTransporter
                    from requisitionDetail in detail.TransportRequisitionDetail.ReliefRequisition.ReliefRequisitionDetails.
                                    Where(m => m.RequisitionDetailID == detail.RequisitionDetailID)
                    select new TransportRequisitionWithoutWinnerModel()
                        {
                          TransportRequisitionID = detail.TransportRequisitionDetailID,
                          TransReqWithoutTransporterID = detail.TransReqWithoutTransporterID,
                           RequisitionDetailID = detail.RequisitionDetailID,
                          Woreda = requisitionDetail.FDP.AdminUnit.Name,
                          FDP = requisitionDetail.FDP.Name,
                          QuantityQtl = requisitionDetail.Amount,
                          Commodity = requisitionDetail.Commodity.Name,
                          CommodityID = requisitionDetail.CommodityID,
                          FdpID = requisitionDetail.FDPID,
                          HubID = requisitionDetail.ReliefRequisition.HubAllocations.First().HubID,
                          OriginWarehouse = requisitionDetail.ReliefRequisition.HubAllocations.First().Hub.Name,
                          RequisitionID = detail.ReliefRequisitionDetail.RequisitionID,
                          beneficiaryNumber = detail.TransportRequisitionDetail.ReliefRequisition.ReliefRequisitionDetails.First().BenficiaryNo,
                          RequisitionNo = detail.TransportRequisitionDetail.ReliefRequisition.RequisitionNo
                        });




        }
       private IEnumerable<TransportOrderDetailViewModel> GetTransportContract(TransportOrder transportOrder)
       {
           var transportContractDetail = transportOrder.TransportOrderDetails;
           return (from detail in transportContractDetail
                   select new TransportOrderDetailViewModel()
                       {
                           TransportOrderID = detail.TransportOrderID,
                           CommodityID = detail.CommodityID,
                           SourceWarehouseID = detail.SourceWarehouseID,
                           QuantityQtl = detail.QuantityQtl,
                           RequisitionID = detail.RequisitionID,
                           TariffPerQtl = detail.TariffPerQtl,
                           Commodity = detail.Commodity.Name,
                           OriginWarehouse = detail.Hub.Name,
                           HubID = detail.Hub.HubID,
                           Woreda = detail.FDP.AdminUnit.Name,
                           FDP = detail.FDP.Name,
                           RequisitionNo = detail.ReliefRequisition.RequisitionNo
                           
                       });

           // return transportContractDetail;
       }
       [AcceptVerbs(HttpVerbs.Post)]
       public ActionResult TransportOrder_Update([DataSourceRequest] DataSourceRequest request, TransportOrderDetailViewModel orderDetails)
       {
           if (orderDetails != null && ModelState.IsValid)
           {
               var detail = _transportOrderDetailService.FindById(orderDetails.TransportOrderDetailID);
               if (detail != null)
               {
                   detail.TransportOrderID = orderDetails.TransportOrderID;
                   detail.TransportOrderDetailID = orderDetails.TransportOrderDetailID;
                   detail.SourceWarehouseID = orderDetails.HubID;
                   detail.TariffPerQtl = orderDetails.TariffPerQtl;

                   _transportOrderDetailService.EditTransportOrderDetail(detail);
               }

           }
           return Json(new[] { orderDetails }.ToDataSourceResult(request, ModelState));
           //return Json(ModelState.ToDataSourceResult());
       }
       [HttpPost]
       public ActionResult AssignTransporter(TransportRequisitionWithTransporter requisitionWithTransporter)
       {

           var selectedTransRequision =requisitionWithTransporter.TransReqwithOutTransporters.Where(m => m.Selected==true);
           try
           {
               _transportOrderService.ReAssignTransporter(selectedTransRequision,requisitionWithTransporter.SelectedTransporterID);
               return RedirectToAction("Index");
           }
           catch (Exception ex)
           {
               var log = new Logger();
               log.LogAllErrorsMesseges(ex, _log);
               ModelState.AddModelError("Errors",ex);
           }
           
           return RedirectToAction("Index");
       }
    }
}
