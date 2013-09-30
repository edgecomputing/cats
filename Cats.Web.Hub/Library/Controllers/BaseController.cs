using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Web.Security;
using Cats.Models.Hub;
using Cats.Web.Hub.Helpers;
using MembershipProvider = Cats.Web.Hub.Helpers.MembershipProvider;

namespace Cats.Web.Hub
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseController : Controller
    {
        public BaseController()
        {
            this.UserProfileService = new Cats.Services.Hub.UserProfileService(_unitOfWork);
        }
        
        private UserProfile userProfile = null;
        private Cats.Services.Hub.IUserProfileService UserProfileService;
        private Cats.Data.Hub.IUnitOfWork _unitOfWork = new Cats.Data.Hub.UnitOfWork();
        
        
        /// <summary>
        /// Gets the user profile.
        /// </summary>
        public UserProfile UserProfile
        {
            get
            {
                if (userProfile == null)
                {
                    userProfile = GetCurrentUserProfile();
                }
                return userProfile;
            }
        }
        
        

        /// <summary>
        /// Gets the current user profile.
        /// </summary>
        /// <returns></returns>
        protected UserProfile GetCurrentUserProfile()
        {
                
                //MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true);
                //MembershipProvider mem = new MembershipProvider();
                //return mem.getUser(currentUser);
                return UserProfileService.GetUser(User.Identity.Name);
        }

        /// <summary>
        /// Invokes the action in the current controller context.
        /// </summary>
        protected override void ExecuteCore()
        {
            
            string cultureName = null;
            
            //get the current user and chek if she/he set the LanguageCode
            if (User.Identity.IsAuthenticated)
            {
                try
                {

                    cultureName = this.GetCurrentUserProfile().LanguageCode;
                }
                catch (Exception e)
                {
                    cultureName = "en";
                }
            }
            else
            {
                // Attempt to read the culture cookie from Request
                HttpCookie cultureCookie = Request.Cookies["_culture"];
                if (cultureCookie != null)
                    cultureName = cultureCookie.Value;
                else
                    cultureName = "en";//Request.UserLanguages[0];  obtain it from HTTP header AcceptLanguages
            }
            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe


            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd-MMM-yyyy";

           base.ExecuteCore();
        }   
      
    }

}

