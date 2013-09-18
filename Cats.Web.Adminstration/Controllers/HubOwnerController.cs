
using System.Web.Mvc;
using Cats.Models.Hub;
using Cats.Services.Administration;

namespace  Cats.Web.Adminstration.Controllers
{
    public partial class HubOwnerController : Controller
    {

       
        private readonly IHubOwnerService _hubOwnerService;

        public HubOwnerController(IHubOwnerService hubOwnerServiceParam)
        {
            this._hubOwnerService = hubOwnerServiceParam;
        }


        public virtual ActionResult Index()
        {
            return View(_hubOwnerService.GetAllHubOwner());
        }

        public virtual ActionResult Update()
        {
            return PartialView(_hubOwnerService.GetAllHubOwner());
        }
        
        //
        // GET: /HubOwners/Details/5

        public virtual ViewResult Details(int id)
        {
            HubOwner HubOwner = _hubOwnerService.FindById(id);
            return View(HubOwner);
        }

        //
        // GET: /HubOwners/Create

        public virtual ActionResult Create()
        {
            return PartialView();
        } 

        //
        // POST: /HubOwners/Create

        [HttpPost]
        public virtual ActionResult Create(HubOwner HubOwner)
        {
            if (ModelState.IsValid)
            {
                _hubOwnerService.AddHubOwner(HubOwner);
                return Json(new { success = true }); 
            }

            return PartialView(HubOwner);
        }
        
        //
        // GET: /HubOwners/Edit/5

        public virtual ActionResult Edit(int id)
        {
            HubOwner HubOwner = _hubOwnerService.FindById(id);
            return PartialView(HubOwner);
        }

        //
        // POST: /HubOwners/Edit/5

        [HttpPost]
        public virtual ActionResult Edit(HubOwner HubOwner)
        {
            if (ModelState.IsValid)
            {
                _hubOwnerService.EditHubOwner(HubOwner);
                return Json(new { success = true }); 
            }
            return PartialView(HubOwner);
        }

        //
        // GET: /HubOwners/Delete/5

        public virtual ActionResult Delete(int id)
        {
            HubOwner HubOwner = _hubOwnerService.FindById(id);
            return View(HubOwner);
        }

        //
        // POST: /HubOwners/Delete/5

        [HttpPost, ActionName("Delete")]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            _hubOwnerService.DeleteById(id);
            return RedirectToAction("Index");
        }
    }
}