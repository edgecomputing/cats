using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Cats.Helpers;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Services.EarlyWarning;
using System.Web.Mvc;
using Cats.Services.Transaction;
using Cats.ViewModelBinder;
using log4net;
using Cats.Services.Common;

namespace Cats.Areas.Logistics.Controllers
{
    [Authorize]
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
        private ILog _log;
        private ILedgerService _ledgerService;
        

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
            ILog log,
            IReliefRequisitionService requisitionService, ITransactionService transactionservice, ILedgerService ledgerService)
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
           this._ledgerService = ledgerService;
            this._log = log;

        }

        public ActionResult Index() {
            return View();
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

            var previousModelState = TempData["ModelState"] as ModelStateDictionary;
            if (previousModelState != null)
            {
                foreach (KeyValuePair<string, ModelState> kvp in previousModelState)
                    if (!ModelState.ContainsKey(kvp.Key))
                        ModelState.Add(kvp.Key, kvp.Value);
            }

            var requisititions = _requisitionService.FindBy(r => r.Status == (int)ReliefRequisitionStatus.HubAssigned);
            var requisitionViewModel = HubAllocationViewModelBinder.ReturnRequisitionGroupByReuisitionNo(requisititions);


            if (requisitionViewModel != null)
            {
                var total = requisitionViewModel.Count();
                ViewData["total"] = total;
            }
            else
            {
                return HttpNotFound();
            }

            return View(requisitionViewModel.ToList());
        }

        public ActionResult Assign(int reqId, double remaining, double totalAssigned)
        {
            var previousModelState = TempData["ModelState"] as ModelStateDictionary;
          
            if (previousModelState != null)
            {
                foreach (KeyValuePair<string, ModelState> kvp in previousModelState)
                    if (!ModelState.ContainsKey(kvp.Key))
                        ModelState.Add(kvp.Key, kvp.Value);
            }
            
            var hubId = _hubAllocationService.GetAllocatedHubId(reqId);
            ReliefRequisition listOfRequsitions = _requisitionService.Get(r => r.RequisitionID == reqId).SingleOrDefault();

            List<LedgerService.AvailableShippingCodes> freeSICodes = _ledgerService.GetFreeSICodesByCommodity(hubId,(int) listOfRequsitions.CommodityID);
            List<LedgerService.AvailableProjectCodes> freePCCodes = _ledgerService.GetFreePCCodesByCommodity(hubId,(int) listOfRequsitions.CommodityID);
            ViewBag.FreeSICodes = freeSICodes;
            ViewBag.FreePCCodes = freePCCodes;
            ViewBag.SI = new SelectList(freeSICodes, "siCodeId", "SIcode");
            ViewBag.PC = new SelectList(freePCCodes, "pcCodeId", "PCcode");


           
            
           
           // ViewBag.SI = new SelectList(_shippingInstructionService.GetAllShippingInstruction(), "ShippingInstructionID", "Value");
            ViewBag.Total = totalAssigned;
            //ViewBag.PC = new SelectList(_projectCodeService.GetAllProjectCode(), "ProjectCodeID", "Value");
            ViewBag.RequetedAmount = Math.Round(listOfRequsitions.ReliefRequisitionDetails.Sum(a => a.Amount));
            ViewBag.Hub = _hubAllocationService.GetAllocatedHub(reqId);
            ViewBag.ReqId = listOfRequsitions.RequisitionID;
            ViewBag.Remaining = Math.Round(remaining);
            return View();
        }

        
        public JsonResult GetSIAmount(int siIndex, int reqId)
        {


            var hubId = _hubAllocationService.GetAllocatedHubId(reqId);
            var result = _projectCodeAllocationService.GetAllocatedAmountBySI(hubId,siIndex);
            return Json(new {Success = true, Result = result}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPCAmount(int pcIndex, int reqId)
        {

            var hubId = _hubAllocationService.GetAllocatedHubId(reqId);
            var result = _projectCodeAllocationService.GetAllocatedAmountByPC(hubId, pcIndex);
            return Json(new { Success = true, Result = result }, JsonRequestBehavior.AllowGet);
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
                                                    string datepicker,
                                                    int RequisitionId,
                                                    int Remaining=0, 
                                                    int PCodeqty=0, 
                                                    int SICodeqty=0)
        {

            DateTime date;
            bool isProjectCodeSelected = false;
            bool isSICodeSelected = false;

            try
            {
                date = DateTime.Parse(datepicker);
                //checkes if date is ethiopian date. if it is then it will enter to the catch and convert to gragorian for  persistance.
            }
            catch (Exception)
            {

                var strEth = new getGregorianDate();
                date = strEth.ReturnGregorianDate(datepicker);
            }

            bool isLastAssignment = false;
            int? pCode =null;
            int? siCode=null;

            if (Remaining < PCodeqty + SICodeqty)
            {
                ModelState.AddModelError("Errors",@"Amount entered is greater than the remaining quantity ");
                TempData["ModelState"] = ModelState;
                return RedirectToAction("Assign", "ProjectAllocation", new { ReqId= RequisitionId, Remaining = Remaining});

            }
              

            var requisitionId = requisitionViewModel.RequisitionId;

            try
            {
                pCode = int.Parse(form["PCCode"].ToString(CultureInfo.InvariantCulture));
                isProjectCodeSelected = true;
            }
            catch
            {
                pCode = null;
            }


            try
            {
                siCode = int.Parse(form["SICode"].ToString(CultureInfo.InvariantCulture));
                isSICodeSelected = true;
            }
            catch(Exception ex)
            {   
                 siCode = null;
                var log = new Logger();
                log.LogAllErrorsMesseges(ex,_log);
               
            }


            if (isProjectCodeSelected == false && isSICodeSelected == false)
            {
                ModelState.AddModelError("Errors", @"SI code or Project Code is not selected.");
                TempData["ModelState"] = ModelState;
                return RedirectToAction("Assign", "ProjectAllocation", new { ReqId = RequisitionId, Remaining = Remaining });
            }

            if ((isProjectCodeSelected && PCodeqty == 0) || (isSICodeSelected && SICodeqty == 0))
            {
                ModelState.AddModelError("Errors", @"Value entered for Projecte Code/SI Code is zero or Invalid. ");
                TempData["ModelState"] = ModelState;
                return RedirectToAction("Assign", "ProjectAllocation", new { ReqId = RequisitionId, Remaining = Remaining });
            }

           

            var hubAllocation = _hubAllocationService.GetAllocatedHubByRequisitionNo(requisitionId);
            var newProjectAllocation = new ProjectCodeAllocation
                                           {

                                               AllocatedBy = 1,
                                               AlloccationDate = date,
                                               Amount_FromProject = PCodeqty,
                                               ProjectCodeID = pCode,
                                               Amount_FromSI = SICodeqty,
                                               SINumberID = siCode,
                                               HubAllocationID = hubAllocation.HubAllocationID
                                           };

            if (Remaining == PCodeqty + SICodeqty)
                isLastAssignment = true;
            try
            {
                _projectCodeAllocationService.AddProjectCodeAllocation(newProjectAllocation, requisitionId, isLastAssignment);

            }
            catch(Exception exception)
            {

                var log = new Logger();
                log.LogAllErrorsMesseges(exception,_log);
                ModelState.AddModelError("Errors",@"Can't add new project code allocation");

            }

           if (isLastAssignment)
           {
               return RedirectToAction("AllocateProjectCode", "ProjectAllocation");
           }
           else
           {
               return RedirectToAction("AssignedprojectCodes", "ProjectAllocation",new {requisitionId= requisitionId});
           }
            

        }

        public ActionResult ProjectCodeAllocation()
        {
            ViewBag.HubId = new SelectList(_hubService.GetAllHub(), "HubId", "Name");
            var hubAllocation = _projectCodeAllocationService.GetHubAllocationByHubID((int)ReliefRequisitionStatus.HubAssigned);
            return View(hubAllocation);
        }
        public  ActionResult Allocate()
        {
            

            ViewBag.HubId = new SelectList(_hubService.GetAllHub(), "HubId", "Name");
            var hubAllocation = _projectCodeAllocationService.GetHubAllocationByHubID((int)ReliefRequisitionStatus.HubAssigned);
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

            ViewBag.Total = Math.Round((decimal)pcAmount + (decimal)siAmount, 2);
            ViewBag.Allocated = Math.Round(requisition.ReliefRequisitionDetails.Sum(a => a.Amount));
            ViewBag.ReqId = requisitionId;
            ViewBag.ReqNo = requisition.RequisitionNo;
            ViewBag.Remaining = Math.Round(ViewBag.Allocated - ViewBag.Total, 2); 
            ViewBag.Hub = hubId.Hub.Name;

            return View(assigned);
        }
     
    }
}