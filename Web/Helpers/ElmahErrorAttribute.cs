using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Helpers
{
    public class ElmahErrorAttribute : System.Web.Http.Filters.ExceptionFilterAttribute
    {
        public override void OnException(
             System.Web.Http.Filters.HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception != null)
                Elmah.ErrorSignal.FromCurrentContext().Raise(actionExecutedContext.Exception);
            base.OnException(actionExecutedContext);
        }
    }
}