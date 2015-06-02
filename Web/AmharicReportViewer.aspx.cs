using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cats.Library;
using Microsoft.Reporting.WebForms;

namespace Cats
{
    public partial class AmharicReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ReportViewerAmh.ProcessingMode = ProcessingMode.Remote;
                var userName = System.Configuration.ConfigurationManager.AppSettings["CatsReportUserName"];
                var password = System.Configuration.ConfigurationManager.AppSettings["CatsReportPassword"];
                var url = System.Configuration.ConfigurationManager.AppSettings["CatsReportServerURL"];
                var credential = new CatsReportServerCredentials(userName, password);
                ReportViewerAmh.ServerReport.ReportServerCredentials = credential;
                ReportViewerAmh.ServerReport.ReportServerUrl = new Uri(url);
                ReportViewerAmh.ServerReport.ReportPath = Request["path"];
                ReportViewerAmh.ServerReport.Refresh();
             
            }
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ReportViewerAmh.ProcessingMode = ProcessingMode.Remote;
            var userName = System.Configuration.ConfigurationManager.AppSettings["CatsReportUserName"];
            var password = System.Configuration.ConfigurationManager.AppSettings["CatsReportPassword"];
            var url = System.Configuration.ConfigurationManager.AppSettings["CatsReportServerURL"];
            var credential = new CatsReportServerCredentials(userName, password);
            ReportViewerAmh.ServerReport.ReportServerCredentials = credential;
            ReportViewerAmh.ServerReport.ReportServerUrl = new Uri(url);
            ReportViewerAmh.ServerReport.ReportPath = Request["path"];
            ReportViewerAmh.ServerReport.Refresh();

            ReportParameter[] reportParameterCollection = new ReportParameter[2];       
            reportParameterCollection[0] = new ReportParameter();
            reportParameterCollection[0].Name = "StartDate";
            var start = txtStartDate.Text;
            var end = txtEndDate.Text;
            reportParameterCollection[0].Values.Add(start);                         

            reportParameterCollection[1] = new ReportParameter();
            reportParameterCollection[1].Name = "EndDate";
            reportParameterCollection[1].Values.Add(end);
            ReportViewerAmh.ServerReport.SetParameters(reportParameterCollection);
            ReportViewerAmh.ServerReport.Refresh();
        }

       

    }
}
