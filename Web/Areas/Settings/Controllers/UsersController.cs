using System;
using System.Collections.Generic;
using System.Web.Security;
using Cats.Helpers;
using Cats.Models.Security;
using Cats.Services.EarlyWarning;
using Cats.Services.Security;
using System.Linq;
using System.Web.Mvc;
using Cats.Areas.Settings.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Cats.Models.Security.ViewModels;
using Cats.Helpers;

namespace Cats.Areas.Settings.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly  IUserAccountService _userService;
        private readonly  IHubService _hubService;
        private readonly IAdminUnitService _adminUnitService;
       // private readonly IUserAccountService _userAccountService;
      
        public UsersController(IUserAccountService service, IHubService hubService, IAdminUnitService adminUnitService)
        {
            _userService = service;
            _hubService = hubService;
            _adminUnitService = adminUnitService;
            //_userAccountService = userAccountService;
        }

        public ActionResult UsersList([DataSourceRequest] DataSourceRequest request)
        {
            var users = _userService.GetUsers();
            return Json(users.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            if (TempData["error"]!=null)
            {
                ViewData["error"] = TempData["error"].ToString();
            }
            return View();
        }

        public void init()
        {
            var caseteams = new List<CaseTeam>
                                    {
                                        new CaseTeam() {ID = 1, CaseTeamName = "EarlyWarning"},
                                        new CaseTeam() {ID = 2, CaseTeamName = "PSNP/FSCD"},
                                        new CaseTeam() {ID = 3, CaseTeamName = "Logistics"},
                                        new CaseTeam() {ID = 4, CaseTeamName = "Procurement"},
                                        new CaseTeam() {ID = 5, CaseTeamName = "Finance"}
                                    };
            var userTypes = new SelectList(new[]
                       {
                           new SelectListItem {Text = "Regional", Value = "1"},
                           new SelectListItem {Text = "Hub", Value = "2"},
                           new SelectListItem {Text = "Case team", Value = "3"},
                       }, "Text", "Value");

            ViewBag.userTypes = userTypes;
            ViewBag.CaseTeams = caseteams;
            ViewBag.hubs = _hubService.GetAllHub().ToList();
            ViewBag.regions = _adminUnitService.GetRegions();
        }

        public ActionResult New()
        {
            var model = new UserViewModel();
            init();
            //var caseteams = new List<CaseTeam>();
            //caseteams.Add(new CaseTeam() { ID = 1,CaseTeamName = "EarlyWarning"});
            //caseteams.Add(new CaseTeam() { ID = 2, CaseTeamName = "PSNP/FSCD" });
            //caseteams.Add(new CaseTeam() { ID = 3, CaseTeamName = "Logistics" });
            //caseteams.Add(new CaseTeam() { ID = 4, CaseTeamName = "Procurement" });
            //ViewBag.CaseTeams = caseteams;
            //ViewBag.Regions = _adminUnitService.GetRegions();
            return View(model);
        }

        public ActionResult add()
        {
            //userService.AddRoleSample();
            return View();
        }

        [HttpPost]
        public ActionResult New(UserViewModel userInfo)
        {
            

            var user_ = _userService.FindBy(u=>u.UserName == userInfo.UserName).FirstOrDefault();

           
            var user = new UserProfile();

            user.UserName = userInfo.UserName;
            user.Password = _userService.HashPassword(userInfo.Password);

            // Set default values for required fields
            user.Disabled = false;
            user.LockedInInd = false;
            user.ActiveInd = true;
            user.NumberOfLogins = 0;

            
            Dictionary<string, List<string>> roles = new Dictionary<string, List<string>>();
           
            user.FirstName = userInfo.FirstName;
            user.LastName = userInfo.LastName;
            user.RegionalUser = userInfo.RegionalUser;
            user.RegionID = userInfo.RegionID;
            user.CaseTeam = userInfo.CaseTeam;

            user.LanguageCode = "EN";
            user.Keyboard = "AM";
            user.PreferedWeightMeasurment = "MT";
            user.DatePreference = "GC";
            user.DefaultTheme = "Default";
            user.FailedAttempts = 0;
            user.LoggedInInd = false;
            user.Email = userInfo.Email;


            if (user_ != null)
            {
                ViewBag.Error = "User Name exits!.Please choose a different User Name!";
                init();
                return View();
            }

            if(_userService.Add(user, roles))
            {
                return View("Index");
            }
            return View();
        }
       
        public ActionResult EditUser(int userId)
        {
            var user = _userService.FindById(userId);
            if (user != null)
            {
                if (user.RegionalUser)
                    ViewBag.Selected = 1;
                else if (user.DefaultHub > 0)
                    ViewBag.Selected = 2;
                else if (user.CaseTeam > 0)
                    ViewBag.Selected = 3;
                else
                    ViewBag.Selected = 1;
                init();
                return View(user);
            }

            return View("Index");

        }

        [HttpPost]
        public ActionResult EditUser(UserProfile userInfo)
        {

            if (ModelState.IsValid)
            {
                var user = _userService.FindById(userInfo.UserProfileID);
                user.UserName = userInfo.UserName;
                user.FirstName = userInfo.FirstName;
                user.LastName = userInfo.LastName;
                user.GrandFatherName = userInfo.GrandFatherName;
                user.RegionalUser = userInfo.RegionalUser;
                user.RegionID = userInfo.RegionalUser ? userInfo.RegionID : null; 
                user.RegionID = userInfo.RegionID;
                user.DefaultHub = userInfo.DefaultHub;
                user.CaseTeam = userInfo.CaseTeam;
                user.MobileNumber = userInfo.MobileNumber;
                user.Email = userInfo.Email;

                if (_userService.UpdateUser(user))
                {
                    return View("Index");
                }
            }
            init();
            ViewBag.hubs = _hubService.GetAllHub().ToList();
            ViewBag.regions = _adminUnitService.GetRegions();
            return View("EditUser");
        }
        public ActionResult UserProfile(int id)
        {

            var user = _userService.GetUserInfo(id);
            if(user!=null)
            {
                if (TempData["Error"]!=null)
                {
                    ModelState.AddModelError("Errors", TempData["Error"].ToString());
                    ViewBag.Error = TempData["Error"].ToString();
                }
                
                return View(user);
            }
            return View();
        }

        public JsonResult GetUsers()
        {
            var users = _userService.GetAll();
            return Json(users.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteAccount(int id)
        {
            var user = _userService.FindById(id);
            if (user != null)
                return View(user);
            return View();
        }

        public ActionResult ConfirmDeleteAccount(int id)
        {
            var user = _userService.FindById(id);
            if (user!=null)
            {
                try
                {
                    _userService.DeleteById(id);
                    
                }
                catch (Exception)
                {
                    TempData["Error"] = "User can not be Deleted. There are related Transaction associated with the user!";
                    return RedirectToAction("UserProfile", new {id = id});
                }
            }
            return View("Index");
        }

        public ActionResult DeactivateUser(int id)
        {
             var user = _userService.FindById(id);
             if (user != null)
             {
                 
                 _userService.DisableAccount(user.UserName);
                 return View("Index");
             }
             return RedirectToAction("UserProfile", new { id = id });
        }

        public ActionResult ActivateDeactivateUser(int id)
        {
             var user = _userService.FindById(id);
             if (user != null)
             {
                 
                 
                 _userService.EnableAccount(user.UserName);
                 return View("Index");
             }
             return RedirectToAction("UserProfile", new { id = id });
        }

        

        [HttpGet]
        public ActionResult EditUserRoles(string UserName)
        {
            //var roles = new string[] { "EW Coordinator", "EW-Experts" };
            //userService.AddRoleSample("Rahel", "Early Warning", roles);
            var user = UserAccountHelper.GetUser(HttpContext.User.Identity.Name);
            var model = new UserViewModel();
            model.UserName = UserName;
            model.Email = user.Email;
            List<Application> Applications = _userService.GetUserPermissions(UserName);
            ViewBag.hubs = new SelectList(_hubService.GetAllHub(), "HubID", "Name");
            model.Applications = Applications;
            return View(model);
        }

        [HttpPost]
        public ActionResult EditUserRoles(UserViewModel userInfo)
        {
            var app = userInfo.Applications;
            var roles = new Dictionary<string, List<Role>>();
            var Roles = new List<Role>();

            //var user = _userService.FindBy(u=>u.UserName == userInfo.UserName).SingleOrDefault();
            
            var user = _userService.GetUserDetail(userInfo.UserName);
            //user.DefaultHub = userInfo.DefaultHub;
            _userService.UpdateUser(user);

            foreach (var application in app)
            {
                foreach (var role in application.Roles)
                {
                    if (role.IsChecked)
                    {
                        _userService.AddRole(userInfo.UserName, application.ApplicationName, role.RoleName);  
                    }      
                    else if(!role.IsChecked)
                    {
                        //userService.RemoveRole(userInfo.UserName, application.ApplicationName, role.RoleName);  
                    }
                }
                
                //if (Roles.Count > 0)
                //  roles.Add(application.ApplicationName, Roles);
            }

            return RedirectToAction("Index");
            //var user = new UserProfile();

            //var model = new UserViewModel();
            //model.UserName = userInfo.UserName;
            //List<Application> Applications = userService.GetUserPermissions(userInfo.UserName);

            //model.Applications = Applications;
            //return View(model);
        }
       
        public ActionResult ChangePassword()
        {
            //var userInfo=userService.FindById(id);
            var model = new ChangePasswordModel();
            return View(model);
        }
       
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            var userid = UserAccountHelper.GetUser(HttpContext.User.Identity.Name).UserProfileID;
            var oldpassword = _userService.HashPassword(model.OldPassword);
            if (ModelState.IsValid)
            {
                bool changePasswordSucceeded;

                if (_userService.GetUserDetail(userid).Password == oldpassword)
                {
                    try
                    {
                        changePasswordSucceeded = _userService.ChangePassword(userid, model.NewPassword);
                    }
                    catch (Exception e)
                    {
                        changePasswordSucceeded = false;
                        //ModelState.AddModelError("Errors", e.Message);
                    }
                    if (changePasswordSucceeded)
                        ModelState.AddModelError("Success", "Password Successfully Changed.");
                    //return RedirectToAction("ChangePasswordSuccess");
                    else
                        ModelState.AddModelError("Errors", "The new password is invalid.");

                }
                else ModelState.AddModelError("Errors", "The current password is incorrect ");
            }
            return View(model);
        }

        public ActionResult ChangePassword2(FormCollection values)
        {
            var userid = UserAccountHelper.GetUser(HttpContext.User.Identity.Name).UserProfileID;
            var oldpassword = _userService.HashPassword(values["OldPassword"]);
            if (ModelState.IsValid)
            {
                bool changePasswordSucceeded;

                if (_userService.GetUserDetail(userid).Password == oldpassword)
                {
                    try
                    {
                        changePasswordSucceeded = _userService.ChangePassword(userid, values["NewPassword"]);
                    }
                    catch (Exception e)
                    {
                        changePasswordSucceeded = false;
                        //ModelState.AddModelError("Errors", e.Message);
                    }
                    if (changePasswordSucceeded)
                        TempData["error"] = "Success, Password Successfully Changed. Please logout and login with the new credential";
                    //return RedirectToAction("ChangePasswordSuccess");
                    else
                        TempData["error"] = "Errors, The new password is invalid.";

                }
                else TempData["error"] ="Errors, The current password is incorrect ";
            }
            return RedirectToAction("Index");
        }
        //public ActionResult ChangePasswordSuccess()
        //{
        //    ModelState.AddModelError("Sucess", "Password Successfully Changed.");
        //    return View();
        //}
       
    }
}
