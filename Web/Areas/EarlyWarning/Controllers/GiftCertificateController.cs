
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Cats.Areas.EarlyWarning.Models;
using Cats.Areas.GiftCertificate.Models;
using Cats.Models.Partial;
using Cats.Services.EarlyWarning;
using Cats.Models;
using System.Web.Mvc;
using Cats.ViewModelBinder;
using Microsoft.Office.Interop.Word;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;


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
            var gifts = _giftCertificateService.Get(null, t => t.OrderByDescending(o => o.GiftCertificateID),"GiftCertificateDetails,Donor,GiftCertificateDetails.Detail,GiftCertificateDetails.Commodity");
            var giftsViewModel = GiftCertificateViewModelBinder.BindListGiftCertificateViewModel(gifts.ToList());
            return View(giftsViewModel);

        }

        public JsonResult GetListOfCertificate([DataSourceRequest] DataSourceRequest request)
        {
            var giftCertList = _giftCertificateDetailService.GetAllGiftCertificateDetail();

            var result = giftCertList.ToList().Select(item => new GiftCertificateViewModel

                                                                  {
                                                                      CommodityName = item.Commodity.Name,
                                                                      GiftDate = item.GiftCertificate.GiftDate,
                                                                      Donor = item.GiftCertificate.Donor.Name,
                                                                      Program = item.GiftCertificate.Program.Name,
                                                                      ReferenceNo = item.GiftCertificate.ReferenceNo,
                                                                      GiftCertificateID = item.GiftCertificateID,
                                                                      SINumber = item.GiftCertificate.SINumber,
                                                                      PortName = item.GiftCertificate.PortName
                                                                  }).ToList();

            return Json(result.ToDataSourceResult(request, ModelState));
        }

        public void GenerateTemplate1(int id)
        {
        }
        public ActionResult GenerateTemplate(int id)
        {
            //if (RefNo.Trim() == "")
            //    return RedirectToAction("Index");
            //OBJECT OF MISSING "NULL VALUE"
            string path = HttpContext.ApplicationInstance.Server.MapPath("~/Templates/gift_Certificate.dotx");

            Object oMissing = System.Reflection.Missing.Value;

            Object oTemplatePath = path;

            List<GiftCertificateDetail> giftCert = GetGiftCertificate(id);//"L1344/SF148/2011");

            if (giftCert.Count < 1)
                return RedirectToAction("Index");

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
                             wordApp.Selection.TypeText(String.Format("{0:0,0.0}",giftCert[0].EstimatedPrice));
                            break;
                        case "AccountNo":
                             myMergeField.Select();
                             wordApp.Selection.TypeText(giftCert[0].AccountNumber.ToString(CultureInfo.InvariantCulture));
                            break;
                        case "money":
                             myMergeField.Select();
                             wordApp.Selection.TypeText(String.Format("{0:0,0.0}", Math.Truncate( giftCert[0].EstimatedTax)));
                            break;
                        case "cent":
                            myMergeField.Select();
                            wordApp.Selection.TypeText( String.Format("{0:0,0.0}",(giftCert[0].EstimatedTax - (Math.Truncate(giftCert[0].EstimatedTax)))));
                           break;
                        case "Tax":
                            myMergeField.Select();
                            wordApp.Selection.TypeText(giftCert[0].EstimatedTax.ToString());

                            break;

                    }


                }

            }
            try
            {

            wordDoc.SaveAs("gift_cert.doc");
            wordApp.Documents.Open("gift_cert.doc");
            //wordApp.Application.Quit();
            return RedirectToAction("Index"); 
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index"); 
            }
            
        }



        private List<GiftCertificateDetail> GetGiftCertificate(int giftCertId)
        {
            var giftCertList = _giftCertificateDetailService.Get(d => d.GiftCertificate.GiftCertificateID == giftCertId,
                                                                 null, "GiftCertificate");

            return giftCertList.ToList();
        }

        public virtual ActionResult NotUnique(string siNumber, int giftCertificateId)
        {


            if (_giftCertificateService.IsSINumberNewOrEdit(siNumber, giftCertificateId))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(string.Format("{0} is invalid, there is an existing record with the same SI Number ", siNumber),
                        JsonRequestBehavior.AllowGet);

            }
        }
        protected override void Dispose(bool disposing)
        {
            _giftCertificateService.Dispose();
            _giftCertificateDetailService.Dispose();
        }

    }
}
