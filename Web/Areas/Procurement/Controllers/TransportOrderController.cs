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

        private readonly ITransportRequisitionService _transportRequisitionService;
        private readonly IWorkflowStatusService _workflowStatusService;
        private readonly ILog _log;
        private readonly IUserAccountService _userAccountService;
        private readonly IAdminUnitService _adminUnitService;
        private readonly ITransporterService _transporterService;

        public TransportOrderController(ITransportOrderService transportOrderService, 
            ITransportRequisitionService transportRequisitionService, 
            IWorkflowStatusService workflowStatusService,ILog log,IUserAccountService userAccountService, 
            IAdminUnitService adminUnitService, ITransporterService transporterService)
        {
            this._transportOrderService = transportOrderService;
            this._transportRequisitionService = transportRequisitionService;
            this._workflowStatusService = workflowStatusService;
            _log = log;
            _userAccountService = userAccountService;
            _adminUnitService = adminUnitService;
            _transporterService = transporterService;
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
                log.LogAllErrorsMesseges(exception,_log);
                return View("TransportRequisitions", "TransportOrder");
            }


        }

        public ActionResult CreateTransportOrder(IEnumerable<int> requisitionToDispatches)
        {

            _transportOrderService.CreateTransportOrder(requisitionToDispatches);


            return RedirectToAction("Index", "TransportOrder");
        }

        public ViewResult Index(int id=0)
        {
            ViewBag.TransportOrdrStatus = id;
            ViewBag.TransportOrderTitle = id == 0
                                              ? "Draft"
                                              : _workflowStatusService.GetStatusName(WORKFLOW.TRANSPORT_ORDER, id);
            return View();
        }

        public ActionResult TransportOrder_Read([DataSourceRequest] DataSourceRequest request,int id=0)
        {
            var transportOrders = id==0?_transportOrderService.Get(t=>t.StatusID==(int)TransportOrderStatus.Draft).ToList():_transportOrderService.Get(t=>t.StatusID==id).ToList();
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var statuses = _workflowStatusService.GetStatus(WORKFLOW.TRANSPORT_ORDER);
            var transportOrderViewModels = TransportOrderViewModelBinder.BindListTransportOrderViewModel(
                transportOrders, datePref, statuses);
            return Json(transportOrderViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
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
            var transportOrderViewModel = TransportOrderViewModelBinder.BindTransportOrderViewModel(transportOrder,datePref,statuses);
            ViewData["Transport.order.detail.ViewModel"] = transportOrder ==null ? null :
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
            if(transportOrderDetail !=null)
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

        //public ActionResult SubTransportersStandingByWoreda([DataSourceRequest] DataSourceRequest request, int changedTransportOrderID)
        //{
        //    var changedTransportOrderObj = _transportOrderService.FindById(changedTransportOrderID);
        //    var substituteTransporterOrder = new SubstituteTransporterOrder();
        //    var changedTransportOrderDetails = changedTransportOrderObj.TransportOrderDetails;
        //    substituteTransporterOrder.WoredaID = changedTransportOrderDetails.First().FDP.AdminUnitID;
        //    foreach (var changedTransportOrderDetail in changedTransportOrderDetails)
        //    {
        //        substituteTransporterOrder
        //    }
        //    return Json(transportOrderViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        //}

        public ActionResult ChangeTransporters(List<SubstituteTransporterOrder> listOfSubTransporterOrders, int changedTransportOrderID)
        {
            var changedTransportOrderObj = _transportOrderService.FindById(changedTransportOrderID);
            var returnedObj = new TransportOrder();
            foreach(var subTransporterOrders in listOfSubTransporterOrders)
            {
                foreach(var transporter in subTransporterOrders.TransporterIDs)
                {
                    var transporterObj = _transporterService.FindById(transporter);
                    var transportOrder = _transportOrderService.Get(t=>t.TransporterID == transporterObj.TransporterID &&
                                                                    t.StatusID == (int)TransportOrderStatus.Draft).FirstOrDefault();
                    if(transportOrder != null)
                    {
                        foreach (var transportOrderDetail in subTransporterOrders.TransportOrderDetails)
                        {
                            var transportOrderDetailObj = new TransportOrderDetail();
                            //transportOrderDetailObj.ZoneID = transportOrderDetail.ReliefRequisition.ZoneID;
                            transportOrderDetailObj.CommodityID = transportOrderDetail.CommodityID;
                            transportOrderDetailObj.FdpID = transportOrderDetail.FdpID;
                            transportOrderDetailObj.RequisitionID = transportOrderDetail.RequisitionID;
                            transportOrderDetailObj.QuantityQtl = transportOrderDetail.QuantityQtl;
                            transportOrderDetailObj.TariffPerQtl = transportOrderDetail.TariffPerQtl;
                            transportOrderDetailObj.SourceWarehouseID = transportOrderDetail.Hub.HubID;
                            transportOrder.TransportOrderDetails.Add(transportOrderDetail);
                        }
                        returnedObj = transportOrder;
                    }
                    else
                    {
                        var transportOrderObj = new TransportOrder
                            {
                                TransporterID = transporter,
                                OrderDate = DateTime.Today,
                                TransportOrderNo = Guid.NewGuid().ToString(),
                                OrderExpiryDate = DateTime.Today.AddDays(10),
                                BidDocumentNo = "BID-DOC-No",
                                PerformanceBondReceiptNo = "PERFORMANCE-BOND-NO",
                                ContractNumber = Guid.NewGuid().ToString(),
                                TransporterSignedDate = DateTime.Today,
                                RequestedDispatchDate = DateTime.Today,
                                ConsignerDate = DateTime.Today,
                                StatusID = (int) TransportOrderStatus.Draft
                            };
                        _transportOrderService.AddTransportOrder(transportOrderObj);
                        var transportOrderDetailList = changedTransportOrderObj.TransportOrderDetails;
                        foreach (var transportOrderDetail in transportOrderDetailList)
                        {
                            var transportOrderDetailObj = new TransportOrderDetail();
                            //transportOrderDetail.ZoneID = reliefRequisitionDetail.ReliefRequisition.ZoneID;
                            transportOrderDetailObj.CommodityID = transportOrderDetail.CommodityID;
                            transportOrderDetailObj.FdpID = transportOrderDetail.FdpID;
                            transportOrderDetailObj.RequisitionID = transportOrderDetail.RequisitionID;
                            transportOrderDetailObj.QuantityQtl = transportOrderDetail.QuantityQtl;
                            transportOrderDetailObj.TariffPerQtl = transportOrderDetail.TariffPerQtl;
                            transportOrderDetailObj.SourceWarehouseID = transportOrderDetail.Hub.HubID;
                            transportOrderObj.TransportOrderDetails.Add(transportOrderDetail);
                        }
                        returnedObj = transportOrderObj;
                        
                    }
                }
                
            }
            changedTransportOrderObj.StatusID = (int) TransportOrderStatus.Failed;
            return RedirectToAction("Index", "TransportOrder",returnedObj);
        }
    }
}
