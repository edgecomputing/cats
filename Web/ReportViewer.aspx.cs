using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace SSRS_Portal
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ReportViewer1.ProcessingMode = ProcessingMode.Remote;
            ReportViewer1.ServerReport.ReportServerUrl = new Uri("http://192.168.1.248/reportserver");
            ReportViewer1.ServerReport.ReportPath = Request["path"];
            //ReportViewer1.ShowPrintButton = CanPrintReport;
            //ReportViewer1.ShowExportControls = CanExportReport;

        }
    }
}