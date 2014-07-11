using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Cats.Models.Hubs.Mapping
{
    public class ReceiveDetailMap : EntityTypeConfiguration<ReceiveDetail>
    {
        public ReceiveDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.ReceiveDetailID);

            // Properties
            this.Property(t => t.Description)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("ReceiveDetail");
            this.Property(t => t.ReceiveDetailID).HasColumnName("ReceiveDetailID");
            this.Property(t => t.PartitionID).HasColumnName("PartitionID");
            this.Property(t => t.ReceiveID).HasColumnName("ReceiveID");
            this.Property(t => t.TransactionGroupID).HasColumnName("TransactionGroupID");
            this.Property(t => t.CommodityID).HasColumnName("CommodityID");
            this.Property(t => t.SentQuantityInUnit).HasColumnName("SentQuantityInUnit");
            this.Property(t => t.UnitID).HasColumnName("UnitID");
            this.Property(t => t.SentQuantityInMT).HasColumnName("SentQuantityInMT");
            this.Property(t => t.Description).HasColumnName("Description");

            // Relationships
            this.HasRequired(t => t.Commodity)
                .WithMany(t => t.ReceiveDetails)
                .HasForeignKey(d => d.CommodityID);
            this.HasOptional(t => t.Receive)
                .WithMany(t => t.ReceiveDetails)
                .HasForeignKey(d => d.ReceiveID);
            this.HasRequired(t => t.Unit)
                .WithMany(t => t.ReceiveDetails)
                .HasForeignKey(d => d.UnitID);
            this.HasOptional(t => t.TransactionGroup)
                .WithMany(t => t.ReceiveDetails)
                .HasForeignKey(d => d.TransactionGroupID);

        }
    }
}
