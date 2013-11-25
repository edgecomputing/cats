using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Cats.Areas.Logistics.Models;
using Cats.Models;
using Cats.Models.Constant;
using Cats.Services.EarlyWarning;
using Cats.Helpers;
using Cats.Services.Security;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using log4net;
using HubAllocation = Cats.Models.HubAllocation;
using Cats.ViewModelBinder;

namespace Cats.Areas.Logistics.Controllers
{
    public class HubAllocationController : Controller
    {
        //
        // GET: /Logistics/HubAllocation/
        private readonly IReliefRequisitionDetailService _reliefRequisitionDetailService;
        private readonly IReliefRequisitionService _reliefRequisitionService;
        private readonly IHubService _hubService;
        private readonly IHubAllocationService _hubAllocationService;
        private readonly ILog _log;
        private readonly IUserAccountService _userAccountService;

        public HubAllocationController(
           IReliefRequisitionDetailService reliefRequisitionDetailService,
           IHubService hubService,
           IHubAllocationService hubAllocationService, 
           IReliefRequisitionService reliefRequisitionService, IUserAccountService userAccountService,
            ILog log)
        {
            this._hubService = hubService;
            this._reliefRequisitionDetailService = reliefRequisitionDetailService;
            this._hubAllocationService = hubAllocationService;
            this._reliefRequisitionService = reliefRequisitionService;
            this._log = log;
            _userAccountService = userAccountService;
        }


        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult AssignHub()
        {

            ViewBag.Months = new SelectList(RequestHelper.GetMonthList(), "Id", "Name");
            return View("AssignHub");
        }
        
        public JsonResult GetRequisitionsForAssignment()
        {
            var user = (UserIdentity)System.Web.HttpContext.Current.User.Identity;
            var unitPreference = user.Profile.PreferedWeightMeasurment.ToString();

            var reliefRequisitions = _reliefRequisitionService.Get(r=>r.Status==(int)ReliefRequisitionStatus.Approved, null);
            var result = reliefRequisitions.ToList().Select(item => new AssignHubViewModel
                                                                        {
                                                                            Commodity = item.Commodity.Name,
                                                                            RegionName = item.AdminUnit.Name, 
                                                                            ZoneName = item.AdminUnit1.Name, 
                                                                            RequisitionNo = item.RequisitionNo, 
                                                                            RequisitionId = item.RequisitionID, 
                                                                            Beneficiaries = item.ReliefRequisitionDetails.Sum(b=>b.BenficiaryNo),
                                                                            Amount = item.ReliefRequisitionDetails.Sum(a=>a.Amount),
                                                                            Selected = true,
                                                                            Unit = unitPreference
                                                                        }).ToList();
           
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetHubs()
        {

           
            List<HubDto> list= new List<HubDto>(1);
            HubDto d = new HubDto(){HubId = 1,HubName = "hub"};
            list.Add(d);

            return Json(list, JsonRequestBehavior.AllowGet);
           
        }

      


        public ActionResult ApprovedRequisitions([DataSourceRequest]DataSourceRequest request)

        {
            ViewBag.Months = new SelectList(RequestHelper.GetMonthList(), "Id", "Name");
            var previousModelState = TempData["ModelState"] as ModelStateDictionary;
            if (previousModelState != null)
            {
                foreach (KeyValuePair<string, ModelState> kvp in previousModelState)
                    if (!ModelState.ContainsKey(kvp.Key))
                        ModelState.Add(kvp.Key, kvp.Value);
            }

           
            var requisititions = _reliefRequisitionService.FindBy(r => r.Status == (int)ReliefRequisitionStatus.Approved);
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
                //.re _reliefRequisitionDetailService.Get(r => r.ReliefRequisition.Status == 2, null, "ReliefRequisition,Donor");
            return View(requisitionViewModel.ToList());
        }
        public ActionResult Request(ICollection<ReliefRequisitionDetail> requisitionDetail)
        {
            ViewBag.Months = new SelectList(RequestHelper.GetMonthList(), "Id", "Name");
            var reliefRequisitions = _reliefRequisitionDetailService.Get(null, null, "ReliefRequisition,Donor");
            return View(reliefRequisitions.ToList());

        }

        
        public ActionResult hubAllocation(ICollection<RequisitionViewModel> requisitionDetail, FormCollection form)
        {
            ViewBag.Hubs = new SelectList(_hubService.GetAllHub(), "HubID", "Name");
            ViewBag.Months = new SelectList(RequestHelper.GetMonthList(), "Id", "Name");

            ICollection<RequisitionViewModel> listOfRequsitions = new List<RequisitionViewModel>();
            RequisitionViewModel[] _requisitionDetail;

            if (requisitionDetail == null)
            {
                ModelState.AddModelError("Error","No approved requisitions or no requisition is selected.");
                TempData["ModelState"] = ModelState;
                return RedirectToAction("ApprovedRequisitions");
            }

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

        [HttpPost]
        public ActionResult InserRequisition(ICollection<RequisitionViewModel> requisitionDetail, FormCollection form,
                                             string datepicker, string rNumber)
        {
            if (rNumber.Trim() == string.Empty)
                return RedirectToAction("ApprovedRequisitions", "HubAllocation");

            var userName = HttpContext.User.Identity.Name;
            var user = _userAccountService.GetUserDetail(userName);

            if (ModelState.IsValid && requisitionDetail !=null )
            {
                string hub = form["hub"].ToString(CultureInfo.InvariantCulture); //retrives Hub id from the view
                DateTime date;
              

                try
                {
                    date = DateTime.Parse(datepicker);
                        //checkes if date is ethiopian date. if it is then it will enter to the catch and convert to gragorian to persist.
                }
                catch (Exception exception)
                {
                    var log = new Logger();
                    log.LogAllErrorsMesseges(exception,_log);
                    var strEth = new getGregorianDate();
                    date = strEth.ReturnGregorianDate(datepicker);
                }


                foreach (RequisitionViewModel appRequisition in requisitionDetail)
                {

                    var newHubAllocation = new HubAllocation
                                               {
                                                   AllocatedBy = user.UserProfileID,
                                                   RequisitionID = appRequisition.RequisitionId,
                                                   AllocationDate = date,
                                                   ReferenceNo = rNumber,
                                                   HubID = int.Parse(hub)
                                               };



                    _hubAllocationService.AddHubAllocation(newHubAllocation);




                }
                return RedirectToAction("ApprovedRequisitions", "HubAllocation");

            }
            return RedirectToAction("ApprovedRequisitions", "HubAllocation");
        }
    }
}
