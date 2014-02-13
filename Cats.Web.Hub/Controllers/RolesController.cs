using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models.Hubs;
using System.Data;
using Cats.Services.Hub;

namespace Cats.Web.Hub.Controllers
{
    public class RolesController : BaseController
    {
        
       // private CTSContext db = new CTSContext();
        private readonly IRoleService _roleService;
        private readonly IUserRoleService _userRoleService;

        public RolesController(IRoleService roleSerivce, IUserRoleService userRoleService, IUserProfileService userProfileService)
            : base(userProfileService)
        {
            _roleService = roleSerivce;
            _userRoleService = userRoleService;
        }
        //
        // GET: /Admin/


        //[AutoMap(typeof(Role),typeof(Cats.Web.Hub.Models.RoleModel))]
        public ViewResult Index()
        {
            var roles =  _roleService.GetAllRole().OrderBy(r=>r.SortOrder).ToList();
            return View("Index",roles);
            //var roles = db.Roles;
            //return View("Index", roles.OrderBy(r => r.SortOrder).ToList());
        }

        public ActionResult Update()
        {
            return PartialView(_roleService.GetAllRole().OrderBy(r=>r.SortOrder));
        }

        //
        // GET: /Admin/Details/5
        //[AutoMap(typeof(Role), typeof(Cats.Web.Hub.Models.RoleModel))]
        public ViewResult Details(int id)
        {

            Role role = _roleService.FindBy(r => r.RoleID == id).FirstOrDefault();

            return View("Details", BindRoleModel(role));


            //Role role = db.Roles.Single(u => u.RoleID == id);
            //return View("Details", role);
        }
        private RoleModel BindRoleModel(Role role)
        {
            RoleModel roleModel = new RoleModel
                                      {
                                          Description = role.Description,
                                          Name = role.Name,
                                          RoleID = role.RoleID,
                                          SortOrder = role.SortOrder
                                      };
            return roleModel;
        }
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(_roleService.GetAllRole().ToList(), "RoleID", "RoleID");
            return PartialView("Create");
        }
        public bool testing(Role role)
        {
            return TryValidateModel(role);
        }

        [HttpPost]
        public ActionResult Create(Role role)
        {
            if (ModelState.IsValid)//testing(role))//
            {
                _roleService.AddRole(role);
                return Json(new { success = true }); 
            }

            ViewBag.UserProfileID = new SelectList(_roleService.GetAllRole(), "RoleID", "RoleID", role.RoleID);
            return PartialView(BindRoleModel(role));
        }

        //
        // GET: /Admin/Edit/5

        public ActionResult Edit(int id)
        {
            Role role = _roleService.FindBy(r => r.RoleID == id).FirstOrDefault();
            
            ViewBag.UserProfileID = new SelectList(_userRoleService.GetAllUserRole(), "UserRoleID", "UserRoleID", role.RoleID);
            return PartialView("Edit", role);
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        public ActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                _roleService.EditRole(role);
                
                return Json(new { success = true }); 
            }
            ViewBag.UserProfileID = new SelectList(_roleService.GetAllRole(), "RoleID", "RoleID", role.RoleID);
            return PartialView("Edit", role);
        }

        //
        // GET: /Admin/Delete/5

        public ActionResult Delete(int id)
        {
            Role role = _roleService.FindById(id);
            
            return View(role);
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Role role = _roleService.FindById(id);

            _roleService.DeleteRole(role);
           
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _roleService.Dispose();
            _userRoleService.Dispose();
            base.Dispose(disposing);
        }
         
    }
}
