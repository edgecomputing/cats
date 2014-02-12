using System;
using System.Linq;
using System.Web.Mvc;
using Cats.Models.Hubs;
using Cats.Services.Hub;
using Cats.Web.Hub;

namespace Cats.Areas.Hub.Controllers
{
    public class ContactController : BaseController
    {
        private readonly IContactService _contactService;
        private readonly IFDPService _fdpService;

        public ContactController(IContactService contactService, IFDPService fdpService, IUserProfileService userProfileService)
            : base(userProfileService)
        {
            _contactService = contactService;
            _fdpService = fdpService;
        }
        //
        // GET: /Contact/

        public ViewResult Index(int? fdpId)
        {
            if (fdpId.HasValue)
            {
                var contacts = _contactService.GetByFdp(fdpId.Value);
                ViewBag.FDPID = fdpId.Value;
                ViewBag.FDPName = _fdpService.FindById(fdpId.Value).Name;
                return View("Index", contacts.ToList());
            }
            else
            {
                var contacts = _contactService.GetAllContact();
                return View("Index", contacts);
            }
        }

        //
        // GET: /Contact/Details/5

        public ViewResult Details(int id)
        {
            var contact = _contactService.FindById(id);
            ViewBag.FDPName = contact.FDP.Name;
            ViewBag.FDPID = contact.FDPID;
            return View("Details", contact);
        }

        //
        // GET: /Contact/Create

        public ActionResult Create(int fdpId)
        {
            ViewBag.FDPName = _contactService.FindById(fdpId).FDP.Name;
            ViewBag.FDPID = fdpId;
            var contact = new Contact { FDPID = fdpId };
            return View("Create", contact);
        }

        //
        // POST: /Contact/Create

        [HttpPost]
        public ActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _contactService.AddContact(contact);
                return RedirectToAction("Index", new { fdpId = contact.FDPID });  
            }
            ViewBag.FDPName = _contactService.FindById(contact.ContactID).FDP.Name;
            ViewBag.FDPID = contact.FDPID;
            return View("Create", contact);
        }
        
        //
        // GET: /Contact/Edit/5
 
        public ActionResult Edit(int id)
        {
            Contact contact = _contactService.FindById(id);
            ViewBag.FDPName = contact.FDP.Name;
            ViewBag.FDPID = contact.FDPID;
            return View("Edit", contact);
        }

        //
        // POST: /Contact/Edit/5

        [HttpPost]
        public ActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _contactService.EditContact(contact);
                return RedirectToAction("Index", new { fdpId = contact.FDPID });
            }

            ViewBag.FDPName = _contactService.FindById(contact.FDPID).FDP.Name;
            ViewBag.FDPID = contact.FDPID;
            return View("Edit", contact);
        }

        //
        // GET: /Contact/Delete/5
 
        public ActionResult Delete(int id)
        {
            Contact contact = _contactService.FindById(id);
            ViewBag.FDPID = contact.FDPID;
            return View("Delete", contact);
        }

        //
        // POST: /Contact/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = _contactService.FindById(id);
            try
            {
                _contactService.DeleteContact(contact);
                return RedirectToAction("Index", new { fdpId = contact.FDPID });
            }
            catch (Exception)
            {
                return View("Delete", contact);
            }
        }
    }
}
