using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;
using Cats.Services.EarlyWarning;

namespace Cats.Controllers
{
    public class SMSGatewayController : Controller
    {
        //
        // GET: /CatsSmsGateway/

        private readonly IFDPService _FDPService;

        public SMSGatewayController(IFDPService fdpService)
        {
            _FDPService = fdpService;
        }

        public JsonResult Index()
        {
            return Json(_FDPService.GetAllFDP(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Send()
        {
            var fdps = _FDPService.GetAllFDP().Take(2);
            var hh = (from fdp in fdps
                      select new
                          {
                              fdp.Name,
                              fdp.NameAM
                          }
                     );
            return Json(hh, JsonRequestBehavior.AllowGet);
        }

    }
}
