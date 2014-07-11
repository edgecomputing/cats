using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using System.Reflection;
using Cats.Models.Hubs;
using Cats.Services.Hub;
namespace Cats.Web.Hub.Helpers
{
    public class LetterTemplateHelper
    {
        
        private static string html = "<br/><table id='detail' style=\"width:100%\">" +
                                        "<thead>" +
                                            "<tr id='headers'>" +
                                                "<th>Currency</th>" +
                                                "<th>Account Number</th>" +
                                                "<th>Bill of Loading</th>" +
                                                "<th>Commodity</th>" +
                                                "<th>Budget Type</th>" +
                                                "<th>Fund Source</th>" +
                                                "<th>Estimated Price</th>" +
                                                "<th>Estimated Tax</th>" +
                                                "<th>Sent Quantitty(MT)</th>" +
                                                "<th>Purchased Year</th>" +
                                            "</tr>" +
                                        "</thead>" +
                                        "<tbody>" +
                                        "<tr id='rows'>" +
                                            "<td>{CURRENCY}</td>" +
                                             "<td>{ACCOUNTNUMBER}</td>" +
                                             "<td>{BILLOFLOADING}</td>" +
                                             "<td>{COMMODITY}</td>" +
                                             "<td>{BUDGETTYPE}</td>" +
                                             "<td>{FUNDSOURCE}</td>" +
                                             "<td>{ESTIMATEDPRICE}</td>" +
                                             "<td>{ESTIMATEDTAX}</td>" +
                                             "<td>{WEIGHTINMT}</td>" +
                                             "<td>{YEARPURCHASED}</td>" +
                                         "</tr>" +
                                         "</tbody>" +
                                       "</table><br/>";
       
        public string Parse(int certificateId, int templateId)
        {
           
            var letterService = (ILetterTemplateService)DependencyResolver.Current.GetService(typeof (ILetterTemplateService));
            var giftService =(IGiftCertificateService)DependencyResolver.Current.GetService(typeof(IGiftCertificateService));

            GiftCertificate gift = giftService.FindById(certificateId);

            LetterTemplate template = letterService.FindById(templateId);
            string raw = HttpContext.Current.Server.HtmlDecode(template.Template);
            string tableString = string.Empty;
            int startIndex = raw.IndexOf("<table id=\"detail\"");//("<table id=\"detail\" style=\"width:100%;\">")
            if (startIndex != -1)
            {
                int lastIndex = raw.IndexOf("</table>", startIndex) + 8;
                if (lastIndex != -1)
                {
                    int length = lastIndex - startIndex;
                    tableString = raw.Substring(startIndex, length);
                }
            }
            if(@tableString != "")
                raw = raw.Replace(@tableString, "{tablePlaceHolder}");
            raw = PopulateCertificate(raw, gift);
            string populatedTable = string.Empty;
            if(@tableString != "")
                populatedTable = PopulateTableData(gift.GiftCertificateDetails.ToList(), tableString.Replace("&nbsp;", ""));
            string finalString = raw.Replace("{tablePlaceHolder}", populatedTable);
            
            return finalString;
        }

        private string PopulateTableData(List<GiftCertificateDetail> giftDetails, string tableString)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(tableString);
            XElement table = XElement.Load(new XmlNodeReader(doc));
            IEnumerable<XElement> tableData = (from el in table.Element("tbody").Descendants("td")
                                               select el);
            XElement newTable = new XElement("table");
            foreach (GiftCertificateDetail det in giftDetails)
            {
                XElement row = new XElement("tr");

                foreach (XElement td in tableData)
                {
                    string key = (string)td.Value;
                    string value = GetValue(key, det);
                    row.Add(new XElement("td", value));
                }
                newTable.Add(row);
            }

            var newRows = newTable.Elements("tr");
            XElement oldRow = (from rw in table.Descendants("tr")
                               where rw.Attribute("id").Value == "rows"
                               select rw).First();
            oldRow.ReplaceWith(newRows);
            string populatedTable = table.ToString();
            return populatedTable;
        }

        private string PopulateCertificate(string templateString, GiftCertificate cert)
        {
            templateString = SearchAndReplace(CertificateFields.DONOR, GetValue(CertificateFields.DONOR, cert), templateString);
            templateString = SearchAndReplace(CertificateFields.ETA, GetValue(CertificateFields.ETA, cert), templateString);
            templateString = SearchAndReplace(CertificateFields.GIFTDATE, GetValue(CertificateFields.GIFTDATE, cert), templateString);
            templateString = SearchAndReplace(CertificateFields.REFERENCENO, GetValue(CertificateFields.REFERENCENO, cert), templateString);
            templateString = SearchAndReplace(CertificateFields.SINUMBER, GetValue(CertificateFields.SINUMBER, cert), templateString);
            templateString = SearchAndReplace(CertificateFields.VESSEL, GetValue(CertificateFields.VESSEL, cert), templateString);
            return templateString;

        }

        private string GetValue(string key, GiftCertificateDetail detail)
        {
            
            LetterTemplateService letterService = new LetterTemplateService();
            if (!string.IsNullOrEmpty(key) && detail != null)
            {
                switch (key.ToUpper())
                {
                    case CertificateDetailFields.CURRENCY:
                        return letterService.FindById(detail.DCurrencyID).Name;
                    case CertificateDetailFields.ACCOUNTNUMBER:
                        return detail.AccountNumber.ToString();
                    case CertificateDetailFields.BILLOFLOADING:
                        return detail.BillOfLoading;
                    case CertificateDetailFields.COMMODITY:
                        return detail.Commodity.Name;
                    case CertificateDetailFields.BUDGETTYPE:
                        return letterService.FindById(detail.DBudgetTypeID).Name;
                    case CertificateDetailFields.FUNDSOURCE:
                        return letterService.FindById(detail.DFundSourceID).Name;
                    case CertificateDetailFields.ESTIMATEDPRICE:
                        return detail.EstimatedPrice.ToString("N3");
                    case CertificateDetailFields.ESTIMATEDTAX:
                        return detail.EstimatedPrice.ToString("N3");
                    case CertificateDetailFields.WEIGHTINMT:
                        return detail.WeightInMT.ToString("N3");
                    case CertificateDetailFields.YEARPURCHASED:
                        return detail.YearPurchased.ToString();
                    case CertificateFields.DONOR:
                        return detail.GiftCertificate.Donor.Name;
                    case CertificateFields.ETA:
                        return detail.GiftCertificate.ETA.ToShortDateString();
                    case CertificateFields.GIFTDATE:
                        return detail.GiftCertificate.GiftDate.ToShortDateString();
                    case CertificateFields.REFERENCENO:
                        return detail.GiftCertificate.ReferenceNo;
                    case CertificateFields.SINUMBER:
                        return detail.GiftCertificate.ShippingInstruction.Value;
                    case CertificateFields.VESSEL:
                        return detail.GiftCertificate.Vessel;
                }
            }
            return string.Empty;
        }

        private string GetValue(string key, GiftCertificate certificate)
        {
            if (!string.IsNullOrEmpty(key) && certificate != null)
            {
                switch (key.ToUpper())
                {
                    case CertificateFields.DONOR:
                        return certificate.Donor.Name;
                    case CertificateFields.ETA:
                        return certificate.ETA.ToShortDateString();
                    case CertificateFields.GIFTDATE:
                        return certificate.GiftDate.ToShortDateString();
                    case CertificateFields.REFERENCENO:
                        return certificate.ReferenceNo;
                    case CertificateFields.SINUMBER:
                        return certificate.ShippingInstruction.Value;
                    case CertificateFields.VESSEL:
                        return certificate.Vessel;
                }
            }
            return string.Empty;
        }

        private string SearchAndReplace(string key, string value, string template)
        {
            key = key.ToUpper();
            while (template.IndexOf(key) != -1)
            {
                template = template.Replace(key, value);
            }
            return template;
        }


        public string GetDefaultGiftDetail()
        {
            return html;
        }

    }

    public class CertificateFields
    {
        public const string DONOR = "{DONOR}";
        public const string ETA = "{ETA}";
        public const string GIFTDATE = "{GIFTDATE}";
        public const string REFERENCENO = "{REFERENCENO}";
        public const string SINUMBER = "{SINUMBER}";
        public const string VESSEL = "{VESSEL}";
    }

    public class CertificateDetailFields
    {
        public const string CURRENCY = "{CURRENCY}";
        public const string ACCOUNTNUMBER = "{ACCOUNTNUMBER}";
        public const string BILLOFLOADING = "{BILLOFLOADING}";
        public const string COMMODITY = "{COMMODITY}";
        public const string BUDGETTYPE = "{BUDGETTYPE}";
        public const string FUNDSOURCE = "{FUNDSOURCE}";
        public const string ESTIMATEDPRICE = "{ESTIMATEDPRICE}";
        public const string ESTIMATEDTAX = "{ESTIMATEDTAX}";
        public const string WEIGHTINMT = "{WEIGHTINMT}";
        public const string YEARPURCHASED = "{YEARPURCHASED}";
    }
}