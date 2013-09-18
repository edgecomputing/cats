using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hub.MetaModels
{

		public sealed class RoleMetaModel
		{
		
			[Required(ErrorMessage="Role is required")]
    		public Int32 RoleID { get; set; }

			[Required(ErrorMessage="Sort Order is required")]
    		public Int32 SortOrder { get; set; }

			[Required(ErrorMessage="Name is required")]
			[StringLength(50)]
    		public String Name { get; set; }

			[StringLength(50)]
    		public String Description { get; set; }

    		public EntityCollection<UserRole> UserRoles { get; set; }

    		public EntityCollection<SessionHistory> SessionHistories { get; set; }

    		public EntityCollection<SessionAttempt> SessionAttempts { get; set; }

	   }
}

