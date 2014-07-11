using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Cats
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
            //config.Formatters.JsonFormatter.SupportedEncodings.RemoveAt(0);
            //config.Formatters.JsonFormatter.SupportedEncodings[]=null;


            //config.Formatters.JsonFormatter.AddUriPathExtensionMapping("json", "application/json");
            //config.Formatters.XmlFormatter.
            //config.Formatters.XmlFormatter.AddUriPathExtensionMapping("xml", "application/xml");
            //config.Formatters.JsonFormatter.SupportedEncodings.FirstOrDefault()            //config.Formatters.JsonFormatter.SupportedMediaTypes.FirstOrDefault()
        }
    }
}
