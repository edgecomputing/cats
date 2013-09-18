using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models.Hub;
using Cats.Services.Hub;

namespace Cats.Web.Hub.Controllers
{
     [Authorize]
    public class UserWarehouseController : BaseController
     {
         private readonly IUserHubService _userHubService;

         public UserWarehouseController(IUserHubService userHubService)
         {
             this._userHubService = userHubService;
         }

        //
        // GET: /UserWarehouse/

        public ViewResult Index()
        {
            var userwarehouses = _userHubService.GetAllUserHub();
            return View(userwarehouses.ToList());
        }

        //public ActionResult Update()
        //{
        //    var userwarehouses = db.UserHubs.Include("UserProfile").Include("Warehouse");
        //    return PartialView(userwarehouses.ToList());
        //}
        ////
        //// GET: /UserWarehouse/Details/5

        //public ViewResult Details(int id)
        //{
        //    UserHub userwarehouse = db.UserHubs.Single(u => u.UserHubID == id);
        //    return View(userwarehouse);
        //}

        ////
        //// GET: /UserWarehouse/Create

        //public ActionResult Create()
        //{
        //    ViewBag.UserProfileID = new SelectList(db.UserProfiles, "UserProfileID", "UserName");
        //    ViewBag.WarehouseID = new SelectList(db.Hubs, "WarehouseID", "Name");
        //    return View();
        //} 

        ////
        //// POST: /UserWarehouse/Create

        //[HttpPost]
        //public ActionResult Create(UserHub userwarehouse)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.UserHubs.AddObject(userwarehouse);
        //        db.SaveChanges();
        //        //if (userwarehouse.IsDefault == "1")
        //        //{
        //        //    var uProfile = userwarehouse.UserProfile;
        //        //    uProfile.ChangeWarehouse(userwarehouse.UserWarehouseID);
        //        //}
        //        //return Json(new { success = true }); 
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.UserProfileID = new SelectList(db.UserProfiles, "UserProfileID", "UserName", userwarehouse.UserProfileID);
        //    ViewBag.WarehouseID = new SelectList(db.Hubs, "HubID", "Name", userwarehouse.HubID);
        //    return View(userwarehouse);
        //}
        
        ////
        //// GET: /UserWarehouse/Edit/5
 
        //public ActionResult Edit(int id)
        //{
        //    UserHub userwarehouse = db.UserHubs.Single(u => u.UserHubID == id);
        //    ViewBag.UserProfileID = new SelectList(db.UserProfiles, "UserProfileID", "UserName", userwarehouse.UserProfileID);
        //    ViewBag.WarehouseID = new SelectList(db.Hubs, "HubID", "Name", userwarehouse.HubID);
        //    return PartialView(userwarehouse);
        //}

        ////
        //// POST: /UserWarehouse/Edit/5

        //[HttpPost]
        //public ActionResult Edit(UserHub userwarehouse)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.UserHubs.Attach(userwarehouse);
        //        db.ObjectStateManager.ChangeObjectState(userwarehouse, EntityState.Modified);
        //        //if (userwarehouse.IsDefault == "1")
        //        //{
        //        //    var uProfile = userwarehouse.UserProfile;
        //        //    uProfile.ChangeWarehouse(userwarehouse.UserWarehouseID);
        //        //}
        //        db.SaveChanges();
        //        //return RedirectToAction("Index");
        //        return Json(new { success = true }); 
        //    }
        //    ViewBag.UserProfileID = new SelectList(db.UserProfiles, "UserProfileID", "UserName", userwarehouse.UserProfileID);
        //    ViewBag.WarehouseID = new SelectList(db.Hubs, "HubID", "Name", userwarehouse.HubID);
        //    return View(userwarehouse);
        //}

        ////
        //// GET: /UserWarehouse/Delete/5
 
        //public ActionResult Delete(int id)
        //{
        //    UserHub userwarehouse = db.UserHubs.Single(u => u.UserHubID == id);
        //    return View(userwarehouse);
        //}

        ////
        //// POST: /UserWarehouse/Delete/5

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        //{            
        //    UserHub userwarehouse = db.UserHubs.Single(u => u.UserHubID == id);
        //    db.UserHubs.DeleteObject(userwarehouse);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}