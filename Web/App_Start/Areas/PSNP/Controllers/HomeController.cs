using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;

namespace Cats.Areas.PSNP.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /PSNP/Home/`

        //public ActionResult Index()
        //{
        //    List<UIWidget> widgets = new List<UIWidget> { 
        //    new UIWidget{ Title = "Quick Shortcuts", Icon = "icon-bookmark", Source = "Dashboard/_Shortcuts" },
        //    new UIWidget{ Title = "For Your Action", Icon = "icon-bullhorn", Source = "Dashboard/_Alerts" },
        //    new UIWidget{ Title = "Beneficiaries Chart", Icon = "icon-bar-chart", Source = "Dashboard/_BarBeneficiaries" },
        //    new UIWidget{ Title = "Regional Monthly Requests", Icon = "icon-bar-chart", Source = "_PieRequests" },
        //    new UIWidget{ Title = "Beneficiaries Comparison for the Last 6 Months", Icon = "icon-bookmark", Source = "_BarRegionalReqDetailCommodity" },
        //    new UIWidget{ Title = "Quick Shortcuts", Icon = "icon-bookmark", Source = "" }
        //    };
        //    return View(widgets);
        //}
        
        public ActionResult Index()
        {
            return View();
        }
    }
}