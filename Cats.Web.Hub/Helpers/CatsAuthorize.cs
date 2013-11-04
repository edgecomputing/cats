using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Security;
using Early_Warning.Security;
using Ninject;

namespace Cats.Web.Hub.Helpers
{

    public class CatsAuthorize : AuthorizeAttribute
    {
        private readonly IEarlyWarningCheckAccess _checkAccessHelper;

        public EarlyWarningCheckAccess.Operation operation;


        public CatsAuthorize()
        {
            _checkAccessHelper = DependencyResolver.Current.GetService<IEarlyWarningCheckAccess>();
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