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
using Cats.Services.EarlyWarning;
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



        //[HttpGet]
        //public JsonResult GetApprovedRequisitions()
        //{
        //    var reliefRequisitions = _reliefRequisitionDetailService.Get(null, null, "ReliefRequisition,Donor");
        //    var result = new List<ReliefRequisitionsViewModel>();

        //    foreach (var item in reliefRequisitions.ToList())
        //    {
        //        var data = new ReliefRequisitionsViewModel();
        //        data.CommodityName = item.Commodity.Name;
        //        data.Region = item.ReliefRequisition.AdminUnit1.Name;
        //        data.Zone = item.ReliefRequisition.AdminUnit1.Name;
        //        if (item.ReliefRequisition.Round != null) data.Round = item.ReliefRequisition.Round.Value;
        //        data.RequistionNo = item.ReliefRequisition.RequisitionNo;
        //        data.Amount = item.Amount;
        //        data.Beneficiaries = item.BenficiaryNo;

        //        result.Add(data);
        //    }

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

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
            //var result = _hubService.GetAllHub().ToList();
            //var hubs = result.Select(item => new HubDto(item.HubId, item.Name)).ToList();

            //Response.Headers.Add("Content-type", "application/json");
            //ViewBag.Hubs = hubs;
            //return Json(hubs,JsonRequestBehavior.AllowGet );
        }

      


        public ActionResult ApprovedRequesitions(ICollection<ReliefRequisitionDetail> requisitionDetail)

        {
            ViewBag.Months = new SelectList(RequestHelper.GetMonthList(), "Id", "Name");
            var reliefRequisitions = _reliefRequisitionDetailService.Get(r=>r.ReliefRequisition.Status == 2, null, "ReliefRequisition,Donor");
            return View(reliefRequisitions.ToList());
        
        }
        public ActionResult Request(ICollection<ReliefRequisitionDetail> requisitionDetail)
        {
            ViewBag.Months = new SelectList(RequestHelper.GetMonthList(), "Id", "Name");
            var reliefRequisitions = _reliefRequisitionDetailService.Get(null, null, "ReliefRequisition,Donor");
            return View(reliefRequisitions.ToList());

        }


        [HttpPost]
        public ActionResult hubAllocation(ICollection<ReliefRequisitionDetail> requisitionDetail, FormCollection form)
        {
            ViewBag.Hubs = new SelectList(_hubService.GetAllHub(), "HubID", "Name");
            ViewBag.Months = new SelectList(RequestHelper.GetMonthList(), "Id", "Name");

            ICollection<ReliefRequisitionDetail> listOfRequsitions = new List<ReliefRequisitionDetail>();
            ReliefRequisitionDetail[] _requisitionDetail;

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

            return View(listOfRequsitions);
        }


        public void InserRequisition(ICollection<ReliefRequisitionDetail> requisitionDetail, FormCollection form, string datepicker, string rNumber)
        {

            string hub = form["hub"].ToString(CultureInfo.InvariantCulture);

            foreach (ReliefRequisitionDetail appRequisition in requisitionDetail)
            {
                var newHubAllocation = new HubAllocation
                                           {
                                               AllocatedBy = appRequisition.CommodityID,
                                               RequisitionID = appRequisition.RequisitionID,
                                               AllocationDate = DateTime.Now,
                                               HubID = int.Parse(hub)
                                           };

                newHubAllocation.AllocatedBy = 1;

                _hubAllocationService.AddHubAllocation(newHubAllocation);
                _hubAllocationService.UpdateRequisitionStatus(appRequisition.ReliefRequisition.RequisitionNo);
            }
        }
        
       
    }
}
