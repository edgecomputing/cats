using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.EasyAccess;
using EasyCats.ViewModels;

namespace Cats.Areas.Regional.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Regional/Home/

        public ActionResult Index()
        {
            var table = new UserProfile();
            //grab all the products
            var products = table.All();

            return View();
        }
    }
}