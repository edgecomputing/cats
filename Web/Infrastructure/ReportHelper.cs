using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models.ViewModels;
using Microsoft.Reporting.WebForms;

namespace Cats.Infrastructure
{
    public static class  ReportHelper
    {
        public static ReportDTO PrintReport(string reportPath, object data, string dataSourceName)
        {
            return PrintReport(reportPath, data, dataSourceName, "PDF");
        }
        public static ReportDTO PrintReport(string reportPath, object data, string dataSourceName, string reportType)
        {
            var localReport = new LocalReport();
            localReport.ReportPath = reportPath;

            var reportDataSource = new ReportDataSource(dataSourceName, data);
            localReport.DataSources.Add(reportDataSource);
           // string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;

            //The DeviceInfo settings should be changed based on the reportType
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx
            string deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>" + reportType + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            //Render the report
            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
       var result=new ReportDTO{RenderBytes=renderedBytes,
           MimeType=mimeType}
            ;
            return result;
        }
    }
}