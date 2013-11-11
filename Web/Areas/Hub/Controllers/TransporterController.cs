using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models.Hubs;
using Cats.Services.Hub;
using Cats.Web.Hub;

namespace Cats.Areas.Hub.Controllers
{
     [Authorize]
    public partial class TransporterController : BaseController
    {

         private readonly ITransporterService _transporterService;
         public TransporterController(ITransporterService transporterService, IUserProfileService userProfileService)
             : base(userProfileService)
         {
             _transporterService = transporterService;
         }
        //
        // GET: /Transporter/

        public virtual ViewResult Index()
        {
            return View(_transporterService.GetAllTransporter().OrderBy(n=>n.Name).ToList());

            //return View(repository.Transporter.GetAll().OrderBy(t => t.Name).ToList());
        }

        public virtual ActionResult Update()
        {
            return PartialView(_transporterService.GetAllTransporter().OrderBy(n=>n.Name).ToList());
            //return PartialView(repository.Transporter.GetAll().OrderBy(t => t.Name).ToList());
        }

        //
        // GET: /Transporter/Details/5

        public virtual ViewResult Details(int id)
        {
            Transporter transporter = _transporterService.FindById(id);
           // Transporter transporter = repository.Transporter.FindById(id);
            return View(transporter);
        }

        //
        // GET: /Transporter/Create

        public virtual ActionResult Create()
        {
            return PartialView();
        } 

        //
        // POST: /Transporter/Create

        [HttpPost]
        public virtual ActionResult Create(Transporter transporter)
        {
            if (_transporterService.IsNameValid(transporter.TransporterID,transporter.Name))
            {
                 ModelState.AddModelError("Name", "Transporter Name should be Unique");
            }
            if (ModelState.IsValid)
            {
                _transporterService.AddTransporter(transporter);
                return Json(new {sucess=true});
            }
            return PartialView(transporter);

            //if (!repository.Transporter.IsNameValid(transporter.TransporterID, transporter.Name))
            //{
            //    ModelState.AddModelError("Name", "Transporter Name should be Unique");
            //}
            //if (ModelState.IsValid)
            //{
            //    repository.Transporter.Add(transporter);
            //    return Json(new { success = true });   
            //}

            //return PartialView(transporter);
        }
        
        //
        // GET: /Transporter/Edit/5

        public virtual ActionResult Edit(int id)
        {
            Transporter transporter = _transporterService.FindById(id);
            return PartialView(transporter);
            //Transporter transporter = repository.Transporter.FindById(id);
            //return PartialView(transporter);
        }

        //
        // POST: /Transporter/Edit/5

        [HttpPost]
        public virtual ActionResult Edit(Transporter transporter)
        {

            if (_transporterService.IsNameValid(transporter.TransporterID,transporter.Name))
            {
                 ModelState.AddModelError("Name", "Transporter Name should be Unique");
            }
            if (ModelState.IsValid)
            {
                _transporterService.EditTransporter(transporter);
                return Json(new {sucess=true});
            }
            return PartialView(transporter);



            //if(!repository.Transporter.IsNameValid(transporter.TransporterID,transporter.Name))
            //{
            //    ModelState.AddModelError("Name","Transporter Name should be Unique");
            //}
            //if (ModelState.IsValid)
            //{
            //    repository.Transporter.SaveChanges(transporter);
            //    return Json(new { success = true }); 
            //}
            //return View(transporter);
        }

        //
        // GET: /Transporter/Delete/5

        public virtual ActionResult Delete(int id)
        {
            Transporter transporter = _transporterService.FindById(id);
            return PartialView(transporter);

            //Transporter transporter = repository.Transporter.FindById(id);
            //return View(transporter);
        }

        //
        // POST: /Transporter/Delete/5

        [HttpPost, ActionName("Delete")]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            _transporterService.DeleteById(id);
            return  RedirectToAction("Index");

            //repository.Transporter.DeleteByID(id);
            //return RedirectToAction("Index");
        }

         public JsonResult IsNameValid(int? TransporterID, string Name)
         {
             return Json(_transporterService.IsNameValid(TransporterID,Name),JsonRequestBehavior.AllowGet);
             //return Json(repository.Transporter.IsNameValid(TransporterID, Name), JsonRequestBehavior.AllowGet);
         }
    }
}