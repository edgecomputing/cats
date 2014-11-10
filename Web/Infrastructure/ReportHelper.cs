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
        //public static ReportDTO PrintReport(string reportPath, object data, string dataSourceName)
        //{
        //    return PrintReport(reportPath, data, dataSourceName);
        //}

        public static ReportDTO PrintReport(string reportPath, object[] data, string[] dataSourceName)
        {
            return PrintReport(reportPath, data, dataSourceName, "PDF");
        }

        public static ReportDTO PrintReport(string reportPath, object[] data, string[] dataSourceName, string reportType, bool isPortriat = true)
        {
            var localReport = new LocalReport();
            localReport.ReportPath = reportPath;

            for (int i = 0; i < data.Count(); i++)
            {
                var reportDataSource = new ReportDataSource(dataSourceName[i], data[i]);
                localReport.DataSources.Add(reportDataSource);     
            }

            //foreach (var d in data)
            //{
            //    var reportDataSource = new ReportDataSource(dataSourceName, d);
            //    localReport.DataSources.Add(reportDataSource);     
            //}
           
            // string reportType = "PDF";
            
            string mimeType;
            string encoding;
            string fileNameExtension;

            string pageLayout = isPortriat
                                    ? "  <PageWidth>8.5in</PageWidth> <PageHeight>11in</PageHeight> "
                                    : "  <PageWidth>11in</PageWidth> <PageHeight>8.5in</PageHeight> ";
            //The DeviceInfo settings should be changed based on the reportType
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx
            string deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>" + reportType + "</OutputFormat>" +
           pageLayout +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>0.25in</MarginLeft>" +
            "  <MarginRight>0.25in</MarginRight>" +
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
                out warnings
                );

            var result=new ReportDTO{
                                      RenderBytes=renderedBytes,
                                      MimeType=mimeType
                                    };
            return result;
        }

        public static ReportDTO PrintReport(string reportPath, object data, string dataSourceName, bool isPdf=true, bool isPortriat = true)
        {
            var localReport = new LocalReport();
            localReport.ReportPath = reportPath;

            
                var reportDataSource = new ReportDataSource(dataSourceName, data);
                localReport.DataSources.Add(reportDataSource);

            
            //foreach (var d in data)
            //{
            //    var reportDataSource = new ReportDataSource(dataSourceName, d);
            //    localReport.DataSources.Add(reportDataSource);     
            //}

                string reportType = isPdf ? "PDF" : "Excel";
            string pageLayout = isPortriat
                                    ? "  <PageWidth>8.5in</PageWidth> <PageHeight>11in</PageHeight> "
                                    : "  <PageWidth>11in</PageWidth> <PageHeight>8.5in</PageHeight> ";
            string mimeType;
            string encoding;
            string fileNameExtension;

            //The DeviceInfo settings should be changed based on the reportType
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx
            string deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>" + reportType + "</OutputFormat>" +
           pageLayout+
            "  <MarginTop>1in</MarginTop>" +
            "  <MarginLeft>0.25in</MarginLeft>" +
            "  <MarginRight>0.25in</MarginRight>" +
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
                out warnings
                );

            var result = new ReportDTO
            {
                RenderBytes = renderedBytes,
                MimeType = mimeType
            };
            return result;
        }
    }
}