using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;

namespace Cats.Controllers
{
    public class CatsSmsGatewayController : Controller
    {
        //
        // GET: /CatsSmsGateway/


        public JsonResult Index()
        {
            return Json(new List<Hub>() , JsonRequestBehavior.AllowGet);
        }

        public JsonResult Send()
        {
            return Json(new List<Hub>(), JsonRequestBehavior.AllowGet);
        }

    }
}
