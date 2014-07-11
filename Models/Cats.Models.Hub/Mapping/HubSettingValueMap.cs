using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hubs.Mapping
{
    public class HubSettingValueMap : EntityTypeConfiguration<HubSettingValue>
    {
        public HubSettingValueMap()
        {
            // Primary Key
            this.HasKey(t => t.HubSettingValueID);

            // Properties
            this.Property(t => t.Value)
                .HasMaxLength(4000);

            // Table & Column Mappings
            this.ToTable("HubSettingValue");
            this.Property(t => t.HubSettingValueID).HasColumnName("HubSettingValueID");
            this.Property(t => t.HubSettingID).HasColumnName("HubSettingID");
            this.Property(t => t.HubID).HasColumnName("HubID");
            this.Property(t => t.Value).HasColumnName("Value");

            // Relationships
            this.HasRequired(t => t.Hub)
                .WithMany(t => t.HubSettingValues)
                .HasForeignKey(d => d.HubID);
            this.HasRequired(t => t.HubSetting)
                .WithMany(t => t.HubSettingValues)
                .HasForeignKey(d => d.HubSettingID);

        }
    }
}
