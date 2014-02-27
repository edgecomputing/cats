using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Security;
using Cats.Security;
using Logistics.Security;
using Ninject;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan;
using NetSqlAzMan.Cache;

namespace Cats.Helpers
{

    public class EarlyWarningAuthorize : AuthorizeAttribute
    {
        private EarlyWarningConstants constants = new EarlyWarningConstants();
        UserPermissionCache ewCache;
        public EarlyWarningConstants.Operation operation;


        public EarlyWarningAuthorize()
        {
            ewCache = UserAccountHelper.GetUserPermissionCache(CatsGlobals.Applications.EarlyWarning);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            ewCache = UserAccountHelper.GetUserPermissionCache(CatsGlobals.Applications.EarlyWarning);
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            if (!httpContext.User.Identity.IsAuthenticated)
                return false;            
            return ewCache.CheckAccess(constants.ItemName(operation), DateTime.Now) == AuthorizationType.Allow ? true : false;
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