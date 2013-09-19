using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hub.Mapping
{
    public class SettingMap : EntityTypeConfiguration<Setting>
    {
        public SettingMap()
        {
            // Primary Key
            this.HasKey(t => t.SettingID);

            // Properties
            this.Property(t => t.Category)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Key)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Value)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Option)
                .HasMaxLength(100);

            this.Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Setting");
            this.Property(t => t.SettingID).HasColumnName("SettingID");
            this.Property(t => t.Category).HasColumnName("Category");
            this.Property(t => t.Key).HasColumnName("Key");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.Option).HasColumnName("Option");
            this.Property(t => t.Type).HasColumnName("Type");
        }
    }
}
