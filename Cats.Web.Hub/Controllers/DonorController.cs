using System.Linq;
using System.Web.Mvc;
using Cats.Models.Hub;
using Cats.Services.Hub;
using Cats.Web.Hub;

namespace Cats.Web.Hub.Controllers
{
    public class DonorController : BaseController
    {
        private readonly IDonorService _donorService;

        public DonorController(IDonorService donorService,IUserProfileService userProfileService):base(userProfileService)
        {
            _donorService = donorService;
        }
        
        public  ViewResult Index()
        {
            return View(_donorService.GetAllDonor().OrderBy(o=>o.Name).ToList());
        }


        public  ActionResult ListPartial()
        {
            return PartialView(_donorService.GetAllDonor().OrderBy(o => o.Name).ToList());
        }

        //
        // GET: /Donor/Details/5

        public  ViewResult Details(int id)
        {
            var donor = _donorService.FindById(id);
            return View(donor);
        }

        //
        // GET: /Donor/Create

        public  ActionResult Create()
        {
            return PartialView();
        } 

        //
        // POST: /Donor/Create

        [HttpPost]
        public  ActionResult Create(Donor donor)
        {
            if (ModelState.IsValid)
            {
                _donorService.AddDonor(donor);
                return Json(new { success = true });  
            }

            return PartialView(donor);
        }
        
        //
        // GET: /Donor/Edit/5

        public  ActionResult Edit(int id)
        {
            var donor = _donorService.FindById(id);
            return PartialView(donor);
        }

        //
        // POST: /Donor/Edit/5

        [HttpPost]
        public  ActionResult Edit(Donor donor)
        {
            if (ModelState.IsValid && _donorService.IsCodeValid(donor.DonorCode,donor.DonorID))
            {
                _donorService.EditDonor(donor);
                return Json(new { success = true });  
            }
            return PartialView(donor);
        }

        //
        // GET: /Donor/Delete/5

        public  ActionResult Delete(int id)
        {
            var donor = _donorService.FindById(id);
            return View(donor);
        }

        //
        // POST: /Donor/Delete/5

        [HttpPost, ActionName("Delete")]
        public  ActionResult DeleteConfirmed(int id)
        {
            _donorService.DeleteById(id);
            return RedirectToAction("Index");
        }

        public JsonResult IsCodeValid(string donorCode, int? donorID)
        {
            return Json(_donorService.IsCodeValid(donorCode, donorID), JsonRequestBehavior.AllowGet);
        }
    }
}