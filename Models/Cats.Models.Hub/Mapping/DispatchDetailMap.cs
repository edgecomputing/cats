using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hubs.Mapping
{
    public class DispatchDetailMap : EntityTypeConfiguration<DispatchDetail>
    {
        public DispatchDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.DispatchDetailID);

            // Properties
            this.Property(t => t.Description)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("DispatchDetail");
            this.Property(t => t.DispatchDetailID).HasColumnName("DispatchDetailID");
            this.Property(t => t.PartitionId).HasColumnName("PartitionId");
            this.Property(t => t.TransactionGroupID).HasColumnName("TransactionGroupID");
            this.Property(t => t.DispatchID).HasColumnName("DispatchID");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.RequestedQunatityInUnit).HasColumnName("RequestedQunatityInUnit");
            this.Property(t => t.UnitID).HasColumnName("UnitID");
            this.Property(t => t.RequestedQuantityInMT).HasColumnName("RequestedQuantityInMT");
            this.Property(t => t.QuantityPerUnit).HasColumnName("QuantityPerUnit");
            this.Property(t => t.Description).HasColumnName("Description");

            // Relationships
            this.HasRequired(t => t.Commodity)
                .WithMany(t => t.DispatchDetails)
                .HasForeignKey(d => d.CommodityID);
            this.HasOptional(t => t.Dispatch)
                .WithMany(t => t.DispatchDetails)
                .HasForeignKey(d => d.DispatchID);
            this.HasRequired(t => t.Unit)
                .WithMany(t => t.DispatchDetails)
                .HasForeignKey(d => d.UnitID);
            this.HasOptional(t => t.TransactionGroup)
                .WithMany(t => t.DispatchDetails)
                .HasForeignKey(d => d.TransactionGroupID);

        }
    }
}
