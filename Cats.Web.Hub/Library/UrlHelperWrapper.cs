using System.Web.Mvc;
using System.Web.Routing;

namespace Cats.Web.Hub
{
    public interface IUrlHelperWrapper
    {
        string Action(string actionName, string controllerName);

        string Action(string actionName, string controllerName, object routeValues);

        string Action(string actionName, string controllerName, RouteValueDictionary routeValues);

        bool IsLocalUrl(string url);
    }

    public class UrlHelperWrapper : UrlHelper, IUrlHelperWrapper
    {
        internal UrlHelperWrapper(RequestContext requestContext)
            : base(requestContext)
        {
        }

        internal UrlHelperWrapper(RequestContext requestContext, RouteCollection routeCollection)
            : base(requestContext, routeCollection)
        {
        }

        public UrlHelperWrapper(UrlHelper helper)
            : base(helper.RequestContext, helper.RouteCollection)
        {
        }
    }
}