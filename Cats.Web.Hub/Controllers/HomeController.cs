using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models.Hub;
using System.Text;
using System.Security.Cryptography;


namespace Cats.Web.Hub.Controllers
{
    
    public class HomeController : BaseController
    {
       public ActionResult Index()
       {
           return View();
       }
      
    }
}
