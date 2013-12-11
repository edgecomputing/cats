using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Administration;
using Cats.Web.Adminstration.Models.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Cats.Web.Adminstration.ViewModelBinder;
using Cats.Models;
namespace Cats.Web.Adminstration.Controllers
{
    public class DonorWoredaController : Controller
    {

        //
        // GET: /DonorWoreda/
        private IDonorService _donorService;
        private IAdminUnitService _adminUnitService;
        private IWoredaDonorService _woredaDonorService;

        public DonorWoredaController(IDonorService donorService, IAdminUnitService adminUnitService, IWoredaDonorService woredaDonorService)
        {
            _donorService = donorService;
            _adminUnitService = adminUnitService;
            _woredaDonorService = woredaDonorService;
        }

        public ActionResult Index()
        {
           ViewData["woredas"] = _woredaDonorService.GetWoredasNotYetAssigned();
           ViewData["donors"] = _woredaDonorService.GetDonors();
          
            return View();
        }




        #region read
        public ActionResult DonorWoredaRead([DataSourceRequest] DataSourceRequest request)
        {

            var woredasByDonor = _woredaDonorService.GetAllWoredaDonor();
            var woredasByDonorToDisplay = DonorWoredaViewModelBinder.BindDonorViewModel(woredasByDonor).ToList();
            return Json(woredasByDonorToDisplay.ToDataSourceResult(request));
        }
        #endregion

        #region add

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DonorWoredaCreate([DataSourceRequest] DataSourceRequest request, DonorWoredaViewModel donorWoredaViewModel)
        {
            if (donorWoredaViewModel != null && ModelState.IsValid)
            {
                try
                {
                    _woredaDonorService.AddWoredaDonor(DonorWoredaViewModelBinder.BindDonorViewModel(donorWoredaViewModel));
                }
                catch (Exception)
                {

                    ModelState.AddModelError("Errors", "Woreda has already been assigned a Donor.");
                    
                }
               
            }

            return Json(new[] { donorWoredaViewModel }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #region Update
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DonorWoredaUpdate([DataSourceRequest] DataSourceRequest request, DonorWoredaViewModel donorWoredaViewModel)
        {
            if (donorWoredaViewModel != null && ModelState.IsValid)
            {
                var origin = _woredaDonorService.FindById(donorWoredaViewModel.WoredaDonorInt);
                origin.WoredaId = donorWoredaViewModel.WoredaId;
                origin.DonorId = donorWoredaViewModel.DonorId;
                _woredaDonorService.EditWoredaDonor(origin);
            }
            return Json(new[] { donorWoredaViewModel }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region delete
        public ActionResult Delete(int id)
        {
            var donor = _woredaDonorService.FindById(id);
            try
            {
                _woredaDonorService.DeleteWoredaDonor(donor);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Errors", "Unable to delete Woreda");
            }
            return RedirectToAction("Index");
        }
        #endregion


    }
}
