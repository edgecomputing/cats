using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Models;
using Cats.Helpers;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Models.ViewModels;
using Cats.Services.EarlyWarning;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.EarlyWarning.Controllers
{
    public class ReliefRequisitionController : Controller
    {
        //
        // GET: /EarlyWarning/ReliefRequisition/

        private readonly IReliefRequisitionService _reliefRequisitionService;
        private readonly IWorkflowStatusService _workflowStatusService;
        private readonly IReliefRequisitionDetailService _reliefRequisitionDetailService;


        public ReliefRequisitionController(IReliefRequisitionService reliefRequisitionService,IWorkflowStatusService workflowStatusService,IReliefRequisitionDetailService reliefRequisitionDetailService)
        {
            this._reliefRequisitionService = reliefRequisitionService;
            this._workflowStatusService = workflowStatusService;
            this._reliefRequisitionDetailService = reliefRequisitionDetailService;

        }

        public ViewResult Index(int id=0)
        {
            ViewBag.StausID=id;
            var releifRequistions = id==0 ? _reliefRequisitionService.GetAllReliefRequisition().ToList(): _reliefRequisitionService.Get(t=>t.Status==id).ToList();
            var requisitionToRender = BuilReliefRequisitionViewModel(releifRequistions.FindAll(t=>t.Status.HasValue)).ToList();
            return View(requisitionToRender);
        }
        private IEnumerable<ReliefRequisitionViewModel> BuilReliefRequisitionViewModel(IEnumerable<ReliefRequisition> reliefRequisitions )
        {
            return (from requisition in reliefRequisitions
                    select new ReliefRequisitionViewModel
                               {
                                  // ApprovedBy = requisition.ApprovedBy,
                                   ApprovedDate = requisition.ApprovedDate,
                                 //  ApprovedDateEt =requisition.ApprovedDate.HasValue ? EthiopianDate.GregorianToEthiopian(requisition.ApprovedDate.Value):"",
                                   Commodity = requisition.Commodity.Name,
                                   CommodityID = requisition.CommodityID,
                                   Program = requisition.Program.Name,
                                   ProgramID = requisition.ProgramID,
                                   Region = requisition.AdminUnit1.Name,
                                   RegionID = requisition.RegionID,
                                   RegionalRequestID = requisition.RegionalRequestID,
                                 //  RequestedBy = requisition.RequestedBy,
                                   RequestedDate = requisition.RequestedDate,
                                  RequestedDateEt = requisition.RequestedDate.HasValue? EthiopianDate.GregorianToEthiopian(requisition.RequestedDate.Value):"",
                                   RequisitionID = requisition.RequisitionID,
                                   RequisitionNo = requisition.RequisitionNo,
                                   Round = requisition.Round,
                                   Status =
                                       _workflowStatusService.GetStatusName(WORKFLOW.RELIEF_REQUISITION,
                                                                            requisition.Status.Value),
                                   ZoneID = requisition.ZoneID,
                                   Zone = requisition.AdminUnit.Name,
                                   StatusID = requisition.Status
                               });
        }
        [HttpGet]
        public ActionResult CreateRequisiton(int id)
        {
            var input = _reliefRequisitionService.CreateRequisition(id);

            return RedirectToAction("NewRequisiton", "ReliefRequisition",new {id=id});


        }
        [HttpGet]
        public ViewResult NewRequisiton(int id)
        {
            var input = _reliefRequisitionService.GetRequisitionByRequestId(id);
            return View(input);


        }

        [HttpPost]
        public ActionResult NewRequisiton(List<DataFromGrid> input)
        {
            var requId = 0;
            if (ModelState.IsValid)
            {
                 requId = input.FirstOrDefault().Number;
                foreach (var reliefRequisitionNewInput in input)
                {

                    _reliefRequisitionService.AssignRequisitonNo(reliefRequisitionNewInput.Number,
                                                                 reliefRequisitionNewInput.RequisitionNo);

                }

                _reliefRequisitionService.Save();
              
            }
            return RedirectToAction("Index", "ReliefRequisition");
        }
        [HttpGet]
        public ActionResult Allocation(int id)
        {
           
            var requisition =
                _reliefRequisitionService.Get(t => t.RequisitionID == id, null, "ReliefRequisitionDetails").
                    FirstOrDefault();
            if(requisition==null)
            {
                HttpNotFound();
            }
            var requisitionViewModel = BindReliefRequisitionViewModel(requisition);

            return View(requisitionViewModel);
        }

        public ActionResult Allocation_Read([DataSourceRequest] DataSourceRequest request, int id)
        {

            var requisitionDetails = _reliefRequisitionDetailService.Get(t => t.RequisitionID == id,null,"ReliefRequisition.AdminUnit,FDP.AdminUnit,FDP,Donor,Commodity");
            var requisitionDetailViewModels = (from dtl in requisitionDetails select BindReliefRequisitionDetailViewModel(dtl));
            return Json(requisitionDetailViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        private ReliefRequisitionDetailViewModel BindReliefRequisitionDetailViewModel(ReliefRequisitionDetail reliefRequisitionDetail)
        {
            return new ReliefRequisitionDetailViewModel()
            {
               Zone= reliefRequisitionDetail.ReliefRequisition.AdminUnit1.Name ,
               Woreda=reliefRequisitionDetail.FDP.AdminUnit.Name ,
               FDP=reliefRequisitionDetail.FDP.Name ,
               Donor=reliefRequisitionDetail.DonorID.HasValue ? reliefRequisitionDetail.Donor.Name : "",
               Commodity=reliefRequisitionDetail.Commodity.Name ,
               BenficiaryNo=reliefRequisitionDetail.BenficiaryNo,
               Amount=reliefRequisitionDetail.Amount,
               RequisitionID=reliefRequisitionDetail.RequisitionID ,
               RequisitionDetailID=reliefRequisitionDetail.RequisitionDetailID,
               CommodityID=reliefRequisitionDetail.CommodityID,
               FDPID=reliefRequisitionDetail.FDPID,
               DonorID = reliefRequisitionDetail.DonorID

            };

        }
        private ReliefRequisitionDetail BindReliefRequisitionDetail(ReliefRequisitionDetailViewModel reliefRequisitionDetailViewModel)
        {
            return new ReliefRequisitionDetail()
            {


                BenficiaryNo = reliefRequisitionDetailViewModel.BenficiaryNo,
                Amount = reliefRequisitionDetailViewModel.Amount,
                RequisitionID = reliefRequisitionDetailViewModel.RequisitionID,
                RequisitionDetailID = reliefRequisitionDetailViewModel.RequisitionDetailID,
                CommodityID = reliefRequisitionDetailViewModel.CommodityID,
                FDPID = reliefRequisitionDetailViewModel.FDPID,
                DonorID = reliefRequisitionDetailViewModel.DonorID,
               
            };
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Allocation_Create([DataSourceRequest] DataSourceRequest request, ReliefRequisitionDetailViewModel reliefRequisitionDetailViewModel)
        {
            if (reliefRequisitionDetailViewModel != null && ModelState.IsValid)
            {
                _reliefRequisitionDetailService.AddReliefRequisitionDetail(BindReliefRequisitionDetail(reliefRequisitionDetailViewModel));
            }

            return Json(new[] { reliefRequisitionDetailViewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Allocation_Update([DataSourceRequest] DataSourceRequest request, ReliefRequisitionDetailViewModel reliefRequisitionDetailViewModel)
        {
            if (reliefRequisitionDetailViewModel != null && ModelState.IsValid)
            {
                var target = _reliefRequisitionDetailService.FindById(reliefRequisitionDetailViewModel.RequisitionDetailID);
                if (target != null)
                {
                    target.Amount = reliefRequisitionDetailViewModel.Amount;
                    target.BenficiaryNo = reliefRequisitionDetailViewModel.BenficiaryNo;
                  
                    _reliefRequisitionDetailService.EditReliefRequisitionDetail(target);
                }
            }

            return Json(new[] { reliefRequisitionDetailViewModel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Allocation_Destroy([DataSourceRequest] DataSourceRequest request,
                                                  ReliefRequisitionDetail reliefRequisitionDetail)
        {
            if (reliefRequisitionDetail != null)
            {
                _reliefRequisitionDetailService.DeleteById(reliefRequisitionDetail.RequisitionDetailID);
            }

            return Json(ModelState.ToDataSourceResult());
        }



        [HttpPost]
     public    ActionResult RequistionDetailEdit(IEnumerable<ReleifRequisitionDetailEdit.ReleifRequisitionDetailEditInput> input )
        {
            var requId = 0;
            if (ModelState.IsValid)
            {
                requId = input.FirstOrDefault().Number;
              
                foreach (var requisitionDetailEditInput in input.ToList())
                {

                    _reliefRequisitionService.EditAllocatedAmount(requisitionDetailEditInput.Number,
                                                                 requisitionDetailEditInput.Amount);

                }

                _reliefRequisitionService.Save();

            }
            return RedirectToAction("Index", "ReliefRequisition");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var relifRequisition = _reliefRequisitionService.FindById(id);
            if(relifRequisition==null)
            {
                HttpNotFound();
            }
            


            return View(relifRequisition);
        }
        [HttpPost]
        public ActionResult Edit(ReliefRequisition reliefrequisition)
        {
            if(ModelState.IsValid)
            {
                _reliefRequisitionService.EditReliefRequisition(reliefrequisition);
               return  RedirectToAction("Index", "ReliefRequisition");
            }
            return View(reliefrequisition);
        }

        [HttpGet]
        public ActionResult SendToLogistics(int id)
        {
            var requistion = _reliefRequisitionService.FindById(id);
            if(requistion==null)
            {
                HttpNotFound();
            }
            return View(requistion);
        }

        [HttpPost]
        public ActionResult ConfirmSendToLogistics(int requisitionid)
        {
            var requisition = _reliefRequisitionService.FindById(requisitionid);
            requisition.Status = (int)ReliefRequisitionStatus.Approved;
            _reliefRequisitionService.EditReliefRequisition(requisition);
            return RedirectToAction("Index", "ReliefRequisition");
        }

        public ActionResult Requisition_Read([DataSourceRequest] DataSourceRequest request)
        {
            
            
            var requests = _reliefRequisitionService.GetAllReliefRequisition();
            var requestViewModels = (from dtl in requests select BindReliefRequisitionViewModel(dtl));
            return Json(requestViewModels.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        private ReliefRequisitionViewModel BindReliefRequisitionViewModel(ReliefRequisition reliefRequisition)
        {
            return new ReliefRequisitionViewModel()
            {

                ProgramID = reliefRequisition.ProgramID,
                Program = reliefRequisition.Program.Name,
                Region = reliefRequisition.AdminUnit.Name,
                RequisitionNo = reliefRequisition.RequisitionNo,
                RegionID = reliefRequisition.RegionID,
                RegionalRequestID = reliefRequisition.RegionalRequestID,
               
                RequestedDateEt = EthiopianDate.GregorianToEthiopian(reliefRequisition.RequestedDate.Value
                ),
                Round = reliefRequisition.Round,
                Status = _workflowStatusService.GetStatusName(WORKFLOW.REGIONAL_REQUEST, reliefRequisition.Status.Value ),
                RequestedDate = reliefRequisition.RequestedDate,
                StatusID = reliefRequisition.Status,
                RequisitionID = reliefRequisition.RequisitionID ,
                CommodityID = reliefRequisition.CommodityID ,
                ZoneID=reliefRequisition.ZoneID ,
                Zone=reliefRequisition.AdminUnit.Name ,
                Commodity=reliefRequisition.Commodity.Name ,
                



            };

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Requisition_Update([DataSourceRequest] DataSourceRequest request, ReliefRequisitionViewModel reliefRequisitionViewModel)
        {
            if (reliefRequisitionViewModel != null && ModelState.IsValid)
            {
                var target = _reliefRequisitionService.FindById(reliefRequisitionViewModel.RequisitionID);
                if (target != null)
                {

                    target.RequisitionNo = reliefRequisitionViewModel.RequisitionNo;

                    _reliefRequisitionService.EditReliefRequisition(target);
                }
            }

            return Json(new[] { reliefRequisitionViewModel }.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Requisition_Destroy([DataSourceRequest] DataSourceRequest request,
                                                  ReliefRequisition reliefRequisition)
        {
            if (reliefRequisition != null)
            {
                _reliefRequisitionDetailService.DeleteById(reliefRequisition.RequisitionID);
            }

            return Json(ModelState.ToDataSourceResult());
        }
       
        public ActionResult Details(int id)
        {
            var requisition = _reliefRequisitionService.FindById(id);
            if(requisition==null)
            {
                return HttpNotFound();
            }
            var requisitionViewModel = BindReliefRequisitionViewModel(requisition);
            return View(requisitionViewModel);
        }
    }
}
