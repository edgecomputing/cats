using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cats.Helpers
{
    public static  class CaseHelper
    {
        public static string ToUpper(this HtmlHelper helper, string stringToBeChanged = "")
        {
            return stringToBeChanged.ToUpper();
        }

        public static string ToLower(this HtmlHelper helper, string stringToBeChanged)
        {
            return stringToBeChanged.ToLower();
        }
    }
}