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
using Cats.ViewModelBinder;
using Early_Warning.Security;
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
        
        public TransportRequisitionController(ITransportRequisitionService transportRequisitionService,
            IWorkflowStatusService workflowStatusService, IUserAccountService userAccountService,ILog log)
        {
            this._transportRequisitionService = transportRequisitionService;
            _workflowStatusService = workflowStatusService;
            _userAccountService = userAccountService;
            _log = log;
        }
        //
        // GET: /Logistics/TransportRequisition/

        public ActionResult Index()
        {
            
            return View();

        }
        public ActionResult TransportRequisition_Read([DataSourceRequest] DataSourceRequest request)
        {
               var datePref = _userAccountService.GetUserInfo(HttpContext.User.Identity.Name).DatePreference;
            var statuses = _workflowStatusService.GetStatus(WORKFLOW.TRANSPORT_REQUISITION);
            var transportRequisitions = _transportRequisitionService.GetAllTransportRequisition();
            var users = _userAccountService.GetUsers();
            var transportRequisitonViewModels =
                TransportRequisitionViewModelBinder.BindListTransportRequisitonViewModel(transportRequisitions, statuses,
                                                                                         datePref, users);
             
            return Json(transportRequisitonViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        
        
        private List<TransportRequisitionDetailViewModel> GetDetail(IEnumerable<TransportRequisitionDetail> transportRequisitionDetails )
        {
          
            var requIds = (from itm in transportRequisitionDetails select itm.RequisitionID).ToList();
            var temp = _transportRequisitionService.GetTransportRequisitionDetail(requIds);
            var result = TransportRequisitionViewModelBinder.BindListTransportRequisitionDetailViewModel(temp);
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
        //[CatsAuthorize(operation = CheckAccessHelper.Operation.ViewTransportRequisition)]
        public ActionResult Details(int id)
        {
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
           
            }
            catch(Exception ex)
            {
                var log = new Logger();
                log.LogAllErrorsMesseges(ex, _log);
                transportRequisitonViewModel.TransportRequisitionDetailViewModels = new List<TransportRequisitionDetailViewModel>();
              
                ViewBag.Error = "An error has occured: Check Detail.";
                ModelState.AddModelError("Errors", ViewBag.Error);
            }
            return View(transportRequisitonViewModel);
        }
       
    }
}
