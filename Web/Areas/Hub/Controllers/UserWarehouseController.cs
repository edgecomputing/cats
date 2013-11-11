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
    public class UserWarehouseController : BaseController
     {
         private readonly IUserHubService _userHubService;
         private readonly IHubService _hubService;
         private readonly IUserProfileService _userProfileService;

         public UserWarehouseController(IUserHubService userHubService, 
             IHubService hubService, 
             IUserProfileService userProfileService)
             : base(userProfileService)
         {
             this._userHubService = userHubService;
             this._hubService = hubService;
             this._userProfileService = userProfileService;
         }

        //
        // GET: /UserWarehouse/

        public ViewResult Index()
        {
            var userwarehouses = _userHubService.GetAllUserHub();
            return View(userwarehouses.ToList());
        }

        public ActionResult Update()
        {
            var userwarehouses = _userHubService.Get(null,null,"UserProfile,Warehouse");
            return PartialView(userwarehouses.ToList());
        }
        //
        // GET: /UserWarehouse/Details/5

        public ViewResult Details(int id)
        {

            UserHub userwarehouse = _userHubService.FindBy(u => u.UserHubID == id).Single();
            return View(userwarehouse);
        }

        //
        // GET: /UserWarehouse/Create

        public ActionResult Create()
        {
            ViewBag.UserProfileID = new SelectList(_userProfileService.GetAllUserProfile(), "UserProfileID", "UserName");
            ViewBag.WarehouseID = new SelectList(_hubService.GetAllHub(), "HubID", "Name");
            return View();
        }

        //
        // POST: /UserWarehouse/Create

        [HttpPost]
        public ActionResult Create(UserHub userwarehouse)
        {
            if (ModelState.IsValid)
            {
               _userHubService.AddUserHub(userwarehouse);
               
                //if (userwarehouse.IsDefault == "1")
                //{
                //    var uProfile = userwarehouse.UserProfile;
                //    uProfile.ChangeWarehouse(userwarehouse.UserWarehouseID);
                //}
                //return Json(new { success = true }); 
                return RedirectToAction("Index");
            }

            ViewBag.UserProfileID = new SelectList(_userProfileService.GetAllUserProfile(), "UserProfileID", "UserName", userwarehouse.UserProfileID);
            ViewBag.WarehouseID = new SelectList(_hubService.GetAllHub(), "HubID", "Name", userwarehouse.HubID);
            return View(userwarehouse);
        }

        //
        // GET: /UserWarehouse/Edit/5

        public ActionResult Edit(int id)
        {
            UserHub userwarehouse = _userHubService.FindBy(u => u.UserHubID == id).Single();
            ViewBag.UserProfileID = new SelectList(_userProfileService.GetAllUserProfile(), "UserProfileID", "UserName", userwarehouse.UserProfileID);
            ViewBag.WarehouseID = new SelectList(_hubService.GetAllHub(), "HubID", "Name", userwarehouse.HubID);
            return PartialView(userwarehouse);
        }

        //
        // POST: /UserWarehouse/Edit/5

        [HttpPost]
        public ActionResult Edit(UserHub userwarehouse)
        {
            if (ModelState.IsValid)
            {
                _userHubService.EditUserHub(userwarehouse);
                //db.UserHubs.Attach(userwarehouse);
                //db.ObjectStateManager.ChangeObjectState(userwarehouse, EntityState.Modified);
                ////if (userwarehouse.IsDefault == "1")
                ////{
                ////    var uProfile = userwarehouse.UserProfile;
                ////    uProfile.ChangeWarehouse(userwarehouse.UserWarehouseID);
                ////}
                //db.SaveChanges();
                //return RedirectToAction("Index");
                return Json(new { success = true });
            }
            ViewBag.UserProfileID = new SelectList(_userProfileService.GetAllUserProfile(), "UserProfileID", "UserName", userwarehouse.UserProfileID);
            ViewBag.WarehouseID = new SelectList(_hubService.GetAllHub(), "HubID", "Name", userwarehouse.HubID);
            return View(userwarehouse);
        }

        //
        // GET: /UserWarehouse/Delete/5

        public ActionResult Delete(int id)
        {
            UserHub userwarehouse = _userHubService.FindBy(u => u.UserHubID == id).Single();
            return View(userwarehouse);
        }

        //
        // POST: /UserWarehouse/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            UserHub userwarehouse = _userHubService.FindBy(u => u.UserHubID == id).Single();
            _userHubService.DeleteUserHub(userwarehouse);
            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _userHubService.Dispose();
            base.Dispose(disposing);
        }
    }
}