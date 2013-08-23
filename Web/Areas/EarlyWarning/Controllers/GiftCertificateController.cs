
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
using Cats.Services.Common;
using Newtonsoft.Json;
using Master = Cats.Models.Constant.Master;


namespace Cats.Areas.EarlyWarning.Controllers
{
    public class GiftCertificateController : Controller
    {
        private readonly IGiftCertificateService _giftCertificateService;
        private readonly IGiftCertificateDetailService _giftCertificateDetailService;
        private readonly ICommonService _commonService;

        public GiftCertificateController(IGiftCertificateService giftCertificateService, IGiftCertificateDetailService giftCertificateDetailService,ICommonService commonService)
        {
            _giftCertificateService = giftCertificateService;
            _giftCertificateDetailService = giftCertificateDetailService;
            _commonService = commonService;
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

        public ActionResult Create()
        {
            PopulateLookup();
            
            return View(new GiftCertificateViewModel());
        }
        [HttpPost]
        public ActionResult Create(GiftCertificateViewModel giftcertificate)
        {
            if (ModelState.IsValid)
            {
                var giftCertificateModel =GiftCertificateViewModelBinder.BindGiftCertificate( giftcertificate);

                InsertGiftCertificate(giftcertificate, giftCertificateModel);
                //repository.Add( giftCertificate );
                return RedirectToAction("Index");
            }

          PopulateLookup();
           
            return Create(); //GiftCertificateViewModel.GiftCertificateModel(giftcertificate));
        }
        private void InsertGiftCertificate(GiftCertificateViewModel giftcertificate, Cats.Models.GiftCertificate giftCertificateModel)
        {
            List<GiftCertificateDetailsViewModel> giftCertificateDetails = GetSelectedGiftCertificateDetails(giftcertificate.JSONInsertedGiftCertificateDetails);
            var giftDetails = GiftCertificateViewModelBinder.BindListGiftCertificateDetail(giftCertificateDetails);
            foreach (GiftCertificateDetail giftDetail in giftDetails)
            {
                giftCertificateModel.GiftCertificateDetails.Add(giftDetail);
            }
            _giftCertificateService.AddGiftCertificate(giftCertificateModel);
        }

        //generate view models from the respective json array of GiftCertificateDetails json elements
        private List<GiftCertificateDetailsViewModel> GetSelectedGiftCertificateDetails(string jsonArray)
        {
            List<GiftCertificateDetailsViewModel> giftCertificateDetails = null;
            if (!string.IsNullOrEmpty(jsonArray))
            {
                giftCertificateDetails = JsonConvert.DeserializeObject<List<GiftCertificateDetailsViewModel>>(jsonArray);
            }
            return giftCertificateDetails;
        }

        public ActionResult Edit(int id)
        {
            var giftcertificate = _giftCertificateService.Get(t => t.GiftCertificateID == id, null, "GiftCertificateDetails,GiftCertificateDetails.Commodity").FirstOrDefault();
            PopulateLookup(false, giftcertificate);
            var giftCertificateViewModel = GiftCertificateViewModelBinder.BindGiftCertificateViewModel(giftcertificate);
            return View(giftCertificateViewModel);
        }

        [HttpPost]
        public ActionResult Edit(GiftCertificateViewModel giftcertificate)
        {
            //just incase the user meses with the the hidden GiftCertificateID field
            var giftcert = _giftCertificateService.FindById(giftcertificate.GiftCertificateID);

            if (ModelState.IsValid && giftcert != null)
            {

                var giftCertificateModel =GiftCertificateViewModelBinder.BindGiftCertificate(giftcertificate);
                
                _giftCertificateService.EditGiftCertificate(giftCertificateModel);

                return RedirectToAction("Index");
            }
            PopulateLookup(false, giftcert);


              return View(giftcertificate);
        }
        public ActionResult Delete(int id)
        {
            var giftcertificate = _giftCertificateService.FindById(id);
            return View(giftcertificate);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _giftCertificateService.DeleteById(id);
            return RedirectToAction("Index");
        }

        public ActionResult IsBillOfLoadingDuplicate(string BillOfLoading)
        {
            return Json(_giftCertificateService.IsBillOfLoadingDuplicate(BillOfLoading), JsonRequestBehavior.AllowGet);
        }
        private void PopulateLookup(bool isNew=true,Cats.Models.GiftCertificate giftCertificate=null)
        {
            ViewData["Commodities"] = _commonService.GetCommodities(null, t => t.OrderBy(o => o.Name));
           
            ViewBag.DCurrencies = _commonService.GetDetails(d => d.MasterID == Master.Constants.CURRENCY, t => t.OrderBy(o => o.SortOrder));
            ViewBag.DFundSources = _commonService.GetDetails(d => d.MasterID == Master.Constants.FUND_SOURCE, t => t.OrderBy(o => o.SortOrder));
            ViewBag.DBudgetTypes = _commonService.GetDetails(d => d.MasterID == Master.Constants.BUDGET_TYPE, t => t.OrderBy(o => o.SortOrder));
          
            ViewBag.DModeOfTransports = new SelectList(_commonService.GetDetails(d => d.MasterID == Master.Constants.TRANSPORT_MODE, t => t.OrderBy(o => o.SortOrder)), "DetailID", "Name");
            var giftCertificateDetails = new List<GiftCertificateDetailsViewModel>();
            ViewBag.GiftCertificateDetails = giftCertificateDetails;
            if (isNew && giftCertificate == null)
            {
                ViewBag.DonorID = new SelectList(_commonService.GetDonors(null, t => t.OrderBy(o => o.Name)), "DonorID",
                                                 "Name");
                ViewBag.CommodityTypeID =
                    new SelectList(_commonService.GetCommodityTypes(null, t => t.OrderBy(o => o.Name)),
                                   "CommodityTypeID", "Name");
                ViewBag.ProgramID = new SelectList(_commonService.GetPrograms(), "ProgramID", "Name");
            }
            else
            {
                var giftDetails = giftCertificate.GiftCertificateDetails.FirstOrDefault();
                ViewBag.DonorID = new SelectList(_commonService.GetDonors(null, t => t.OrderBy(o => o.Name)), "DonorID",
                                                "Name");
                ViewBag.CommodityTypeID =
                    new SelectList(_commonService.GetCommodityTypes(null, t => t.OrderBy(o => o.Name)),
                                   "CommodityTypeID", "Name", giftDetails == null ? string.Empty : giftDetails.Commodity.CommodityTypeID.ToString());
                ViewBag.ProgramID = new SelectList(_commonService.GetPrograms(), "ProgramID", "Name");
            }
        }

        protected override void Dispose(bool disposing)
        {
            _giftCertificateService.Dispose();
        }

    }
}
