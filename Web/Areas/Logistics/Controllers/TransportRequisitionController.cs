using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Logistics.Models;
using Cats.Areas.Procurement.Models;
using Cats.Helpers;
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
        private readonly IAdminUnitService _adminUnitService;
        private readonly IProgramService _programService;
        private readonly IReliefRequisitionService _reliefRequisitionService;
        
        public TransportRequisitionController(ITransportRequisitionService transportRequisitionService,
            IWorkflowStatusService workflowStatusService, IUserAccountService userAccountService,ILog log, 
            IAdminUnitService adminUnitService, IProgramService programService, IReliefRequisitionService reliefRequisitionService)
        {
            this._transportRequisitionService = transportRequisitionService;
            _workflowStatusService = workflowStatusService;
            _userAccountService = userAccountService;
            _log = log;
            _adminUnitService = adminUnitService;
            _programService = programService;
            _reliefRequisitionService = reliefRequisitionService;
        }
        //
        // GET: /Logistics/TransportRequisition/

        public ActionResult Index()
        {
            
            return View();

        }
        public ActionResult TransportRequisition_Read([DataSourceRequest] DataSourceRequest request, string searchIndex)
        {
            var transportRequisitions = _transportRequisitionService.Get(t => t.TransportRequisitionNo.Contains(searchIndex));
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
                transportRequisitionViewModel.Region = _adminUnitService.FindById(transportRequisition.RegionID).Name;
                transportRequisitionViewModel.Program = _programService.FindById(transportRequisition.ProgramID).Name;

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

        public ActionResult GenerateTransportRequisitionForRegion(int regionID)
        {
            var reliefRequisitionslist = _reliefRequisitionService.Get(t => t.Status == (int)ReliefRequisitionStatus.ProjectCodeAssigned && t.RegionID == regionID, null,
                                                          "ReliefRequisitionDetails,Program,AdminUnit1,AdminUnit,Commodity");
            var uniquePrograms = new List<Program>();
            foreach (var uniqueProgram in from reliefRequisitionsID in reliefRequisitionslist let uniqueProgram = new Program()
                                          let programID = reliefRequisitionsID.ProgramID
                                          where programID != null
                                          select _programService.FindById((int)programID) 
                                          into uniqueProgram where !uniquePrograms.Contains(uniqueProgram) select uniqueProgram)
            {
                uniquePrograms.Add(uniqueProgram);
            }
            var transportRequisition = new TransportRequisition();
            foreach (var partitionedReliefRequisitiionIDList in uniquePrograms.Select(program1 => 
                _reliefRequisitionService.Get(t => t.Status == (int)ReliefRequisitionStatus.ProjectCodeAssigned && t.RegionID == regionID &&
                t.ProgramID == program1.ProgramID)).Select(partitionedReliefRequisitiions => partitionedReliefRequisitiions.Select(t => t.RequisitionID).ToList()))
            {
                transportRequisition  = _transportRequisitionService.CreateTransportRequisition(partitionedReliefRequisitiionIDList);
            }
            return RedirectToAction("Index", "TransportRequisition");
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
       
    }
}
