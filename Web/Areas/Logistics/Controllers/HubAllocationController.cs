using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.Logistics.Models;
using Cats.Models;
using Cats.Services.EarlyWarning;
using Cats.Models.ViewModels;
using Cats.Helpers;
using HubAllocation = Cats.Models.HubAllocation;

namespace Cats.Areas.Logistics.Controllers
{
    public class HubAllocationController : Controller
    {
        //
        // GET: /Logistics/HubAllocation/

        
        private readonly IReliefRequisitionDetailService _reliefRequisitionDetailService;
        private readonly IHubService _hubService;
        private readonly IHubAllocationService _hubAllocationService;
        public HubAllocationController(IReliefRequisitionDetailService reliefRequisitionDetailService,IHubService hubService,
           IHubAllocationService hubAllocationService)
        {
            this._hubService = hubService;
            this._reliefRequisitionDetailService = reliefRequisitionDetailService;
            this._hubAllocationService = hubAllocationService;
        }

    
      


        public ActionResult AssignHub()
        {

            ViewBag.Months = new SelectList(RequestHelper.GetMonthList(), "Id", "Name");
            return View();
        }
        
        public JsonResult GetRequisitionsForAssignment()
        {
            var reliefRequisitions = _reliefRequisitionDetailService.Get(null, null, "ReliefRequisition,Donor");
            var result = reliefRequisitions.ToList().Select(item => new AssignHubViewModel
                                                                        {
                                                                            Commodity = item.Commodity.Name,
                                                                            RegionName = item.ReliefRequisition.AdminUnit1.Name, 
                                                                            ZoneName = item.ReliefRequisition.AdminUnit1.Name, 
                                                                            RequisitionNo = item.ReliefRequisition.RequisitionNo, 
                                                                            RequisitionId = item.ReliefRequisition.RequisitionID, 
                                                                           Hub = string.Empty
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

      


        public ActionResult ApprovedRequesitions()

        {
            ViewBag.Months = new SelectList(RequestHelper.GetMonthList(), "Id", "Name");


            var reliefRequisitions = _hubAllocationService.ReturnRequisitionGroupByReuisitionNo(2);//.re _reliefRequisitionDetailService.Get(r => r.ReliefRequisition.Status == 2, null, "ReliefRequisition,Donor");
            return View(reliefRequisitions.ToList());
        
        }
        public ActionResult Request(ICollection<ReliefRequisitionDetail> requisitionDetail)
        {
            ViewBag.Months = new SelectList(RequestHelper.GetMonthList(), "Id", "Name");
            var reliefRequisitions = _reliefRequisitionDetailService.Get(null, null, "ReliefRequisition,Donor");
            return View(reliefRequisitions.ToList());

        }


        [HttpPost]
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
            if (ModelState.IsValid)
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
