using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Models;
using Cats.Areas.Logistics.Models;
using Cats.Areas.Procurement.Models;
using Cats.Helpers;
using Cats.Infrastructure;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Models.ViewModels;
using Cats.Services.Administration;
using Cats.Services.Common;
using Cats.Services.EarlyWarning;
using Cats.Services.Logistics;
using Cats.ViewModelBinder;
using Cats.Security;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Cats.Services.Security;
using log4net;
using IAdminUnitService = Cats.Services.EarlyWarning.IAdminUnitService;
using IProgramService = Cats.Services.EarlyWarning.IProgramService;
using System.Collections.ObjectModel;

namespace Cats.Areas.Logistics.Controllers
{
    [Authorize]
    public class TransportRequisitionController : Controller
    {
        private readonly ITransportRequisitionService _transportRequisitionService;
        private readonly IWorkflowStatusService _workflowStatusService;
        private readonly IUserAccountService _userAccountService;
        private readonly ILog _log;
        private readonly IAdminUnitService _adminUnitService;
        private readonly IProgramService _programService;
        private readonly IHubAllocationService _hubAllocationService;
        private readonly IProjectCodeAllocationService _projectCodeAllocationService;
        private readonly IReliefRequisitionService _reliefRequisitionService;
        private readonly IReliefRequisitionDetailService _reliefRequisitionDetailService;
        private readonly IRationService _rationService;
        private readonly INotificationService _notificationService;
        //private readonly IUserProfileService _userProfileService;
        
        public TransportRequisitionController(
            ITransportRequisitionService transportRequisitionService,
            IWorkflowStatusService workflowStatusService,
            IUserAccountService userAccountService,
            ILog log, 
            IAdminUnitService adminUnitService,
            IProgramService programService,
            IReliefRequisitionService reliefRequisitionService,
            IHubAllocationService hubAllocationService,
            IProjectCodeAllocationService projectCodeAllocationService,
            IReliefRequisitionDetailService reliefRequisitionDetailService,
            IRationService rationService, INotificationService notificationService)
        
            {
            this._transportRequisitionService = transportRequisitionService;
            _workflowStatusService = workflowStatusService;
            _userAccountService = userAccountService;
            _log = log;
            _adminUnitService = adminUnitService;
            _programService = programService;
            _reliefRequisitionService = reliefRequisitionService;
            _hubAllocationService = hubAllocationService;
            _projectCodeAllocationService = projectCodeAllocationService;
            _reliefRequisitionDetailService = reliefRequisitionDetailService;
            _reliefRequisitionService = reliefRequisitionService;
            _rationService = rationService;
            _notificationService = notificationService;
            //_userProfileService = userProfileService;
    }
        //
        // GET: /Logistics/TransportRequisition/

        public ActionResult Index(int id = -1)
        {
            ViewBag.Status = id;
            return View();

        }
        public ActionResult TransportRequisition_Read([DataSourceRequest] DataSourceRequest request, string searchIndex,int status)
        {
            var transportRequisitions = status ==-1 ? _transportRequisitionService.Get(t => t.TransportRequisitionNo.Contains(searchIndex)):
                _transportRequisitionService.Get(t=>t.TransportRequisitionNo.Contains(searchIndex) && t.Status==(int)status).OrderByDescending(t=>t.TransportRequisitionID);
            var statuses = _workflowStatusService.GetStatus(WORKFLOW.TRANSPORT_REQUISITION);
            var users = _userAccountService.GetUsers();
            var transportRequisitonViewModels =
                (from itm in transportRequisitions select BindTransportRequisitionViewModel(itm));
            return Json(transportRequisitonViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


        private TransportRequisitionViewModel BindTransportRequisitionViewModel(TransportRequisition transportRequisition)
        {
            string userPreference = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            
            TransportRequisitionViewModel transportRequisitionViewModel = null;
            if (transportRequisition != null)
            {
                var transportRequisitionObj =
                    _transportRequisitionService.FindById(transportRequisition.TransportRequisitionID);
                transportRequisitionViewModel = new TransportRequisitionViewModel();
                transportRequisitionViewModel.CertifiedBy = _userAccountService.FindById(transportRequisitionObj.CertifiedBy).FullName;
                transportRequisitionViewModel.CertifiedDate = transportRequisitionObj.CertifiedDate;
                transportRequisitionViewModel.DateCertified = transportRequisitionObj.CertifiedDate.ToCTSPreferedDateFormat(userPreference);
                //EthiopianDate.GregorianToEthiopian(transportRequisition.CertifiedDate);
                transportRequisitionViewModel.Remark = transportRequisitionObj.Remark;
                transportRequisitionViewModel.RequestedBy = _userAccountService.FindById(transportRequisitionObj.RequestedBy).FullName;
                transportRequisitionViewModel.RequestedDate = transportRequisitionObj.RequestedDate;
                transportRequisitionViewModel.DateRequested = transportRequisitionObj.RequestedDate.ToCTSPreferedDateFormat(userPreference);
                //EthiopianDate.GregorianToEthiopian( transportRequisition.RequestedDate);
                transportRequisitionViewModel.Status = _workflowStatusService.GetStatusName(WORKFLOW.TRANSPORT_REQUISITION, transportRequisitionObj.Status);
                transportRequisitionViewModel.StatusID = transportRequisitionObj.Status;
                transportRequisitionViewModel.TransportRequisitionID = transportRequisitionObj.TransportRequisitionID;
                transportRequisitionViewModel.TransportRequisitionNo = transportRequisitionObj.TransportRequisitionNo;
                transportRequisitionViewModel.Region = _adminUnitService.FindById(transportRequisitionObj.RegionID).Name;
                transportRequisitionViewModel.Program = _programService.FindById(transportRequisitionObj.ProgramID).Name;

            }
            return transportRequisitionViewModel;
        }

        private TransportRequisition BindTransportRequisition(TransportRequisitionViewModel transportRequisitionViewModel)
        {
            string userPreference = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;

            TransportRequisition transportRequisition = null;
            if (transportRequisitionViewModel != null)
            {
                transportRequisition = new TransportRequisition();
                var firstOrDefault = _userAccountService.FindBy(t => t.UserName == transportRequisitionViewModel.CertifiedBy).FirstOrDefault();
                if (firstOrDefault != null)
                    transportRequisition.CertifiedBy = firstOrDefault.UserProfileID;
                transportRequisition.CertifiedDate = transportRequisitionViewModel.CertifiedDate;
                transportRequisition.CertifiedDate = transportRequisitionViewModel.CertifiedDate;
                //EthiopianDate.GregorianToEthiopian(transportRequisition.CertifiedDate);
                transportRequisition.Remark = transportRequisition.Remark;

                var firstOrDefault1 = _userAccountService.FindBy(t => t.UserName == transportRequisitionViewModel.RequestedBy).FirstOrDefault();
                if (firstOrDefault1 != null)
                    transportRequisition.RequestedBy = firstOrDefault1.UserProfileID;

                transportRequisition.RequestedDate = transportRequisitionViewModel.RequestedDate;
                transportRequisition.RequestedDate = transportRequisitionViewModel.RequestedDate;
                //EthiopianDate.GregorianToEthiopian( transportRequisition.RequestedDate);
                transportRequisition.Status = transportRequisitionViewModel.StatusID;
                transportRequisition.TransportRequisitionID = transportRequisitionViewModel.TransportRequisitionID;
                transportRequisition.TransportRequisitionNo = transportRequisitionViewModel.TransportRequisitionNo;
                transportRequisition.RegionID = transportRequisitionViewModel.RegionID;
                transportRequisition.AdminUnit = _adminUnitService.FindById(transportRequisitionViewModel.RegionID);
                transportRequisition.ProgramID = transportRequisitionViewModel.ProgramID;
                transportRequisition.Program = _programService.FindById(transportRequisitionViewModel.ProgramID);

            }
            return transportRequisition;
        }

        
        private List<TransportRequisitionDetailViewModel> GetDetail(IEnumerable<TransportRequisitionDetail> transportRequisitionDetails )
        {
          
            var requIds = (from itm in transportRequisitionDetails select itm.RequisitionID).ToList();
            var temp = _transportRequisitionService.GetTransportRequisitionDetail(requIds);
            
            var result = TransportRequisitionViewModelBinder.BindListTransportRequisitionDetailViewModel(temp);
            return result.ToList();
        }

        //[HttpGet]
        //public ActionResult Requisitions()
        //{
        //    var assignedRequisitions = _transportRequisitionService.GetRequisitionToDispatch();
        //    var transportReqInput = (from item in assignedRequisitions
        //                             select new RequisitionToDispatchSelect()
        //                             {
        //                                 CommodityName = item.CommodityName,
        //                                 CommodityID = item.CommodityID,
        //                                 HubID = item.HubID,
        //                                 OrignWarehouse = item.OrignWarehouse,
        //                                 QuanityInQtl = item.QuanityInQtl,
        //                                 RegionID = item.RegionID,
        //                                 RegionName = item.RegionName,
        //                                 RequisitionID = item.RequisitionID,
        //                                 RequisitionNo = item.RequisitionNo,
        //                                 ZoneID = item.ZoneID,
        //                                 Zone = item.Zone,
        //                                 RequisitionStatusName = item.RequisitionStatusName,
        //                                 RequisitionStatus = item.RequisitionStatus,
                                         
        //                                 Input = new RequisitionToDispatchSelect.RequisitionToDispatchSelectInput
        //                                 {
        //                                     Number = item.RequisitionID,
        //                                     IsSelected = false
        //                                 }


        //                             });

        //    var  result1 = new List<RequisitionToDispatchSelect>();
        //    try
        //    {
        //        result1=transportReqInput.ToList();
        //    }
        //    catch (Exception exception)
        //    {
        //        var log = new Logger();
        //        log.LogAllErrorsMesseges(exception, _log);
        //        //TODO: modelstate should be put
        //    }
            
        //    return View(result1);


          
        //}
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
       
        //[HttpPost]
        //public ActionResult Requisitions(IList<SelectFromGrid> input)
        //{
           
        //    var requisionIds = (from item in input where (item.IsSelected !=null ?((string[])item.IsSelected)[0]: "off")=="on"    select item.Number).ToList();
        //    var req = new List<List<int>>();
        //    req.Add(requisionIds);
        //    return CreateTransportRequisition(req);

        //}
 
        public ActionResult CreateTransportRequisition(int regionId)
        {
            try

            {
                var requisitions = _reliefRequisitionService.FindBy(t => t.RegionID == regionId && t.Status == (int)ReliefRequisitionStatus.SiPcAllocationApproved);
                var programs = (from item in requisitions select item.ProgramID).Distinct().ToList();
                var requisitionToDispatches = new List<List<int>>();
                var currentUser = UserAccountHelper.GetUser(User.Identity.Name).UserProfileID;
                foreach (var program in programs)
                {
                    var requisitionToDispatche =
                        (from itm in requisitions where itm.ProgramID == program select itm.RequisitionID).ToList();
                    requisitionToDispatches.Add(requisitionToDispatche);

                }
                _transportRequisitionService.CreateTransportRequisition(requisitionToDispatches, currentUser);

                return RedirectToAction("Index", "TransportRequisition");//,new {id=(int)TransportRequisitionStatus.Draft});
            }
            catch (Exception exception)
            {
                var log = new Logger();
                log.LogAllErrorsMesseges(exception, _log);
                ModelState.AddModelError("",exception.Message);

                return RedirectToAction("Main", "DispatchAllocation");
                throw;
            }
          
        }

        //public ActionResult GenerateTransportRequisitionForRegion(int regionID)
        //{
        //    var reliefRequisitionslist = _reliefRequisitionService.Get(t => t.Status == (int)ReliefRequisitionStatus.ProjectCodeAssigned && t.RegionID == regionID, null,
        //                                                  "ReliefRequisitionDetails,Program,AdminUnit1,AdminUnit,Commodity");
        //    var uniquePrograms = new List<Program>();
        //    foreach (var uniqueProgram in from reliefRequisitionsID in reliefRequisitionslist let uniqueProgram = new Program()
        //                                  let programID = reliefRequisitionsID.ProgramID
        //                                  where programID != null
        //                                  select _programService.FindById((int)programID) 
        //                                  into uniqueProgram where !uniquePrograms.Contains(uniqueProgram) select uniqueProgram)
        //    {
        //        uniquePrograms.Add(uniqueProgram);
        //    }
        //    var transportRequisition = new TransportRequisition();
        //    foreach (var partitionedReliefRequisitiionIDList in uniquePrograms.Select(program1 => 
        //        _reliefRequisitionService.Get(t => t.Status == (int)ReliefRequisitionStatus.ProjectCodeAssigned && t.RegionID == regionID &&
        //        t.ProgramID == program1.ProgramID)).Select(partitionedReliefRequisitiions => partitionedReliefRequisitiions.Select(t => t.RequisitionID).ToList()))
        //    {
        //        transportRequisition  = _transportRequisitionService.CreateTransportRequisition(partitionedReliefRequisitiionIDList);
        //    }
        //    return RedirectToAction("Index", "TransportRequisition");
        //}

        public ActionResult Edit(int id)
        {
            var transportRequisition = _transportRequisitionService.FindById(id);
            if(transportRequisition ==null)
            {
                return HttpNotFound();
            }
            ViewBag.CertifiedBy = new SelectList(_userAccountService.GetAll(), "UserProfileID", "UserName", transportRequisition.CertifiedBy);
            ViewBag.RequestedBy = new SelectList(_userAccountService.GetAll(), "UserProfileID", "UserName", transportRequisition.RequestedBy);
            var transportRequisitionViewModel = BindTransportRequisitionViewModel(transportRequisition);
            return View(transportRequisitionViewModel);
        }
        [HttpPost]
        public ActionResult Edit(TransportRequisitionViewModel transportRequisitionViewModel)
        {
          
            if (!ModelState.IsValid)
            {
                return View(transportRequisitionViewModel);
            }
            //var transportRequisition = BindTransportRequisition(transportRequisitionViewModel);
            var transportRequisition = _transportRequisitionService.FindById(transportRequisitionViewModel.TransportRequisitionID);
            transportRequisition.CertifiedBy = int.Parse(transportRequisitionViewModel.CertifiedBy);
            transportRequisition.CertifiedDate = transportRequisitionViewModel.CertifiedDate;
            transportRequisition.RequestedBy = int.Parse(transportRequisitionViewModel.RequestedBy);
            transportRequisition.RequestedDate = transportRequisitionViewModel.RequestedDate;
            transportRequisition.TransportRequisitionNo = transportRequisitionViewModel.TransportRequisitionNo;
            transportRequisition.Remark = transportRequisitionViewModel.Remark;
            _transportRequisitionService.EditTransportRequisition(transportRequisition);
            return RedirectToAction("Index","TransportRequisition");
        }

        public ActionResult Approve(int id)
        {
            var transportRequisition = _transportRequisitionService.FindById(id);
            if (transportRequisition == null)
            {
                return HttpNotFound();
            }
            var transportRequisitionViewModel = BindTransportRequisitionViewModel(transportRequisition);
            return View(transportRequisitionViewModel);
        }
        public ActionResult ConfirmGenerateTransportOrder(int id)
        {
            ViewBag.RequisistionId = id;
            if (!_transportRequisitionService.CheckIfBidIsCreatedForAnOrder(id))
            {
                ViewBag.Msg = "Winner transporter is not created for this Requisition!";
            }
            var transportRequisition = _transportRequisitionService.FindById(id);
            if (transportRequisition == null)
            {
                return HttpNotFound();
            }
            var transportRequisitionViewModel = BindTransportRequisitionViewModel(transportRequisition);
            return View(transportRequisitionViewModel);
        }
        [HttpPost]
        public ActionResult ApproveConfirmed(int TransportRequisitionID)
        {
            var currentUser = UserAccountHelper.GetUser(User.Identity.Name).UserProfileID;
            _transportRequisitionService.ApproveTransportRequisition(TransportRequisitionID, currentUser);
           
            return RedirectToAction("Index", "TransportRequisition");
        }

        
            [HttpGet]
        //[LogisticsAuthorize(operation = LogisticsCheckAccess.Operation.Edit__transport_order)]

        public ActionResult Details(int id)
        {
            ViewBag.RequisistionId = id;

            var transportRequisitonViewModel = new TransportRequisitionViewModel();
            try
            {
            
            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var statuses = _workflowStatusService.GetStatus(WORKFLOW.TRANSPORT_REQUISITION);
            var users = _userAccountService.GetUsers();

            var transportRequisition = _transportRequisitionService.FindById(id);
            transportRequisitonViewModel = TransportRequisitionViewModelBinder.BindTransportRequisitionViewModel(transportRequisition, statuses, datePref, users);
            transportRequisitonViewModel.TransportRequisitionDetailViewModels =
            GetDetail(transportRequisition.TransportRequisitionDetails.ToList());

            foreach (var transportRequisitionDetailViewModel in transportRequisitonViewModel.TransportRequisitionDetailViewModels)
            {
                var count =
                    _reliefRequisitionDetailService.FindBy(
                        t => t.RequisitionID == transportRequisitionDetailViewModel.RequisitionID).Count;
                transportRequisitionDetailViewModel.DestinationsCount = count;
            }
            }

            catch(Exception ex)
            {
                var log = new Logger();
                log.LogAllErrorsMesseges(ex, _log);
                transportRequisitonViewModel.TransportRequisitionDetailViewModels = new List<TransportRequisitionDetailViewModel>();
              
                ViewBag.Error = "An error has occured: Check Detail.";
                ModelState.AddModelError("Errors", ViewBag.Error);
            }
               // Session["transport_requisiton_return_id"]=id;
            return View(transportRequisitonViewModel);
        }

        [HttpGet]
        //[LogisticsAuthorize(operation = LogisticsCheckAccess.Operation.Edit__transport_order)]
            public ActionResult Destinations(int id, int transportRequistionId)
        {
            ViewBag.RequisitionID = id;
            ViewBag.TransportRequisitonID = transportRequistionId;
            return View();
        }

        public ActionResult PrintSummary(int id)
        {
            var reportPath = Server.MapPath("~/Report/Logisitcs/TransportRequisitionSummary.rdlc");
            var transportR = _transportRequisitionService.FindBy(t=>t.TransportRequisitionID==id);

            var transportRequisitonViewModel = new TransportRequisitionViewModel();
            var headerInfo = new List<TransportRequisitionViewModel>();
            headerInfo.Add(transportRequisitonViewModel);

            var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var statuses = _workflowStatusService.GetStatus(WORKFLOW.TRANSPORT_REQUISITION);
            var users = _userAccountService.GetUsers();
            var transportRequisition = _transportRequisitionService.FindById(id);
            
            transportRequisitonViewModel = TransportRequisitionViewModelBinder.
                                            BindTransportRequisitionViewModel(
                                            transportRequisition,
                                            statuses,
                                            datePref,
                                            users
                                            );
            
            //transportRequisitonViewModel.TransportRequisitionDetailViewModels = GetDetail(transportRequisition.TransportRequisitionDetails.ToList());
            //var transportRequisition = _transportRequisitionService.FindById(id);
            
            var details = GetDetail(transportRequisition.TransportRequisitionDetails.ToList());
            var header = (from requisition in headerInfo
                         select new
                            {
                                //TransportRequisitionID=requisition.TransportRequisitionNo,
                                //requisition.Remark,
                                //DateRequested=requisition.RequestedDate,
                                //DateRecieved = requisition.CertifiedDate,
                                //requisition.RequestedBy,
                                //requisition.CertifiedBy
                                requisition.TransportRequisitionNo,
                                requisition.Remark,
                                DateRequested=requisition.RequestedDate,
                                DateRecieved = requisition.CertifiedDate,
                                RequestedBy = requisition.RequestedBy,
                                CertifiedBy = requisition.CertifiedBy
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
                         Program = transportRequisitionDetail.Program,
                         Donor = "WFP",
                         transportRequisitionDetail.RequisitionID,
                         fromSIPC = (from projectCodeAllocation in _projectCodeAllocationService.FindBy(p => p.HubAllocationID == _hubAllocationService.GetAllocatedHubId(transportRequisitionDetail.RequisitionID))
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
            reportData[0] = new Collection<TransportRequisitionViewModel> { transportRequisitonViewModel };
            reportData[1] = requisitionsSummary;
            reportData[2] = detailsT;

            var result = ReportHelper.PrintReport(reportPath, reportData, dataSources);
            return File(result.RenderBytes, result.MimeType);
        }

        public decimal GetCommodityRation(int requisitionID, int commodityID)
        {
            var reliefRequisition = _reliefRequisitionService.FindById(requisitionID);
            var ration = _rationService.FindById(reliefRequisition.RegionalRequest.RationID);
            var rationModel = ration.RationDetails.FirstOrDefault(m => m.CommodityID == commodityID).Amount;

            return rationModel;

        }

        public ActionResult PrintAttachment(int id) {
            
            var reportPath = Server.MapPath("~/Report/Logisitcs/transportRequisitionAttachment.rdlc");
            
            var requisitionDetails = _reliefRequisitionDetailService.Get(t => t.RequisitionID == id, null, "ReliefRequisition.AdminUnit,FDP.AdminUnit,FDP,Donor,Commodity");
            var commodityID = requisitionDetails.FirstOrDefault().CommodityID;
            var RationAmount = GetCommodityRation(id, commodityID);
            var requisitionDetailViewModels = RequisitionViewModelBinder.BindReliefRequisitionDetailListViewModel(requisitionDetails, RationAmount);
            
            var header = (from destination in requisitionDetailViewModels
                          select new
                          {
                              destination.Zone,
                              destination.Woreda,
                              destination.FDP,
                              destination.Donor,
                              destination.Commodity,
                              destination.Amount
                          });


            var dataSources="attachment";
            var reportData= header;

            var result = ReportHelper.PrintReport(reportPath, reportData, dataSources);
            return File(result.RenderBytes, result.MimeType);
        }
       
    }
}
