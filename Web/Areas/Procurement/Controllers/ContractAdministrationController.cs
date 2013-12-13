using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Procurement;

namespace Cats.Areas.Procurement.Controllers
{
    public class ContractAdministrationController : Controller
    {
        private readonly ITransporterService _transporterService;

        public ContractAdministrationController(ITransporterService transporterService)
        {
            _transporterService = transporterService;
        }
        //
        // GET: /Procurement/ContractAdministration/

        public ActionResult Index()
        {
            return View();
        }

        
    }
}
