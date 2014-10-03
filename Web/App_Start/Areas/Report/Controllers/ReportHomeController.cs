using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.ReportPortal;
using Cats.Models;

namespace Cats.Areas.Report.Controllers
{
    public class ReportHomeController : Controller
    {
        //
        // GET: /Report/Home/

        public ActionResult Index()
        {
            var rs = new ReportingService2010
            {
                Credentials = System.Net.CredentialCache.DefaultCredentials,
                //Url = "http://USER-PC/ReportServer/ReportService2010.asmx"
            };

            //var folders = rs.ListChildren("/", false);

            //var reportFolders = new List<ReportFolder>();

            //foreach (var folder in folders)
            //{
            //    var reportFolder = new ReportFolder {Name = folder.Name, URL = folder.Path};

            //    var reports = rs.ListChildren("/" + folder.Name + "/", false);
            //    foreach (var report in reports)
            //    {
            //        var reportObj = new ReportObj {Name = report.Name, URL = report.Path};
            //        reportFolder.Reports.Add(reportObj);
            //    }
            //    reportFolders.Add(reportFolder);
            //}
            var reportFolders = new List<ReportFolder>()
                {
                    new ReportFolder{ Name = "EarlyWarning", URL = "/EarlyWarning", Reports = new List<ReportObj>{new ReportObj{Name = "StockStatus", URL = "/EarlyWarning/StockStatus", Description = "Sample Desc 1"}}},
                    new ReportFolder{ Name = "PSNP", URL = "/PSNP", Reports = new List<ReportObj>{new ReportObj{Name = "StockStatus", URL = "/EarlyWarning/StockStatus", Description = "Sample Desc 1"}}},
                    new ReportFolder{ Name = "Logistics", URL = "/Logistics", Reports = new List<ReportObj>{new ReportObj{Name = "StockStatus", URL = "/EarlyWarning/StockStatus", Description = "Sample Desc 1"}}},
                    new ReportFolder{ Name = "Procurement", URL = "/Procurement", Reports = new List<ReportObj>{new ReportObj{Name = "StockStatus", URL = "/EarlyWarning/StockStatus", Description = "Sample Desc 1"}}},
                };
            return View(reportFolders);
        }
    }
}
