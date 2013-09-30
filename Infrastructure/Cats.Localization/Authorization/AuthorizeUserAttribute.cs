using System;
using System.Web;
using NetSqlAzMan.Interfaces;
using System.Configuration;
//using System.Web.Http;
using System.Net.Http;
using System.Web.Mvc;
using System.Security.Principal;


namespace Cats.AzManHelpers
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        private string User { get { return HttpContext.Current.User.Identity.Name; } }
        private string Store { get { return "CATS"; } }
        public AzManHelper.Applications Application { get; set; }
        public object Item { get; set; }

        public AuthorizeUserAttribute()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SecurityContext"].ConnectionString;
            AzManStore = new NetSqlAzMan.SqlAzManStorage(connectionString);
        }
        private string connectionString = string.Empty;
        protected IAzManStorage AzManStore = null;

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            IPrincipal user = httpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                return false;
            }

            AuthorizationType authorization = AzManStore.CheckAccess(Store, Application.ToString().Replace('_', ' '), Item.ToString().Replace('_', ' '), AzManStore.GetDBUser(User), DateTime.Now, false, null);
            if (authorization == AuthorizationType.Allow || authorization == AuthorizationType.AllowWithDelegation)
                return true;
            else
                return false;
        }

        protected void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (AuthorizeCore(filterContext.HttpContext))
            {
                HttpCachePolicyBase cachePolicy = filterContext.HttpContext.Response.Cache;
                cachePolicy.SetProxyMaxAge(new TimeSpan(0));
                cachePolicy.AddValidationCallback(CacheValidateHandler, null);
            }
            else
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        protected override HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            bool isAuthorized = AuthorizeCore(httpContext);
            return (isAuthorized) ? HttpValidationStatus.Valid : HttpValidationStatus.IgnoreThisRequest;
        }
    }
}
