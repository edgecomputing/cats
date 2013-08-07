using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Services.EarlyWarning;
using Cats.Models;
using System.Web.Mvc;
using Microsoft.Office.Interop.Word;

namespace Cats.Areas.EarlyWarning.Controllers
{
    public class GiftCertificateController : Controller
    {
        private readonly IGiftCertificateService _giftCertificateService;
        private readonly IGiftCertificateDetailService _giftCertificateDetailService;

        public GiftCertificateController(IGiftCertificateService giftCertificateService, IGiftCertificateDetailService giftCertificateDetailService)
        {
            _giftCertificateService = giftCertificateService;
            _giftCertificateDetailService = giftCertificateDetailService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public void GenerateTemplate(string RefNo)
        {
            //OBJECT OF MISSING "NULL VALUE"
            string path = HttpContext.ApplicationInstance.Server.MapPath("~/Templates/gift_Certificate.dotx");
            if (path != null) Response.Write(path);
            Object oMissing = System.Reflection.Missing.Value;

            Object oTemplatePath = path ;

            List<GiftCertificateDetail> giftCert = GetGiftCertificate(RefNo);//"L1344/SF148/2011");

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
                             wordApp.Selection.TypeText(giftCert[0].GiftCertificate.GiftDate.Year.ToString());
                            break;
                        case "Bill":
                             myMergeField.Select();
                             wordApp.Selection.TypeText(giftCert[0].BillOfLoading);
                            break;
                        case "Estimated":
                             myMergeField.Select();
                             wordApp.Selection.TypeText(giftCert[0].EstimatedPrice.ToString());
                            break;
                        case "AccountNo":
                             myMergeField.Select();
                             wordApp.Selection.TypeText(giftCert[0].AccountNumber.ToString());
                            break;
                        case "Tax":
                             myMergeField.Select();
                             wordApp.Selection.TypeText(giftCert[0].EstimatedTax.ToString());
                            break;
                        
                    }
                   

                }

            }
            wordDoc.SaveAs("gift_cert.doc");
            wordApp.Documents.Open("gift_cert.doc");
            //wordApp.Application.Quit();
        }



        private List<GiftCertificateDetail> GetGiftCertificate(string giftReferenceNo)
        {
            var giftCertList = _giftCertificateDetailService.Get(d => d.GiftCertificate.ReferenceNo == giftReferenceNo,
                                                                 null, "GiftCertificate");

            return giftCertList.ToList();
        }


    }
}
