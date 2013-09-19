using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hub.Mapping
{
    public class HubSettingMap : EntityTypeConfiguration<HubSetting>
    {
        public HubSettingMap()
        {
            // Primary Key
            this.HasKey(t => t.HubSettingID);

            // Properties
            this.Property(t => t.Key)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ValueType)
                .HasMaxLength(50);

            this.Property(t => t.Options)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("HubSetting");
            this.Property(t => t.HubSettingID).HasColumnName("HubSettingID");
            this.Property(t => t.Key).HasColumnName("Key");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ValueType).HasColumnName("ValueType");
            this.Property(t => t.Options).HasColumnName("Options");
        }
    }
}
