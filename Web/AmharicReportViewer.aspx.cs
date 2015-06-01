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
            
            ReportViewerAmh.ProcessingMode = ProcessingMode.Remote;
   ReportViewerAmh.ServerReport.ReportServerUrl = new Uri("http://FISH:80/ReportServer");
   
   ReportViewerAmh.ServerReport.ReportPath = "/Logistics/GRNEntryReport";
   ReportViewerAmh.ServerReport.Refresh();
 
   ReportParameter[] reportParameterCollection = new ReportParameter[2];       //Array size describes the number of paramaters.
   reportParameterCollection[0] = new ReportParameter();
   reportParameterCollection[0].Name = "StartDate";                                 //Give Your Parameter Name
   reportParameterCollection[0].Values.Add("12/12/2010");                         //Pass Parametrs's value here.

   reportParameterCollection[1] = new ReportParameter();
   reportParameterCollection[1].Name = "EndDate";                                 //Give Your Parameter Name
   reportParameterCollection[1].Values.Add("12/12/2015");  
   //ReportViewerAmh.ServerReport.SetParameters(reportParameterCollection);
   ReportViewerAmh.ServerReport.Refresh();
}

        }
    }
