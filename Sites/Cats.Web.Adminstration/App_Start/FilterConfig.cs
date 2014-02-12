using System.Web;
using System.Web.Mvc;

namespace Cats.Web.Adminstration
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}