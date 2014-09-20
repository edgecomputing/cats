using System.Web;
using System.Web.Mvc;
using Cats.Filters;
namespace Cats
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LocalizationFilter());
            filters.Add(new AuthorizeAttribute());
            
        }
    }
}