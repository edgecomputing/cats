using System.Web.Mvc;

namespace Cats.Web.Administration.Helpers
{
    public static class AmharicEditorHelper
    {
        public static MvcHtmlString AmharicTextBox(this HtmlHelper helper, string name, string value, bool showToggleButton = true)
        {

            var variable = (string.Format(@"<input id='{0}' type='text' class='amharic' jgeez-englishbutton='englishplain-{0}'
                                                jgeez-geezbutton='geezplain-{0}' name='{0}' value='{1}'>
                                                <span style='margin-top: 5px; width: 355px;'>
                                                    <a id='geezplain-{0}' class='t-button icon-button geeztext' >አማርኛ</a>
                                                    <a id='englishplain-{0}' class='t-button icon-button' >English</a>
                                                </span>", name, value));
            if (!helper.ViewData.Keys.Contains("amharic"))
            {
                variable += @"<script src='/Scripts/jquery.jgeez.min.js' type='text/javascript'></script>
                            <script type='text/javascript'>
                                $(function () {
                                    $('.amharic').jGeez();
                                });
                          </script>";
                helper.ViewData["amharic"] = true;
            }
            return new MvcHtmlString(variable);
        }
    }
}