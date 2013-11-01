using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Logistics.Models;
using Cats.Areas.Procurement.Models;
using Cats.Helpers;
using Cats.Infrastructure;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Models.ViewModels;
using Cats.Services.EarlyWarning;
using Cats.Services.Logistics;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Cats.Services.Security;
using log4net;

namespace Cats.Areas.Logistics.Controllers
{
    public class TransportRequisitionController : Controller
    {
        private readonly ITransportRequisitionService _transportRequisitionService;
        private readonly IWorkflowStatusService _workflowStatusService;
        private readonly IUserAccountService _userAccountService;
        private readonly ILog _log;
        private readonly IHubAllocationService _hubAllocationService;
        private readonly IProjectCodeAllocationService _projectCodeAllocationService;
        
        public TransportRequisitionController(ITransportRequisitionService transportRequisitionService,
            IWorkflowStatusService workflowStatusService,
            IUserAccountService userAccountService,
            ILog log,
            IHubAllocationService hubAllocationService,
            IProjectCodeAllocationService projectCodeAllocationService)
        {
            this._transportRequisitionService = transportRequisitionService;
            _workflowStatusService = workflowStatusService;
            _userAccountService = userAccountService;
            _log = log;
            _hubAllocationService = hubAllocationService;
            _projectCodeAllocationService = projectCodeAllocationService;
        }
        //
        // GET: /Logistics/TransportRequisition/

        public ActionResult Index()
        {
            
            return View();

        }

        public ActionResult TransportRequisition_Read([DataSourceRequest] DataSourceRequest request)
        {
            
            var transportRequisitions = _transportRequisitionService.GetAllTransportRequisition();
            var transportRequisitonViewModels =
                (from itm in transportRequisitions select BindTransportRequisitionViewModel(itm));
            return Json(transportRequisitonViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        
        private TransportRequisitionViewModel BindTransportRequisitionViewModel(TransportRequisition transportRequisition)
        {
            string userPreference = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;

            TransportRequisitionViewModel transportRequisitionViewModel=null;
            if(transportRequisition !=null)
            {
                transportRequisitionViewModel = new TransportRequisitionViewModel();
                transportRequisitionViewModel.CertifiedBy = transportRequisition.CertifiedBy;
                transportRequisitionViewModel.CertifiedDate = transportRequisition.CertifiedDate;
                transportRequisitionViewModel.DateCertified = transportRequisition.CertifiedDate.ToCTSPreferedDateFormat(userPreference);
                //EthiopianDate.GregorianToEthiopian(transportRequisition.CertifiedDate);
                transportRequisitionViewModel.Remark = transportRequisition.Remark;
                transportRequisitionViewModel.RequestedBy = transportRequisition.RequestedBy;
                transportRequisitionViewModel.RequestedDate = transportRequisition.RequestedDate;
                transportRequisitionViewModel.DateRequested = transportRequisition.RequestedDate.ToCTSPreferedDateFormat(userPreference);
                //EthiopianDate.GregorianToEthiopian( transportRequisition.RequestedDate);
                transportRequisitionViewModel.Status = _workflowStatusService.GetStatusName(WORKFLOW.TRANSPORT_REQUISITION,transportRequisition.Status);
                transportRequisitionViewModel.StatusID = transportRequisition.Status;
                transportRequisitionViewModel.TransportRequisitionID = transportRequisition.TransportRequisitionID;
                transportRequisitionViewModel.TransportRequisitionNo = transportRequisition.TransportRequisitionNo;
            }

            return transportRequisitionViewModel;
        }
        private TransportRequisitionDetailViewModel BindTransportRequisitionDetailViewModel(RequisitionToDispatch requisitionToDispatch)
        {
            TransportRequisitionDetailViewModel transportRequisitionDetailViewModel = null;
            if (requisitionToDispatch != null)
            {
                transportRequisitionDetailViewModel = new TransportRequisitionDetailViewModel();
                transportRequisitionDetailViewModel.CommodityID = requisitionToDispatch.CommodityID;
                transportRequisitionDetailViewModel.CommodityName = requisitionToDispatch.CommodityName;
                transportRequisitionDetailViewModel.HubID =requisitionToDispatch.HubID;
                transportRequisitionDetailViewModel.OrignWarehouse = requisitionToDispatch.OrignWarehouse;
                transportRequisitionDetailViewModel.QuanityInQtl = requisitionToDispatch.QuanityInQtl;
                transportRequisitionDetailViewModel.Region = requisitionToDispatch.RegionName;
                transportRequisitionDetailViewModel.Zone = requisitionToDispatch.Zone;
                transportRequisitionDetailViewModel.RequisitionNo = requisitionToDispatch.RequisitionNo;



            }
            return transportRequisitionDetailViewModel;
        }

        private List<TransportRequisitionDetailViewModel> GetDetail(IEnumerable<TransportRequisitionDetail> transportRequisitionDetails )
        {
            var requIds = (from itm in transportRequisitionDetails select itm.RequisitionID).ToList();
            var temp = _transportRequisitionService.GetTransportRequisitionDetail(requIds);
            var result = (from itm in temp select BindTransportRequisitionDetailViewModel(itm));
            return result.ToList();
        }

        [HttpGet]
        public ActionResult Requisitions()
        {
            var assignedRequisitions = _transportRequisitionService.GetRequisitionToDispatch();
            var transportReqInput = (from item in assignedRequisitions
                                     select new RequisitionToDispatchSelect()
                                     {
                                         CommodityName = item.CommodityName,
                                         CommodityID = item.CommodityID,
                                         HubID = item.HubID,
                                         OrignWarehouse = item.OrignWarehouse,
                                         QuanityInQtl = item.QuanityInQtl,
                                         RegionID = item.RegionID,
                                         RegionName = item.RegionName,
                                         RequisitionID = item.RequisitionID,
                                         RequisitionNo = item.RequisitionNo,
                                         ZoneID = item.ZoneID,
                                         Zone = item.Zone,
                                         RequisitionStatusName = item.RequisitionStatusName,
                                         RequisitionStatus = item.RequisitionStatus,
                                         
                                         Input = new RequisitionToDispatchSelect.RequisitionToDispatchSelectInput
                                         {
                                             Number = item.RequisitionID,
                                             IsSelected = false
                                         }

                                     });

            var  result1 = new List<RequisitionToDispatchSelect>();
            try
            {
                result1=transportReqInput.ToList();
            }
            catch (Exception exception)
            {
                var log = new Logger();
                log.LogAllErrorsMesseges(exception, _log);
                //TODO: modelstate should be put
            }
            
            return View(result1);


          
        }
        [HttpGet]
        public ActionResult Requisitions1()
        {
            var assignedRequisitions = _transportRequisitionService.GetRequisitionToDispatch();
            var transportReqInput = (from item in assignedRequisitions
                                     select new RequisitionToDispatchSelect()
                                     {
                                         CommodityName = item.CommodityName,
                                         CommodityID = item.CommodityID,
                                         HubID = item.HubID,
                                         OrignWarehouse = item.OrignWarehouse,
                                         QuanityInQtl = item.QuanityInQtl,
                                         RegionID = item.RegionID,
                                         RegionName = item.RegionName,
                                         RequisitionID = item.RequisitionID,
                                         RequisitionNo = item.RequisitionNo,
                                         ZoneID = item.ZoneID,
                                         Zone = item.Zone,
                                         RequisitionStatusName = item.RequisitionStatusName,
                                         RequisitionStatus = item.RequisitionStatus,
                                         Input = new RequisitionToDispatchSelect.RequisitionToDispatchSelectInput
                                         {
                                             Number = item.RequisitionID,
                                             IsSelected = false
                                         }


                                     });

            var result1 = new List<RequisitionToDispatchSelect>();
            try
            {
                result1 = transportReqInput.ToList();
            }
            catch (Exception exception)
            {

                var log = new Logger();
                log.LogAllErrorsMesseges(exception, _log);
                //TODO: modelstate should be put
            }

            return Json(result1.AsQueryable(),JsonRequestBehavior.AllowGet);
        }
       
        [HttpPost]
        public ActionResult Requisitions(IList<SelectFromGrid> input)
        {
           
            var requisionIds = (from item in input where (item.IsSelected !=null ?((string[])item.IsSelected)[0]: "off")=="on"    select item.Number).ToList();
            return CreateTransportRequisition(requisionIds);

        }

        public ActionResult CreateTransportRequisition(List<int> requisitionToDispatches)
        {
           var transportRequisition=  _transportRequisitionService.CreateTransportRequisition(requisitionToDispatches);
            return RedirectToAction("Edit", "TransportRequisition",new {id=transportRequisition.TransportRequisitionID});
        }

        public ActionResult Edit(int id)
        {
            var transportRequisition = _transportRequisitionService.FindById(id);
            if(transportRequisition ==null)
            {
                return HttpNotFound();
            }
            return View(transportRequisition);
        }
        [HttpPost]
        public ActionResult Edit(TransportRequisition transportRequisition)
        {
          
            if (!ModelState.IsValid)
            {
                return View(transportRequisition);
            }
            return RedirectToAction("Index","TransportRequisition");
        }

        public ActionResult Approve(int id)
        {
            var transportRequisition = _transportRequisitionService.FindById(id);
            if (transportRequisition == null)
            {
                return HttpNotFound();
            }
            return View(transportRequisition);
        }
        [HttpPost]
        public ActionResult ApproveConfirmed(int TransportRequisitionID)
        {
            _transportRequisitionService.ApproveTransportRequisition(TransportRequisitionID);
           
            return RedirectToAction("Index", "TransportRequisition");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var transportRequisition = _transportRequisitionService.FindById(id);
            var transportRequisitonViewModel = BindTransportRequisitionViewModel(transportRequisition);
            ViewData["Transport.Requisition.detail.ViewModel"] =
                GetDetail(transportRequisition.TransportRequisitionDetails.ToList());
            return View(transportRequisitonViewModel);
        }

        public ActionResult PrintSummary(int id)
        {
            var reportPath = Server.MapPath("~/Report/Logisitcs/TransportRequisitionSummary.rdlc");
            var transportR = _transportRequisitionService.FindBy(t=>t.TransportRequisitionID==id);
            var transportRequisition = _transportRequisitionService.FindById(id);
            var details = GetDetail(transportRequisition.TransportRequisitionDetails.ToList());

            var header = (from requisition in transportR
                         select new
                            {
                                TransportRequisitionID=requisition.TransportRequisitionNo,
                                requisition.Remark,
                                DateRequested=requisition.RequestedDate,
                                DateRecieved = requisition.CertifiedDate,
                                requisition.RequestedBy,
                                requisition.CertifiedBy
                            });

            var requisitionsSummary =
                (from transportRequisitionDetail in details
                 select new
                     {
                         Commodity = transportRequisitionDetail.CommodityName,
                         RequisitionNumber = transportRequisitionDetail.RequisitionNo,
                         Amount = transportRequisitionDetail.QuanityInQtl,
                         Warehouse = transportRequisitionDetail.OrignWarehouse,
                         Region = transportRequisitionDetail.Region,
                         Zone = transportRequisitionDetail.Zone,
                         Donor = "WFP",
                         transportRequisitionDetail.RequisitionID,
                         fromSIPC = (from projectCodeAllocation in _projectCodeAllocationService.FindBy(p => p.HubAllocationID == 3/*_hubAllocationService.GetAllocatedHubId(transportRequisitionDetail.RequisitionID)*/)
                                     select new
                                         {
                                             CommodityName = transportRequisitionDetail.CommodityName,
                                             fromSIPC = projectCodeAllocation.Amount_FromSI,
                                             //fromSIPCKEy = projectCodeAllocation.ProjectCodeAllocationID
                                         }
                                     )
                     }
                );

            var detailsT =
                        (
                            from requisition in requisitionsSummary
                            select requisition.fromSIPC
                        );

           //foreach (var detail in details)
           //{
           //    var hubAllocationID = _hubAllocationService.GetAllocatedHubId(detail.RequisitionID);
           //    var projectCodeAllocations = _projectCodeAllocationService.FindBy(p=>p.HubAllocationID==hubAllocationID);
           //    var ds = from projectCodeAllocation in projectCodeAllocations
           //             select new
           //             {
           //                 CommodityName = detail.CommodityName,
           //                 fromSIPC = projectCodeAllocation.Amount_FromSI,
           //                 fromSIPCKEy = projectCodeAllocation.ProjectCodeAllocationID
           //             }
           //}
         /*
          var detail =
               (from projectCodeAllocation in _projectCodeAllocationService.FindBy(
                        p =>
                        p.HubAllocationID ==
                        _hubAllocationService.GetAllocatedHubId(transportRequisitionDetail.RequisitionID))
                select new
                    {
                        CommodityName = transportRequisitionDetail.CommodityName,
                        fromSIPC = projectCodeAllocation.Amount_FromSI,
                        fromSIPCKEy = projectCodeAllocation.ProjectCodeAllocationID
                    }
               );

           //var data = (from requisition in transportRequisition
           //            select new
           //                {
           //                    requisition.TransportRequisitionID,
           //                    requisition.Remark,
           //                    requisition.RequestedDate,
           //                    requisition.CertifiedDate,
           //                    requisition.RequestedBy,
           //                    requisition.CertifiedBy,
           //                    requisitionsSummary = (from transportRequisitionDetail in GetDetail(requisition.TransportRequisitionDetails.ToList())
           //                                           select new
           //                                               {
           //                                                   Commodity = transportRequisitionDetail.CommodityName,
           //                                                   RequisitionNumber = transportRequisitionDetail.RequisitionNo,
           //                                                   Amount = transportRequisitionDetail.QuanityInQtl,
           //                                                   Warehouse = transportRequisitionDetail.OrignWarehouse,
           //                                                   Region = transportRequisitionDetail.Region,
           //                                                   Zone = transportRequisitionDetail.Zone,
           //                                                   Donor = "WFP",
           //                                                   RecievedDate = DateTime.Now,
           //                                                   DateofRequisition = DateTime.Now,
           //                                                   fromSIPC = (from projectCodeAllocation in _projectCodeAllocationService.FindBy(p => p.HubAllocationID == _hubAllocationService.GetAllocatedHubId(transportRequisitionDetail.RequisitionID))
           //                                                               select new
           //                                                                   {
           //                                                                       CommodityName = transportRequisitionDetail.CommodityName,
           //                                                                       fromSIPC = projectCodeAllocation.Amount_FromSI,
           //                                                                       fromSIPCKEy = projectCodeAllocation.ProjectCodeAllocationID
           //                                                                   }
           //                                                              )
           //                                               }
           //                                          )

           //                }
           //           );

           

           var reportData = (from detail in details
                             select new
                             {
                                 Commodity = detail.CommodityName,
                                 RequisitionNumber = detail.RequisitionNo,
                                 Amount = detail.QuanityInQtl,
                                 Warehouse = detail.OrignWarehouse,
                                 Region = detail.Region,
                                 Zone= detail.Zone,
                                 Donor = "WFP",
                                 RecievedDate= DateTime.Now,
                                 DateofRequisition = DateTime.Now,
                                 Remark = transportRequisition.Remark,
                                 RequestedBy = transportRequisition.RequestedBy,
                                 CertifiedBy = transportRequisition.CertifiedBy,
                                 //fromSIPCAllocations = detail.
                             });*/

            //var dataSourceName = "RequisitionsSummary";
            //var dataSourceName2 = "Header";
            //var dataSourceName3 = "details";

            var dataSources = new string[3];
            // dataSources[0] = "";
            dataSources[0] = "Header";
            dataSources[1] = "RequisitionsSummary";
            dataSources[2] = "details";

            var reportData = new IEnumerable[3];
            reportData[0] = header;
            reportData[1] = requisitionsSummary;
            reportData[2] = detailsT;

            var result = ReportHelper.PrintReport(reportPath, reportData, dataSources);
            return File(result.RenderBytes, result.MimeType);
        }
    }
}