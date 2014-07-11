using System.Linq;
using System.Web.Mvc;
using Cats.Models.Hubs;
using Cats.Services.Hub;
using Cats.Web.Hub;

namespace Cats.Web.Hub.Controllers
{
    public class HubController : BaseController
    {
        private readonly IHubOwnerService _hubOwnerService;
        private readonly IHubService _hubService;

        public HubController(IHubOwnerService hubOwnerService, IHubService hubService, IUserProfileService userProfileService)
            : base(userProfileService)
        {
            _hubOwnerService = hubOwnerService;
            _hubService = hubService;
        }

        public virtual ActionResult Index()
        {
            return View(_hubService.GetAllHub().OrderBy(o => o.HubOwner.Name).ThenBy(o => o.Name));
        }

        public virtual ActionResult ListPartial()
        {
            return PartialView(_hubService.GetAllHub().OrderBy(o => o.HubOwner.Name).ThenBy(o => o.Name));
        }

        //
        // GET: /Warehouse/Details/5

        public virtual ViewResult Details(int id)
        {
            return View(_hubService.FindById(id));
        }

        //
        // GET: /Warehouse/Create

        public virtual ActionResult Create()
        {
            ViewBag.HubOwnerID = new SelectList(_hubOwnerService.GetAllHubOwner().OrderBy(o => o.Name), "HubOwnerID", "Name");
            return PartialView();
        } 

        //
        // POST: /Warehouse/Create

        [HttpPost]
        public virtual ActionResult Create(Cats.Models.Hubs.Hub warehouse)
        {
            if (ModelState.IsValid)
            {
                _hubService.AddHub(warehouse);
                return Json(new { success = true }); 
            }

            ViewBag.HubOwnerID = new SelectList(_hubOwnerService.GetAllHubOwner().OrderBy(o => o.Name), "HubOwnerID", "Name", warehouse.HubOwnerID);
            return PartialView(warehouse);
        }
        
        //
        // GET: /Warehouse/Edit/5

        public virtual ActionResult Edit(int id)
        {
            var hub = _hubService.FindById(id);
            ViewBag.HubOwnerID = new SelectList(_hubOwnerService.GetAllHubOwner().OrderBy(o => o.Name), "HubOwnerID", "Name", hub.HubOwnerID);
            return PartialView(hub);
        }

        //
        // POST: /Warehouse/Edit/5

        [HttpPost]
        public virtual ActionResult Edit(Cats.Models.Hubs.Hub warehouse)
        {
            if (ModelState.IsValid)
            {
                _hubService.EditHub(warehouse);
                //return RedirectToAction("Index");
                return Json(new { success = true }); 
            }
            ViewBag.HubOwnerID = new SelectList(_hubOwnerService.GetAllHubOwner().OrderBy(o => o.Name), "HubOwnerID", "Name", warehouse.HubOwnerID);
            return PartialView(warehouse);
        }

        //
        // GET: /Warehouse/Delete/5

        public virtual ActionResult Delete(int id)
        {
            return View(_hubService.FindById(id));
        }

        //
        // POST: /Warehouse/Delete/5

        [HttpPost, ActionName("Delete")]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            _hubService.DeleteById(id);
            return RedirectToAction("Index");
        }
    }
}