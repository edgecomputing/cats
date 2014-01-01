using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cats.Models;
using Cats.ReportPortal;

namespace Cats.Helpers
{
    public static class ReportMenuHelper
    {
        public static MvcHtmlString ReportMenu(this HtmlHelper helper)
        {
            var html = ReportMenu();
            return MvcHtmlString.Create(html);
        }

        public static string ReportMenu()
        {
            var html = string.Empty;
            var userName = System.Configuration.ConfigurationManager.AppSettings["CatsReportUserName"];
            var password = System.Configuration.ConfigurationManager.AppSettings["CatsReportPassword"];
            try
            {
                var rs = new ReportingService2010
                {
                    Credentials = new NetworkCredential(userName, password)
                };

                var folders = rs.ListChildren("/", false);
                html += "<ul class='dropdown-menu'>";
                foreach (var folder in folders)
                {
                    if (folder.TypeName == "Folder" && folder.Name != "Data Sources")
                    {
                        var reports = rs.ListChildren("/" + folder.Name, false);

                        html += "<li class='dropdown-submenu'>";
                        html += "<a href='#' data-toggle='dropdown'>" + folder.Name + "</a>";
                        html += "<ul class='dropdown-menu'>";
                        foreach (var report in reports)
                        {
                            html += "<li><a href='/ReportViewer.aspx?path=" + report.Path + "'>" + report.Name + "</a></li>";
                        }
                        html += "</ul>";
                        html += "</li>";
                    }
                }
                html += "</ul>";
            }
            catch(Exception ex)
            {
                html = "";
            }
            
            return html;
        }
    }
}