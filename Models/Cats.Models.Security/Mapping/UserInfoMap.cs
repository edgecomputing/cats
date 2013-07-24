using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Security.Mapping
{
    public class UserInfoMap : EntityTypeConfiguration<UserInfo>
    {
        public UserInfoMap()
        {
            // Primary Key
            this.HasKey(t => new { t.UserAccountId, t.UserName, t.Password, t.Disabled, t.LoggedIn, t.FailedAttempts, t.FullName, t.LanguageCode, t.Calendar, t.Keyboard, t.PreferedWeightMeasurment, t.DefaultTheme });

            // Properties
            this.Property(t => t.UserAccountId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Password)
                .IsRequired();

            this.Property(t => t.FailedAttempts)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UserSID)
                .HasMaxLength(85);

            this.Property(t => t.FullName)
                .IsRequired()
                .HasMaxLength(401);

            this.Property(t => t.Email)
                .HasMaxLength(50);

            this.Property(t => t.LanguageCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.Calendar)
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

            // Table & Column Mappings
            this.ToTable("UserInfo");
            this.Property(t => t.UserAccountId).HasColumnName("UserAccountId");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.Disabled).HasColumnName("Disabled");
            this.Property(t => t.LoggedIn).HasColumnName("LoggedIn");
            this.Property(t => t.LogginDate).HasColumnName("LogginDate");
            this.Property(t => t.LogOutDate).HasColumnName("LogOutDate");
            this.Property(t => t.FailedAttempts).HasColumnName("FailedAttempts");
            this.Property(t => t.UserSID).HasColumnName("UserSID");
            this.Property(t => t.FullName).HasColumnName("FullName");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.LanguageCode).HasColumnName("LanguageCode");
            this.Property(t => t.Calendar).HasColumnName("Calendar");
            this.Property(t => t.Keyboard).HasColumnName("Keyboard");
            this.Property(t => t.PreferedWeightMeasurment).HasColumnName("PreferedWeightMeasurment");
            this.Property(t => t.DefaultTheme).HasColumnName("DefaultTheme");
        }
    }
}
