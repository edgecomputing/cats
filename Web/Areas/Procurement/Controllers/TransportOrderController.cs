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
            if (TempData["Error"]!=null)
            {
                ModelState.AddModelError("Error", TempData["Error"].ToString());
                ViewBag.Error = TempData["Error"].ToString();
            }
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


        public ActionResult CreateTransportOrder(string saveButton, string cancelButton, int id, int BidId=-1)
        {
            try
            {
                if (BidId == -1 && cancelButton == null)
                {
                    TempData["Error"] = "Transport order not created. Please select Bid and try again";
                    return RedirectToAction("TransportRequisitions");
                }
                if (saveButton != null)
                {


                    _transportOrderService.CreateTransportOrder(id,BidId);
                    return RedirectToAction("Index", "TransportOrder");
                }
                return RedirectToAction("TransportRequisitions");
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
        public ViewResult Index(int id = 0, int woredaId = 0,int transporterId=0, int zoneId=0, int transReqId=0)
        {
            ViewBag.Month = new SelectList(RequestHelper.GetMonthList(), "Id", "Name");
            ViewBag.TransportOrdrStatus = id;
            ViewBag.TransportOrderTitle = id == 0
                                              ? "Draft"
                                              : _workflowStatusService.GetStatusName(WORKFLOW.TRANSPORT_ORDER, id);
           var allTransporters = _transportOrderService.GetTransporter();

           ViewBag.TransporterID = transporterId == 0 ? new SelectList(allTransporters, "TransporterID", "Name", 0) : new SelectList(allTransporters, "TransporterID", "Name", transporterId);
           ViewBag.Zones = zoneId == 0 ? new SelectList(_transportOrderService.GetZone(transReqId), "ZoneId", "ZoneName") : new SelectList(_transportOrderService.GetZone(transReqId), "ZoneId", "ZoneName", zoneId);
           
            ViewBag.RegionID = new SelectList(_adminUnitService.GetRegions(), "AdminUnitID", "Name");
            var viewModel = GetRequisitionsWithoutTransporter(woredaId, transReqId);

            ViewBag.TransReq = transReqId == 0 ? new SelectList(_transReqWithoutTransporterService.Get(t => t.IsAssigned == false).Select(u => u.TransportRequisitionDetail != null ? new
                                                                                                                                                                                          {
                                                                                                                                                                                              TransReqID = u.TransportRequisitionDetail.TransportRequisition.TransportRequisitionID,
                                                                                                                                                                                              TransReqNo=u.TransportRequisitionDetail.TransportRequisition.TransportRequisitionNo
                                                                                                                                                                                          } : null).Distinct(), "TransReqID", "TransReqNo")
                               : new SelectList(_transReqWithoutTransporterService.Get(t => t.IsAssigned == false).Select(u => u.TransportRequisitionDetail != null ? new
                                                                                                                                                                          {
                                                                                                                                                                              TransReqID=u.TransportRequisitionDetail.TransportRequisition.TransportRequisitionID,
                                                                                                                                                                              TransReqNo=u.TransportRequisitionDetail.TransportRequisition.TransportRequisitionNo
                                                                                                                                                                          } : null).Distinct(), "TransReqID", "TransReqNo", transReqId);

           
            //viewModel.Transporters = allTransporters;
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError("Errors", TempData["CustomError"].ToString());
            }
           
            else if (TempData["CustomError2"] != null)
            {
                ModelState.AddModelError("Success", TempData["CustomError2"].ToString());
            }
            ViewBag.ProgramID = new SelectList(_transportOrderService.GetPrograms(), "ProgramID", "Name");
            var transportOrderStatus = new List<RequestStatus>
                {
                    new RequestStatus() {StatusID = 1, StatusName = "Draft"},
                    new RequestStatus() {StatusID = 2, StatusName = "Approved"},
                    new RequestStatus() {StatusID = 3, StatusName = "Signed"},
                     new RequestStatus() {StatusID = 4, StatusName = "Closed"},
                     new RequestStatus() {StatusID = 5, StatusName = "Failed"}
                };
            ViewBag.StatusID = new SelectList(transportOrderStatus, "StatusID", "StatusName");
            return View(viewModel);
        }



        public JsonResult GetWoredas(int zoneId,int transReqId)
        {
            //var result = _adminUnitService.GetWoreda(zoneId);
            var result = _transportOrderService.GetWoredas(zoneId, transReqId);
            return Json(new SelectList(result.ToArray(), "WoredaId", "WoredaName"), JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadUnAssinedByReqNo(int id, int woredaId,int zone,int transporter,int TransReqID)
        {
            return RedirectToAction("Index", new { id = id, woredaId = woredaId, transporterId = transporter, zoneId = zone, transReqId = TransReqID });
        }
        public ActionResult TransportOrder_Read([DataSourceRequest] DataSourceRequest request, int id = 0,int programId=0,int regionId = 0)
        {
            var transportRequistions = programId==0 ?_transportRequisitionService.GetTransportRequsitionDetails(): _transportRequisitionService.GetTransportRequsitionDetails(programId);
             List<TransportOrder> transportRequisitionRegion;
            
            //var filteredTransportOrder=_transportOrderDetailService.FindBy(m=>m.RequisitionID=)
            var transportOrders = id == 0 ? _transportOrderService.GetFilteredTransportOrder(transportRequistions, (int)TransportOrderStatus.Draft).OrderByDescending(m => m.TransportOrderID).ToList()
                                          : _transportOrderService.GetFilteredTransportOrder(transportRequistions, id).ToList();


            transportRequisitionRegion = regionId == 0
                                             ? transportOrders
                                             : (from detail in transportOrders
                                                let orDefault = detail.TransportOrderDetails.FirstOrDefault()
                                                where
                                                    orDefault != null &&
                                                    orDefault.FDP.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID ==
                                                    regionId
                                                select detail).ToList();


            var datePref = UserAccountHelper.GetUser(User.Identity.Name).DatePreference;
            var statuses = _workflowStatusService.GetStatus(WORKFLOW.TRANSPORT_ORDER);
            var transportOrderViewModels = TransportOrderViewModelBinder.BindListTransportOrderViewModel(
                transportRequisitionRegion, datePref, statuses);
            return Json(transportOrderViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetZonesByTransReqNo(int selectedValue)
        {
            var zones = _transportOrderService.GetZone(selectedValue);
            return Json(new SelectList(zones.ToArray(), "ZoneId", "ZoneName"), JsonRequestBehavior.AllowGet);
        }
        public TransportRequisitionWithTransporter GetRequisitionsWithoutTransporter(int woredaId, int transReqId)
        {
            var req = new TransportRequisitionWithTransporter();
            



            var transReqDetail = _transportRequisitionService.FindBy(f=>f.TransportRequisitionID == transReqId).SelectMany(d=>d.TransportRequisitionDetails);

            var reliefRequisitionDetail = transReqDetail.SelectMany(detail => detail.ReliefRequisition.ReliefRequisitionDetails).ToList();
            var filteredTrans = reliefRequisitionDetail.Where(d => d.FDP.AdminUnit.AdminUnitID == woredaId).Select(s => s.RequisitionDetailID).ToList();


            var x = _transReqWithoutTransporterService.FindBy(m => filteredTrans.Contains(m.RequisitionDetailID) && m.IsAssigned == false);// &&
                                                                         //  m.ReliefRequisitionDetail != null && (m.IsAssigned == false)).OrderByDescending(t => t.TransportRequisitionDetailID));
            //}
            //var transReqWithoutTransport = _transReqWithoutTransporterService.Get(m => m.ReliefRequisitionDetail != null && (m.IsAssigned == false && m.ReliefRequisitionDetail.FDP.AdminUnit.AdminUnitID == woredaId && m.TransportRequisitionDetail.TransportRequisition.TransportRequisitionID == transReqId)).Distinct().OrderByDescending(t => t.TransportRequisitionDetailID);
            req.TransReqwithOutTransporters = GetTransReqWithoutTransporter(x).ToList();
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
            var changedTransportOrderDetails = changedTransportOrderObj.TransportOrderDetails;
            var uniqueWoredas = changedTransportOrderDetails.Select(changedTransportOrderDetail => changedTransportOrderDetail.FDP.AdminUnitID).ToList().Distinct();
            //substituteTransporterOrder.WoredaID = changedTransportOrderDetails.First().FDP.AdminUnitID;
            //var uniqueWoredas = changedTransportOrderDetails.Where(changedTransportOrderDetail => changedTransportOrderDetail.BidID != null).ToDictionary(changedTransportOrderDetail => changedTransportOrderDetail.FDP.AdminUnitID, changedTransportOrderDetail => changedTransportOrderDetail.BidID != null ? (int) changedTransportOrderDetail.BidID : 0);

            var substituteTransportersStanding = (from uniqueWoreda in uniqueWoredas
                                                  let woreda = uniqueWoreda
                                                  let changedTransporterPostition =_bidQuotationService.Get(t =>t.TransporterID == changedTransportOrderObj.TransporterID && t.DestinationID == woreda).Select(t => t.Position).FirstOrDefault()
                                                  let woredaWinnersList = _bidQuotationService.GetSecondWinner(changedTransportOrderObj.TransporterID, woreda)// _bidQuotationService.Get(t => t.DestinationID == woreda && t.Position >= changedTransporterPostition && t.TransporterID != changedTransportOrderObj.TransporterID).ToList().OrderBy(t => t.Position)
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

            var orderDetails = new List<TransportOrderDetail>();
            var weightPref = UserAccountHelper.GetUser(User.Identity.Name).PreferedWeightMeasurment;
            var changedTransportOrderObj = _transportOrderService.FindById(changedTransportOrderID);
            var returnedObj = new List<TransportOrder>();
            var tempContratNo = string.Empty;
               foreach (var subTransporterOrders in listOfSubTransporterOrders)
            {
                if (subTransporterOrders.TransportersStandingList.All(t => t.IsChecked == false))
                    break; // No transporter is checked
                var filteredTransporterList = from transporters in subTransporterOrders.TransportersStandingList
                                               where transporters.IsChecked
                                               select transporters;

                var transportBidQuotationViewModels = filteredTransporterList as List<TransportBidQuotationViewModel> ?? filteredTransporterList.ToList();
                var transporterCount = transportBidQuotationViewModels.Count();

                foreach (var transporter in transportBidQuotationViewModels)
                {
                    changedTransportOrderObj = _transportOrderService.FindById(changedTransportOrderID);
                    var transporterObj = _transporterService.FindById(transporter.TransporterID);
                    //var transportOrder =
                    //    _transportOrderService.Get(t => t.TransporterID == transporterObj.TransporterID && 
                    //                                    t.StatusID == (int)TransportOrderStatus.Draft).
                    //        FirstOrDefault();

                    SubstituteTransporterOrder orders = subTransporterOrders;
                    TransportOrder transportOrderOld = null;

                    var transportOrder = _transportOrderService.FindBy(t=>t.TransporterID == transporterObj.TransporterID && t.StatusID == (int) TransportOrderStatus.Draft).Distinct();
                    var transportOrders = transportOrder as List<TransportOrder> ?? transportOrder.ToList();
                    foreach (var order in transportOrders)
                    {
                        if (tempContratNo!=string.Empty)
                        {
                            if (_transportOrderService.GetTransportRequisitionNo(order.ContractNumber) == _transportOrderService.GetTransportRequisitionNo(tempContratNo))
                          {
                              transportOrderOld = order;
                          }
                        }
                        //else if (_transportOrderService.GetTransportRequisitionNo(order.ContractNumber) == _transportOrderService.GetTransportRequisitionNo(changedTransportOrderObj.ContractNumber))
                        //{
                        //    transportOrderOld = order;
                        //}
                    }
                    if (transportOrderOld != null)
                    {
                        foreach (var transportOrderDetail in changedTransportOrderObj.TransportOrderDetails.ToList())
                        {
                            if (transportOrderDetail.FDP.AdminUnitID == subTransporterOrders.WoredaID)
                            {
                                //var qty = weightPref == "QTL"
                                //                  ? transportOrderDetail.QuantityQtl.ToMetricTone()/transporterCount
                                //                  : transportOrderDetail.QuantityQtl/transporterCount;

                                var transportOrderDetailObj = new TransportOrderDetail
                                {
                                    CommodityID = transportOrderDetail.CommodityID,
                                    FdpID = transportOrderDetail.FdpID,
                                    RequisitionID = transportOrderDetail.RequisitionID,
                                    QuantityQtl = transportOrderDetail.QuantityQtl / transporterCount,
                                    TariffPerQtl = transportOrderDetail.TariffPerQtl,
                                    SourceWarehouseID = transportOrderDetail.Hub.HubID,
                                    BidID = transportOrderDetail.BidID
                                    //transportOrderDetailObj.ZoneID = transportOrderDetail.ReliefRequisition.ZoneID;
                                };
                                _transportOrderService.UpdateTransporterOrder(transportOrderDetail.TransportOrderID,subTransporterOrders.WoredaID);
                                transportOrderOld.TransportOrderDetails.Add(transportOrderDetailObj);
                                _transportOrderService.EditTransportOrder(transportOrderOld);
                                orderDetails.Add(transportOrderDetailObj);
                            }
                        }
                        //_transportOrderService.DeleteTransportOrderDetails(orderDetails); // Delete details from the previous TO
                        returnedObj.Add(transportOrderOld);
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
                        //var transporterName = _transporterService.FindById(transportOrderObj.TransporterID).Name;
                        transportOrderObj.TransportOrderNo = string.Format("TRN-ORD-{0}", transportOrderObj.TransportOrderID);
                        transportOrderObj.ContractNumber = string.Format("{0}/{1}",changedTransportOrderObj.ContractNumber,"N");
                        tempContratNo = transportOrderObj.ContractNumber;
                        _transportOrderService.EditTransportOrder(transportOrderObj);
                        //var transportOrderDetailList = subTransporterOrders.TransportOrderDetails;
                        foreach (var transportOrderDetail in changedTransportOrderObj.TransportOrderDetails.ToList())
                        {
                            if (transportOrderDetail.FDP.AdminUnitID == subTransporterOrders.WoredaID)
                            {
                                var qty =
                                    _transportOrderService.CheckIfCommodityIsDipatchedToThisFdp(
                                        transportOrderDetail.FdpID, transportOrderDetail.TransportOrder.BidDocumentNo,
                                        transportOrderDetail.TransportOrder.TransporterID,
                                        transportOrderDetail.TransportOrderID,transportOrderDetail.CommodityID);

                                if (qty<=0)
                                {
                                   continue;
                                }
                                //var qty = weightPref == "QTL"
                                //                 ? transportOrderDetail.QuantityQtl.ToMetricTone() / transporterCount
                                //                 : transportOrderDetail.QuantityQtl / transporterCount;

                                var transportOrderDetailObj = new TransportOrderDetail
                                    {
                                        TransportOrderID = transportOrderObj.TransportOrderID,
                                        CommodityID = transportOrderDetail.CommodityID,
                                        FdpID = transportOrderDetail.FdpID,
                                        RequisitionID = transportOrderDetail.RequisitionID,
                                        QuantityQtl = qty,
                                        TariffPerQtl = transportOrderDetail.TariffPerQtl,
                                        SourceWarehouseID = transportOrderDetail.Hub.HubID,
                                        BidID = transportOrderDetail.BidID
                                    };
                                _transportOrderService.UpdateTransporterOrder(transportOrderDetail.TransportOrderID, subTransporterOrders.WoredaID);
                                //transportOrderDetail.ZoneID = reliefRequisitionDetail.ReliefRequisition.ZoneID;
                                _transportOrderDetailService.AddTransportOrderDetail(transportOrderDetailObj);
                            }
                        }
                        
                        returnedObj.Add(transportOrderObj);

                    }
                }
            }
            //changedTransportOrderObj.StatusID = (int)TransportOrderStatus.Failed;
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
                            RequisitionNo = detail.ReliefRequisition.RequisitionNo,
                            WinnerAssignedByLogistics = detail.WinnerAssignedByLogistics
                           // Donor=detail.Donor.Name
	

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
               var transportOrderId =  _transportOrderService.ReAssignTransporter(selectedTransRequision, requisitionWithTransporter.SelectedTransporterID);

               return RedirectToAction("OrderDetail", new { id = transportOrderId });
            }
            catch (Exception ex)
            {
                var log = new Logger();
                log.LogAllErrorsMesseges(ex, _log);
                ModelState.AddModelError("Errors", @"Unable to create transport order");
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
                    if (transportOrderDetail.TariffPerQtl <= 0 && transportOrderDetail.WinnerAssignedByLogistics != true)
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
                ModelState.AddModelError("Errors", @"Unable to approve");
            }
           
            return RedirectToAction("Index");
        }
        public ActionResult Signed(int id)
        {
            var transportOrder = _transportOrderService.FindById(id);
            int orderDetailWithoutTarrif = 0;
            try
            {
                foreach (var transportOrderDetail in transportOrder.TransportOrderDetails)
                {
                    if (transportOrderDetail.TariffPerQtl <= 0 && (transportOrderDetail.TransportOrder.Transporter.TransporterID != 7 && transportOrderDetail.TransportOrder.Transporter.TransporterID != 26)) // DRMFSS (7) and Emergency (26) are allowed be signed without tarrif
                    {
                        orderDetailWithoutTarrif = 1;
                        break;
                    }
                }
                if (orderDetailWithoutTarrif == 0)
                {


                    _transportOrderService.SignTransportOrder(transportOrder);
                    return RedirectToAction("Index");
                }
                TempData["CustomError"] = "Transport Order Without Tariff can not be signed! Please Specify Tariff for each transport order detail! ";
                return RedirectToAction("Index");
                //ModelState.AddModelError("Errors", @"Transport Order Without Tariff can not be approved!");
            }
            catch (Exception ex)
            {
                var log = new Logger();
                log.LogAllErrorsMesseges(ex, _log);
                ModelState.AddModelError("Errors", @"Unable to sign");
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
                    ModelState.AddModelError("Errors", @"Unable to generate dispatch allocation.");
                    return RedirectToAction("Index", "TransportOrder", new { id = (int)TransportOrderStatus.Signed, Area = "Hub" });
                }
            }
            catch (Exception ex)
            {
                 var log = new Logger();
                log.LogAllErrorsMesseges(ex, _log);
                ModelState.AddModelError("Errors", @"Unable to generate dispatch allocation.");
               
            }
            return RedirectToAction("Index", "TransportOrder", new { id = (int)TransportOrderStatus.Signed, Area = "Hub" });
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
        public ActionResult MultipleApproval()
        {
            var draftTransportOrders = _transportOrderService.FindBy(m => m.StatusID == (int) TransportOrderStatus.Draft);
            if(draftTransportOrders.Count==0)
            {
                TempData["CustomError"] = "There are no draft Transport Orders to be Approved! ";
                return RedirectToAction("Index");
            }
            var transportOrderModels = GetTransportOrderApprovalViewModel(draftTransportOrders);
            return View(transportOrderModels.ToList());
        }
        [HttpPost]
        public ActionResult MultipleApproval(List<TransportOrderApprovalViewModel> transportOrderApprovalViewModels)
        {
            var checkedTransportOrders = transportOrderApprovalViewModels.Where(m => m.Checked == true).ToList();
            if (checkedTransportOrders.Count() != 0)
            {
                var noOfApproval = 0;
                foreach (var chekedTransportOrder in checkedTransportOrders)
                {
                    var transportOrder = _transportOrderService.FindById(chekedTransportOrder.TransportOrderID);
                    if (transportOrder != null)
                    {
                        var orderDetailWithoutTarrif = 0;
                        
                        foreach (var transportOrderDetail in transportOrder.TransportOrderDetails)
                        {

                            if (transportOrderDetail.TariffPerQtl <= 0 &&
                                transportOrderDetail.WinnerAssignedByLogistics != true)
                            {
                                orderDetailWithoutTarrif = 1;
                                break;
                            }
                        }
                        if (orderDetailWithoutTarrif == 0)
                        {
                            _transportOrderService.ApproveTransportOrder(transportOrder);
                            ++noOfApproval;
                        }
                    }
                }
                if (noOfApproval < 1)
                {
                    TempData["CustomError"] = "Transport Order Without Tariff can not be approved! Please Specify Tariff for each transport order detail! ";
                    return RedirectToAction("Index");
                }
                TempData["CustomError2"] = noOfApproval + " Transport Order(s) has been successfully Approved! ";
                return RedirectToAction("Index");
            }
             TempData["CustomError"] = "No Transport Order has been approved! ";
            return RedirectToAction("Index");
        }
        
        private IEnumerable<TransportOrderApprovalViewModel> GetTransportOrderApprovalViewModel(IEnumerable<TransportOrder> transportOrders)
        {
            return (from detail in transportOrders
                    select new TransportOrderApprovalViewModel()
                    {
                        TransportOrderID = detail.TransportOrderID,
                        TransportOrderNo = detail.TransportOrderNo,
                        ContractNumber = detail.ContractNumber,
                        Transporter = detail.Transporter.Name,
                        Checked = false
                        // Donor=detail.Donor.Name
                    });
        }
        public ActionResult Revert(int id)
        {
            if (_transportOrderService.RevertRequsition(id))
            {
               
                TempData["CustomError2"] = "Transport Order Successfully Reverted back to Approved Requsitions!  ";
                return RedirectToAction("Index");
            }
            TempData["CustomError"] = "This Transport Order Can not be Reverted!  ";
            return RedirectToAction("Index");
        }
        public ActionResult RevertRequsitions()
        {
            var requisitions = _transportOrderService.GetRequsitionsToBeReverted().ToList();
            if (requisitions.Count!=0)
            {
                var nonDispatchedRequsitions = GetRequsitionsViewModel(requisitions).ToList();
                return View(nonDispatchedRequsitions);
            }
            TempData["CustomError"] = "This Transport Order Can not be Reverted!  ";
            return RedirectToAction("Index",new {Area="Logistics"});
        }
         [HttpPost]
         public ActionResult RevertRequsitions(List<RevertRequsitionsViewModel> revertRequsitionsViewModels)
         {
            var checkedRequsitions = revertRequsitionsViewModels.Where(m => m.Checked).ToList();
            if (checkedRequsitions.Count() != 0)
            {
                var noRevertedRequisitios = 0;
                foreach (var revertRequsitionsViewModel in checkedRequsitions)
                {
                    if (_transportOrderService.RevertRequsition(revertRequsitionsViewModel.RequsitionID))
                    {
                        ++noRevertedRequisitios;
                       
                    }
                }
                if (noRevertedRequisitios!=0)
                {
                    TempData["CustomError2"] =noRevertedRequisitios + "Requisition (s) Successfully Reverted back to Approved Requsitions!  ";
                    return RedirectToAction("Index");
               
                }
                
            }
            TempData["CustomError"] = "Requsition (s) Can not be Reverted!  ";
            return RedirectToAction("Index");
         }
        private IEnumerable<RevertRequsitionsViewModel> GetRequsitionsViewModel(IEnumerable<ReliefRequisition> reliefRequisitions)
        {
            return (from requsition in reliefRequisitions
                    select new RevertRequsitionsViewModel()
                    {
                        RequsitionID = requsition.RequisitionID,
                        RequsitionNumber = requsition.RequisitionNo,
                        Zone = requsition.AdminUnit1.Name,
                        Checked = false
                       
                    });
        }

        public ActionResult reverseTOFromClosedtoDraft(int id)
        {
            var dispatch = _transportOrderService.ReverseDispatchAllocation(id);
            if (dispatch.Count > 0)
               return PartialView(dispatch);
            return RedirectToAction("Index");



        }
    }
}
