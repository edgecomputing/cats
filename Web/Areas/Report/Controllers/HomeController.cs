using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.ReportPortal;
using Cats.Models;

namespace Cats.Areas.Report.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Report/Home/

        public ActionResult Index()
        {
            var reportList = new List<string>();
            var reportData = new Dictionary<string, string>();

            var rs = new ReportingService2010
            {
                Credentials = System.Net.CredentialCache.DefaultNetworkCredentials,
                //Url = "http://USER-PC/ReportServer/ReportService2010.asmx"
            };

            CatalogItem[] items = rs.ListChildren("/", true);

            var reportFolders = new ReportFolder();

            foreach (CatalogItem item in items)
            {
                if(item.TypeName == "Folder")
                {
                    
                }
                else if(item.TypeName == "Report")
                {
                    
                }
                var itemDetail = string.Format("{0} - {1}", item.Name, item.Path);
                reportList.Add(itemDetail);
                reportData.Add(item.Name, item.Path);
            }

            return View(reportData);
        }
    }
}
