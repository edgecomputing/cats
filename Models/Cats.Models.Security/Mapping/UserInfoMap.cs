using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Security.Mapping
{
    public class UserInfoMap : EntityTypeConfiguration<UserInfo>
    {
        public UserInfoMap()
        {
            this.HasKey(t => t.UserId);
            this.ToTable("VWUserInfo");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.DBUserSid).HasColumnName("DBUserSid");
            this.Property(t => t.UserProfileID).HasColumnName("UserProfileID");
            this.Property(t => t.UILanguage).HasColumnName("LanguageCode");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.FullName).HasColumnName("FullName");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.Disabled).HasColumnName("Disabled");
            this.Property(t => t.ActiveInd).HasColumnName("ActiveInd");
            this.Property(t => t.LoggedInInd).HasColumnName("LoggedInInd");
            this.Property(t => t.LogInDate).HasColumnName("LogginDate");
            this.Property(t => t.LogOutDate).HasColumnName("LogOutDate");
            this.Property(t => t.FailedAttempts).HasColumnName("FailedAttempts");
            this.Property(t => t.LockedInInd).HasColumnName("LockedInInd");
            this.Property(t => t.PreferedWeightMeasurment).HasColumnName("PreferedWeightMeasurment");
            this.Property(t => t.Calendar).HasColumnName("Calendar");
            this.Property(t => t.KeyBoardLanguage).HasColumnName("Keyboard");
        }
    }
}
