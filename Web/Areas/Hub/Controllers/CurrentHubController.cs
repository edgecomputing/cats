using System.Web.Mvc;
using Cats.Models.Hubs;
using Cats.Services.Hub;
using Cats.Web.Hub;

namespace Cats.Areas.Hub.Controllers
{
     [Authorize]
    public class CurrentHubController : BaseController
    {
         private readonly IUserProfileService _userProfileService;
         private readonly IUserHubService _userHubService;
        //
        // GET: /CurrentWarehouse/
         public CurrentHubController(IUserProfileService userProfileService,
             IUserHubService userHubService):base(userProfileService)
         {
             _userProfileService = userProfileService;
             _userHubService = userHubService;
         }

        public virtual ActionResult DisplayCurrentHub()
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            return PartialView("DisplayHub", new CurrentUserModel(user)); 
        }


        public UserProfile CurrentUser(string user)
        {
            
            return _userProfileService.GetUser(user);
        }

        public virtual ActionResult HubList()
        {
            var user = _userProfileService.GetUser(User.Identity.Name);
            return user != null ? PartialView("SelectHub", new CurrentUserModel(user)) : null;
        }




        public virtual ActionResult ChangeHub(CurrentUserModel currentUser)
        {

            var user = _userProfileService.GetUser(User.Identity.Name);
           
            _userHubService.ChangeHub(user.UserProfileID,currentUser.DefaultHubId);
            
            //return RedirectToAction("Index", "Dispatch");
            //return Json(new { success = true });
            return Request.UrlReferrer != null ? Redirect(Request.UrlReferrer.PathAndQuery) : null;
        }

    }
}
