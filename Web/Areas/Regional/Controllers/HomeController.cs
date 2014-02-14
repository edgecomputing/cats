using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.EasyAccess;
using EasyCats.ViewModels;
using Cats.Services.Dashboard;

namespace Cats.Areas.Regional.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Regional/Home/
        public ActionResult Index()
        {
            ViewBag.RegionName = "Afar";
            return View();
        }

       
    }
}