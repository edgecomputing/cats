using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hubs.MetaModels
{

		public sealed class UserRoleMetaModel
		{
		
			[Required(ErrorMessage="User Role is required")]
    		public Int32 UserRoleID { get; set; }

			[Required(ErrorMessage="User Profile is required")]
    		public Int32 UserProfileID { get; set; }

			[Required(ErrorMessage="Role is required")]
    		public Int32 RoleID { get; set; }

    		public EntityCollection<Role> Role { get; set; }

    		public EntityCollection<UserProfile> UserProfile { get; set; }

	   }
}

