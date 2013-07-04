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


    }
}
