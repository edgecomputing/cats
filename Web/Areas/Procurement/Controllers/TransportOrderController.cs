using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Models;
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
using Cats.Helpers;


namespace Cats.Areas.Procurement.Controllers
{
    public class TransportOrderController : Controller
    {
        /// GET: /Procurement/TransportOrder/
        private readonly ITransportOrderService _transportOrderService;
        private readonly ITransportOrderDetailService _transportOrderDetailService;
        private readonly ITransportRequisitionService _transportRequisitionService;
        private readonly IWorkflowStatusService _workflowStatusService;
        private readonly ILog _log;
        private readonly IAdminUnitService _adminUnitService;
        private readonly ITransReqWithoutTransporterService _transReqWithoutTransporterService;
        private readonly ITransporterService _transporterService;
        private readonly ITransportBidQuotationService _bidQuotationService;

        public TransportOrderController(ITransportOrderService transportOrderService,
            ITransportRequisitionService transportRequisitionService,
            IWorkflowStatusService workflowStatusService, ILog log,
            ITransReqWithoutTransporterService transReqWithoutTransporterService, ITransportOrderDetailService transportOrderDetailService,
            IAdminUnitService adminUnitService, ITransporterService transporterService, ITransportBidQuotationService bidQuotationService)
        {
            this._transportOrderService = transportOrderService;
            this._transportRequisitionService = transportRequisitionService;
            this._workflowStatusService = workflowStatusService;
            _log = log;
            _adminUnitService = adminUnitService;
            _transporterService = transporterService;
            _transReqWithoutTransporterService = transReqWithoutTransporterService;
            _transportOrderDetailService = transportOrderDetailService;
            _bidQuotationService = bidQuotationService;
        }



        [HttpGet]
        public ViewResult TransportRequisitions()
        {
            return View();
        }

        public FileResult Print(int id)
        {
            var reportPath = Server.MapPath("~/Report/Procurment/TransportOrder.rdlc");

            var Data = _transportOrderService.GeTransportOrderRpt(id);
            var datePref = UserAccountHelper.GetUser(User.Identity.Name).DatePreference;
            var reportData = vwTransportOrderViewModelBinder.BindListvwTransportOrderViewModel(Data, datePref);

           var dataSourceName = "TransportOrders";
            var result = ReportHelper.PrintReport(reportPath, reportData, dataSourceName);

            return File(result.RenderBytes, result.MimeType);
        }

       
        public ActionResult CreateTransportOrder(int id)
        {
            try
            {
                _transportOrderService.CreateTransportOrder(id);
                return RedirectToAction("Index", "TransportOrder");
            }
            catch (Exception exception)
            {
                var log = new Logger();
                log.LogAllErrorsMesseges(exception, _log);
                return RedirectToAction("ConfirmGenerateTransportOrder", "TransportRequisition",
                                        new { Area="Logistics", id = id });
            }
        }

        public ActionResult NotificationIndex(int recordId)
        {

            NotificationHelper.MakeNotificationRead(recordId);
            return RedirectToAction("Index", new { id = 2 });//get approved transport orders

        }
        public ActionResult NotificationNewRequisitions(int recordId)
        {

            NotificationHelper.MakeNotificationRead(recordId);
            return RedirectToAction("TransportRequisitions");//get newly created transport requisitions

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
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError("Errors", TempData["CustomError"].ToString());
            }
            ViewBag.ProgramID = new SelectList(_transportOrderService.GetPrograms(), "ProgramID", "Name");
            var transportOrderStatus = new List<RequestStatus>
                {
                    new RequestStatus() {StatusID = 1, StatusName = "Draft"},
                    new RequestStatus() {StatusID = 2, StatusName = "Approved"}
                };
            ViewBag.StatusID = new SelectList(transportOrderStatus, "StatusID", "StatusName");
            return View(viewModel);
        }

        public ActionResult TransportOrder_Read([DataSourceRequest] DataSourceRequest request, int id = 0,int programId=0)
        {
            var transportRequistions = programId==0 ?_transportRequisitionService.GetTransportRequsitionDetails(): _transportRequisitionService.GetTransportRequsitionDetails(programId);
            //var filteredTransportOrder=_transportOrderDetailService.FindBy(m=>m.RequisitionID=)
            var transportOrders = id == 0 ? _transportOrderService.GetFilteredTransportOrder(transportRequistions, (int)TransportOrderStatus.Draft ).OrderByDescending(m => m.TransportOrderID).ToList()
                                          : _transportOrderService.GetFilteredTransportOrder(transportRequistions,id).ToList();            
            var datePref = UserAccountHelper.GetUser(User.Identity.Name).DatePreference;
            var statuses = _workflowStatusService.GetStatus(WORKFLOW.TRANSPORT_ORDER);
            var transportOrderViewModels = TransportOrderViewModelBinder.BindListTransportOrderViewModel(
                transportOrders, datePref, statuses);
            return Json(transportOrderViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public TransportRequisitionWithTransporter GetRequisitionsWithoutTransporter()
        {
            var req = new TransportRequisitionWithTransporter();
            //req.Transporters = _transportOrderService.GetTransporter();
            var transReqWithoutTransport = _transReqWithoutTransporterService.FindBy(m => m.IsAssigned == false).OrderByDescending(t=>t.TransportRequisitionDetailID);
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
                target.PerformanceBondAmount = transportOrder.PerformanceBondAmount;
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
            var datePref = UserAccountHelper.GetUser(User.Identity.Name).DatePreference;
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

        public ActionResult SuggestedSubstituteTransporters([DataSourceRequest] DataSourceRequest request, int changedTransportOrderID)
        {
            var changedTransportOrderObj = _transportOrderService.FindById(changedTransportOrderID);
            var uniqueWoredas = new List<int>();
            var changedTransportOrderDetails = changedTransportOrderObj.TransportOrderDetails;
            //substituteTransporterOrder.WoredaID = changedTransportOrderDetails.First().FDP.AdminUnitID;
            foreach (var changedTransportOrderDetail in changedTransportOrderDetails.Where(changedTransportOrderDetail =>
                                                            !uniqueWoredas.Contains(changedTransportOrderDetail.FDP.AdminUnitID)))
            {
                uniqueWoredas.Add(changedTransportOrderDetail.FDP.AdminUnitID);
            }

            var substituteTransportersStanding = (from uniqueWoreda in uniqueWoredas
                                                  let woreda = uniqueWoreda
                                                  let changedTransporterPostition = _bidQuotationService.Get(t => t.TransporterID == changedTransportOrderObj.TransporterID && t.DestinationID == woreda).Select(t => t.Position).FirstOrDefault()
                                                  let woredaWinnersList = _bidQuotationService.Get(t => t.DestinationID == woreda && t.Position >= changedTransporterPostition && t.TransporterID != changedTransportOrderObj.TransporterID).ToList().OrderBy(t => t.Position)
                                                  let substituteTransportersStandingList = woredaWinnersList.ToList()
                                                  select new SubstituteTransporterOrder
                                                      {
                                                          WoredaID = uniqueWoreda,
                                                          Woreda = _adminUnitService.FindById(uniqueWoreda).Name,
                                                          TransportersStandingList = TransportBidQuotationBinding.TransportBidQuotationListViewModelBinder(substituteTransportersStandingList)
                                                      }).ToList();
            return Json(substituteTransportersStanding.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }



        public ActionResult ChangeTransporters([DataSourceRequest] DataSourceRequest request, List<SubstituteTransporterOrder> listOfSubTransporterOrders, int changedTransportOrderID)
        {
            var changedTransportOrderObj = _transportOrderService.FindById(changedTransportOrderID);
            var returnedObj = new List<TransportOrder>();
            foreach (var subTransporterOrders in listOfSubTransporterOrders)
            {
                var transporterCount = subTransporterOrders.TransportersStandingList.Count();

                foreach (var transporter in subTransporterOrders.TransportersStandingList)
                {
                    changedTransportOrderObj = _transportOrderService.FindById(changedTransportOrderID);
                    var transporterObj = _transporterService.FindById(transporter.TransporterID);
                    var transportOrder =
                        _transportOrderService.Get(t => t.TransporterID == transporterObj.TransporterID &&
                                                        t.StatusID == (int)TransportOrderStatus.Draft).
                            FirstOrDefault();
                    if (transportOrder != null)
                    {
                        foreach (var transportOrderDetail in changedTransportOrderObj.TransportOrderDetails.ToList())
                        {
                            if (transportOrderDetail.FDP.AdminUnitID == subTransporterOrders.WoredaID)
                            {
                                var transportOrderDetailObj = new TransportOrderDetail
                                {
                                    CommodityID = transportOrderDetail.CommodityID,
                                    FdpID = transportOrderDetail.FdpID,
                                    RequisitionID = transportOrderDetail.RequisitionID,
                                    QuantityQtl = transportOrderDetail.QuantityQtl.ToPreferedWeightUnit() / transporterCount,
                                    TariffPerQtl = transportOrderDetail.TariffPerQtl,
                                    SourceWarehouseID = transportOrderDetail.Hub.HubID,
                                    //transportOrderDetailObj.ZoneID = transportOrderDetail.ReliefRequisition.ZoneID;
                                };
                                transportOrder.TransportOrderDetails.Add(transportOrderDetailObj);
                                _transportOrderService.EditTransportOrder(transportOrder);
                            }
                        }

                        returnedObj.Add(transportOrder);
                    }
                    else
                    {
                        var transportOrderObj = new TransportOrder
                        {
                            TransporterID = transporter.TransporterID,
                            OrderDate = DateTime.Today,
                            TransportOrderNo = Guid.NewGuid().ToString(),
                            OrderExpiryDate = DateTime.Today.AddDays(10),
                            BidDocumentNo = "BID-DOC-No",
                            PerformanceBondReceiptNo = "PERFORMANCE-BOND-NO",
                            ContractNumber = Guid.NewGuid().ToString(),
                            TransporterSignedDate = DateTime.Today,
                            RequestedDispatchDate = DateTime.Today,
                            ConsignerDate = DateTime.Today,
                            StatusID = (int)TransportOrderStatus.Draft,
                            StartDate = DateTime.Today,
                            EndDate = DateTime.Today.AddDays(10),
                        };
                        _transportOrderService.AddTransportOrder(transportOrderObj);
                        var transporterName = _transporterService.FindById(transportOrderObj.TransporterID).Name;
                        transportOrderObj.TransportOrderNo = string.Format("TRN-ORD-{0}", transportOrderObj.TransportOrderID);
                        transportOrderObj.ContractNumber = string.Format("{0}/{1}/{2}/{3}", "LTCD", DateTime.Today.Day, DateTime.Today.Year, transporterName.Substring(0, 2));
                        _transportOrderService.EditTransportOrder(transportOrderObj);
                        //var transportOrderDetailList = subTransporterOrders.TransportOrderDetails;
                        foreach (var transportOrderDetail in changedTransportOrderObj.TransportOrderDetails.ToList())
                        {
                            if (transportOrderDetail.FDP.AdminUnitID == subTransporterOrders.WoredaID)
                            {
                                var transportOrderDetailObj = new TransportOrderDetail
                                    {
                                        TransportOrderID = transportOrderObj.TransportOrderID,
                                        CommodityID = transportOrderDetail.CommodityID,
                                        FdpID = transportOrderDetail.FdpID,
                                        RequisitionID = transportOrderDetail.RequisitionID,
                                        QuantityQtl =
                                            transportOrderDetail.QuantityQtl.ToPreferedWeightUnit() / transporterCount,
                                        TariffPerQtl = transportOrderDetail.TariffPerQtl,
                                        SourceWarehouseID = transportOrderDetail.Hub.HubID
                                    };
                                //transportOrderDetail.ZoneID = reliefRequisitionDetail.ReliefRequisition.ZoneID;
                                _transportOrderDetailService.AddTransportOrderDetail(transportOrderDetailObj);
                            }
                        }

                        returnedObj.Add(transportOrderObj);

                    }
                }
            }
            changedTransportOrderObj.StatusID = (int)TransportOrderStatus.Failed;
            _transportOrderService.EditTransportOrder(changedTransportOrderObj);
            return RedirectToAction("Index", "TransportOrder", returnedObj);
            //return Json(returnedObj.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult OrderDetail(int id)
        {
            var transportOrder = _transportOrderService.FindById(id);
            ViewBag.HubID = _transportOrderService.GetHubs();
            ViewBag.TransportOrderID = id;
            var transportContract = GetTransportOrder(transportOrder);
            return View(transportContract);
        }
        public ActionResult Contract_Read([DataSourceRequest] DataSourceRequest request, int id = 0)
        {
            var transportOrder = _transportOrderService.Get(m => m.TransportOrderID == id, null, "TransportOrderDetails").OrderByDescending(m => m.TransportOrderID).FirstOrDefault();
            if (transportOrder != null)
            {
                var detailToDisplay = GetTransportContract(transportOrder).ToList();
                return Json(detailToDisplay.ToDataSourceResult(request));
            }
            return RedirectToAction("Index");
        }
        public ActionResult TransReqWithoutTransporter_Read([DataSourceRequest] DataSourceRequest request)
        {
            var transReqWithoutTransport = _transReqWithoutTransporterService.GetAllTransReqWithoutTransporter();
            if (transReqWithoutTransport != null)
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
                            QuantityQtl = requisitionDetail.Amount.ToPreferedWeightUnit(),
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
                            TransportOrderDetailID = detail.TransportOrderDetailID,
                            CommodityID = detail.CommodityID,
                            SourceWarehouseID = detail.SourceWarehouseID,
                            QuantityQtl = detail.QuantityQtl.ToPreferedWeightUnit(),
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
        private TransportContractViewModel GetTransportOrder(TransportOrder transportOrder)
        {
            var datePref = UserAccountHelper.GetUser(User.Identity.Name).DatePreference;
            var transportContract = new TransportContractViewModel()
                {
                    TransportOrderID = transportOrder.TransportOrderID,
                    TransportOrderNo = transportOrder.TransportOrderNo,
                    TransporterID = transportOrder.TransporterID,
                    RequisitionNo = transportOrder.TransportOrderDetails.First().ReliefRequisition.RequisitionNo,
                    Transporter = transportOrder.Transporter.Name,
                    BidDocumentNo = transportOrder.BidDocumentNo,
                    ConsignerName = transportOrder.ConsignerName,
                    ContractNumber = transportOrder.ContractNumber,
                    OrderDate = transportOrder.OrderDate.ToCTSPreferedDateFormat(datePref),
                    OrderExpiryDate = transportOrder.OrderExpiryDate.ToCTSPreferedDateFormat(datePref),
                    RequestedDispatchDate = transportOrder.RequestedDispatchDate.ToCTSPreferedDateFormat(datePref),
                    ConsignerDate = transportOrder.ConsignerDate.ToCTSPreferedDateFormat(datePref),
                    PerformanceBondReceiptNo = transportOrder.PerformanceBondReceiptNo,
                    TransporterSignedDate = transportOrder.TransporterSignedDate.ToCTSPreferedDateFormat(datePref),
                    TransporterSignedName = transportOrder.TransporterSignedName,
                    StatusID = transportOrder.StatusID,
                    Zone = transportOrder.TransportOrderDetails.First().FDP.AdminUnit.AdminUnit2.Name,
                    Region = transportOrder.TransportOrderDetails.First().FDP.AdminUnit.AdminUnit2.AdminUnit2.Name

                };
            return transportContract;
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

            var selectedTransRequision = requisitionWithTransporter.TransReqwithOutTransporters.Where(m => m.Selected == true);
            try
            {
                _transportOrderService.ReAssignTransporter(selectedTransRequision, requisitionWithTransporter.SelectedTransporterID);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var log = new Logger();
                log.LogAllErrorsMesseges(ex, _log);
                ModelState.AddModelError("Errors", "Unable to create transport order");
            }

            return RedirectToAction("Index");
        }
        public ActionResult Approve(int id)
        {
            var transportOrder = _transportOrderService.FindById(id);
           int orderDetailWithoutTarrif = 0;
            try
            {
                foreach (var transportOrderDetail in transportOrder.TransportOrderDetails)
                {
                    if (transportOrderDetail.TariffPerQtl<= 0)
                    {
                        orderDetailWithoutTarrif = 1;
                        break;
                    }
                }
                if (orderDetailWithoutTarrif == 0)
                {


                    _transportOrderService.ApproveTransportOrder(transportOrder);
                    return RedirectToAction("Index");
                }
                TempData["CustomError"] = "Transport Order Without Tariff can not be approved! Please Specify Tariff for each transport order detail! ";
                return RedirectToAction("Index");
                //ModelState.AddModelError("Errors", @"Transport Order Without Tariff can not be approved!");
            }
            catch (Exception ex)
            {
                var log = new Logger();
                log.LogAllErrorsMesseges(ex, _log);
                ModelState.AddModelError("Errors", "Unable to approve");
            }
           
            return RedirectToAction("Index");
        }
        public ActionResult GenerateDispatchAllocation(int id)
        {
            try
            {
             var result=  _transportOrderService.GeneratDispatchPlan(id);
                if (result)
                {
                     return RedirectToAction("Index", "Dispatch", new {Area = "Hub"});
                }
                else
                {
                    ModelState.AddModelError("Errors", "Unable to generate dispatch allocation.");
                    return RedirectToAction("Index", "TransportOrder", new { Area = "Hub" });
                }
            }
            catch (Exception ex)
            {
                 var log = new Logger();
                log.LogAllErrorsMesseges(ex, _log);
                ModelState.AddModelError("Errors", "Unable to generate dispatch allocation.");
               
            }
           return RedirectToAction("Index", "TransportOrder", new { Area = "Hub" });
        }
        private TransportContractReportViewModel GetTransportOrderReport(TransportOrder transportOrder)
        {
            var datePref = UserAccountHelper.GetUser(User.Identity.Name).DatePreference;
            var transportOrderReport = new TransportContractReportViewModel()
            {
                TransportOrderID = transportOrder.TransportOrderID,
                TransportOrderNo = transportOrder.TransportOrderNo,
                TransporterID = transportOrder.TransporterID,
                RequisitionNo = transportOrder.TransportOrderDetails.First().ReliefRequisition.RequisitionNo,
                TransporterName = transportOrder.Transporter.Name,
                BidDocumentNo = transportOrder.BidDocumentNo,
                ConsignerName = transportOrder.ConsignerName,
                ContractNumber = transportOrder.ContractNumber,
                OrderDate = transportOrder.OrderDate.ToCTSPreferedDateFormat(datePref),
                OrderExpiryDate = transportOrder.OrderExpiryDate.ToCTSPreferedDateFormat(datePref),
                RequestedDispatchDate = transportOrder.RequestedDispatchDate.ToCTSPreferedDateFormat(datePref),
                ConsignerDate = transportOrder.ConsignerDate.ToCTSPreferedDateFormat(datePref),
                PerformanceBondReceiptNo = transportOrder.PerformanceBondReceiptNo,
                TransporterSignedDate = transportOrder.TransporterSignedDate.ToCTSPreferedDateFormat(datePref),
                TransporterSignedName = transportOrder.TransporterSignedName,
                ZoneName = transportOrder.TransportOrderDetails.First().FDP.AdminUnit.AdminUnit2.Name,
                ZoneID = transportOrder.TransportOrderDetails.First().FDP.AdminUnit.AdminUnit2.AdminUnitID,
                RegionName = transportOrder.TransportOrderDetails.First().FDP.AdminUnit.AdminUnit2.AdminUnit2.Name,
                CommodityID = transportOrder.TransportOrderDetails.First().CommodityID,
                CommodityName = transportOrder.TransportOrderDetails.First().Commodity.Name,
                RequisitionID = transportOrder.TransportOrderDetails.First().FDP.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID

            };
            return transportOrderReport;
        }
    }
}
