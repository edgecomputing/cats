using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;
using Cats.Models.ViewModels.HRD;
using Cats.Services.EarlyWarning;

namespace Cats.Areas.EarlyWarning.Controllers
{
    public class HRDController : Controller
    {
        //
        // GET: /EarlyWarning/HRD/
        private IAdminUnitService _adminUnitService;

        public HRDController(IAdminUnitService adminUnitService)
        {
            _adminUnitService = adminUnitService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var humanitarianRequirement = new HumanitarianRequirement();
            humanitarianRequirement.HumanitarianRequirementDetails = new List<HumanitarianRequirementDetail>();
            var woredas = _adminUnitService.FindBy(m => m.AdminUnitTypeID == 3);
            foreach (var woreda in woredas)
            {
                var detail = new HumanitarianRequirementDetail();
                detail.Woreda = woreda;
                humanitarianRequirement.HumanitarianRequirementDetails.Add(detail);
            }

            var viewModel = new CreateHumanitarianRequirementViewModel(humanitarianRequirement);
            ViewData["HRDDetail"] = viewModel;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateHumanitarianRequirementViewModel viewModel)
        {
            var requirement = viewModel.HumanitarianRequirement;
            // _hrdService.Add(requirement);
            return RedirectToAction("Index");
        }
    }
}
