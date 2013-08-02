using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Web.Mvc;
using Cats.Models;
using Cats.Services.Security;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Cats.Services.EarlyWarning;
using Cats.Areas.EarlyWarning.Models;

namespace Cats.Areas.EarlyWarning.Controllers
{
    public class NeedAssessmentController : Controller
    {
        //service declarations
        private readonly INeedAssessmentHeaderService _needAssessmentHeaderService;
        private readonly INeedAssessmentDetailService _needAssessmentDetailService;
        private readonly IAdminUnitService _adminUnitService;
        //service injection
        public NeedAssessmentController(INeedAssessmentHeaderService needAssessmentHeader, IAdminUnitService adminUnitService, INeedAssessmentDetailService needAssessmentDetailService)
        {
            this._needAssessmentHeaderService = needAssessmentHeader;
            this._adminUnitService = adminUnitService;
            this._needAssessmentDetailService = needAssessmentDetailService;
        }


        //
        // GET: /EarlyWarning/NeedAssessment/

        public ActionResult Index()
        {
            var zone = _adminUnitService.FindBy(t => t.AdminUnitTypeID == 3);
            var woreda = _adminUnitService.FindBy(t => t.AdminUnitTypeID == 4);
            ViewData["zone"] = zone;
            ViewData["woreda"] = woreda;

            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var needAssessment = _needAssessmentDetailService.GetDraft();
            List<NeedAssessmentViewModel> result = new List<NeedAssessmentViewModel>();


            

            foreach (NeedAssessmentDetail t in needAssessment)
            {
                var tempViewModel = new NeedAssessmentViewModel {NaHeaderId = t.NeedAssessmentHeader.NAHeaderId};

                tempViewModel.NAId = t.NAId;

                var vPoorNoOfM = t.VPoorNoOfM;
                if (vPoorNoOfM != null) tempViewModel.VPoorNoOfM = (int)vPoorNoOfM;
                var vPoorNoOfB = t.VPoorNoOfB;
                if (vPoorNoOfB != null) tempViewModel.VPoorNoOfB = (int) vPoorNoOfB;
                var poorNoOfM = t.PoorNoOfM;
                if (poorNoOfM != null) tempViewModel.PoorNoOfM = (int) poorNoOfM;
                var poorNoOfB = t.PoorNoOfB;
                if (poorNoOfB != null) tempViewModel.PoorNoOfB = (int) poorNoOfB;
                var middleNoOfM = t.MiddleNoOfM;
                if (middleNoOfM != null) tempViewModel.MiddleNoOfM = (int) middleNoOfM;
                var middleNoOfB = t.MiddleNoOfB;
                if (middleNoOfB != null) tempViewModel.MiddleNoOfB = (int) middleNoOfB;
                var bOffNoOfM = t.BOffNoOfM;
                if (bOffNoOfM != null) tempViewModel.BOffNoOfM = (int) bOffNoOfM;
                                           if (t.BOffNoOfB != null) tempViewModel.BOffNoOfB = (int) t.BOffNoOfB;
                                           if (t.Zone != null) tempViewModel.Zone = (int) t.Zone;
                                           if (t.District != null) tempViewModel.District = (int) t.District;
                                           tempViewModel.NeedACreatedDate = t.NeedAssessmentHeader.NeedACreatedDate;
                tempViewModel.NeedAApproved = t.NeedAssessmentHeader.NeedAApproved;
                tempViewModel.NeedACreatedBy = t.NeedAssessmentHeader.NeddACreatedBy;
                                         
                result.Add(tempViewModel);



            }





            return Json(result.ToDataSourceResult(request, ModelState));
           
        }

        //


        //
        // POST: /EarlyWarning/NeedAssessment/Create

        public ActionResult Create([DataSourceRequest] DataSourceRequest request, NeedAssessmentViewModel needAssessment)
        {
            NeedAssessmentHeader needheader = null;
            if (ModelState.IsValid && needAssessment != null)
            {
                var user = (UserIdentity)System.Web.HttpContext.Current.User.Identity;
               

                needheader = new NeedAssessmentHeader
                                     {
                                         NeedAApproved = false,
                                         NeedACreatedDate = DateTime.Now,
                                         NeddACreatedBy = user.Profile.UserAccountId
                                     };

                
                    needheader.NeedAssessmentDetails.Add( new NeedAssessmentDetail()
                                                              {

                                                                  VPoorNoOfM = needAssessment.VPoorNoOfM,
                                                                  VPoorNoOfB = needAssessment.VPoorNoOfB,
                                                                  PoorNoOfM = needAssessment.PoorNoOfM,
                                                                  PoorNoOfB = needAssessment.PoorNoOfB,
                                                                  MiddleNoOfM = needAssessment.MiddleNoOfM,
                                                                  MiddleNoOfB = needAssessment.MiddleNoOfB,
                                                                  BOffNoOfM = needAssessment.BOffNoOfM,
                                                                  BOffNoOfB = needAssessment.BOffNoOfB,
                                                                  Zone = needAssessment.Zone,
                                                                  District = needAssessment.District
                                                              });
                    _needAssessmentHeaderService.AddNeedAssessmentHeader(needheader);

                }



            return Json(new[] { needAssessment }.ToDataSourceResult(request, ModelState));
        }


        // POST: /EarlyWarning/NeedAssessment/Edit/5
         [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit([DataSourceRequest] DataSourceRequest request, NeedAssessmentViewModel needAssessment)
         {
            
            if (ModelState.IsValid && needAssessment != null)
            {
                 var user = (UserIdentity)System.Web.HttpContext.Current.User.Identity;
                NeedAssessmentDetail needHeader=null;


                    needHeader = _needAssessmentDetailService.FindBy(n=>n.NAId == needAssessment.NAId).SingleOrDefault();
                    needHeader.NeedAssessmentHeader.NeedAApproved = false;
                    needHeader.NeedAssessmentHeader.NeedACreatedDate = DateTime.Now;
                    needHeader.NeedAssessmentHeader.NeddACreatedBy = user.Profile.UserAccountId;



                needHeader.VPoorNoOfM = needAssessment.VPoorNoOfM;
                needHeader.VPoorNoOfB = needAssessment.VPoorNoOfB;
                needHeader.PoorNoOfM = needAssessment.PoorNoOfM;
                needHeader.PoorNoOfB = needAssessment.PoorNoOfB;
                needHeader.MiddleNoOfM = needAssessment.MiddleNoOfM;
                needHeader.MiddleNoOfB = needAssessment.MiddleNoOfB;
                needHeader.BOffNoOfM = needAssessment.BOffNoOfM;
                needHeader.BOffNoOfB = needAssessment.BOffNoOfB;
                needHeader.Zone = needAssessment.Zone;
                needHeader.District = needAssessment.District;
                  

                


                _needAssessmentDetailService.EditNeedAssessmentDetail(needHeader);



            }
            return Json(new[] { needAssessment }.ToDataSourceResult(request, ModelState));
        }






        //
        // POST: /EarlyWarning/NeedAssessment/Delete/5

         public ActionResult Delete([DataSourceRequest] DataSourceRequest request, NeedAssessmentViewModel needAssessment)
        {
            if (ModelState.IsValid && needAssessment != null)
            {
                var needHeader = _needAssessmentDetailService.FindById(needAssessment.NAId);
                if (needHeader != null)
                {
                    _needAssessmentDetailService.DeleteNeedAssessmentDetail(needHeader);
                }
               

            }
            return Json(ModelState.ToDataSourceResult());
        }


        public ActionResult NewNeedAssessment()
        {
            
            return View();
        }


       
        
    }
}
