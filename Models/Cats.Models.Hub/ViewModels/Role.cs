using System.ComponentModel.DataAnnotations;
using System.Linq;
using Cats.Models.Hub.MetaModels;

namespace Cats.Models.Hub
{
   
    partial class Role
    {

        public static void AddRole(Role role)
        {
            //CTSContext entities = new CTSContext();
            //entities.Roles.Add(role);
            //entities.SaveChanges();
        }

        public void RemoveRole(string roleName, string userName)
        {
            //CTSContext entities = new CTSContext();
            //UserRole role = entities.UserRoles.Where(r => r.Role.Name == roleName && r.UserProfile.UserName == userName).SingleOrDefault();
            //if (role != null)
            //{
            //    entities.UserRoles.Remove(role); 
            //    entities.SaveChanges();
            //} 
        }

        public void AddUserToRole(int roleId, string userName)
        {
            //CTSContext entities = new CTSContext();
            //UserProfile user = entities.UserProfiles.Where(p => p.UserName == userName).FirstOrDefault();
            //if (user != null)
            //{
            //    UserRole role = new UserRole();
            //    role.RoleID = roleId;
            //    role.UserProfileID = user.UserProfileID;
            //    entities.UserRoles.Add(role);
            //    entities.SaveChanges();
            //}
            
        }

        public static Role GetRole(string name)
        {
            //CTSContext entities = new CTSContext();
            //return entities.Roles.Where(p => p.Name == name).SingleOrDefault();
            return null;
        }


        public static bool RoleExists(string name)
        {
            //CTSContext entities = new CTSContext();
            //var count = entities.Roles.Where(p => p.Name == name).Count();
            //return (count > 0);
            return true;
        }
                
        public enum RoleEnum
        {
            Admin,
            DataEntry,
        }


    }
  
}
