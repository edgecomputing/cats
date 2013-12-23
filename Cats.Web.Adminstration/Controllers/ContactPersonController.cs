using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;
using Cats.Services.Administration;
using Cats.Web.Adminstration.Models.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Web.Adminstration.Controllers
{
    public class ContactPersonController : Controller
    {
        private readonly IContactService _contactService;

        public ContactPersonController(IContactService contactService)
        {
            _contactService = contactService;
        }
        //
        // GET: /ContactPerson/

        public ActionResult Index()
        {
           return View();
        }

        public ActionResult Contact_Read([DataSourceRequest] DataSourceRequest request)
        {
            var contacts = _contactService.GetAllContact();
            var r = (from contact in contacts
             select new ContactViewModel()
                 {
                     //ContactID = contact.ContactID,
                     FDPName = contact.FDP.Name,
                     FirstName = contact.FirstName,
                     LastName = contact.LastName,
                     PhoneNo = contact.PhoneNo
                 });
            
            return Json(r.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Contact_Delete(int id)
        {
            var donor = _contactService.FindById(id);
            try
            {
                _contactService.DeleteContact(donor);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Errors", "Unable to delete contact");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Contact_Update()
        {
            return RedirectToAction("Index");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Contact_Create([DataSourceRequest] DataSourceRequest request, ContactViewModel contact)
        {
            if (contact != null && ModelState.IsValid)
            {
                var c = new Contact()
                    {
                        //ContactID = contact.ContactID,
                        FDPID = contact.FDPID,
                        FirstName = contact.FirstName,
                        LastName = contact.LastName,
                        PhoneNo = contact.PhoneNo

                    };
                
                _contactService.AddContact(c);
            }


            return Json(new[] { contact }.ToDataSourceResult(request, ModelState));
        }

        //public  ActionResult Contact_Create()
        //{
        //    return RedirectToAction("Index");
        //}
    }
}