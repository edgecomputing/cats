using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Cats.Areas.Logistics.Models;
using Cats.Models;
using Cats.Services.EarlyWarning;
using Cats.Helpers;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using HubAllocation = Cats.Models.HubAllocation;

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
        public HubAllocationController(
           IReliefRequisitionDetailService reliefRequisitionDetailService,
           IHubService hubService,
           IHubAllocationService hubAllocationService, 
           IReliefRequisitionService reliefRequisitionService)
        {
            this._hubService = hubService;
            this._reliefRequisitionDetailService = reliefRequisitionDetailService;
            this._hubAllocationService = hubAllocationService;
            this._reliefRequisitionService = reliefRequisitionService;
        }

    
      
      

        public ActionResult AssignHub()
        {

            ViewBag.Months = new SelectList(RequestHelper.GetMonthList(), "Id", "Name");
            return View();
        }
        
        public JsonResult GetRequisitionsForAssignment()
        {
            var reliefRequisitions = _reliefRequisitionService.Get(r=>r.Status==2, null);
            var result = reliefRequisitions.ToList().Select(item => new AssignHubViewModel
                                                                        {
                                                                            Commodity = item.Commodity.Name,
                                                                            RegionName = item.AdminUnit1.Name, 
                                                                            ZoneName = item.AdminUnit1.Name, 
                                                                            RequisitionNo = item.RequisitionNo, 
                                                                            RequisitionId = item.RequisitionID, 
                                                                            Beneficiaries = item.ReliefRequisitionDetails.Sum(b=>b.BenficiaryNo),
                                                                            Amount = item.ReliefRequisitionDetails.Sum(a=>a.Amount),
                                                                            Selected = true
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

      


        public ActionResult ApprovedRequesitions([DataSourceRequest]DataSourceRequest request)

        {
            ViewBag.Months = new SelectList(RequestHelper.GetMonthList(), "Id", "Name");


            var reliefRequisitions = _hubAllocationService.ReturnRequisitionGroupByReuisitionNo(2);
            if (reliefRequisitions != null)
            {
                var total = reliefRequisitions.Count();
                ViewData["total"] = total;
            }
            else
            {
                return HttpNotFound();
            }
                //.re _reliefRequisitionDetailService.Get(r => r.ReliefRequisition.Status == 2, null, "ReliefRequisition,Donor");
            return View(reliefRequisitions.ToList());
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

        [HttpPost]
        public ActionResult InserRequisition(ICollection<RequisitionViewModel> requisitionDetail, FormCollection form,
                                             string datepicker, string rNumber)
        {
            if (ModelState.IsValid && requisitionDetail !=null )
            {
                string hub = form["hub"].ToString(CultureInfo.InvariantCulture); //retrives Hub id from the view
                DateTime date;


                try
                {
                    date = DateTime.Parse(datepicker);
                        //checkes if date is ethiopian date. if it is then it will enter to the catch and convert to gragorian to persist.
                }
                catch (Exception)
                {

                    var strEth = new getGregorianDate();
                    date = strEth.ReturnGregorianDate(datepicker);
                }


                foreach (RequisitionViewModel appRequisition in requisitionDetail)
                {

                    var newHubAllocation = new HubAllocation
                                               {
                                                   AllocatedBy = 1,
                                                   RequisitionID = appRequisition.RequisitionId,
                                                   AllocationDate = date,
                                                   ReferenceNo = rNumber,
                                                   HubID = int.Parse(hub)
                                               };



                    _hubAllocationService.AddHubAllocation(newHubAllocation);




                }
                return RedirectToAction("ApprovedRequesitions", "HubAllocation");

            }
            return RedirectToAction("ApprovedRequesitions", "HubAllocation");
        }
    }
}
