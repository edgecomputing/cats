using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Cats.Web.Hub
{
    public class FormsAuthenticationWrapper : IFormsAuthenticationWrapper
    {
        public void SetAuthCookie(string userName, bool rememberMe)
        {
            FormsAuthentication.SetAuthCookie(userName, rememberMe);
        }
    }

    public interface IFormsAuthenticationWrapper
    {
        void SetAuthCookie(string userName, bool rememberMe);
    }
}