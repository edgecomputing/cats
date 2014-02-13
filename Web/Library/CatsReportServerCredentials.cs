using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Microsoft.Reporting;
using Microsoft.Reporting.WebForms;

namespace Cats.Library
{
    public class CatsReportServerCredentials:IReportServerCredentials
    {
        private string _userName;
        private string _password;

        public CatsReportServerCredentials(string userName, string password)
        {
            _userName = userName;
            _password = password;
        }

        public bool GetFormsCredentials(out System.Net.Cookie authCookie, out string userName, out string password, out string authority)
        {
            authCookie = null;
            userName = _userName;
            password = _password;
            authority = null;
            return false;
        }

        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get { return null; }
        }

        public System.Net.ICredentials NetworkCredentials
        {
            get { return new NetworkCredential(_userName,_password);}
        }
    }
}