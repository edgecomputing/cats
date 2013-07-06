using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Areas.EarlyWarning.Models;
using Cats.Models;
using Cats.Models.ViewModels;
using Cats.Services.EarlyWarning;

namespace Cats.Areas.EarlyWarning.Controllers
{
    public class ReliefRequisitionController : Controller
    {
        //
        // GET: /EarlyWarning/ReliefRequisition/

        private readonly IReliefRequisitionService _reliefRequisitionService;


        public ReliefRequisitionController(IReliefRequisitionService reliefRequisitionService)
        {
            this._reliefRequisitionService = reliefRequisitionService;


        }

        public ViewResult Requistions()
        {
            var releifRequistions = _reliefRequisitionService.GetAllReliefRequisition();

            return View(releifRequistions);
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
        public ActionResult NewRequisiton(List<ReliefRequisitionNew.ReliefRequisitionNewInput> input)
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
            return RedirectToAction("Requistions", "ReliefRequisition");
        }
        [HttpGet]
        public ActionResult RequistionDetailEdit(int id)
        {
            var requisition =
                _reliefRequisitionService.Get(t => t.RequisitionID == id, null, "ReliefRequisitionDetails").
                    FirstOrDefault();
            if(requisition==null)
            {
                HttpNotFound();
            }
            ViewBag.CurrentRegion = requisition.AdminUnit1.Name;
            ViewBag.CurrentMonth = requisition.RequestedDate.HasValue ? requisition.RequestedDate.Value.ToString("MMM") : "";
            ViewBag.CurrentRound = requisition.Round;
            ViewBag.CurrentYear = requisition.RequestedDate.HasValue ? requisition.RequestedDate.Value.Year.ToString():"";
            ViewBag.CurrentZone = requisition.AdminUnit.Name;
            var editRequisionDetails = (from requitionDetail in requisition.ReliefRequisitionDetails
                                        select new ReleifRequisitionDetailEdit()
                                                   {
                                                       Amount = 0,
                                                       BenficiaryNo = requitionDetail.BenficiaryNo,
                                                       Commodity = requitionDetail.Commodity.Name,
                                                       Donor = requitionDetail.Donor !=null ? requitionDetail.Donor.Name:"",
                                                       FDP = requitionDetail.FDP.Name,
                                                       RequisitionID = requitionDetail.RequisitionID,
                                                       RequisitionDetailID = requitionDetail.RequisitionDetailID,
                                                       RequisitionNo = requitionDetail.ReliefRequisition.RequisitionNo,
                                                       Input =
                                                           new ReleifRequisitionDetailEdit.
                                                           ReleifRequisitionDetailEditInput()
                                                               {
                                                                   Amount = requitionDetail.Amount ,
                                                                   Number = requitionDetail.RequisitionDetailID
                                                               }
                                                   });
            return View(editRequisionDetails);
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
            return RedirectToAction("Requistions", "ReliefRequisition");
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
               return  RedirectToAction("Requistions", "ReliefRequisition");
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
            requisition.Status = (int)REGIONAL_REQUEST_STATUS.Submitted;
            _reliefRequisitionService.Save();
            return RedirectToAction("Requistions", "ReliefRequisition");
        }
    }
}
