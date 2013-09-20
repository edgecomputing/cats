using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cats.Models;
using Cats.Services.Administration;
using Cats.Web.Adminstration.Models.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Web.Adminstration.Controllers
{
    public class DonorController : Controller
    {
        //
        private IDonorService _donorService;

        public DonorController(IDonorService donorService)
        {
            _donorService = donorService;
        }

        public ActionResult Index()
        {
            var donor = _donorService.GetAllDonor();
            return View(donor);
        }

        public ActionResult Donor_Read([DataSourceRequest] DataSourceRequest request)
        {

            var donor = _donorService.GetAllDonor();
            var donorToDisplay = GetDonors(donor).ToList();
            return Json(donorToDisplay.ToDataSourceResult(request));
        }


        private IEnumerable<DonorModel> GetDonors(IEnumerable<Donor> donor)
        {
            return (from donors in donor
                    select new DonorModel()
                    {
                        DonorID = donors.DonorID,
                        DonorCode = donors.DonorCode,
                        Name = donors.Name,
                        IsResponsibleDonor = donors.IsResponsibleDonor,
                        IsSourceDonor = donors.IsResponsibleDonor,
                        LongName = donors.LongName

                    });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Donor_Create([DataSourceRequest] DataSourceRequest request, DonorModel donor)
        {
            if (donor != null && ModelState.IsValid)
            {


                _donorService.AddDonor(BindDonor(donor));
            }

            return Json(new[] { donor }.ToDataSourceResult(request, ModelState));
        }


        private Donor BindDonor(DonorModel model)
        {
            if (model == null) return null;
            var donor = new Donor()
            {
                DonorID = model.DonorID,
                DonorCode = model.DonorCode,
                Name = model.Name,
                IsResponsibleDonor = model.IsResponsibleDonor,
                IsSourceDonor = model.IsResponsibleDonor,
                LongName = model.LongName
            };
            return donor;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Donor_Update([DataSourceRequest] DataSourceRequest request, DonorModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var origin = _donorService.FindById(model.DonorID);
                origin.DonorCode = model.DonorCode;
                origin.Name = model.Name;
                origin.IsResponsibleDonor = model.IsResponsibleDonor;
                origin.IsSourceDonor = model.IsSourceDonor;
                origin.LongName = model.LongName;
                _donorService.EditDonor(origin);
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Donor_Destroy([DataSourceRequest] DataSourceRequest request, DonorModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var donor = _donorService.FindById(model.DonorID);
                _donorService.DeleteDonor(donor);
            }
            return Json(ModelState.ToDataSourceResult());
        }
        // GET: /Donor/Delete/5

        public ActionResult Delete(int id)
        {
            var donor = _donorService.FindById(id);
            try
            {
                    _donorService.DeleteDonor(donor);
                    return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
