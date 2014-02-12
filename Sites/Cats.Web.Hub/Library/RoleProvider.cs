using System.Web.Mvc;
using Cats.Services.Hub;
using System;
using System.Linq;


namespace Cats.Web.Hub
{
    public class RoleProvider :System.Web.Security.RoleProvider
    {
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }
         
        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            var userRoldeService = (IUserRoleService) DependencyResolver.Current.GetService(typeof (IUserRoleService));
            var roles =
                userRoldeService.Get(t => t.UserProfile.UserName == username, null, "UserProfile,Role").Select(
                    t => t.Role.Name);
            

            return roles.ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            return new string[1];
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var userRoldeService = (IUserRoleService)DependencyResolver.Current.GetService(typeof(IUserRoleService));
            var count =
                userRoldeService.Get(t => t.UserProfile.UserName == username && t.Role.Name == roleName, null,
                                     "UserProfile,Role").Count();
            
         
          
            return (count > 0);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        
    }
}