using System.Collections;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Cats.Models.Hub;
using Cats.Services.Hub;
using System.Collections.Generic;

namespace Cats.Web.Hub.Controllers
{
     [Authorize]
    public partial class StoreController : BaseController
    {
        
         private readonly IStoreService _storService;
         private readonly IUserProfileService _userProfileService;
         public StoreController(IStoreService storeServiceParam, IUserProfileService userProfileService):base(userProfileService)
         {
             _storService = storeServiceParam;
             _userProfileService = userProfileService;
         }
        public virtual ActionResult Index()
        {
            UserProfile user = _userProfileService.GetUser(User.Identity.Name);
            var stores = _storService.GetAllByHUbs(user.UserAllowedHubs);
            return View(stores);
        }

        public virtual ActionResult Update()
        {
            UserProfile user = _userProfileService.GetUser(User.Identity.Name);
            var stores = _storService.GetAllByHUbs(user.UserAllowedHubs);
            return PartialView(stores.OrderBy(o => o.Hub.HubOwner.Name).ThenBy(o => o.Hub.Name).ThenBy(o=>o.Name).ToList());
        }
        //
        // GET: /Store/Details/5

        public virtual ViewResult Details(int id)
        {
            Store store = _storService.FindById(id);
            return View(store);
        }

        //
        // GET: /Store/Create

        public virtual ActionResult Create()
        {
            UserProfile user = _userProfileService.GetUser(User.Identity.Name);
            ViewBag.HubID = new SelectList(user.UserAllowedHubs.OrderBy(p => p.Name), "HubID", "HubNameWithOwner");
            return PartialView();
        } 

        //
        // POST: /Store/Create

        [HttpPost]
        public virtual ActionResult Create(Store store)
        {
            if (ModelState.IsValid)
            {
                _storService.AddStore(store);
                return Json(new { success = true }); 
            }

            UserProfile user = _userProfileService.GetUser(User.Identity.Name);
            ViewBag.HubID = new SelectList(user.UserAllowedHubs.OrderBy(p => p.Name), "HubID", "HubNameWithOwner");
            return PartialView(store);
        }
        
        //
        // GET: /Store/Edit/5

        public virtual ActionResult Edit(int id)
        {
            UserProfile user = _userProfileService.GetUser(User.Identity.Name);
            var stores = _storService.GetAllByHUbs(user.UserAllowedHubs);
            Store store = stores.Find(p => p.StoreID == id);//only look inside the allowed stores
            if (store != null){
                ViewBag.HubID = new SelectList(user.UserAllowedHubs.OrderBy(p => p.Name), "HubID", "HubNameWithOwner", store.HubID);
            }
            else
            {
                store = new Store();
                ViewBag.HubID = new SelectList(Enumerable.Empty<SelectListItem>());
            }
            return PartialView(store);
        }

        //
        // POST: /Store/Edit/5

        [HttpPost]
        public virtual ActionResult Edit(Store store)
        {
            if (ModelState.IsValid)
            {
                _storService.EditStore(store);
                //return RedirectToAction("Index");
                return Json(new { success = true }); 
            }
            UserProfile user = _userProfileService.GetUser(User.Identity.Name);
            ViewBag.HubID = new SelectList(user.UserAllowedHubs.OrderBy(p => p.Name), "HubID", "HubNameWithOwner");
            return PartialView(store);
        }

        //
        // GET: /Store/Delete/5

        public virtual ActionResult Delete(int id)
        {
            Store store = _storService.FindById(id);
            return View(store);
        }

        //
        // POST: /Store/Delete/5

        [HttpPost, ActionName("Delete")]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            _storService.DeleteById(id);
            return RedirectToAction("Index");
        }

        public ActionResult StackNumbers(int? StoreID,int ?editModval )
        {
            if (StoreID != null)
            {
                Store store = _storService.FindById(StoreID.Value);
                var stacks = new List<SelectListItemModel>();
                stacks.Add(new SelectListItemModel { Name = "N/A", Id = "0" }); //TODO just a hack for now for unknown stacks
                foreach(var i in store.Stacks){
                    stacks.Add(new SelectListItemModel { Name = i.ToString(), Id = i.ToString() });
                }
               return Json(new SelectList(stacks, "Id", "Name",editModval), JsonRequestBehavior.AllowGet);          
            }
            else {
                return Json(new SelectList(Enumerable.Empty<SelectListItem>()), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult StoreManName(int? storeId)
        {
            if (storeId.HasValue)
            {
                Store store = _storService.FindById(storeId.Value);
                if (store != null && !string.IsNullOrEmpty(store.StoreManName))
                {
                    return Json(store.StoreManName, JsonRequestBehavior.AllowGet);
                }
                return Json("Please Specify", JsonRequestBehavior.AllowGet);
            }
            return Json("",JsonRequestBehavior.AllowGet);

        }

        public virtual ActionResult Stores(int warehouseId)
        {
            var stores = from s in _storService.GetStoreByHub(warehouseId)
                         select new { Name = s.Name, Id = s.StoreID };
            return Json(stores, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            _userProfileService.Dispose();
           
        }
    }
}