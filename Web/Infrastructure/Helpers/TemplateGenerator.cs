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
           

            string path = HttpContext.Current.Server.MapPath("~/Templates/" + templateName + ".dot");

            Object oMissing = System.Reflection.Missing.Value;

            Object oTemplatePath = path;

           
            var giftCert = GetGiftCertificate(id);
            if (giftCert==null)
                return false;
            var sourceOfFund =_unitofwork.DetailRepository.FindBy(s => s.DetailID == giftCert.DFundSourceID).Select(p=>p.Name).Single();
            var currency =  _unitofwork.DetailRepository.FindBy(s => s.DetailID == giftCert.DCurrencyID).Select(p=>p.Name).Single();
            var budgetType = _unitofwork.DetailRepository.FindBy(s => s.DetailID == giftCert.DBudgetTypeID).Select(p => p.Name).Single();
            

            Application wordApp = new Application();
            Document wordDoc = new Document();
            try
            {
                wordDoc = wordApp.Documents.Add(ref oTemplatePath, ref oMissing, ref oMissing, ref oMissing);
            }catch

            {
                

            }
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

                        case "ReferenceNo":
                            myMergeField.Select();
                            wordApp.Selection.TypeText(giftCert.Commodity.Name);
                            break;
                    case "Commodity":
                            myMergeField.Select();
                            wordApp.Selection.TypeText(giftCert.Commodity.Name);
                            break;
                        case "Year":
                            myMergeField.Select();
                            wordApp.Selection.TypeText(giftCert.GiftCertificate.GiftDate.Year.ToString(CultureInfo.InvariantCulture));
                            break;
                        case "Bill":
                            myMergeField.Select();
                            wordApp.Selection.TypeText(giftCert.BillOfLoading);
                            break;
                        case "Estimated":
                            myMergeField.Select();
                            wordApp.Selection.TypeText(String.Format("{0:0,0.0}", giftCert.EstimatedPrice));
                            break;
                        case "AccountNo":
                            myMergeField.Select();
                            wordApp.Selection.TypeText(giftCert.AccountNumber.ToString(CultureInfo.InvariantCulture));
                            break;
                        case "money":
                            myMergeField.Select();
                            wordApp.Selection.TypeText(String.Format("{0:0,0.0}", Math.Truncate(giftCert.EstimatedTax)));
                            break;
                        case "cent":
                            myMergeField.Select();
                            wordApp.Selection.TypeText(String.Format("{0:0,0.0}", (giftCert.EstimatedTax - (Math.Truncate(giftCert.EstimatedTax)))));
                            break;
                        case "Tax":
                            myMergeField.Select();
                            wordApp.Selection.TypeText(giftCert.EstimatedTax.ToString());
                            break;
                        case "ETA":
                             myMergeField.Select();
                            wordApp.Selection.TypeText(giftCert.GiftCertificate.ETA.ToString());
                            break;
                        case "currency":
                             myMergeField.Select();
                            wordApp.Selection.TypeText(currency.ToString());
                            break;
                        case "AccountNumber":
                             myMergeField.Select();
                            wordApp.Selection.TypeText(giftCert.AccountNumber.ToString());
                            break;
                        case "BillFloading":
                             myMergeField.Select();
                            wordApp.Selection.TypeText(giftCert.BillOfLoading.ToString());
                            break;
                        case "Donor":
                             myMergeField.Select();
                            wordApp.Selection.TypeText(giftCert.GiftCertificate.Donor.Name.ToString());
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
                            wordApp.Selection.TypeText(String.Format("{0:0,0.0}", Math.Truncate(double.Parse(giftCert.EstimatedPrice.ToString()))));
                            break;
                        case "EstimatedTax":
                             myMergeField.Select();
                            wordApp.Selection.TypeText(String.Format("{0:0,0.0}", Math.Truncate(double.Parse(giftCert.EstimatedTax.ToString()))));
                            break;
                        case "WeightInMT":
                             myMergeField.Select();
                            wordApp.Selection.TypeText(giftCert.WeightInMT.ToString());
                            break;
                        case "YearPurchased":
                             myMergeField.Select();
                            wordApp.Selection.TypeText(giftCert.YearPurchased.ToString());
                            break;
                        case "Vessle":
                            myMergeField.Select();
                            wordApp.Selection.TypeText(giftCert.GiftCertificate.Vessel.ToString());
                            break;
                        case "GiftDate":
                            myMergeField.Select();
                            wordApp.Selection.TypeText(giftCert.GiftCertificate.GiftDate.ToString());
                            break;
                        case "SINumber":
                            myMergeField.Select();
                            wordApp.Selection.TypeText(giftCert.GiftCertificate.SINumber.ToString());
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

        private GiftCertificateDetail GetGiftCertificate(int giftCertId)
        {
            var giftCertList = _unitofwork.GiftCertificateDetailRepository.Get(d => d.GiftCertificate.GiftCertificateID == giftCertId,
                                                                 null, "GiftCertificate").SingleOrDefault();

            return giftCertList;
        }
    }


}