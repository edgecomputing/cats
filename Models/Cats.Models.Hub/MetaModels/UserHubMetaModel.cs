using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hub.MetaModels
{

		public sealed class UserHubMetaModel
		{
		
			[Required(ErrorMessage="User Hub is required")]
    		public Int32 UserHubID { get; set; }

			[Required(ErrorMessage="User Profile is required")]
    		public Int32 UserProfileID { get; set; }

			[Required(ErrorMessage="Hub is required")]
    		public Int32 HubID { get; set; }

			[StringLength(1)]
    		public String IsDefault { get; set; }

    		public EntityCollection<Hub> Hub { get; set; }

    		public EntityCollection<UserProfile> UserProfile { get; set; }

	   }
}

