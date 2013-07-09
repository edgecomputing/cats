using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace Cats.Services.Security
{
    public class UserIdentity : IIdentity
    {
        private string userName = string.Empty;
        private string fullName = string.Empty;
        private ArrayList roles = new ArrayList();
        private bool authenticated = false;


        #region IIdentity Members

        public string AuthenticationType
        {
            get { return "CatsSecurity"; }
        }

        public bool IsAuthenticated
        {
            get { return authenticated; }
        }

        public string Name
        {
            get { return userName; }
        }

        public string FullName
        {
            get { return fullName; }
        }
        internal bool IsInRole(string role)
        {
            return roles.Contains(role);
        }

        #endregion

        #region Constructor(s)

        public UserIdentity(string userName, UserAccountService service)
        {
            //Assign the incoming user name to the current one and clear the roles collection
            var user = service.GetUserDetail(userName);
            this.userName = userName;
            this.fullName = user.FullName;

            this.roles.Clear();
            authenticated = true;
            roles.AddRange(service.GetUserPermissions(userName));
        }

        public UserIdentity(int userId, UserAccountService service)
        {
            //Assign the incoming user name to the current one and clear the roles collection
            var user = service.GetUserDetail(userId);
            this.userName = user.UserName;
            this.fullName = user.FullName;
            this.roles.Clear();
            authenticated = true;
            /* Retrive the list of all authorized Tasks and Operations from NetSqlAzMan database
             * and persist it with the roles arraylist collection
             */
            roles.AddRange(service.GetUserPermissions(userName));
        }
        #endregion
    }
}
