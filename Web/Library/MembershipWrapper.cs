using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Cats.Web.Hub
{
    public class MembershipWrapper : IMembershipWrapper
    {
        public bool ValidateUser(string userName, string password)
        {
            //new Cats.Web.Hub.MembershipProvider()
            return Membership.ValidateUser(userName, password);
        }
    }

    public interface IMembershipWrapper
    {
        bool ValidateUser(string userName, string password);
    }
}