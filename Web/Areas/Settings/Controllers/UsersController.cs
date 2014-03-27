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
            return View();
        }
        
        public ActionResult New()
        {
            var model = new UserViewModel();

          
           

            var caseteams = new List<CaseTeam>();

            caseteams.Add(new CaseTeam() { ID = 1,CaseTeamName = "EarlyWarning"});
            caseteams.Add(new CaseTeam() { ID = 2, CaseTeamName = "PSNP/FSCD" });
            caseteams.Add(new CaseTeam() { ID = 3, CaseTeamName = "Logistics" });
            caseteams.Add(new CaseTeam() { ID = 4, CaseTeamName = "Procurement" });
          

            ViewBag.CaseTeams = caseteams;
           
            ViewBag.Regions = _adminUnitService.GetRegions();

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
            //var messages = new List<string>();

            //// Check business rule and validations
            //if (userInfo.UserName == string.Empty)
            //    messages.Add("User name cannot be empty");
            //if (userInfo.FirstName == string.Empty)
            //    messages.Add("First name cannot be empty");
            //if (userInfo.LastName == string.Empty)
            //    messages.Add("Last Name cannot be empty");
            //if (userInfo.Password == string.Empty)
            //    messages.Add("Password cannot be empty");
            //if (userInfo.Password != userInfo.PasswordConfirm)
            //    messages.Add("Passwords do not match");


            //if (messages.Count > 0)
            //    return View();


            // If the supplied information is correct then persist it to the database
            var user = new UserProfile();

            user.UserName = userInfo.UserName;
            user.Password = _userService.HashPassword(userInfo.Password);

            // Set default values for required fields
            user.Disabled = false;
            user.LockedInInd = false;
            user.ActiveInd = true;
            user.NumberOfLogins = 0;

            //List<Cats.Models.Security.ViewModels.Application> app = userInfo.Applications;
            Dictionary<string, List<string>> roles = new Dictionary<string, List<string>>();
            //List<string> Roles;
            //foreach (var application in app)
            //{
            //    Roles = new List<string>();
            //    foreach (var role in application.Roles)
            //    {
            //        if (role.IsChecked)
            //            Roles.Add(role.RoleName);
            //    }
            //    if (Roles.Count > 0)
            //        roles.Add(application.ApplicationName, Roles);
            //}

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

            if(_userService.Add(user, roles))
            {
                return View("Index");
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
                                        new CaseTeam() {ID = 4, CaseTeamName = "Procurement"}
                                    };
            var userTypes = new SelectList(new[]
                       {
                           new SelectListItem {Text = "Regional", Value = "1"},
                           new SelectListItem {Text = "Hub", Value = "2"},
                            new SelectListItem {Text = "Case team", Value = "3"},
                       }, "Text", "Value");

            ViewBag.CaseTeams = caseteams;
            ViewBag.userTypes = userTypes;
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
                ViewBag.hubs = _hubService.GetAllHub().ToList();
                ViewBag.regions = _adminUnitService.GetRegions();
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
                user.RegionID = userInfo.RegionalUser ? user.RegionID : null; 
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
            return View(user);
        }

        public JsonResult GetUsers()
        {
            var users = _userService.GetAll();
            return Json(users.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditUserRoles(string UserName)
        {
            //var roles = new string[] { "EW Coordinator", "EW-Experts" };
            //userService.AddRoleSample("Rahel", "Early Warning", roles);
            var model = new UserViewModel();
            model.UserName = UserName;
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
            user.DefaultHub = userInfo.DefaultHub;
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

        public JsonResult ChangePassword2(FormCollection values)
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
                        ModelState.AddModelError("Success", "Password Successfully Changed.");
                    //return RedirectToAction("ChangePasswordSuccess");
                    else
                        ModelState.AddModelError("Errors", "The new password is invalid.");

                }
                else ModelState.AddModelError("Errors", "The current password is incorrect ");
            }
            return new JsonResult();
        }
        //public ActionResult ChangePasswordSuccess()
        //{
        //    ModelState.AddModelError("Sucess", "Password Successfully Changed.");
        //    return View();
        //}
       
    }
}
