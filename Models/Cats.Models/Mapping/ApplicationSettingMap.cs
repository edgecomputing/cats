using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Mapping
{
    public class ApplicationSettingMap : EntityTypeConfiguration<ApplicationSetting>
    {
        public ApplicationSettingMap()
        {
            this.ToTable("ApplicationSetting");
            this.HasKey(t => t.SettingID);
            this.Property(t => t.SettingID).HasColumnName("SettingID");

            this.Property(t => t.SettingName).HasColumnName("SettingName");

            this.Property(t => t.SettingValue).HasColumnName("SettingValue");

        }
    }
}