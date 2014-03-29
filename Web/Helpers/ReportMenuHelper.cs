using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cats.Models;
using Cats.Models.Constant;
using Cats.ReportPortal;
using Cats.Services.Security;

namespace Cats.Helpers
{
    public static class ReportMenuHelper
    {
        public static MvcHtmlString ReportList(this HtmlHelper helper)
        {
            var html = ReportList();
            return MvcHtmlString.Create(html);
        }

        public static string ReportList()
        {
            var html = string.Empty;
            var userName = System.Configuration.ConfigurationManager.AppSettings["CatsReportUserName"];
            var password = System.Configuration.ConfigurationManager.AppSettings["CatsReportPassword"];
            var user = (UserIdentity)HttpContext.Current.User.Identity;
            var role = UserRoleHelper.GetUserRole(user.Profile.UserName);
            var lang = user.Profile.LanguageCode;
            var currentUser = UserAccountHelper.GetCurrentUser();

            try
            {
                var rs = new ReportingService2010
                {
                    Credentials = new NetworkCredential(userName, password)
                };

                //var folders = rs.ListChildren("/", false);
                ////html += "<ul class='dropdown-menu'>";
                //foreach (var folder in folders)
                //{
                    //if (folder.TypeName == "Folder" && folder.Name != "Data Sources" && folder.Name != "Datasets")
                    //{
                        var reports = new CatalogItem[100];
                        if(currentUser.RegionalUser)
                        {
                            reports = rs.ListChildren("/Regional", false);
                        }
                        else if (currentUser.DefaultHub != null)
                        {
                            reports = rs.ListChildren("/Hub", false);
                        }
                        else if (currentUser.CaseTeam != null)
                        {
                            switch (currentUser.CaseTeam)
                            {
                                case (int)UserType.CASETEAM.EARLYWARNING:
                                    reports = rs.ListChildren("/Early Warning", false);
                                    break;
                                case (int)UserType.CASETEAM.PSNP:
                                    reports = rs.ListChildren("/FSCD", false);
                                    break;
                                case (int)UserType.CASETEAM.LOGISTICS:
                                    reports = rs.ListChildren("/Logistics", false);
                                    break;
                                case (int)UserType.CASETEAM.PROCUREMENT:
                                    reports = rs.ListChildren("/Procurement", false);
                                    break;
                                case (int)UserType.CASETEAM.FINANCE:
                                    reports = rs.ListChildren("/Finance", false);
                                    break;
                            }
                        }

                        //html += "<li class='dropdown-submenu'>";
                        //html += "<a href='#' data-toggle='dropdown'>" + folder.Name + "</a>";
                        //html += "<ul class='dropdown-menu'>";
                        html += "<div id='list8'><ul>";
                        foreach (var report in reports)
                        {
                            var baseUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";
                            html += "<li><a href=' " + baseUrl + "ReportViewer.aspx?path=" + report.Path + "'>" + report.Name + "</a></li>";
                        }
                        html += "</ul>";
                        html += "</li>";
                    //}
                //}
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