using Cats.Models.Security;
using System.Collections;
using System.Security.Principal;

namespace Cats.Services.Security
{
    public class UserIdentity : IIdentity
    {
        private string userName = string.Empty;
        private string fullName = string.Empty;
        private ArrayList roles = new ArrayList();
        private UserInfo _profile;
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

        public UserInfo Profile
        {
            get
            {
                return _profile;
            }
        }
        internal bool IsInRole(string role)
        {
            return roles.Contains(role);
        }

        #endregion

        #region Constructor(s)

        public UserIdentity(User user)
        {
            this.userName = user.UserName;
            this.authenticated = true;
        }

        public UserIdentity(UserInfo info)
        {
            this.userName = info.UserName;
            this.fullName = info.FullName;
            this._profile = info;
            this.authenticated = true;            
        }

        public void SetPermissions(string[] permissions)
        {
            roles.AddRange(permissions);
        }

        // TODO: Remember to refactor code!!!
        public UserIdentity(int userId, UserAccountService service, UserInfo userInfo, string store, string application)
        {
            //Assign the incoming user name to the current one and clear the roles collection
            var user = service.GetUserDetail(userId);
            this.userName = user.UserName;
            this._profile = userInfo;
            this.roles.Clear();
            authenticated = true;
            /* Retrive the list of all authorized Tasks and Operations from NetSqlAzMan database
             * and persist it with the roles arraylist collection
             */
            roles.AddRange(service.GetUserPermissions(userInfo.UserName, "CATS", application));
        }
        #endregion
    }
}
