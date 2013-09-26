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

namespace Cats.Areas.Logistics.Controllers
{
    public class TransportRequisitionController : Controller
    {
        private readonly ITransportRequisitionService _transportRequisitionService;
        private readonly IWorkflowStatusService _workflowStatusService;
        
        public TransportRequisitionController(ITransportRequisitionService transportRequisitionService,IWorkflowStatusService workflowStatusService)
        {
            this._transportRequisitionService = transportRequisitionService;
            _workflowStatusService = workflowStatusService;
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
            TransportRequisitionViewModel transportRequisitionViewModel=null;
            if(transportRequisition !=null)
            {
                transportRequisitionViewModel = new TransportRequisitionViewModel();
                transportRequisitionViewModel.CertifiedBy = transportRequisition.CertifiedBy;
                transportRequisitionViewModel.CertifiedDate = transportRequisition.CertifiedDate;
                transportRequisitionViewModel.CertifiedDateET =
                    EthiopianDate.GregorianToEthiopian(transportRequisition.CertifiedDate);
                transportRequisitionViewModel.Remark = transportRequisition.Remark;
                transportRequisitionViewModel.RequestedBy = transportRequisition.RequestedBy;
                transportRequisitionViewModel.RequestedDate = transportRequisition.RequestedDate;
                transportRequisitionViewModel.RequestedDateET =EthiopianDate.GregorianToEthiopian( transportRequisition.RequestedDate);
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
            catch (Exception)
            {
                
                
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
            catch (Exception)
            {


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
       
    }
}
