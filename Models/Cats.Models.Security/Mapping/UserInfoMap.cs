using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Security.Mapping
{
    public class UserInfoMap : EntityTypeConfiguration<UserInfo>
    {
        public UserInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.UserProfileID);

            // Properties
            this.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Password)
                .IsRequired();

            this.Property(t => t.FailedAttempts);

            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.GrandFatherName)
                .HasMaxLength(200);

            this.Property(t => t.Email)
                .HasMaxLength(50);

            this.Property(t => t.LanguageCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.DatePreference)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.Keyboard)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.PreferedWeightMeasurment)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.DefaultTheme)
                .IsRequired()
                .HasMaxLength(50);
            this.Property(t => t.RegionalUser)
                .IsRequired();
            this.Property(t => t.IsAdmin)
              .IsRequired();

            //this.Property(t => t.UserSID)
            //    .HasMaxLength(85);

            // Table & Column Mappings
            this.ToTable("UserInfo");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.Disabled).HasColumnName("Disabled");
            this.Property(t => t.LoggedInInd).HasColumnName("LoggedInInd");
            this.Property(t => t.LogginDate).HasColumnName("LogginDate");
            this.Property(t => t.LogOutDate).HasColumnName("LogOutDate");
            this.Property(t => t.FailedAttempts).HasColumnName("FailedAttempts");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.GrandFatherName).HasColumnName("GrandFatherName");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.CaseTeam).HasColumnName("CaseTeam");
            this.Property(t => t.LanguageCode).HasColumnName("LanguageCode");
            this.Property(t => t.DatePreference).HasColumnName("DatePreference");
            this.Property(t => t.Keyboard).HasColumnName("Keyboard");
            this.Property(t => t.PreferedWeightMeasurment).HasColumnName("PreferedWeightMeasurment");
            this.Property(t => t.DefaultTheme).HasColumnName("DefaultTheme");
            this.Property(t => t.ActiveInd).HasColumnName("ActiveInd");
            this.Property(t => t.LockedInInd).HasColumnName("LockedInInd");
            this.Property(t => t.UserProfileID).HasColumnName("UserProfileID");
            this.Property(t => t.NumberOfLogins).HasColumnName("NumberOfLogins");
            this.Property(t => t.RegionID).HasColumnName("RegionID");
            this.Property(t => t.DefaultHub).HasColumnName("DefaultHub");
            this.Property(t => t.IsAdmin).HasColumnName("IsAdmin");

            // this.Property(t => t.UserSID).HasColumnName("UserSID");
        }
    }
}