using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Cats.Models;
using Cats.Services.EarlyWarning;
using System.Web.Mvc;



namespace Cats.Areas.Logistics.Controllers
{
    public class ProjectAllocationController : Controller
    {
        //
        // GET: /Logistics Project Allocation/

        private IRegionalRequestService _regionalRequestService;
        private IFDPService _fdpService;
        //private IRoundService _roundService;
        private IAdminUnitService _adminUnitService;
        private IProgramService _programService;
        private ICommodityService _commodityService;
        private IRegionalRequestDetailService _reliefRequisitionDetailService;
        private IProjectCodeAllocationService _projectCodeAllocationService;
        private IProjectCodeService _projectCodeService;
        private IShippingInstructionService _shippingInstructionService;
        private IHubService _hubService;
        private IHubAllocationService _hubAllocationService;
        private IReliefRequisitionService _requisitionService;
        private ITransactionService _transactionService;
        public ProjectAllocationController(IRegionalRequestService reliefRequistionService
           , IFDPService fdpService
            , IAdminUnitService adminUnitService,
            IProgramService programService,
            ICommodityService commodityService,
            IRegionalRequestDetailService reliefRequisitionDetailService,
            IProjectCodeAllocationService projectCodeAllocationService, 
            IProjectCodeService projectCodeService,
            IShippingInstructionService shippingInstructionService, 
            IHubService hubService, 
            IHubAllocationService hubAllocationService,
            IReliefRequisitionService requisitionService, ITransactionService transactionservice)
        {
            this._regionalRequestService = reliefRequistionService;
            this._adminUnitService = adminUnitService;
            this._commodityService = commodityService;
            this._fdpService = fdpService; 
            this._programService = programService;
            this._reliefRequisitionDetailService = reliefRequisitionDetailService;
            this._projectCodeAllocationService = projectCodeAllocationService;
            this._projectCodeService = projectCodeService;
            this._shippingInstructionService = shippingInstructionService;
            this._hubService = hubService;
            this._hubAllocationService = hubAllocationService;
            this._requisitionService = requisitionService;
            this._transactionService = transactionservice;
        }

       
        [HttpPost]
        public ActionResult HubAllocationDetail(Cats.Models.HubAllocation hubAllocation)
        {
            var seleList = new SelectList(_hubService.GetAllHub(), "HubId", "Name");
            ViewBag.HubId = seleList;
            IEnumerable<Cats.Models.HubAllocation> detail =
               _projectCodeAllocationService.Get(
                   t => t.HubID == hubAllocation.HubID);
             
            return View(detail);
            
        }

        public ActionResult AllocateProjectCode()
        {
            var reliefRequisitions = _hubAllocationService.ReturnRequisitionGroupByReuisitionNo(3);
            if (reliefRequisitions != null)
            {
                var total = reliefRequisitions.Count();
                ViewData["total"] = total;
            }
            else
            {
                return HttpNotFound();
            }
            
            return View(reliefRequisitions.ToList());
        }

        public ActionResult Assign( int ReqId,double Remaining)
        {

            ReliefRequisition listOfRequsitions = _requisitionService.Get(r => r.RequisitionID == ReqId).SingleOrDefault();
            
           
            ViewBag.SI = new SelectList(_shippingInstructionService.GetAllShippingInstruction(), "ShippingInstructionID", "Value");
            ViewBag.PC = new SelectList(_projectCodeService.GetAllProjectCode(), "ProjectCodeID", "Value");
            ViewBag.RequetedAmount = listOfRequsitions.ReliefRequisitionDetails.Sum(a => a.Amount);
            ViewBag.Hub = _hubAllocationService.GetAllocatedHub(ReqId);
            ViewBag.ReqId = listOfRequsitions.RequisitionID;
            ViewBag.Remaining = Remaining;
            return View();
        }
        public ActionResult AllocatePC(ICollection<RequisitionViewModel> requisitionDetail, FormCollection form)
        {
            ViewBag.Hubs = new SelectList(_hubService.GetAllHub(), "HubID", "Name");
            List<ReliefRequisition> projectCodeAllocatedReq = _requisitionService.Get(r => r.Status == 4 && r.RequisitionID == requisitionDetail.SingleOrDefault().RequisitionId).ToList();



            ICollection<RequisitionViewModel> listOfRequsitions = new List<RequisitionViewModel>();
            RequisitionViewModel[] _requisitionDetail;

            if (requisitionDetail == null) return View();

            _requisitionDetail = requisitionDetail.ToArray();

            var chkValue = form["IsChecked"]; // for this code the chkValue will return all value of each checkbox that is checked


            if (chkValue != null)
            {
                string[] arrChkValue = form["IsChecked"].Split(',');

                foreach (var value in arrChkValue)
                {
                    listOfRequsitions.Add(_requisitionDetail[int.Parse(value)]);
                }
            }

            return View(listOfRequsitions.ToList());
            
        }

        public ActionResult SaveProjectAllocation(RequisitionViewModel requisitionViewModel, 
                                                    FormCollection form, 
                                                    string hub, 
                                                    int RequisitionId,
                                                    int Remaining=0, 
                                                    int PCodeqty=0, 
                                                    int SICodeqty=0)
        {

            bool isLastAssignment = false;


            if (Remaining < PCodeqty + SICodeqty)
                return RedirectToAction("AllocateProjectCode", "ProjectAllocation");

            var requisitionId = requisitionViewModel.RequisitionId;
            var pCode = int.Parse(form["PCCode"].ToString(CultureInfo.InvariantCulture));
            var siCode = int.Parse(form["SICode"].ToString(CultureInfo.InvariantCulture));
            
            var hubAllocation = _hubAllocationService.GetAllocatedHubByRequisitionNo(requisitionId);
           
            var newProjectAllocation = new ProjectCodeAllocation
                                           {

                                               AllocatedBy = 1,
                                               AlloccationDate = DateTime.Parse(form["datepicker"]),
                                               Amount_FromProject = PCodeqty,
                                               ProjectCodeID = pCode,
                                               Amount_FromSI = SICodeqty,
                                               SINumberID = siCode,
                                               HubAllocationID = hubAllocation.HubAllocationID
                                           };

            if (Remaining == PCodeqty + SICodeqty)
                isLastAssignment = true;
            _projectCodeAllocationService.AddProjectCodeAllocation(newProjectAllocation,requisitionId,isLastAssignment);
            return RedirectToAction("AllocateProjectCode","ProjectAllocation");

        }

        public ActionResult ProjectCodeAllocation()
        {
            ViewBag.HubId = new SelectList(_hubService.GetAllHub(), "HubId", "Name");
            var hubAllocation = _projectCodeAllocationService.GetHubAllocationByHubID(3);
            return View(hubAllocation);
        }
        public  ActionResult Allocate()
        {
            ViewBag.HubId = new SelectList(_hubService.GetAllHub(), "HubId", "Name");
            var hubAllocation = _projectCodeAllocationService.GetHubAllocationByHubID(3);
            return View(hubAllocation);
        }

        public ActionResult AssignedprojectCodes(int requisitionId)
        {

            var hubId = _hubAllocationService.GetAllocatedHubByRequisitionNo(requisitionId);


            var assigned = _projectCodeAllocationService.GetHubAllocationByID(hubId.HubAllocationID).ToList();
            var requisition = _requisitionService.FindById(requisitionId);
            int? siAmount=0;
            int? pcAmount=0;
            for (int i = 0; i < assigned.Count; i++)
            {
                siAmount = siAmount + assigned[i].Amount_FromSI;
                pcAmount = pcAmount + assigned[i].Amount_FromProject;
            }

            ViewBag.AmountPCAssigned = pcAmount;
            ViewBag.AmountSIAssined = siAmount;

            ViewBag.Total = pcAmount + siAmount;
            ViewBag.Allocated = requisition.ReliefRequisitionDetails.Sum(a => a.Amount);
            ViewBag.ReqId = requisitionId;
            ViewBag.ReqNo = requisition.RequisitionNo;
            ViewBag.Remaining = ViewBag.Allocated - ViewBag.Total;
            ViewBag.Hub = hubId.Hub.Name;

            return View(assigned);
        }
     
    }
}