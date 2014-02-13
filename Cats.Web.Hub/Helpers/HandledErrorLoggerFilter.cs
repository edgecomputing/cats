using System;
using System.Web.Mvc;
using Elmah;
using System.Web;
using System.Web.Routing;

namespace Cats.Web.Hub
{
    public class ElmahHandledErrorLoggerFilter : IExceptionFilter
    {
        public void OnException (ExceptionContext context)
        {
            // Log only handled exceptions, because all other will be caught by ELMAH anyway.
            if (context.ExceptionHandled)
                ErrorSignal.FromCurrentContext().Raise(context.Exception);
        }
    }
    
    
    /*
    // for using custome error handling with custome attributes
    
    public class HandleErrorWithELMAHAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            var e = context.Exception;
            if (!context.ExceptionHandled   // if unhandled, will be logged anyhow
                    || RaiseErrorSignal(e)      // prefer signaling, if possible
                    || IsFiltered(context))     // filtered?
                return;

            LogException(e);
        }

        private static bool RaiseErrorSignal(Exception e)
        {
            var context = HttpContext.Current;
            if (context == null)
                return false;
            var signal = ErrorSignal.FromContext(context);
            if (signal == null)
                return false;
            signal.Raise(e, context);
            return true;
        }

        private static bool IsFiltered(ExceptionContext context)
        {
            var config = context.HttpContext.GetSection("elmah/errorFilter")
                                     as ErrorFilterConfiguration;

            if (config == null)
                return false;

            var testContext = new ErrorFilterModule.AssertionHelperContext(
                                                                context.Exception, HttpContext.Current);

            return config.Assertion.Test(testContext);
        }

        private static void LogException(Exception e)
        {
            var context = HttpContext.Current;
            ErrorLog.GetDefault(context).Log(new Error(e, context));
        }
    
    }

    
     // we don't need to add [HandleErrorWithELMAH] on each action
      
    public class ErrorHandlingActionInvoker : ControllerActionInvoker
    {
        private readonly IExceptionFilter filter;

        public ErrorHandlingActionInvoker(IExceptionFilter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter");
            }

            this.filter = filter;
        }

        protected override FilterInfo GetFilters(
        ControllerContext controllerContext,
        ActionDescriptor actionDescriptor)
        {
            var filterInfo =
            base.GetFilters(controllerContext,
            actionDescriptor);

            filterInfo.ExceptionFilters.Add(this.filter);

            return filterInfo;
        }
    }

    //the factory class
     
    public class ErrorHandlingControllerFactory : DefaultControllerFactory
    {
        public override IController CreateController(
        RequestContext requestContext,
        string controllerName)
        {
            var controller =
            base.CreateController(requestContext,
            controllerName);

            var c = controller as Controller;

            if (c != null)
            {
                c.ActionInvoker =
                new ErrorHandlingActionInvoker(
                new HandleErrorWithELMAHAttribute());
            }

            return controller;
        }
    }*/

}
