using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs
{
    public class UserRolesModel
    {
        public UserRoleModel[] UserRoles { get; set; }
    }

    public class UserRoleModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Selected { get; set; }
        public int SortOrder { get; set; }
    }
}