using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Early_Warning.Security;
using Ninject;

namespace Cats.Web.Hub.Helpers
{

    public class CatsAuthorize : AuthorizeAttribute
    {
        private readonly ICheckAccessHelper _checkAccessHelper;
        
        public CheckAccessHelper.Operation operation;
       

        public CatsAuthorize()
        {
            _checkAccessHelper = DependencyResolver.Current.GetService<ICheckAccessHelper>();
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            if (!httpContext.User.Identity.IsAuthenticated)
                return false;
            return _checkAccessHelper.CheckAccess(operation,
                                                 _checkAccessHelper.Storage.GetDBUser(httpContext.User.Identity.Name).
                                                     CustomSid);
        }
    }
}