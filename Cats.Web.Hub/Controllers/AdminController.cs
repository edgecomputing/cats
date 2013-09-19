using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cats.Models.Hub;

using System.Web.Security;
using Cats.Services.Hub;
using Cats.Web.Hub.Infrastructure;
using Cats.Web.Hub;

namespace Cats.Web.Hub.Controllers
{
    public class AdminController : BaseController
    {
        //CTSContext db = new CTSContext();
        //IUnitOfWork repository = new UnitOfWork();
        private readonly IUserProfileService _userProfileService;
        private readonly IUserRoleService _userRoleService;
        private readonly IRoleService _roleService;
        private readonly IUserHubService _userHubService;
        private readonly IHubService _hubService;

        public AdminController(IUserProfileService userProfileService, IUserRoleService userRoleService, IRoleService roleService, IUserHubService userHubService, IHubService hubService)
        {
            _userProfileService = userProfileService;
            _userRoleService = userRoleService;
            _roleService = roleService;
            _userHubService = userHubService;
            _hubService = hubService;
        }

        public virtual ViewResult Index()
        {
            var userProfiles = _userProfileService.Get(null,t=>t.OrderBy(o=>o.UserName)).ToList();
            return View("Users/Index", userProfiles);
        }

        public virtual ActionResult Update()
        {
            var userProfiles = _userProfileService.Get(null, t => t.OrderBy(o => o.UserName)).ToList();
            return PartialView("Users/Update", userProfiles);
        }

        public virtual ViewResult Home()
        {   
            return View("Users/Home");
        }

        public virtual ViewResult Details(int id)
        {
            var userprofile = _userProfileService.FindById(id);
            return View("Users/Details", userprofile);
        }

        public virtual ActionResult Create()
        {
            ViewBag.UserRoles = new SelectList(_userRoleService.GetAllUserRole(), "UserRoleID", "UserRoleID");
            return PartialView("Users/Create");
        }

        //
        // POST: /Admin/Create

        [HttpPost]
        public virtual ActionResult Create(UserProfile userprofile)
        {
            if (ModelState.IsValid)
            {
                userprofile.Password = MD5Hashing.MD5Hash(userprofile.Password);
                _userProfileService.AddUserProfile(userprofile);
                return Json(new { success = true });  
            }

            ViewBag.UserProfileID = new SelectList(_userRoleService.GetAllUserRole(), "UserRoleID", "UserRoleID", userprofile.UserProfileID);
            return PartialView("Users/Create", userprofile);
        }
        
        //
        // GET: /Admin/Edit/5

        public virtual ActionResult Edit(int id)
        {
            var userprofile = _userProfileService.FindById(id);
            //ViewData["roles"] = new SelectList(db.Roles, "RoleID", "Name", userprofile.UserRole.RoleID);
            //ViewBag.UserProfileID = new SelectList(db.Roles, "RoleID", "Name", userprofile.UserRole.RoleID);
            Session["SELECTEDUSER"] = userprofile;
            return PartialView("Users/Edit", userprofile);
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        public virtual ActionResult Edit(UserProfile userprofile)
        {
            var cachedProfile = Session["SELECTEDUSER"] as UserProfile;
            ModelState.Remove("Password");
            if (ModelState.IsValid && cachedProfile != null)
            {
                userprofile.Password = cachedProfile.Password;
                _userProfileService.EditUserProfile(userprofile);
                Session.Remove("SELECTEDUSER");
                return Json(new { success = true });
            }
            ViewBag.UserProfileID = new SelectList(_userRoleService.GetAllUserRole(), "UserRoleID", "UserRoleID", userprofile.UserProfileID);
            return PartialView("Users/Edit", userprofile);
        }

        //
        // GET: /Admin/Delete/5

        public virtual ActionResult Delete(int id)
        {
            UserProfile userprofile = _userProfileService.FindById(id);
            return View("Users/Delete", userprofile);
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost, ActionName("Delete")]
        public virtual ActionResult DeleteConfirmed(int id)
        {   
            _userProfileService.DeleteById(id);
            return RedirectToAction("Index");
        }

        public virtual ActionResult UserRoles(string userName)
        {
            var userroles = new UserRolesModel
                {UserRoles = GetUserRoles(userName).OrderBy(o => o.SortOrder).ToArray()};

            Session["Roles"] = userroles;
            Session["UserName"] = userName;
            return PartialView("Users/UserRoles", userroles);
        }

        public virtual ActionResult UserHubs(string userName)
        {
            var userhubs = new Cats.Models.Hub.UserHubsModel {UserHubs = GetUserHubs(userName).OrderBy(o => o.Name).ToList()};
            Session["Hubs"] = userhubs;
            Session["UserName"] = userName;
            return PartialView( "Users/UserHubs", userhubs);
        }

        [HttpPost]
        public virtual ActionResult UserHubs(FormCollection userHubs)
        {
            var hubModel = Session["Hubs"] as Cats.Models.Hub.UserHubsModel;
            var userName = Session["UserName"].ToString();
            if (hubModel!=null)
            {
                for (var i = 0; i < hubModel.UserHubs.Count(); i++)
                {
                    var model = new Cats.Models.Hub.UserHubModel
                        {HubID = hubModel.UserHubs[i].HubID, Name = hubModel.UserHubs[i].Name};
                    model.Selected = userHubs.GetValue(string.Format("[{0}].Selected", model.HubID)).AttemptedValue.Contains("true");
                    if (model.Selected == hubModel.UserHubs[i].Selected) continue;
                    var userID = (from v in _userProfileService.GetAllUserProfile()
                                  where v.UserName == userName
                                  select v.UserProfileID).FirstOrDefault();

                    var hub = new Cats.Models.Hub.Hub();
                    if (model.Selected)
                    {
                        _userHubService.AddUserHub(model.HubID,userID);
                    }else
                    {
                        _userHubService.RemoveUserHub(model.HubID, userID);
                    }
                }
            }
            
            return Json(new { success = true });
        }

        [HttpPost]
        public virtual ActionResult UserRoles(FormCollection userRoles)
        {
            
            var roleModel = Session["Roles"] as UserRolesModel;
            var userName = Session["UserName"].ToString();
            if(roleModel!=null)
            {
                for (var i = 0; i < roleModel.UserRoles.Count(); i++)
                {
                    var model = new UserRoleModel { RoleId = roleModel.UserRoles[i].RoleId, RoleName = roleModel.UserRoles[i].RoleName };
                    model.Selected = userRoles.GetValue(string.Format("[{0}].Selected", model.RoleId)).AttemptedValue.Contains("true");
                    if (model.Selected != roleModel.UserRoles[i].Selected)
                    {
                        if (model.Selected)
                            new Role().AddUserToRole(model.RoleId, userName);
                        else
                            new Role().RemoveRole(model.RoleName, userName);
                    }

                }
            }
            
            return Json(new { success = true }); 
        }

        protected override void Dispose(bool disposing)
        {
            _userProfileService.Dispose();
            _userRoleService.Dispose();
            _userHubService.Dispose();
            _hubService.Dispose();
            _roleService.Dispose();
            base.Dispose(disposing);
        }

        private IEnumerable<UserRoleModel> GetUserRoles(string userName)
        {
            var roles = Roles.GetRolesForUser(userName);
            var userRoles = from role in _roleService.GetAllRole()
                            select new UserRoleModel { RoleId = role.RoleID, RoleName = role.Name, Selected = roles.Contains(role.Name),SortOrder = role.SortOrder};
            return userRoles.ToList();
        }

        private IEnumerable<Cats.Models.Hub.UserHubModel> GetUserHubs(string userName)
        {
            
            var warehouses = from v in _userHubService.GetAllUserHub()
                             where v.UserProfile.UserName == userName
                             select v.HubID;
           
            var userHubs = from v in _hubService.GetAllHub()
                            select new Cats.Models.Hub.UserHubModel { HubID = v.HubID, Name = v.Name + " : " + v.HubOwner.Name, Selected = warehouses.Contains(v.HubID) } ;
            return userHubs.ToList();
        }
    }
}
