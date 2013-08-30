using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;
using Microsoft.Office.Interop.Word;
using Cats.Data.UnitWork;

namespace Cats.Infrastructure.Helpers
{
    public class TemplateGenerator
    {
       private readonly UnitOfWork _unitofwork = new UnitOfWork();
       public bool GenerateTemplate(int id, string templateName)
        {
            //string templateName = string.Empty;
           

            string path = HttpContext.Current.Server.MapPath("~/Templates/" + templateName + ".dotx");

            Object oMissing = System.Reflection.Missing.Value;

            Object oTemplatePath = path;

           
            var giftCert = GetGiftCertificate(id);
            var sourceOfFund =_unitofwork.DetailRepository.FindBy(s => s.DetailID == giftCert[0].DFundSourceID).Single();
            var currency =  _unitofwork.DetailRepository.FindBy(s => s.DetailID == giftCert[0].DCurrencyID).Single();
            var budgetType =  _unitofwork.DetailRepository.FindBy(s => s.DetailID == giftCert[0].DBudgetTypeID).Single();
            if (giftCert.Count < 1)
                return false;

            Application wordApp = new Application();
            Document wordDoc = new Document();

            wordDoc = wordApp.Documents.Add(ref oTemplatePath, ref oMissing, ref oMissing, ref oMissing);

            foreach (Field myMergeField in wordDoc.Fields)
            {
                Range rngFieldCode = myMergeField.Code;
                String fieldText = rngFieldCode.Text;


                if (fieldText.StartsWith(" MERGEFIELD"))
                {


                    Int32 endMerge = fieldText.IndexOf("\\");
                    Int32 fieldNameLength = fieldText.Length - endMerge;
                    String fieldName = fieldText.Substring(11, endMerge - 11);

                    fieldName = fieldName.Trim();

                    switch (fieldName)
                    {
                        case "Commodity":
                            myMergeField.Select();
                            wordApp.Selection.TypeText(giftCert[0].Commodity.Name);
                            break;
                        case "Year":
                            myMergeField.Select();
                            wordApp.Selection.TypeText(giftCert[0].GiftCertificate.GiftDate.Year.ToString(CultureInfo.InvariantCulture));
                            break;
                        case "Bill":
                            myMergeField.Select();
                            wordApp.Selection.TypeText(giftCert[0].BillOfLoading);
                            break;
                        case "Estimated":
                            myMergeField.Select();
                            wordApp.Selection.TypeText(String.Format("{0:0,0.0}", giftCert[0].EstimatedPrice));
                            break;
                        case "AccountNo":
                            myMergeField.Select();
                            wordApp.Selection.TypeText(giftCert[0].AccountNumber.ToString(CultureInfo.InvariantCulture));
                            break;
                        case "money":
                            myMergeField.Select();
                            wordApp.Selection.TypeText(String.Format("{0:0,0.0}", Math.Truncate(giftCert[0].EstimatedTax)));
                            break;
                        case "cent":
                            myMergeField.Select();
                            wordApp.Selection.TypeText(String.Format("{0:0,0.0}", (giftCert[0].EstimatedTax - (Math.Truncate(giftCert[0].EstimatedTax)))));
                            break;
                        case "Tax":
                            myMergeField.Select();
                            wordApp.Selection.TypeText(giftCert[0].EstimatedTax.ToString());
                            break;
                        case "ETA":
                             myMergeField.Select();
                            wordApp.Selection.TypeText(giftCert[0].GiftCertificate.ETA.ToString());
                            break;
                        case "Currency":
                             myMergeField.Select();
                            wordApp.Selection.TypeText(currency.ToString());
                            break;
                        case "AccountNumber":
                             myMergeField.Select();
                            wordApp.Selection.TypeText(giftCert[0].AccountNumber.ToString());
                            break;
                        case "BillFloading":
                             myMergeField.Select();
                            wordApp.Selection.TypeText(giftCert[0].BillOfLoading.ToString());
                            break;
                        case "Donor":
                             myMergeField.Select();
                            wordApp.Selection.TypeText(giftCert[0].GiftCertificate.Donor.Name.ToString());
                            break;
                        case "BudgetType":
                             myMergeField.Select();
                            wordApp.Selection.TypeText(budgetType.ToString());
                            break;
                        case "FundSource":
                             myMergeField.Select();
                            wordApp.Selection.TypeText(sourceOfFund.ToString());
                            break;
                        case "EstimatedPrice":
                             myMergeField.Select();
                            wordApp.Selection.TypeText(giftCert[0].EstimatedPrice.ToString());
                            break;
                        case "EstimatedTax":
                             myMergeField.Select();
                            wordApp.Selection.TypeText(giftCert[0].EstimatedTax.ToString());
                            break;
                        case "WeightINMT":
                             myMergeField.Select();
                            wordApp.Selection.TypeText(giftCert[0].WeightInMT.ToString());
                            break;
                        case "YearPurchased":
                             myMergeField.Select();
                            wordApp.Selection.TypeText(giftCert[0].YearPurchased.ToString());
                            break;
                       
                    }


                }

            }
            try
            {

                wordDoc.SaveAs("gift_cert.doc");
                wordApp.Documents.Open("gift_cert.doc");
               
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private List<GiftCertificateDetail> GetGiftCertificate(int giftCertId)
        {
            var giftCertList = _unitofwork.GiftCertificateDetailRepository.Get(d => d.GiftCertificate.GiftCertificateID == giftCertId,
                                                                 null, "GiftCertificate");

            return giftCertList.ToList();
        }
    }


}