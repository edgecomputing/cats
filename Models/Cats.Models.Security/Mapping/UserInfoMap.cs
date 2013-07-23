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
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.DBUserSid).HasColumnName("DBUserSid");
            this.Property(t => t.UserProfileID).HasColumnName("UserProfileID");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.UILanguage).HasColumnName("LanguageCode");
            this.Property(t => t.Calendar).HasColumnName("DatePreference");
        }
    }
}
