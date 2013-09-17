using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace Cats.Models.Hub.MetaModels
{

		public sealed class UserProfileMetaModel
		{
		    [Required(ErrorMessage="User Profile is required")]
			[Key]
    		public Int32 UserProfileID { get; set; }

			[Required(ErrorMessage="User Name is required")]
			[StringLength(50)]
    		public String UserName { get; set; }

			[Required(ErrorMessage="Password is required")]
			[StringLength(50)]
    		public String Password { get; set; }

			[Required(ErrorMessage="First Name is required")]
			[StringLength(30)]
    		public String FirstName { get; set; }

			[Required(ErrorMessage="Last Name is required")]
			[StringLength(30)]
    		public String LastName { get; set; }

			[StringLength(30)]
    		public String GrandFatherName { get; set; }

			[Required(ErrorMessage="Active Ind is required")]
    		public Boolean ActiveInd { get; set; }

			[Required(ErrorMessage="Logged In Ind is required")]
    		public Boolean LoggedInInd { get; set; }

			[DataType(DataType.DateTime)]
    		public DateTime LogginDate { get; set; }

			[DataType(DataType.DateTime)]
    		public DateTime LogOutDate { get; set; }

			[Required(ErrorMessage="Failed Attempts is required")]
    		public Int32 FailedAttempts { get; set; }

			[Required(ErrorMessage="Locked In Ind is required")]
    		public Boolean LockedInInd { get; set; }

			[Required(ErrorMessage="Language Code is required")]
			[StringLength(2)]
    		public String LanguageCode { get; set; }

			[Required(ErrorMessage="Date Preference is required")]
			[StringLength(2)]
    		public String DatePreference { get; set; }

			[Required(ErrorMessage="Prefered Weight Measurment is required")]
			[StringLength(2)]
    		public String PreferedWeightMeasurment { get; set; }

			[StringLength(20)]
    		public String MobileNumber { get; set; }

			[StringLength(100)]
			[DataType(DataType.EmailAddress)]
    		public String Email { get; set; }

			[Required(ErrorMessage="Default Theme is required")]
			[StringLength(50)]
    		public String DefaultTheme { get; set; }

    		public EntityCollection<Adjustment> Adjustments { get; set; }

    		public EntityCollection<Receive> Receives { get; set; }

    		public EntityCollection<UserHub> UserHubs { get; set; }

    		public EntityCollection<UserRole> UserRoles { get; set; }

    		public EntityCollection<SessionHistory> SessionHistories { get; set; }

    		public EntityCollection<SessionAttempt> SessionAttempts { get; set; }

	   }
}

