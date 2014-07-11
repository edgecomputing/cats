using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Security;
using Cats.Security;
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
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            if (!httpContext.User.Identity.IsAuthenticated)
                return false;
            return true;
            //return ewCache.CheckAccess(constants.ItemName(operation), DateTime.Now) == AuthorizationType.Allow ? true : false;
        }
    }

    public class PSNPAuthorize : AuthorizeAttribute
    {
        private PsnpConstants constants = new PsnpConstants();
        UserPermissionCache ewCache;
        public PsnpConstants.Operation operation;


        public PSNPAuthorize()
        {
            ewCache = UserAccountHelper.GetUserPermissionCache(CatsGlobals.Applications.PSNP);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            if (!httpContext.User.Identity.IsAuthenticated)
                return false;
            return ewCache.CheckAccess(constants.ItemName(operation), DateTime.Now) == AuthorizationType.Allow ? true : false;
        }
    }

    public class LogisticsAuthorize : AuthorizeAttribute
    {
        private LogisticsConstants constants = new LogisticsConstants();
        UserPermissionCache ewCache;
        public LogisticsConstants.Operation operation;

        public LogisticsAuthorize()
        {
            ewCache = UserAccountHelper.GetUserPermissionCache(CatsGlobals.Applications.Logistics);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            if (!httpContext.User.Identity.IsAuthenticated)
                return false;
            return ewCache.CheckAccess(constants.ItemName(operation), DateTime.Now) == AuthorizationType.Allow ? true : false;
        }
    }

    public class ProcurementAuthorize : AuthorizeAttribute
    {
        private ProcurementConstants constants = new ProcurementConstants();
        UserPermissionCache ewCache;
        public ProcurementConstants.Operation operation;


        public ProcurementAuthorize()
        {
            ewCache = UserAccountHelper.GetUserPermissionCache(CatsGlobals.Applications.Procurement);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            if (!httpContext.User.Identity.IsAuthenticated)
                return false;
            return ewCache.CheckAccess(constants.ItemName(operation), DateTime.Now) == AuthorizationType.Allow ? true : false;
        }
    }

    public class HubAuthorize : AuthorizeAttribute
    {
        private HubConstants constants = new HubConstants();
        UserPermissionCache ewCache;
        public HubConstants.Operation operation;


        public HubAuthorize()
        {
            ewCache = UserAccountHelper.GetUserPermissionCache(CatsGlobals.Applications.Hub);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            if (!httpContext.User.Identity.IsAuthenticated)
                return false;
            return ewCache.CheckAccess(constants.ItemName(operation), DateTime.Now) == AuthorizationType.Allow ? true : false;
        }
    }

    public class RegionalAuthorize : AuthorizeAttribute
    {
        private RegionalConstants constants = new RegionalConstants();
        UserPermissionCache ewCache;
        public RegionalConstants.Operation operation;


        public RegionalAuthorize()
        {
            ewCache = UserAccountHelper.GetUserPermissionCache(CatsGlobals.Applications.Region);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            if (!httpContext.User.Identity.IsAuthenticated)
                return false;
            return ewCache.CheckAccess(constants.ItemName(operation), DateTime.Now) == AuthorizationType.Allow ? true : false;
        }
    }
    
}