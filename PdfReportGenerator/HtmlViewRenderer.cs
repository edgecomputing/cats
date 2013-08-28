// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlViewRenderer.cs" company="SemanticArchitecture">
//   http://www.SemanticArchitecture.net pkalkie@gmail.com
// </copyright>
// <summary>
//   This class is responsible for rendering a HTML view to a string.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ReportManagement
{
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    /// <summary>
    /// This class is responsible for rendering a HTML view into a string. 
    /// </summary>
    public class HtmlViewRenderer
    {
        public string RenderViewToString(Controller controller, string viewName, object viewData)
        {
            var renderedView = new StringBuilder();
            using (var responseWriter = new StringWriter(renderedView))
            {
                var fakeResponse = new HttpResponse(responseWriter);
                var fakeContext = new HttpContext(HttpContext.Current.Request, fakeResponse);
                var fakeControllerContext = new ControllerContext(new HttpContextWrapper(fakeContext), controller.ControllerContext.RouteData, controller.ControllerContext.Controller);

                var oldContext = HttpContext.Current;
                HttpContext.Current = fakeContext;

                using (var viewPage = new ViewPage())
                {
                    var html = new HtmlHelper(CreateViewContext(responseWriter, fakeControllerContext), viewPage);
                    html.RenderPartial(viewName, viewData);
                    HttpContext.Current = oldContext;
                }
            }

            return renderedView.ToString();
        }

        private static ViewContext CreateViewContext(TextWriter responseWriter, ControllerContext fakeControllerContext)
        {
            return new ViewContext(fakeControllerContext, new FakeView(), new ViewDataDictionary(), new TempDataDictionary(), responseWriter);
        }
    }
}