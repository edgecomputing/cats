using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Security;
using Early_Warning.Security;
using Ninject;

namespace Cats.Helpers
{

    public class EarlyWarningAuthorize : AuthorizeAttribute
    {
        private readonly IEarlyWarningCheckAccess _checkAccessHelper;
        
        public EarlyWarningCheckAccess.Operation operation;


        public EarlyWarningAuthorize()
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

    public class LogisticsAuthorize : AuthorizeAttribute
    {
        private readonly ILogisticsCheckAccess _checkAccessHelper;

        public LogisticsCheckAccess.Operation operation;


        public LogisticsAuthorize()
        {
            _checkAccessHelper = DependencyResolver.Current.GetService<ILogisticsCheckAccess>();
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

    public class ProcurementAuthorize : AuthorizeAttribute
    {
        private readonly IProcurementCheckAccess _checkAccessHelper;

        public ProcurementCheckAccess.Operation operation;


        public ProcurementAuthorize()
        {
            _checkAccessHelper = DependencyResolver.Current.GetService<IProcurementCheckAccess>();
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

    public class PSNPAuthorize : AuthorizeAttribute
    {
        private readonly IPSNPCheckAccess _checkAccessHelper;

        public PSNPCheckAccess.Operation operation;


        public PSNPAuthorize()
        {
            _checkAccessHelper = DependencyResolver.Current.GetService<IPSNPCheckAccess>();
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