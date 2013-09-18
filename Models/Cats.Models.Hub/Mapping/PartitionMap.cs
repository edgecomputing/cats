using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hub.Mapping
{
    public class PartitionMap : EntityTypeConfiguration<Partition>
    {
        public PartitionMap()
        {
            // Primary Key
            this.HasKey(t => t.PartitionID);

            // Properties
            this.Property(t => t.ServerUserName)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Partition");
            this.Property(t => t.PartitionID).HasColumnName("PartitionID");
            this.Property(t => t.HubID).HasColumnName("HubID");
            this.Property(t => t.ServerUserName).HasColumnName("ServerUserName");
            this.Property(t => t.PartitionCreatedDate).HasColumnName("PartitionCreatedDate");
            this.Property(t => t.LastUpdated).HasColumnName("LastUpdated");
            this.Property(t => t.LastSyncTime).HasColumnName("LastSyncTime");
            this.Property(t => t.HasConflict).HasColumnName("HasConflict");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}
