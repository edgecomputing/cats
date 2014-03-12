using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cats.Library;
using Microsoft.Reporting.WebForms;

namespace SSRS_Portal
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                var userName = System.Configuration.ConfigurationManager.AppSettings["CatsReportUserName"];
                var password = System.Configuration.ConfigurationManager.AppSettings["CatsReportPassword"];
                var url = System.Configuration.ConfigurationManager.AppSettings["CatsReportServerURL"];
                var credential = new CatsReportServerCredentials(userName, password);
                ReportViewer1.ServerReport.ReportServerCredentials = credential;
                ReportViewer1.ServerReport.ReportServerUrl = new Uri(url);
                ReportViewer1.ServerReport.ReportPath = Request["path"];
                ReportViewer1.ServerReport.Refresh();
                //ReportViewer1.ShowPrintButton = CanPrintReport;
                //ReportViewer1.ShowExportControls = CanExportReport; 
            }

        }
    }
}